using Microsoft.AspNetCore.Mvc;
using OdeToFood.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewComponents
{
    public class ResturantCountViewComponent:ViewComponent
    {
        private readonly IResturantData resturantService;

        public ResturantCountViewComponent(IResturantData resturantService)
        {
            this.resturantService = resturantService;
        }
        public IViewComponentResult Invoke()
        {
            var count = resturantService.GetCount();
            return View(count);
        }
    }
}
