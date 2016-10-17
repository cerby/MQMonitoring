using System;
using System.Collections;
using IBM.WMQ;


public class ModelMQConnect
{

    MQQueueManager _mqManager;
    private const int OpenInputOptions = MQC.MQOO_INPUT_SHARED | MQC.MQOO_BROWSE | MQC.MQOO_INQUIRE | MQC.MQOO_FAIL_IF_QUIESCING;
    public void ConnectMQ(string mqManagerName, string mqServerIP, int mqServerPort, string mqChanelName)
    {
        string transport = "";
        try
        {
            Hashtable connectionProperties = new Hashtable();

            //server
            if (mqServerIP == "" || mqChanelName == "")
            {
                transport = MQC.TRANSPORT_MQSERIES_BINDINGS;

                connectionProperties.Add(MQC.TRANSPORT_PROPERTY, transport);
            }
            else //client
            {
                transport = MQC.TRANSPORT_MQSERIES_CLIENT;
                //connect as client

                connectionProperties.Add(MQC.TRANSPORT_PROPERTY, transport);
                connectionProperties.Add(MQC.HOST_NAME_PROPERTY, mqServerIP);
                if (mqServerPort != -1)
                {
                    connectionProperties.Add(MQC.PORT_PROPERTY, mqServerPort);
                }
                connectionProperties.Add(MQC.CHANNEL_PROPERTY, mqChanelName);
            }
            _mqManager = new MQQueueManager(mqManagerName, connectionProperties);
            
        }
        catch (MQException exp)
        {
            //no exception handling currently
            //a possibility is to convert the method from void to bool and then if false, raise an alert in view

        }
    }

    public void DisconnectMQ()
    {
        if (_mqManager != null)
        {
            _mqManager.Commit();
            _mqManager.Disconnect();
        }
    }


    public ModelMQReader ReadQueueDetails(ModelMQReader mqr)
    {
        MQQueue queue = _mqManager.AccessQueue(mqr.GetQueueName, OpenInputOptions);
        DateTime currentTime = DateTime.Now;
        mqr.SetLastUpdated = currentTime;

        try
        {
            //MQQueue queue = _mqManager.AccessQueue(queuename, OpenInputOptions); //MQC.MQOO_INPUT_SHARED + MQC.MQOO_FAIL_IF_QUIESCING
            MQMessage message = new MQMessage();
            MQGetMessageOptions gmo = new MQGetMessageOptions();

            gmo.Options = MQC.MQGMO_BROWSE_FIRST;
            
            queue.Get(message, gmo);
            
            int depth = queue.CurrentDepth;
            DateTime msgage = message.PutDateTime;
            DateTime now = DateTime.UtcNow;
            TimeSpan difference = now - msgage;
            double age = Math.Round(difference.TotalMinutes, 2);
            
            mqr.SetQueueAge = age;
            mqr.SetQueueDepth = depth;      
        }
        catch (MQException MQexp)
        {
            mqr.SetQueueAge = 0;
            mqr.SetQueueDepth = 0;
            //no exception handling currently
            //a possibility is to add a bool to modelmqreader and then use get/set to notify if queueread failed.
        }
        queue.Close();
        return mqr;
    }
}