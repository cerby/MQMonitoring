using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModelQueue
/// </summary>
public class ModelQueue
{
    private String QueueName;
    private double MessageAge;
    private int QueueDepth;
	public ModelQueue(string queueName, double messageAge, int queueDepth)
	{
	    this.QueueName = queueName;
	    this.MessageAge = messageAge;
	    this.QueueDepth = queueDepth;
	}

    public string GetQueueName
    {
        get { return QueueName; }
    }

    public double GetMessageAge
    {
        get { return MessageAge; }
    }

    public int GetQueueDepth
    {
        get { return QueueDepth; }
    }
}