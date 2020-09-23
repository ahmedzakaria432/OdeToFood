using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Data_Entities;
using OdeToFood.DataAccess;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyApp.Namespace
{
    public class EditModel : PageModel
    {
        public IEnumerable<SelectListItem> cusines;
        [BindProperty(SupportsGet = true)]
        public int ID { get; set; }
        [BindProperty(SupportsGet = true)]
        public Resturant ResturantToEdit { get; set; }
        public IResturantData ResturantsService { get; set; }
        public IHtmlHelper Helper { get; }
        public EditModel(IResturantData ResturantsService, IHtmlHelper helper)
        {
            this.Helper = helper;
            this.ResturantsService = ResturantsService;

        }
        public IActionResult OnGet()
        {
            if (ResturantsService.SearchById(ID)==null)
               return RedirectToPage("NotFound");
            cusines=Helper.GetEnumSelectList<CusineType>();

            ResturantToEdit = ResturantsService.SearchById(ID);
            return Page();


        }
        public IActionResult OnPost()
        {
            ResturantToEdit= ResturantsService.Update( ResturantToEdit);
            ResturantsService.commit();
            cusines = Helper.GetEnumSelectList<CusineType>();
            return Page();
        }
    }
}
