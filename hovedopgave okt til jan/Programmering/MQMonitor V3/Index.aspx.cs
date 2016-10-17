using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

public partial class Index : System.Web.UI.Page
{
    ModelMQConnect _mqc = new ModelMQConnect();
    ModelXMLReader qr = new ModelXMLReader();
    public static Dictionary<string, ModelMQReader> queuedic = new Dictionary<string, ModelMQReader>();
    string htmlstring;
    private readonly string _queuemanagername = ConfigurationManager.AppSettings["QueueManagerName"];
    private readonly string _serverip = ConfigurationManager.AppSettings["QueueManagerIP"];
    private readonly int _queuemanagerport = Convert.ToInt32(ConfigurationManager.AppSettings["QueueManagerPort"]);
    private readonly string _channelname = ConfigurationManager.AppSettings["QueueManagerChannel"];

    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {              
             _mqc.ConnectMQ(_queuemanagername, _serverip, _queuemanagerport, _channelname);
            if (!IsPostBack)
            {
                //queuedic gets information from getqueue about all queues in xml-file
                queuedic = qr.GetQueue();

                //first run of the program,it reads all the queues in queuedic
                foreach (ModelMQReader t in queuedic.Values.ToList())
                {
                    ReadQueue(t);
                }
                //and then sends it all to the innerhtml
               Sendtoview();
            }
        }

        catch (Exception)
        {
            _mqc.DisconnectMQ();
        }

    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        DateTime timeNow = DateTime.Now;
        TimeSpan tstimer;
        
        foreach (ModelMQReader r in queuedic.Values.ToList())
        {
            tstimer = timeNow - r.GetLastUpdated;
            double timerspanner = tstimer.TotalSeconds;

            if (timerspanner >= r.GetUpdateInterval)
            {
                ReadQueue(r);
            }
        }
        Sendtoview();        
    }
    public void Sendtoview() 
    {
        htmlstring = "";
        foreach (ModelMQReader t in queuedic.Values.ToList())
        {
            htmlstring += "<div class='box'><table ><tr>";
            htmlstring += "<table ><tr>";
            if (t.GetAgeChecker)
            {
                htmlstring += "<td style='background-color:green'>" + t.GetQueueName + "</td>";
            }
            else
            {
                htmlstring += "<td style='background-color:red'>" + t.GetQueueName + "</td>";
            }
            htmlstring += "<tr><td> Queue age: " + t.GetQueueAge + "</td></tr>";
            htmlstring += "<tr><td>Queue depth: " + t.GetQueueDepth + " </td></tr>";
            htmlstring += "</table></div>";
        }
        div.InnerHtml = htmlstring;
    }
    public void ReadQueue(ModelMQReader d)
    {
        lock (this)
        {
            d = _mqc.ReadQueueDetails(d); //read from queue

            if (d.GetMaxQueueAge >= d.GetQueueAge) //is age higher or lower than maxage?
            {
                d.SetAgeChecker = true; //if it is less than maxage, set agechecker equal to true
                if (queuedic.ContainsKey(d.GetQueueName)) //if queue with queuename exists in dictionary do
                {
                    queuedic[d.GetQueueName] = d; //if yes, then set its values to d
                }
                else
                {
                    queuedic.Add(d.GetQueueName, d); //else add it to the dictionary.
                }

            }
            else //if age is higher than maxage do this
            {
                d.SetAgeChecker = false; //agechecker false
                if (queuedic.ContainsKey(d.GetQueueName)) //same as above
                {
                    queuedic[d.GetQueueName] = d;
                }
                else
                {
                    queuedic.Add(d.GetQueueName, d);
                }
            }
        }
    }
}