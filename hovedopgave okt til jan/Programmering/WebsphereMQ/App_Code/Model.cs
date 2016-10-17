using System;
using System.Collections;
using System.Collections.Generic;
using IBM.WMQ;
using System.Web.UI;

/// <summary>
/// Summary description for Model
/// </summary>
public class Model
{
	public Model()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    MQQueueManager _mqManager;
    private const int OpenInputOptions = MQC.MQOO_INPUT_SHARED | MQC.MQOO_BROWSE | MQC.MQOO_INQUIRE | MQC.MQOO_FAIL_IF_QUIESCING;
    public string ConnectMQ(string mqManagerName, string mqServerIP, int mqServerPort, string mqChanelName)
    {
        string strReturn = "";
        string transport = "";
        string transportInfo = "";

        try
        {
            Hashtable connectionProperties = new Hashtable();

            //server
            if (mqServerIP == "" || mqChanelName == "")
            {
                transport = MQC.TRANSPORT_MQSERIES_BINDINGS;

                transportInfo = MQC.TRANSPORT_MQSERIES_BINDINGS;

                connectionProperties.Add(MQC.TRANSPORT_PROPERTY, transport);
            }
            else //client
            {
                transport = MQC.TRANSPORT_MQSERIES_CLIENT;
                transportInfo = MQC.TRANSPORT_MQSERIES_CLIENT;
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
            strReturn = "Connected";
        }
        catch (MQException exp)
        {
            strReturn = "Exception: " + transportInfo + "\n" + exp.Message;

        }
        return strReturn;
    }

    public string DisconnectMQ()
    {
        string strReturn = "";
        if (_mqManager != null)
        {
            _mqManager.Commit();
            _mqManager.Disconnect();
            strReturn = "Disconnected Successfully";
        }
        else
        {
            strReturn = "Queue Manager is null and cannot be disconnected";

        }
        return strReturn;
    }


    public ModelQueue ReadQueueDetails(string queuename)
    {
        ModelQueue mqd;
        string strReturn = "";
        //string output = ""; //skal den her bruges?
        List<ModelXMLQueue> mqdList = new List<ModelXMLQueue>();
        try
        {
            MQQueue queue = _mqManager.AccessQueue(queuename, OpenInputOptions); //MQC.MQOO_INPUT_SHARED + MQC.MQOO_FAIL_IF_QUIESCING
            MQMessage message = new MQMessage();
            MQGetMessageOptions gmo = new MQGetMessageOptions();

            gmo.Options = MQC.MQGMO_BROWSE_FIRST;
            
            queue.Get(message, gmo);
            
            int depth = queue.CurrentDepth;
            DateTime msgage = message.PutDateTime;
            DateTime now = DateTime.UtcNow;
            TimeSpan difference = now - msgage;
            double tid = Math.Round(difference.TotalMinutes, 2);
            mqd = new ModelQueue(queuename, tid, depth);
            queue.Close();
            // output = message.ReadString(message.MessageLength); 
            return mqd;
        
        }
        catch (MQException MQexp)
        {
            mqd = new ModelQueue(MQexp.Message,0,0);
            
        }
        catch (Exception exp)
        {
            mqd = new ModelQueue(exp.Message,1,1);
        }
        return mqd;
    }
}