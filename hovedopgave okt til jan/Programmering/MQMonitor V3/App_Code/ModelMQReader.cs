using System;

public class ModelMQReader
{
    private string queueName;
    private double queueAge;
    private double queueDepth;
    private double maxQueueAge;
    private double updateInterval;
    private DateTime lastUpdated;
    private bool agechecker;

    public ModelMQReader(string queuename, double maxqueueage, double updateinterval)
    {
        this.queueName = queuename;
        this.maxQueueAge = maxqueueage;
        this.updateInterval = updateinterval;
    }
    public double GetUpdateInterval
    {
        get { return updateInterval; }
    }
    
    public double GetMaxQueueAge
    {
        get { return maxQueueAge; }
    }
    
    public string GetQueueName
    {
        get { return queueName; }
    }
    public DateTime GetLastUpdated
    {
        get { return lastUpdated; }
    }
    public DateTime SetLastUpdated
    {
        set { lastUpdated = value; }
    }
    public double GetQueueDepth
    {
        get { return queueDepth; }
    }
    public double SetQueueDepth
    {
        set { queueDepth = value; }
    }
    public double GetQueueAge
    {
        get { return queueAge; }
    }
    public double SetQueueAge
    {
        set { queueAge = value; }
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
