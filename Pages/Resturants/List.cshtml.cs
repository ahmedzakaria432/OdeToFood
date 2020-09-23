using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using OdeToFood.Data_Entities;
using OdeToFood.DataAccess;
using Microsoft.Extensions.Logging;

namespace MyApp.Namespace
{
    public class ListModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        private IConfiguration Config { get; }
        private IResturantData IResturant{get;set;}
        [BindProperty(SupportsGet=true)]
        public string searchedTerm{get;set;}
        public  IEnumerable<Resturant> Resturants;
        private readonly ILogger<ListModel> logger;

        public ListModel(IConfiguration config,IResturantData ResturantData,ILogger<ListModel> Logger)
        {
            this.Config = config;
            IResturant=ResturantData;
            logger = Logger;
        }
        public void OnGet()
        {
            logger.LogError("executing Get method for list");
            Message = Config["message"];
            
            Resturants=IResturant.SearchByName(searchedTerm);
            
        }
        
        
    }
}
