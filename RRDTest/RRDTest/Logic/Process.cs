using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRDTest.Logic
{
    internal class Process
    {
        int rowCounter = 0;
        CSV CSV = new CSV();
        List<Wide> wide = new List<Wide>();
        List<Narrow> narrow = new List<Narrow>();

        public void Run()
        {
            string fileName = GetFileName();

            FillWideList(fileName);
            ConvertWideToNarrow();
            CSV.CreateFile(narrow, fileName);
            Console.WriteLine("The conversion is finished, press ENTER exit.");
            Console.Read();
            Environment.Exit(0);
        }
        
        private string GetFileName()
        {
            Console.WriteLine("The file name to convert is MOCK_DATA?");
            Console.WriteLine("Y = \"Yes\", N = \"No\"");
            string response = Console.ReadLine().ToUpper();

            string file;
            if (response.Equals("Y"))
            {
                file = "MOCK_DATA";
            }
            else if (response.Equals("N"))
            {
                Console.WriteLine("Write the CSV file name:");
                file = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Try Again...{0}", Environment.NewLine);
                file = GetFileName();
            }

            return file;
        }

        private void FillWideList(string fileName)
        {
            try
            {
                DataTable dataTable = CSV.ReadFile(fileName);

                if (dataTable != null)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            wide.Add(new Wide
                            {
                                Flag1 = dataTable.Rows[i][0].ToString(),
                                Flag2 = dataTable.Rows[i][1].ToString(),
                                Flag3 = dataTable.Rows[i][2].ToString(),
                                Flag4 = dataTable.Rows[i][3].ToString(),
                                Flag5 = dataTable.Rows[i][4].ToString(),
                                Name1 = dataTable.Rows[i][5].ToString(),
                                Name2 = dataTable.Rows[i][6].ToString(),
                                Name3 = dataTable.Rows[i][7].ToString(),
                                Name4 = dataTable.Rows[i][8].ToString(),
                                Address1 = dataTable.Rows[i][9].ToString(),
                                Address2 = dataTable.Rows[i][10].ToString(),
                                Address3 = dataTable.Rows[i][11].ToString(),
                                Address4 = dataTable.Rows[i][12].ToString(),
                                City1 = dataTable.Rows[i][13].ToString(),
                                City2 = dataTable.Rows[i][14].ToString(),
                                City3 = dataTable.Rows[i][15].ToString(),
                                City4 = dataTable.Rows[i][16].ToString(),
                                State1 = dataTable.Rows[i][17].ToString(),
                                State2 = dataTable.Rows[i][18].ToString(),
                                State3 = dataTable.Rows[i][19].ToString(),
                                State4 = dataTable.Rows[i][20].ToString(),
                                Zip1 = dataTable.Rows[i][21].ToString(),
                                Zip2 = dataTable.Rows[i][22].ToString(),
                                Zip3 = dataTable.Rows[i][23].ToString(),
                                Zip4 = dataTable.Rows[i][24].ToString()
                            });
                        }
                    }
                    else
                    {
                        AskRetry(1);
                    }
                }
                else
                {
                    AskRetry(2);
                }
            }
            catch (Exception ex)
            {
                AskRetry(0);
            }
        }

        private void ConvertWideToNarrow()
        {
            foreach (var r in wide)
            {
                FillNarrowList(r.Flag1, r.Flag2, r.Flag3, r.Flag4, r.Flag5, r.Name1, r.Address1, r.City1, r.State1, r.Zip1);
                FillNarrowList(r.Flag1, r.Flag2, r.Flag3, r.Flag4, r.Flag5, r.Name2, r.Address2, r.City2, r.State2, r.Zip2);
                FillNarrowList(r.Flag1, r.Flag2, r.Flag3, r.Flag4, r.Flag5, r.Name3, r.Address3, r.City3, r.State3, r.Zip3);
                FillNarrowList(r.Flag1, r.Flag2, r.Flag3, r.Flag4, r.Flag5, r.Name4, r.Address4, r.City4, r.State4, r.Zip4);
            }
            Console.WriteLine();
        }

        private void FillNarrowList(string flag1, string flag2, string flag3, string flag4, string flag5, string name, string address, string city, string state, string zip)
        {
            if(!name.Equals("") && !address.Equals("") && !city.Equals("") && !state.Equals("") && !zip.Equals(""))
            {
                narrow.Add(new Narrow
                {
                    Flag1 = flag1,
                    Flag2 = flag2,
                    Flag3 = flag3,
                    Flag4 = flag4,
                    Flag5 = flag5,
                    Name = name,
                    Address = address,
                    City = city,
                    State = state,
                    Zip = zip
                });

                rowCounter++;
                if (rowCounter > 1)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                }  
                Console.WriteLine("{0} rows processed.", rowCounter);
            }
        }

        private void AskRetry(int e)
        {
            switch (e)
            {
                case 1:
                    Console.WriteLine("Empty data. Would you like to try another file?");
                    break;
                case 2:
                    Console.WriteLine("The file does not exist or has errors. Would you like to try another file?");
                    break;
                default:
                    Console.WriteLine("Error. Would you like to try again?");
                    break;
            }
            Console.WriteLine("Y = \"Yes\", N = \"No\"");
            string response = Console.ReadLine().ToUpper();

            if (response.Equals("Y"))
            {
                Run();
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
