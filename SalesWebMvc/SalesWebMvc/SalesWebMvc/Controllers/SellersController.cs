using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using System.Collections.Generic;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;
using System;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departartmentService;

        public SellersController(SellerService sellerService, DepartmentService departartmentService)
        {
            _sellerService = sellerService;
            _departartmentService = departartmentService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departartmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = await _departartmentService.FindAllAsync();
                SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(Id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new {message = "id not found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                await _sellerService.RemoveAsync(Id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = "Can't delete seller because he/she has sales" });
            }
        }

        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(Id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new {message = "id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return RedirectToAction(nameof(Error), new {message = "id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(Id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new {message = "id not found" });
            }

            List<Department> departments = await _departartmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                List<Department> departments = await _departartmentService.FindAllAsync();
                SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (Id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new {message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
