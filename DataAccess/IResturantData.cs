using System.IO;
using System.Collections.Generic;
using System.Collections;
using OdeToFood.Data_Entities;

namespace OdeToFood.DataAccess
{
    public interface IResturantData
    {
         IEnumerable<Resturant> GetAll();
         IEnumerable<Resturant> SearchByName(string Name);
         Resturant SearchById(int id);
         Resturant Update(Resturant ResturantToEdit);
         void AddResturant(Resturant ResturantToAdd);
         Resturant Delete(int id);
         int commit();
        int GetCount();
    }
}
