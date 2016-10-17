using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;


public partial class Index : System.Web.UI.Page
{
    private ModelMQConnect _mqc = new ModelMQConnect();
    private ModelXMLReader QueueXML = new ModelXMLReader();
    private List<ModelXMLQueue> QList;
    private bool connected;
    private readonly Dictionary<string, ModelMQReader> queuedic = new Dictionary<string, ModelMQReader>();
    private string htmlstring;
    private readonly string _queuemanagername = ConfigurationManager.AppSettings["QueueManagerName"];
    private readonly string _serverip = ConfigurationManager.AppSettings["QueueManagerIP"];
    private readonly int _queuemanagerport = Convert.ToInt32(ConfigurationManager.AppSettings["QueueManagerPort"]);
    private readonly string _channelname = ConfigurationManager.AppSettings["QueueManagerChannel"];
    
    protected void Page_Load(object sender, EventArgs e)
    {
        QList = QueueXML.GetQueue();
        DateTime[] lastUpdate = new DateTime[QList.Count]; //isthis constantly being updated?
        if (!IsPostBack)//first run, update all
        {                     
            connected = _mqc.ConnectMQ(_queuemanagername, _serverip, _queuemanagerport, _channelname);
            //td1_1.Text = conDetails;
            
                for (int i = 0; i < QList.Count; i++)
                {
                    int k = i;
                    RunThread(QList[k]);
                    lastUpdate[k] = DateTime.Now;
                }
        }
         /* The thought here is that first time around, lastupdate array is filled with times from the initial run
          * When Page_load runs second time, the if sentence will not run and will not update times
          * However the code below WILL
          * The Timespan t then calculates the time difference between the initial run and the current loop runthrough
          * the if sentence then checks whether the time from timespan T is more than the set updateinterval in the xmlfile
          * if the time since last update of the table is more than the set updateinterval, then it will go into the if sentence
          * it will then set a new time in lastupdate array, at the point where it is updating and run the runthread method
          * the new update time prevents the loop from running at the specific queue again, before the appropriate amount of time has gone
         */        
            for (int i = 0; i < QList.Count; i++)
            {
                DateTime updattime = DateTime.Now;
                TimeSpan t = lastUpdate[i] - updattime;
                double timespanner = Convert.ToDouble(t.TotalMinutes);
                if (timespanner >= QList[i].GetUpdateInterval)
                {
                    lastUpdate[i] = DateTime.Now;
                    RunThread(QList[i]);
                }
                
            }
            
        

       
    }

    public void Sendtoview(ModelMQReader mqr )
    {
                htmlstring += "<div class='box'><table ><tr>";
                if (mqr.GetAgeChecker)
                {
                    htmlstring += "<td style='background-color:green'>" + mqr.GetQueueName + "</td>";
                }
                else
                {
                    htmlstring += "<td style='background-color:red'>" + mqr.GetQueueName + "</td>";
                }
                htmlstring += "<tr><td> Queue age: " + mqr.GetQueueAge + "</td></tr>";
                htmlstring += "<tr><td>Queue depth: " + mqr.GetQueueDepth + " </td></tr>";
                htmlstring += "</table></div>";
                div.InnerHtml = htmlstring;
    }
    public void RunThread(ModelXMLQueue d)
    {


        TimeSpan updateInterval = TimeSpan.FromSeconds(d.GetUpdateInterval);
        ModelMQReader mqr = _mqc.ReadQueueDetails(d.queueName);
        if (mqr.GetQueueAge == 0)
        {
            mqr = _mqc.ReadQueueDetails(d.queueName);
        }
        if (d.maxQueueAge >= mqr.GetQueueAge)
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

        }
        Sendtoview(mqr);
        Thread.Sleep(updateInterval);
    }
}