using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

public class ModelXMLReader
{
    List<ModelXMLQueue> returnlist = new List<ModelXMLQueue>();// listen used for returns

    /* gets queue info, if the file doesnt exist it will make a new default file with default data
		from how the queues looked at d.27-11-2014
     */
    public List<ModelXMLQueue> GetQueue()
    {
        string filepath = ConfigurationManager.AppSettings["Path"];
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

                XmlSerializer serializer = new XmlSerializer(listQueues.GetType());
                StreamWriter writer = new StreamWriter(filepath);
                
                serializer.Serialize(writer.BaseStream, listQueues);

                writer.Close();
                GetQueue();
            }
        }
        catch (Exception ex)
        {
            return returnlist;// if something fails an empty list is returned
        }
		return returnlist;
    }
}


