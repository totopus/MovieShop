using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using Infrastructure.Services;
using ApplicationCore.ServiceInterfaces;

namespace MovieShopMVC.Controllers
{
    public class CastsController : Controller
    {
        private readonly ICastService _castService;
        public CastsController(ICastService castService)
        {
            _castService = castService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var castDetails = await _castService.GetCastDetails(id);
            return View(castDetails);
        }

        

    }
}
