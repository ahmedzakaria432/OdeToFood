using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using OdeToFood.Data_Entities;
using OdeToFood.DataAccess;

namespace OdeToFood
{
    public class AddResturanrModel : PageModel
    {
        [BindProperty]
        public Resturant ResturantToAdd { get; set; }
      
        public  IEnumerable<SelectListItem> Cusines;
        public IResturantData ResturantService { get; set; }
        public IHtmlHelper Helper { get; }
        [TempData]
        public string Message { get; set; }


        public IConfiguration configuration;
        public AddResturanrModel(IResturantData resturantService,IConfiguration configuration,IHtmlHelper helper)
        {
            ResturantService = resturantService;
            Helper = helper;
        }

        
        
        public void OnGet()
        {
            Cusines = Helper.GetEnumSelectList<CusineType>();
            

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid) { 
            ResturantService.AddResturant(ResturantToAdd);
                TempData["Message"] = "Added successfully!";
                ResturantService.commit();
                return RedirectToPage("List");
            }
            else
            {
                TempData["Message"] = "some thing went wrong!";
                Cusines = Helper.GetEnumSelectList<CusineType>();
                return Page();

            }
            
            
        
        
        }

    }
}
