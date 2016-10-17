using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

/// <summary>
/// Summary description for ModelXML
/// </summary>
public class ModelXML
{
    List<ModelQueue> returnlist = new List<ModelQueue>();// listen som bruger til return

    /* henter queue info fra xml fil, vis filen ikke eksistere laver den en ny default fil med
       køerne som de så ud d.27-11-2014
     */
    public List<ModelQueue> GetQueue()
    {
        string filepath = @"C:\Users\Christopher\Dropbox\Dat5\Queuedetails.xml";// sæt xml-fil navn

        try
        {
            if (File.Exists(filepath))//Vis filen findes læses man denne her via Deserializer
            {
                XmlSerializer reader = new XmlSerializer(returnlist.GetType());

                StreamReader sr = new StreamReader(filepath);

                returnlist = (List<ModelQueue>)reader.Deserialize(sr);

                return returnlist;

            }
            else// vis filen ikke eksistere skriver man en ny fil via listQueues og serializer
            {
                List<ModelQueue> listQueues = new List<ModelQueue>();
                listQueues.Add(new ModelQueue("TEST.QUEUE", 5, 5));
                listQueues.Add(new ModelQueue("TEST.QUEUE2", 5, 5));
                listQueues.Add(new ModelQueue("TEST.QUEUE3", 5, 5));
                //listQueues.Add(new ModelQueue("q4", 5, 5));

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
