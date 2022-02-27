using DataLayer.Models;
using DataLayer.Repositories;

namespace DataLayer
{
    public interface IUnitOfWork
    {
        IRepository<Customer> Customers { get; }
        IRepository<User> Users { get; }
        IRepository<Transaction> Transactions { get; }
        IRepository<Service> Services { get; }
        IRepository<Product> Products { get; }
        IRepository<Stylist> Stylists { get; }
        IRepository<GiftCertificate> GiftCertificates { get; }
        IRepository<Payment> Payments { get; }
        IRepository<TransactionLine> TransactionLines { get; }

        IRepository<T> GetRepository<T>() where T : class, ICrudObject;
    }
}
