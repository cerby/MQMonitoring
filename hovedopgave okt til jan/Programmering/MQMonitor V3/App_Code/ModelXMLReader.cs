using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

public class ModelXMLReader
{

    Dictionary<string, ModelMQReader> queuedic = new Dictionary<string, ModelMQReader>();
    List<ModelXMLQueue> returnlist = new List<ModelXMLQueue>();// listen used for returns

    /* gets queue info, if the file doesnt exist it will make a new default file with default data
     */
    public Dictionary<string, ModelMQReader> GetQueue()
    {
        string filepath = ConfigurationManager.AppSettings["Path"].ToString();

        try
        {
            if (File.Exists(filepath))//If the files exist it will be read via Deserializer
            {
                XmlSerializer reader = new XmlSerializer(returnlist.GetType());
                StreamReader sr = new StreamReader(filepath);
                returnlist = (List<ModelXMLQueue>)reader.Deserialize(sr);            

            }
            else// id the file doesn't exist a new file is made via serializer
            {
                List<ModelXMLQueue> listQueues = new List<ModelXMLQueue>();
                listQueues.Add(new ModelXMLQueue("Configurate XML File", 5, 5));

                XmlSerializer serializer = new XmlSerializer(listQueues.GetType());
                StreamWriter writer = new StreamWriter(filepath);
                
                serializer.Serialize(writer.BaseStream, listQueues);

                writer.Close();
                GetQueue();
                
            }

            foreach (ModelXMLQueue xmlq in returnlist)
            {
                ModelMQReader mqr = new ModelMQReader(xmlq.GetQueueName, xmlq.GetMaxQueueAge, xmlq.GetUpdateInterval);
                queuedic.Add(mqr.GetQueueName , mqr);
            }
        }
        catch (Exception ex)
        {
            return new Dictionary<string,ModelMQReader>();// if something fails an empty dictionary is returned
            //no exception handling currently
            //a possibility is to add a bool to modelmqreader and then use get/set to notify if xmlread failed.
        }
		return queuedic;
    }
}


