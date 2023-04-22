
namespace File_Manipulator.Test
{
    [TestFixture]
    public class ProgramTests
    {
        private const string CsvFilePath = "Data.csv";
        private const string NamesFilePath = "Names.txt";
        private const string AddressesFilePath = "Addresses.txt";

        [Test]
        public void TestMainMethod()
        {
            // Arrange

            // Delete the output files if they already exist
            if (File.Exists(NamesFilePath))
            {
                File.Delete(NamesFilePath);
            }
            if (File.Exists(AddressesFilePath))
            {
                File.Delete(AddressesFilePath);
            }

            // Act
            Program.Main(new[] { CsvFilePath });

            // Assert
            Assert.That(File.Exists(NamesFilePath), Is.True);
            Assert.That(File.Exists(AddressesFilePath), Is.True);

            var names = File.ReadAllLines(NamesFilePath);
            Assert.That(names, Is.Not.Empty);
            Assert.That(names.Length, Is.EqualTo(8));
            Assert.That(names[0], Is.EqualTo("Brown, Graham, 1"));
            Assert.That(names[7], Is.EqualTo("Smith, Jimmy, 1"));

            var addresses = File.ReadAllLines(AddressesFilePath);
            Assert.That(addresses, Is.Not.Empty);
            Assert.That(addresses.Length, Is.EqualTo(8));
            Assert.That(addresses[0], Is.EqualTo("102 Long Lane"));
            Assert.That(addresses[7], Is.EqualTo("94 Roland St"));
        }

        [Test]
        public void TestReadCsvMethod()
        {
            // Arrange

            // Act
            var records = Program.ReadCsv(CsvFilePath);

            // Assert
            Assert.That(records, Is.Not.Null);
            Assert.That(records, Is.Not.Empty);
            Assert.That(records.Count, Is.EqualTo(8));
            Assert.That(records[0].FirstName, Is.EqualTo("Jimmy"));
            Assert.That(records[0].LastName, Is.EqualTo("Smith"));
            Assert.That(records[0].Address, Is.EqualTo("102 Long Lane"));
            Assert.That(records[0].PhoneNumber, Is.EqualTo("29384857"));
        }
    }
}