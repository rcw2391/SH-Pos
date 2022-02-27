using DataLayer.Models;
using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<Customer> Customers { get; private set; }
        public IRepository<User> Users { get; private set; }

        public IRepository<Transaction> Transactions { get; private set; }

        public IRepository<Service> Services { get; private set; }

        public IRepository<Product> Products { get; private set; }

        public IRepository<Stylist> Stylists { get; private set; }

        public IRepository<GiftCertificate> GiftCertificates { get; private set; }

        public IRepository<Payment> Payments { get; private set; }

        public IRepository<TransactionLine> TransactionLines { get; private set; }

        public UnitOfWork()
        {
            DbConnectionFactory.Instance.SetConnectionStringAsync(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SHTest;User Id=testUser; Password=testing;").ConfigureAwait(false);
            CreateRepos();   
        }

        private Dictionary<Type, IRepository> _repos = new();

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
            Users = new UserRepository();

            _repos = new();

            _repos.Add(Customers.RepositoryType, Customers);
            //_repos.Add(Transactions);
            //_repos.Add(Services);
            //_repos.Add(Products);
            //_repos.Add(Stylists);
            //_repos.Add(GiftCertificates);
            //_repos.Add(Payments);
            //_repos.Add(TransactionLines);
            _repos.Add(Users.RepositoryType ,Users);
        }

        public IRepository<T> GetRepository<T>() where T : class, ICrudObject
        {
            return (IRepository<T>)_repos[typeof(T)];
        }
    }
}
