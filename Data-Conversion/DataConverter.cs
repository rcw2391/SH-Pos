using DataConversion.OldShape;
using DataLayer;
using DataLayer.Models;
using Newtonsoft.Json;
using System.Globalization;

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

        public static async Task<bool> ConvertServices(IUnitOfWork uow)
        {
            List<OldService> oldServices = new(GetJson<OldService>("services.json"));

            List<Service> newServices = new();

            foreach (OldService os in oldServices)
            {
                newServices.Add(new Service()
                {
                    Description = os.desc,
                    Price = decimal.Parse(os.price),
                    IsCustom = os.isCustom,
                    OldID = os._id.oldId
                });
            }

            newServices = new(await uow.Services.CreateAsync(newServices));

            return true;
        }

        public static async Task<bool> ConvertStylists(IUnitOfWork uow)
        {
            List<OldStylist> oldStylists = new(GetJson<OldStylist>("stylists.json"));

            List<Stylist> newStylists = new();
            List<User> newUsers = new();

            foreach (OldStylist s in oldStylists)
            {
                newStylists.Add(new Stylist()
                {
                    FirstName = s.firstName,
                    LastName = s.lastName,
                    IsActive = s.isActive
                });

                
            }

            newStylists = new(await uow.Stylists.CreateAsync(newStylists));

            for (int i = 0; i < oldStylists.Count; i++)
            {
                newUsers.Add(new User()
                {
                    Username = oldStylists[i].username,
                    Password = oldStylists[i].password,
                    StylistId = newStylists[i].ID,
                    IsAdmin = false,
                    IsActive = true
                });
            }

            await uow.Users.CreateAsync(newUsers);

            return true;
        }

        public static async Task<bool> ConvertTransactions(IUnitOfWork uow)
        {
            List<OldTransaction> oldTransactions = new(GetJson<OldTransaction>("transactions.json"));

            List<Stylist> stylists = new(await uow.Stylists.GetAllAsync());
            List<Customer> customers = new(await uow.Customers.GetAllAsync());
            List<Service> services = new(await uow.Services.GetAllAsync());
            List<Product> products = new(await uow.Products.GetAllAsync());

            List<Task> tasks = new();

            foreach (OldTransaction old in oldTransactions)
            {
                List<TransactionLine> lines = new();
                List<Payment> payments = new();
                List<GiftCertificate> giftCertificates = new();



                Transaction newTrans = new Transaction
                {
                    Details = old.details,
                    GrandTotal = old.grandTotal.numberInt != null ? decimal.Parse(old.grandTotal.numberInt) : decimal.Parse(old.grandTotal.numberDouble),
                    ProductTotal = old.productTotal.numberInt != null ? decimal.Parse(old.productTotal.numberInt) : decimal.Parse(old.productTotal.numberDouble),
                    ServiceTotal = old.serviceTotal.numberInt != null ? decimal.Parse(old.serviceTotal.numberInt) : decimal.Parse(old.serviceTotal.numberDouble),
                    Taxes = old.taxes.numberInt != null ? decimal.Parse(old.taxes.numberInt) : decimal.Parse(old.taxes.numberDouble),
                    LegacyID = old._id.oldId,
                    IsVoid = false
                };

                newTrans.CreatedById = stylists.First(s => s.FirstName == old.stylist || s.FullName == old.stylist).ID;

                newTrans.CustomerId = customers.First(c => c.OldID == old.customerId.oldId).ID;

                newTrans.Date = new DateTime(int.Parse(old.dateYear.numberInt), DateTime.ParseExact(old.dateMonth, "MMMM", CultureInfo.CurrentCulture).Month, int.Parse(old.dateDate.numberInt));

                newTrans = await uow.Transactions.CreateAsync(newTrans);

                foreach (OldServiceTransaction os in old.services)
                {
                    lines.Add(new TransactionLine()
                    {
                        TransactionId = newTrans.ID,
                        ServiceId = services.FirstOrDefault(s => s.OldID == os.service.oldId, null)?.ID,
                        StylistId = stylists.First(s => s.FirstName == os.stylist.name || s.FullName == os.stylist.name).ID,
                        Amount = os.price.numberInt != null ? decimal.Parse(os.price.numberInt) : decimal.Parse(os.price.numberDouble)
                    });
                }

                foreach (OldProductTransaction op in old.products)
                {
                    lines.Add(new TransactionLine()
                    {
                        TransactionId = newTrans.ID,
                        ProductId = products.FirstOrDefault(p => p.OldID == op.product.oldId, null)?.ID,
                        StylistId = stylists.First(s => s.FirstName == op.stylist.name || s.FullName == op.stylist.name).ID,
                        Amount = op.price.numberInt != null ? decimal.Parse(op.price.numberInt) : decimal.Parse(op.price.numberDouble),
                        Tax = op.price.numberInt != null ? Math.Round(decimal.Parse(op.price.numberInt) * .0825M, 2) : Math.Round(decimal.Parse(op.price.numberDouble) * .0825M, 2)
                    });
                }

                foreach (OldPayment op in old.payments)
                {
                    payments.Add(new Payment()
                    {
                        PaymentMethod = op.method,
                        Amount = op.amount.numberInt != null ? decimal.Parse(op.amount.numberInt) : decimal.Parse(op.amount.numberDouble),
                        TransactionId = newTrans.ID
                    });
                }

                if (old.giftCerts != null)
                {
                    foreach (OldGiftCertificate ogc in old.giftCerts)
                    {
                        giftCertificates.Add(new GiftCertificate()
                        {
                            Amount = ogc.amount.numberInt != null ? decimal.Parse(ogc.amount.numberInt) : decimal.Parse(ogc.amount.numberDouble),
                            StylistId = newTrans.CreatedById,
                            TransactionId = newTrans.ID
                        });
                    }
                }

                tasks.Add(uow.TransactionLines.CreateAsync(lines));
                tasks.Add(uow.Payments.CreateAsync(payments));
                tasks.Add(uow.GiftCertificates.CreateAsync(giftCertificates));

                await Task.WhenAll(tasks);
            }

            return true;
        }
    }
}
