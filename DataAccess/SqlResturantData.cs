using OdeToFood.Data_Entities;
using System.Linq;
using System.Collections.Generic;

namespace OdeToFood.DataAccess
{
    public class SqlResturantData : IResturantData
    {
        private readonly OdeToFoodDBContext db;

        public SqlResturantData(OdeToFoodDBContext db)
        {
            this.db = db;
        }
        public void AddResturant(Resturant ResturantToAdd)
        {
            db.Add(ResturantToAdd);
        }

        public Resturant Delete(int id)
        {
            var resturant = SearchById(id);

           return  db.Remove<Resturant>(resturant).Entity;


        }

        public IEnumerable<Resturant> GetAll()
        {
            return db.Resturants.ToList();
            
        }

        public Resturant Update(Resturant ResturantToEdit)
        {
            var entity= db.Attach(ResturantToEdit);
            entity.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return ResturantToEdit;

        }

        public Resturant SearchById(int id)
        {
            return db.Find<Resturant>(id);
        }

        public IEnumerable<Resturant> SearchByName(string Name)
        {
            return from r in db.Resturants
                   where string.IsNullOrEmpty(Name) || r.Name.StartsWith(Name)
                        orderby r.Name
                        select r;
             
        }

        public int commit()
        {
            return db.SaveChanges();
        }

        public int GetCount()
        {
            return db.Resturants.Count();
        }
    }
}
