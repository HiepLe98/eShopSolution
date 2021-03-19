using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.AdminApp.Services;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace eShopSolution.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IProductApiClient _productApiClient;
        public ProductController(IConfiguration configuration, IProductApiClient productApiClient)
        {
            _configuration = configuration;
            _productApiClient = productApiClient;
        }
        public async Task<IActionResult> Index(string keyWork,List<int> categoryIds , int pageIndex = 1, int pageSize = 5)
        {
           var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageID);

            var request = new GetManageProductPagingRequest()
            {
                Keyword = keyWork,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId,
                CategoryIds = categoryIds,
            };
            var data = await _productApiClient.GetPagings(request);
            ViewBag.Keyword = keyWork;
            if (TempData["result"] != null)
                ViewBag.SuccessMsg = TempData["result"];
            return View(data);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!ModelState.IsValid)
                return View();

            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageID);
            var result = await _productApiClient.GetById(id, languageId);
            if (result.IsSuccessed)
                return View(result.ResultObj);
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm]ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Thêm sản phẩm thất bại.");
            return View(request);
        }

        //[HttpPost]
        //public async Task<IActionResult> Edit(ProductViewModel request)
        //{
        //    if (!ModelState.IsValid)
        //        return View();
        //    var result = await _productApiClient.up(request.Id, request);

        //    if (result.IsSuccessed)
        //    {
        //        TempData["result"] = "Cập nhật thành công";
        //        return RedirectToAction("Index");
        //    }

        //    ModelState.AddModelError("", result.Message);
        //    return View(request);
        //}
        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var result = await _productApiClient.GetById(id);
        //    if (result.IsSuccessed)
        //    {
        //        var user = result.ResultObj;
        //        var updateRequest = new UserUpdateRequest()
        //        {
        //            Dob = user.Dob,
        //            Email = user.Email,
        //            FirstName = user.FirstName,
        //            LastName = user.LastName,
        //            PhoneNumber = user.PhoneNumer,
        //            Id = user.Id
        //        };
        //        return View(updateRequest);
        //    }
        //    return RedirectToAction("Error", "Home");
        //}

    }
}
