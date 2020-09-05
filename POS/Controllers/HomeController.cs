using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POS.BAL.Models;
using POS.BAL.Repository;
using POS.Models;
using POS.Utilities;

namespace POS.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(IPosRepo repo, IGenerateID generateID)
        {
            _repo = repo;
            _generateID = generateID;
        }
        private readonly IPosRepo _repo;
        private readonly IGenerateID _generateID;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Stock()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> StockDataTable([ModelBinder(BinderType = typeof(ModelBinder))] IDataTablesRequest requestModel)
        {
            var stock = await _repo.MasterItemDataTable(requestModel);
            var record = new
            {
                draw = stock.Draw,
                recordsTotal = stock.TotalRecords,
                recordsFiltered = stock.TotalRecordsFiltered,
                data = stock.Data,
                error = stock.Error,
                additionalParameters = stock.AdditionalParameters
            };
            return Json(record);
        }

        [HttpPost]
        public async Task<IActionResult> Stock(MasterItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            MasterItemModel masterItem = new MasterItemModel()
            {
                Id = _generateID.GenerateId(),
                Name = model.Name,
                Rate = model.Rate,
                Stock = model.Stock
            };

            await _repo.AddMasterItem(model: masterItem);
            return RedirectToAction(nameof(Stock));
        }

        [HttpPost]
        public async Task<JsonResult> AddStock(string id, int stock)
        {
            bool isAdded = await _repo.AddStockMasterItem(id: id, stock: stock);
            if (isAdded)
            {
                return Json(data: new { success = "success", message = "Stock added succesfully", data = "" });
            }
            return Json(data: new { success = "fail", message = "Technical error! Please try again after some time.", data = "" });
        }


        [HttpPost]
        public async Task<JsonResult> PurchaseItem(ItemModel[] items, string customerName, string mobileNumber, string saleOrReturn)
        {
            if (items == null || items.Count() == 0)
            {
                return Json(data: new { success = "fail", message = "There is no item selected from item list", data = "" });
            }
            if (string.IsNullOrEmpty(customerName) || string.IsNullOrWhiteSpace(customerName))
            {
                return Json(data: new { success = "fail", message = "Customer Name is a required field", data = "" });
            }
            if (string.IsNullOrEmpty(mobileNumber) || string.IsNullOrWhiteSpace(mobileNumber))
            {
                return Json(data: new { success = "fail", message = "Mobile Number is a required field", data = "" });
            }
            if (mobileNumber.Length > 10)
            {
                return Json(data: new { success = "fail", message = "Invalid mobile number", data = "" });
            }
            if (customerName.Length > 10)
            {
                return Json(data: new { success = "fail", message = "Invalid customer name", data = "" });
            }
            if (string.IsNullOrEmpty(saleOrReturn) || string.IsNullOrWhiteSpace(saleOrReturn))
            {
                return Json(data: new { success = "fail", message = "Invalid Sale Or Return", data = "" });
            }
            if (saleOrReturn != "S" && saleOrReturn != "R")
            {
                return Json(data: new { success = "fail", message = "Invalid Sale Or Return", data = "" });
            }

            var customer = await _repo.MasterCustomerByPhone(mobileNumber);
            if (customer == null)
            {
                MasterCustomerModel customerModel = new MasterCustomerModel()
                {
                    Id = _generateID.GenerateId(),
                    Name = customerName,
                    Phone = mobileNumber
                };
                bool isAdded = await _repo.AddMasterCustomer(model: customerModel);
                if (!isAdded)
                {
                    return Json(data: new { success = "fail", message = "Technical error! Please try again after some time.", data = "" });
                }
                customer = customerModel;
            }

            bool isItemAvailable = await _repo.IsItemAvailable(items: items);
            if (!isItemAvailable)
            {
                return Json(data: new { success = "fail", message = "Selected item is more than the stock", data = "" });
            }

            decimal totalPrice = await _repo.TotalAmmountOfItems(items: items, ids: items.Select(x => x.Id).ToArray());

            POSMainModel mainModel = new POSMainModel()
            {
                Id = _generateID.GenerateId(),
                CustomerId = customer.Id,
                PosDate = DateTime.Now,
                TotalAmount = totalPrice,
                TotalQuantity = items.Select(x => x.Quantity).Sum()
            };

            bool isPosAdded = await _repo.AddPOSMain(mainModel);
            if (isPosAdded)
            {
                bool isDetailsAdded = await _repo.AddPosDetails(items: items, posMainId: mainModel.Id, saleOrReturn: saleOrReturn);
                if (isDetailsAdded)
                {
                    if (saleOrReturn == "S")
                    {
                        bool isStockRemoved = await _repo.RemoveStockMasterItem(items: items);
                    }
                    else
                    {
                        bool isStockAdded = await _repo.AddStockMasterItem(items: items);
                    }
                    return Json(data: new { success = "success", message = "Purchase success.", data = "" });
                }
                else
                {
                    await _repo.DeletePosMain(id: mainModel.Id);
                    return Json(data: new { success = "fail", message = "Technical error! Please try again after some time.", data = "" });
                }
            }
            else
            {
                return Json(data: new { success = "fail", message = "Technical error! Please try again after some time.", data = "" });
            }
        }


        [HttpPost]
        public async Task<JsonResult> MasterItemsSelect2(string term, int pageNumber, int pageSize)
        {
            var items = await _repo.MasterItemsSelect2(term: term, pageNumber: pageNumber, pageSize: pageSize);
            Select2Pagination pagination = new Select2Pagination()
            {
                More = items.HasNextPage
            };

            Select2ModelResult result = new Select2ModelResult()
            {
                Pagination = pagination,
                Results = items.ToList()
            };

            return Json(data: result);
        }

        [HttpPost]
        public async Task<JsonResult> GetItem(string id)
        {
            var masterItem = await _repo.MasterItem(id: id);
            return Json(data: masterItem);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
