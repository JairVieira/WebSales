using Microsoft.AspNetCore.Mvc;
using SalesWeb.Models;
using SalesWeb.Models.ViewModels;
using SalesWeb.Services;
using SalesWeb.Services.Exceptions;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SalesWeb.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }        

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var vm = new SellerFormViewModel { Departments = departments };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            var obj = _sellerService.FindById(id.Value);
            if (obj is null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            var obj = _sellerService.FindById(id.Value);
            if (obj is null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id is null)
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            var obj = _sellerService.FindById(id.Value);
            if (obj is null)
                return RedirectToAction(nameof(Error), new { message = "Id not found" });

            var departments = _departmentService.FindAll();
            var vm = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller obj)
        {
            if (id != obj.Id)
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            try
            {
                _sellerService.Update(obj);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
            catch (DbConcurrencyException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var vm = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(vm);
        }
    }
}
