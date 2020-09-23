using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Data_Entities;
using OdeToFood.DataAccess;

namespace MyApp.Namespace
{
    public class DetailModel : PageModel
    {
        private IConfiguration config{get;}
        [BindProperty (SupportsGet=true)]
        public int Id { get; set; }
        public Resturant Resturant{get;set;}
        public IResturantData ResturantList{get;set;}
        public DetailModel(IConfiguration configuration, IResturantData ResturantList)
        {this.ResturantList=ResturantList;
        this.config=config;
        }

        public IActionResult OnGet()
        {           
            Resturant=ResturantList.SearchById(Id);
        if(Resturant==null)
        return RedirectToPage("./NotFound");;
        
        return Page();

        }
    }
}
