using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomConfigSectionApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            string greetingsRecipient = ConfigurationManager.AppSettings["GreetingsRecipient"];
            Console.WriteLine("Hello, " + greetingsRecipient + "!");

            foreach (MyElement myElement in Config.Instance.MyElements)
            {
                Console.WriteLine("Element Name: " + myElement.Name);
                Console.WriteLine("Enabled: " + myElement.Enabled);
                Console.WriteLine("Check Last Item: " + myElement.MySpecifics.CheckLastItem);
                Console.WriteLine("Bulk Count: " + myElement.MySpecifics.BulkCount);

                foreach (MyFrequency myFrequency in myElement.MyFrequencies)
                { 
                    Console.WriteLine("Frequency: " + myFrequency.Name);
                    Console.WriteLine("Start: " + myFrequency.Start.ToString());
                    Console.WriteLine("End: " + myFrequency.End.ToString());
                    Console.WriteLine("Check In Every: " + myFrequency.CheckInEveryXMinutes);
                }
            }

            Console.ReadKey();
        }
    }
}
