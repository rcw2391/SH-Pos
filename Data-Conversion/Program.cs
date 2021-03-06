using DataConversion;
using DataLayer;
using DataLayer.Models;

static void Main(string[] args)
{

}

string folder = @"C:\SH\SH\Data-Conversion\Data\";

string[] files = Directory.GetFiles(folder);

CleaningOption oidOption = new()
{
    OldValue = "$oid",
    NewValue = "oldId"
};

CleaningOption dollarSignOption = new()
{
    OldValue = "$",
    NewValue = ""
};

List<CleaningOption> options = new List<CleaningOption>();
options.Add(oidOption);
options.Add(dollarSignOption);

foreach (string file in files)
    MongoJsonCleaner.CleanFile(file, options);

UnitOfWork uow = new();

await DataConverter.ConvertCustomers(uow);

await DataConverter.ConvertProducts(uow);

await DataConverter.ConvertServices(uow);

await DataConverter.ConvertStylists(uow);

await DataConverter.ConvertTransactions(uow);