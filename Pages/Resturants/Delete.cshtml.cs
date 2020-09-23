using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Data_Entities;
using OdeToFood.DataAccess;

namespace OdeToFood
{
    public class DeleteModel : PageModel
    {
        private readonly IResturantData resturantService;
        public Resturant resturant { get; set; }
        public DeleteModel(IResturantData resturantService)
        {
            this.resturantService = resturantService;
        }
        public IActionResult OnGet(int id)
        {
            resturant = resturantService.SearchById(id);
            if (resturant.Equals(null))
                return RedirectToPage("./NotFound");
            return Page();

        }
        public IActionResult OnPost(int id)
        {
            resturant= resturantService.Delete(id);
            if (resturant.Equals(null))
                return RedirectToPage("./NotFound");
            resturantService.commit();
            TempData["Message"] = $" {resturant.Name} resturant Deleted successfully!";
            return RedirectToPage("List");

        }
    }
}
