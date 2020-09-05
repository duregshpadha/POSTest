using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using POS.BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace POS.BAL.Repository
{
    public interface IPosRepo
    {
        Task<IPagedList<Select2Model>> MasterItemsSelect2(string term, int pageNumber, int pageSize);

        Task<MasterItemModel> MasterItem(string id);

        Task<DataTablesResponse> MasterItemDataTable([ModelBinder(BinderType = typeof(ModelBinder))] IDataTablesRequest requestModel);

        Task<bool> AddMasterItem(MasterItemModel model);

        Task<bool> IsItemAvailable(ItemModel[] items);

        Task<MasterCustomerModel> MasterCustomerByPhone(string mobileNumber);

        Task<bool> AddMasterCustomer(MasterCustomerModel model);

        Task<bool> AddPOSMain(POSMainModel model);

        Task<bool> DeletePosMain(string id);

        Task<decimal> TotalAmmountOfItems(ItemModel[] items, string[] ids);

        Task<bool> AddPosDetails(ItemModel[] items, string posMainId, string saleOrReturn);

        Task<bool> RemoveStockMasterItem(ItemModel[] items);

        Task<bool> AddStockMasterItem(ItemModel[] items);

        Task<bool> AddStockMasterItem(string id, int stock);
    }
}
