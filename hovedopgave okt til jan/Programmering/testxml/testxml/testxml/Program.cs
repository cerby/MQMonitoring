using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace testxml
{
    class Program
    {
        static void Main(string[] args)
        {
            using (XmlReader reader = XmlReader.Create(@"C:\ArcheAge\test.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "Name":
                                Console.WriteLine("Name of student: " + reader.ReadString());
                                break;

                            case "Location":
                                Console.WriteLine("Location is: " + reader.ReadString());
                                break;
                        }
                    }
                    Console.WriteLine("---");
                }                             
            }
            Console.ReadKey();
        }
    }
}
