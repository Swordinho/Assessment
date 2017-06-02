using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assessment.Models;
using System.Reflection;

namespace Assessment
{
    public static class BL
    {
        public static string DataFile = "data.csv";
        public static string OutputNames { get { return "OrderedNames.txt"; } }
        public static string OutputAddresses { get { return "OrderedAddress.txt"; } }

        public static List<string> FullNames { get; set; }
        public static List<string> Addresses { get; set; }
        public static List<string> PhoneNumbers { get; set; }

        public static void ReadData()
        {
            try
            {
                if (!File.Exists(DataFile))
                    throw new Exception("Data file not found");

                FullNames = new List<string>();
                Addresses = new List<string>();
                PhoneNumbers = new List<string>();

                using (var sr = new StreamReader(new BufferedStream(new FileStream(DataFile, FileMode.Open))))
                {
                    var line = string.Empty;
                    string[] vals;
                    var isFirstLine = true;

                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine();
                        vals = line.Split(',');

                        if (isFirstLine)
                        { //Skip "Header" line
                            isFirstLine = false;
                            continue;
                        }

                        if (!string.IsNullOrEmpty(vals[0]) && vals[0].Trim() != string.Empty)
                        {
                            FullNames.Add(vals[0]);
                        }

                        if (!string.IsNullOrEmpty(vals[1]) && vals[1].Trim() != string.Empty)
                        {
                            FullNames.Add(vals[1]);
                        }

                        if (!string.IsNullOrEmpty(vals[2]) && vals[2].Trim() != string.Empty)
                        {
                            Addresses.Add(vals[2]);
                        }

                        if (!string.IsNullOrEmpty(vals[3]) && vals[3].Trim() != string.Empty)
                        {
                            PhoneNumbers.Add(vals[3]);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void OutPutNameFrequency()
        {
            try
            {
                using (var sw = new StreamWriter(new BufferedStream(new FileStream(OutputNames, FileMode.OpenOrCreate))))
                {
                    var names = NameFrequency();

                    if (names == null || names.Count() < 1)
                        return;

                    foreach(var name in names)
                    {
                        sw.WriteLine(name.Name + name.Frequency.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void OutputOrderedAddresses()
        {
			try 
            {
                using (var swr = new StreamWriter(new BufferedStream(new FileStream(OutputAddresses, FileMode.OpenOrCreate))))
				{
                    var addresses = OrderedAddresses();

					if (addresses == null || addresses.Count() < 1)
						return;

					foreach (var address in addresses)
					{
                        swr.WriteLine(address.StreetNumber + " " + address.StreetName);
					}
				}
            }
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
        }

        static List<NameFrequency> NameFrequency()
        {
            //The first should show the frequency of the first and last names ordered by frequency descending and then alphabetically ascending
            try
            {
                var NameFrequencies = (from name in FullNames.AsQueryable()
                                       group name by name into names
                                       select new NameFrequency()
                                       {
                                           Name = names.Key + ", ",
                                           Frequency = names.Count()
                                       }
                                      ).OrderByDescending(x => x.Frequency).ThenBy(y => y.Name)
                                       .ToList<NameFrequency>(); ;


                return NameFrequencies ?? new List<NameFrequency>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static List<Address> OrderedAddresses()
        { //The second should show the addresses sorted alphabetically by street name.
			try
            {
                var OrderedAddress = new List<Address>();
                string[] vals;
                foreach(var item in Addresses)
                {
                    vals = item.Split(' ');
                    OrderedAddress.Add(new Address() { StreetNumber = Convert.ToInt32(vals[0]), StreetName = vals[1] + " " + vals[2] });
                }

                if (OrderedAddress == null || OrderedAddress.Count() < 1)
                    return new List<Address>();

                return OrderedAddress.OrderBy(x => x.StreetName).ToList<Address>();
			}
			catch (Exception ex)
			{
                throw ex;
			}
        }
    }

}