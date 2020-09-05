using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.BAL.Models;
using POS.DAL;
using POS.DAL.Models;
using POS.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace POS.BAL.Repository
{
    class PosRepo : IPosRepo
    {
        public PosRepo(POSDBContext dbContext, IGenerateID generateID)
        {
            _dbContext = dbContext;
            _generateID = generateID;
        }
        private readonly POSDBContext _dbContext;
        private readonly IGenerateID _generateID;

        public async Task<IPagedList<Select2Model>> MasterItemsSelect2(string term, int pageNumber, int pageSize)
        {
            if (string.IsNullOrEmpty(term) || string.IsNullOrWhiteSpace(term))
            {
                return await _dbContext.MasterItems.Select(x => new Select2Model()
                {
                    Id = x.Id,
                    Text = x.Name
                }).ToPagedListAsync(pageNumber: pageNumber, pageSize: pageSize);
            }
            else
            {
                return await _dbContext.MasterItems.Where(i => i.Name.Contains(term))
                    .Select(x => new Select2Model()
                    {
                        Id = x.Id,
                        Text = x.Name
                    }).ToPagedListAsync(pageNumber: pageNumber, pageSize: pageSize);
            }
        }

        public async Task<MasterItemModel> MasterItem(string id)
        {
            return await _dbContext.MasterItems.Where(m => m.Id == id)
                .Select(x => new MasterItemModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Rate = x.Rate,
                    Stock = x.Stock
                }).FirstOrDefaultAsync();
        }

        public async Task<DataTablesResponse> MasterItemDataTable([ModelBinder(BinderType = typeof(ModelBinder))] IDataTablesRequest requestModel)
        {
            var query = _dbContext.MasterItems.Select(x => new
            {
                x.Id,
                x.Name,
                x.Rate,
                x.Stock
            });

            int totalCount = await query.CountAsync();

            if (!string.IsNullOrEmpty(requestModel.Search.Value))
            {
                var value = requestModel.Search.Value.Trim();
                value = (string.IsNullOrEmpty(value)) ? value : value.ToLower();

                query = query.Where(x => x.Name.Contains(value));
            }

            int filterCount = await query.CountAsync();

            var sortedColumns = requestModel.Columns.Where(x => x.Sort != null);
            string orderByString = string.Empty;

            foreach (var column in sortedColumns)
            {
                orderByString = orderByString != string.Empty ? "," : "";
                orderByString = (column.Field) + (column.Sort.Direction == SortDirection.Ascending ? "asc" : "desc");
            }

            query = query.OrderBy(orderByString == string.Empty ? "id asc" : orderByString);

            query = query.Skip(requestModel.Start).Take(requestModel.Length);

            var data = await query.ToListAsync();

            return DataTablesResponse.Create(requestModel, totalCount, filterCount, data);
        }


        public async Task<bool> AddMasterItem(MasterItemModel model)
        {
            MasterItem item = new MasterItem()
            {
                Id = model.Id,
                Name = model.Name,
                Rate = model.Rate,
                Stock = model.Stock
            };

            await _dbContext.MasterItems.AddAsync(item);
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> IsItemAvailable(ItemModel[] items)
        {
            string[] ids = items.Select(x => x.Id).ToArray();
            var masterItems = await _dbContext.MasterItems.Where(x => ids.Contains(x.Id)).ToListAsync();
            foreach (var item in items)
            {
                int count = masterItems.Where(x => x.Id == item.Id && x.Stock < item.Quantity).Count();
                if (count > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<MasterCustomerModel> MasterCustomerByPhone(string mobileNumber)
        {
            return await _dbContext.MasterCustomers.Where(m => m.Phone == mobileNumber)
                .Select(x => new MasterCustomerModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> AddMasterCustomer(MasterCustomerModel model)
        {
            MasterCustomer customer = new MasterCustomer()
            {
                Id = model.Id,
                Name = model.Name,
                Phone = model.Phone
            };

            await _dbContext.MasterCustomers.AddAsync(customer);
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> AddPOSMain(POSMainModel model)
        {
            POSMain main = new POSMain()
            {
                CustomerId = model.CustomerId,
                Id = model.Id,
                PosDate = model.PosDate,
                TotalAmount = model.TotalAmount,
                TotalQuantity = model.TotalQuantity
            };

            await _dbContext.POSMains.AddAsync(main);
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> DeletePosMain(string id)
        {
            var pos = await _dbContext.POSMains.FindAsync(id);
            if (pos == null)
                return false;

            _dbContext.POSMains.Remove(pos);
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<decimal> TotalAmmountOfItems(ItemModel[] items, string[] ids)
        {
            decimal totalAmmount = 0;
            var masterItems = await _dbContext.MasterItems.Where(x => ids.Contains(x.Id)).ToListAsync();
            if (masterItems.Count == 0)
            {
                return totalAmmount;
            }
            foreach (var item in items)
            {
                decimal price = masterItems.Where(x => x.Id == item.Id).Select(x => x.Rate).FirstOrDefault();
                totalAmmount += price * item.Quantity;
            }
            return totalAmmount;
        }


        public async Task<bool> AddPosDetails(ItemModel[] items, string posMainId, string saleOrReturn)
        {
            string[] ids = items.Select(x => x.Id).ToArray();
            var masterItems = await _dbContext.MasterItems.Where(x => ids.Contains(x.Id)).ToListAsync();
            List<POSDetail> details = new List<POSDetail>();
            foreach (var item in items)
            {
                POSDetail detail = new POSDetail()
                {
                    Id = _generateID.GenerateId(),
                    ItemId = item.Id,
                    ItemQuantity = item.Quantity,
                    ItemRate = masterItems.Where(x => x.Id == item.Id).Select(x => x.Rate).FirstOrDefault(),
                    POSMainID = posMainId,
                    SaleOrReturn = saleOrReturn,
                    TotalAmount = masterItems.Where(x => x.Id == item.Id).Select(x => x.Rate).FirstOrDefault() * item.Quantity,
                };
                details.Add(detail);
            }

            await _dbContext.POSDetails.AddRangeAsync(details);
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> RemoveStockMasterItem(ItemModel[] items)
        {
            string[] ids = items.Select(x => x.Id).ToArray();
            var masterItems = await _dbContext.MasterItems.Where(x => ids.Contains(x.Id)).ToListAsync();
            foreach (var item in masterItems)
            {
                item.Stock = item.Stock - items.Where(x => x.Id == item.Id).Select(x => x.Quantity).FirstOrDefault();
            }
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> AddStockMasterItem(ItemModel[] items)
        {
            string[] ids = items.Select(x => x.Id).ToArray();
            var masterItems = await _dbContext.MasterItems.Where(x => ids.Contains(x.Id)).ToListAsync();
            foreach (var item in masterItems)
            {
                item.Stock = item.Stock + items.Where(x => x.Id == item.Id).Select(x => x.Quantity).FirstOrDefault();
            }
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }

        public async Task<bool> AddStockMasterItem(string id, int stock)
        {
            var item = await _dbContext.MasterItems.FindAsync(id);
            if (item == null)
                return false;

            item.Stock = stock;
            int affectedRows = await _dbContext.SaveChangesAsync();
            return affectedRows > 0;
        }
    }
}
