using System.Linq;
using System.Collections.Generic;
using OdeToFood.Data_Entities;

namespace OdeToFood.DataAccess
{
    public class InMemoryResturantData : IResturantData
    {
       static List<Resturant> ResturantsList;
        public InMemoryResturantData()
        {
            ResturantsList=new List<Resturant>{
                new Resturant{ID=1,Name="mexica",Location="torinto",Cusine=CusineType.mexican},
                 new Resturant{ID=2,Name="mandi",Location="cairo",Cusine=CusineType.egyptian},
                  new Resturant{ID=3,Name="pizza",Location="roma",Cusine=CusineType.italian}


            };
        }

        public IEnumerable<Resturant> GetAll()
        {
            return from r in ResturantsList
            orderby r.Name
            select r;
        }

        public Resturant SearchById(int id)
        {
            return (from r in ResturantsList 
            where r.ID.Equals(id)
            select r).FirstOrDefault();
        }
        public Resturant SaveResturantEdit(int id,Resturant ResturantToEdit)
        {
           Resturant res= SearchById(id);
           res.Name=ResturantToEdit.Name;
           res.Location=ResturantToEdit.Location;
           res.Cusine=ResturantToEdit.Cusine;
           
          return res;  
        }


        public IEnumerable<Resturant> SearchByName(string Name)
        {
            return from r in ResturantsList
            where string.IsNullOrEmpty(Name)||r.Name.StartsWith(Name)
            orderby r.Name
            select r;
        }
        public void AddResturant(Resturant ResturantToAdd)
        {

            ResturantsList.Add(ResturantToAdd);

            
        }

        public Resturant Delete(int id)
        {
            var resturant= ResturantsList.FirstOrDefault(r => r.ID == id);
            

                ResturantsList.Remove(resturant);
            return resturant;
        }

        public Resturant Update(Resturant ResturantToEdit)
        {
          var resturant = ResturantsList.SingleOrDefault(r => r.ID == ResturantToEdit.ID);

            if (!resturant.Equals(null))
            {
                resturant.Name = ResturantToEdit.Name;
                resturant.Location = ResturantToEdit.Location;
                resturant.Cusine = ResturantToEdit.Cusine;
            }
           
            return resturant;
        }

        public int commit()
        {
            return 0;
        }

        public int GetCount()
        {
          return   ResturantsList.Count();
        }
    }

}
