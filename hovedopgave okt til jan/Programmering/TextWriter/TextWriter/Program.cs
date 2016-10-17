using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TextWriter
{
    class Program
    {
        static void Main(string[] args)
        {
            QueueList queueList = new QueueList();
            List<Queue> q = queueList.GetQueue();
            Console.WriteLine("elementer "+q.Count);
            foreach (Queue ql in q)
            {
                Console.WriteLine(ql.queueName+" "+ql.maxQueueAge+" "+ql.updateInterval);
            }
        }
    }

    public class QueueList
    {
        List<Queue> returnlist = new List<Queue>();// listen som bruger til return

        /* henter queue info fra xml fil, vis filen ikke eksistere laver den en ny default fil med
           køerne som de så ud d.27-11-2014
         */
        public List<Queue> GetQueue()
        {
            string filepath = @"C:\Users\Christopher\Dropbox\Dat5\test.xml";

            try
            {
                if (File.Exists(filepath))//Vis filen findes læses man denne her via Deserializer
                {
                    System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(returnlist.GetType());

                    System.IO.StreamReader sr = new System.IO.StreamReader(filepath);
                    
                    returnlist = (List<Queue>)reader.Deserialize(sr);

                    return returnlist;

                }
                else// vis filen ikke eksistere skriver man en ny fil via listQueues og serializer
                {
                    List<Queue> listQueues = new List<Queue>();
                    listQueues.Add(new Queue("q1", 5, 5));
                    listQueues.Add(new Queue("q2", 5, 5));
                    listQueues.Add(new Queue("q3", 5, 5));
                    listQueues.Add(new Queue("q4", 5, 5));

                    XmlSerializer serializer = new XmlSerializer(listQueues.GetType());
                    StreamWriter writer = new StreamWriter(filepath);

                    serializer.Serialize(writer.BaseStream, listQueues);

                    writer.Close();
                    GetQueue();
                    return returnlist;// bør ikke nåes er her for at stoppe compile error
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex);
                return returnlist;// returner en tom liste
            }
        }
    }
    [Serializable, XmlRoot("queues")]
    public class Queue
    {
        public string queueName;
        public double maxQueueAge;
        public double updateInterval;

        public Queue() { }
        public Queue(string queueName, double maxQueueAge, double updateInterval)
        {
            this.queueName = queueName;
            this.maxQueueAge = maxQueueAge;
            this.updateInterval = updateInterval;
        }
    }
}
