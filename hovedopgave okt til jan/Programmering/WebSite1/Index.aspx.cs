using System;
using System.Collections.Generic;
using System.Threading;


public partial class Index : System.Web.UI.Page
{
    ModelMQ _mqc = new ModelMQ();
    ModelXMLReader qr = new ModelXMLReader();
    private ModelMQReader MMQR;
    private List<ModelXMLQueue> QList; //items from XML
    private bool connected;
    Dictionary<string, ModelMQReader> queuedic = new Dictionary<string, ModelMQReader>();
    string htmlstring;


    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            connected = _mqc.ConnectMQ("QMTEST", "", 1414, ""); //connection info: mqmanagername, ip, port, channelname
            //td1_1.Text = conDetails;
            QList = qr.GetQueue();
      
            
        }

        div.InnerHtml = htmlstring;


    }
    public void RunThread(ModelXMLQueue d, bool connected)
    {
        string queueName = d.GetQueueName;
        double maxQueueAge = d.GetMaxQueueAge;
        TimeSpan updateInterval = TimeSpan.FromSeconds(d.GetUpdateInterval);
        ModelMQReader mqr;
            lock (this)
            {
                mqr = _mqc.ReadQueueDetails(queueName);

                if (maxQueueAge >= mqr.GetQueueAge)
                {
                    mqr.SetAgeChecker = true;
                    if (queuedic.ContainsKey(mqr.GetQueueName))
                    {
                        queuedic[mqr.GetQueueName] = mqr;
                    }
                    else
                    {
                        queuedic.Add(mqr.GetQueueName, mqr);
                    }
                    Thread.Sleep(updateInterval);
                }
                else
                {
                    mqr.SetAgeChecker = false;
                    if (queuedic.ContainsKey(mqr.GetQueueName))
                    {
                        queuedic[mqr.GetQueueName] = mqr;
                    }
                    else
                    {
                        queuedic.Add(mqr.GetQueueName, mqr);
                    }
                    Thread.Sleep(updateInterval);
                }
            }
    }
}