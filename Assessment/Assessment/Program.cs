using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assessment
{
    public class Program
    {
        static void Main(string[] args)
        {
            try 
            {
                BL.ReadData();
                BL.OutPutNameFrequency();
                BL.OutputOrderedAddresses();

                Console.WriteLine("Open Output files? [Y/N]");

                if (Console.ReadKey().KeyChar.ToString().ToUpper() == "Y")
                {
                    try
                    {
                        Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), BL.OutputNames));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to open the Names output file.");
                    }
                    try
                    {
                        Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), BL.OutputAddresses));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Failed to open the Addresses output file.");
                    }
                }

                Console.WriteLine("All Done...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops...Something went wrong there...");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
