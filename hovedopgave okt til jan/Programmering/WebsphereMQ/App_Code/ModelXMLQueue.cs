using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Summary description for Queue
///	container class for queue info 
/// </summary>
[Serializable, XmlRoot("queues")]
public class ModelXMLQueue
{
    private string QueueName; //these should not be public, as then someone might fuck up and change a value...
    private double MaxQueueAge;
    private double UpdateInterval;

    public ModelXMLQueue() { }
    public ModelXMLQueue(string queueName, double maxQueueAge, double updateDepth)
    {
        this.QueueName = queueName;
        this.MaxQueueAge = maxQueueAge;
        this.UpdateInterval = updateDepth;
    }

    public string GetQueueName
    {
        get { return QueueName; }
    }

    public double GetMaxQueueAge
    {
        get { return MaxQueueAge; }
    }

    public double GetUpdateInterval
    {
        get { return UpdateInterval; }
    }
}