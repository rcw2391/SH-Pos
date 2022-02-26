using DataConversion;
using DataLayer;
using DataLayer.Models;

//string folder = @"C:\SH\SH\Data-Conversion\Data\";

//string[] files = Directory.GetFiles(folder);

//CleaningOption oidOption = new()
//{
//    OldValue = "$oid",
//    NewValue = "oldId"
//};

//CleaningOption dollarSignOption = new()
//{
//    OldValue = "$",
//    NewValue = ""
//};

//List<CleaningOption> options = new List<CleaningOption>();
//options.Add(oidOption);
//options.Add(dollarSignOption);

//foreach (string file in files)
//    MongoJsonCleaner.CleanFile(file, options);

UnitOfWork uow = new(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=SHTest;User Id=testUser; Password=testing;");

//await DataConverter.ConvertCustomers(uow);

await DataConverter.ConvertProducts(uow);