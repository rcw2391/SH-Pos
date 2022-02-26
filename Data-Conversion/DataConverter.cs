using DataConversion.OldShape;
using DataLayer;
using DataLayer.Models;
using Newtonsoft.Json;

namespace DataConversion
{
    public class DataConverter
    {
        private const string FOLDER = @"C:\SH\SH\Data-Conversion\Data\";

        private static IEnumerable<T> GetJson<T>(string file)
        {
            List<T> old = new List<T>();

            using (StreamReader sr = new StreamReader(FOLDER + file))
            {
                string json = sr.ReadToEnd();

                old = JsonConvert.DeserializeObject<List<T>>(json);

                return old;
            }
        }
        
        public static async Task<bool> ConvertCustomers(IUnitOfWork uow)
        {
            List<OldCustomer> oldCustomers = new();
            
            using (StreamReader r = new(FOLDER + "customers.json"))
            {
                string json = r.ReadToEnd();

                oldCustomers = JsonConvert.DeserializeObject<List<OldCustomer>>(json);

                List<Customer> newCustomers = new();

                foreach (OldCustomer oc in oldCustomers)
                    newCustomers.Add(new Customer()
                    {
                        FirstName = oc.firstName,
                        LastName = oc.lastName,
                        Phone = oc.phone,
                        Email = oc.email,
                        Formula = oc.formula,
                        Duration = oc.duration,
                        OldID = oc._id.oldId
                    });

                newCustomers = new(await uow.Customers.CreateAsync(newCustomers));

                return true;
            }
        }

        public static async Task<bool> ConvertProducts(IUnitOfWork uow)
        {
            List<OldProduct> oldProducts = new(GetJson<OldProduct>("products.json"));

            List<Product> newProducts = new();

            foreach (OldProduct op in oldProducts)
            {
                newProducts.Add(new Product()
                {
                    Description = op.desc,
                    Price = decimal.Parse(op.price),
                    IsCustom = op.isCustom,
                    OldID = op._id.oldId
                });
            }

            newProducts = new(await uow.Products.CreateAsync(newProducts));

            return true;
        }
    }
}
