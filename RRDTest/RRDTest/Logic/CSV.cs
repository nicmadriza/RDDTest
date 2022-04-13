using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LumenWorks.Framework.IO.Csv;
using System.Globalization;
using CsvHelper;

namespace RRDTest.Logic
{
    internal class CSV
    {
        public DataTable ReadFile(string fileName)
        {
            try 
            {
                DataTable dataTable = new DataTable();
                using (var csvReader = new LumenWorks.Framework.IO.Csv.CsvReader(new StreamReader(File.OpenRead(fileName + ".csv")), true))
                {
                    dataTable.Load(csvReader);
                }
                return dataTable;
            }
            catch
            {
                return null;
            }
        }

        public void CreateFile(List<Narrow> data, string fileName)
        {
            try
            {
                using (var writer = new StreamWriter("Converted_" + fileName + ".csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(data);
                }
                Console.WriteLine("Your {0}.csv file was successfully converted. The new file name is Converted_{0}.csv {1}", fileName, Environment.NewLine);
            }
            catch
            {
                Console.WriteLine("An error occurred creating the {0}.csv file. {1}", fileName, Environment.NewLine);
            }
        }
    }
}
