namespace DataConversion
{
    public class MongoJsonCleaner
    {
        // Python to execute possibly:
        // filepath = "C:\\SensationalHairPOS\\SensationHairPOS\\SensationalHairPOSCommon\\DataConversion\\Data\\transactions.json"
        // with open(filepath) as f:
        //    lines = f.read().splitlines()

        // with open(filepath, "w") as f:
        //    for line in lines:
        //        f.write(line + ",\n")

        public static void CleanFile(string filePath, IEnumerable<CleaningOption> cleaningOptions)
        {
            string text = File.ReadAllText(filePath);

            foreach (CleaningOption co in cleaningOptions)
                text = text.Replace(co.OldValue, co.NewValue);

            File.WriteAllText(filePath, text);
        }
    }

    public class CleaningOption
    {
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}