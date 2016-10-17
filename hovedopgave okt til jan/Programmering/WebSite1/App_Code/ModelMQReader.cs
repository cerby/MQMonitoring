using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModelMQReader
/// </summary>
public class ModelMQReader
{
    private string queueName;
    private double queueAge;
    private double queueDepth;
    private bool agechecker;
    public ModelMQReader(string queuename, double queueage, double queuedepth)
    {
        this.queueAge = queueage;
        this.queueDepth = queuedepth;
        this.queueName = queuename;
    }
    public string GetQueueName
    {
        get { return queueName; }
    }
    public double GetQueueDepth
    {
        get { return queueDepth; }
    }
    public double GetQueueAge
    {
        get { return queueAge; }
    }
    public bool GetAgeChecker
    {
        get { return agechecker; }
    }
    public bool SetAgeChecker
    {
        set { agechecker = value; }
    }
}
