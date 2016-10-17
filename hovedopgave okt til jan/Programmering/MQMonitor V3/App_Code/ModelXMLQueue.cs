using System;
using System.Xml.Serialization;

/// <summary>
/// Summary description for Queue
///	container class for queue info 
/// </summary>
[Serializable, XmlRoot("queues")]
public class ModelXMLQueue
{
    public string queueName; 
    public double maxQueueAge;
    public double updateInterval;

    public ModelXMLQueue() { }
    public ModelXMLQueue(string queueName, double maxQueueAge, double updateInterval)
    {
        this.queueName = queueName;
        this.maxQueueAge = maxQueueAge;
        this.updateInterval = updateInterval;
    }

   public string GetQueueName
    {
        get { return queueName; }
    }

    public double GetMaxQueueAge
    {
        get { return maxQueueAge; }
    }

    public double GetUpdateInterval
    {
        get { return updateInterval; }
    }
}