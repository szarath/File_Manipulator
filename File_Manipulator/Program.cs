using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Linq;

namespace File_Manipulator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var records = ReadCsv("Data.csv");

            // Get frequency of first and last names
            var names = records.GroupBy(r => new { r.FirstName, r.LastName })
                               .Select(g => new { Name = $"{g.Key.LastName}, {g.Key.FirstName}", Frequency = g.Count() })
                               .OrderByDescending(n => n.Frequency)
                               .ThenBy(n => n.Name);

            // Write names to text file
            using (StreamWriter sw = new StreamWriter("Names.txt"))
            {
                foreach (var name in names)
                {
                    sw.WriteLine($"{name.Name}, {name.Frequency}");
                }
            }

            // Get addresses sorted alphabetically by street name
            var addresses = records.OrderBy(r => r.Address);

            // Write addresses to text file
            using (StreamWriter sw = new StreamWriter("Addresses.txt"))
            {
                foreach (var address in addresses)
                {
                    sw.WriteLine(address.Address);
                }
            }
        }

        public static List<Record> ReadCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<Record>().ToList();
                }
            }
        }

    }

    public class Record
    {
        [Name("FirstName")]
        public string FirstName { get; set; }

        [Name("LastName")]
        public string LastName { get; set; }

        [Name("Address")]
        public string Address { get; set; }

        [Name("PhoneNumber")]
        public string PhoneNumber { get; set; }
    }

}


