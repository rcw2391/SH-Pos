using DataLayer.Models;
using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Customer> Customers { get; private set; }

        public IRepository<Transaction> Transactions { get; private set; }

        public IRepository<Service> Services { get; private set; }

        public IRepository<Product> Products { get; private set; }

        public IRepository<Stylist> Stylists { get; private set; }

        public IRepository<GiftCertificate> GiftCertificates { get; private set; }

        public IRepository<Payment> Payments { get; private set; }

        public IRepository<TransactionLine> TransactionLines { get; private set; }

        public UnitOfWork(string connString)
        {
            DbConnectionFactory.Instance.SetConnectionStringAsync(connString).ConfigureAwait(false);
            CreateRepos();   
        }

        private void CreateRepos()
        {
            Customers = new CustomerRepository();
            Transactions = new TransactionRepository();
            Services = new ServiceRepository();
            Products = new ProductRepository();
            Stylists = new StylistRepository();
            GiftCertificates = new GiftCertificateRepository();
            Payments = new PaymentRepository();
            TransactionLines = new TransactionLineRepository();
        }
    }
}
