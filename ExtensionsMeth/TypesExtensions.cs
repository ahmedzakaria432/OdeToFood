using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdeToFood.ExtensionsMeth
{
    public static class TypesExtensions
    {
        public static string ScripterToString(this StringCollection collection)
        {
            StringBuilder script = new StringBuilder();
            foreach (var item in collection)
            {
                script.Append(item);
                

            }
            return script.ToString();

        }
    }
}
