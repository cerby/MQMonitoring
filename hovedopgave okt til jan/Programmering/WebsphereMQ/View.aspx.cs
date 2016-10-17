using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class View : System.Web.UI.Page
{
    Model _model = new Model();
    QueueList ql = new QueueList();
    private ModelXMLQueue Queueclass;
    private List<ModelXMLQueue> _qList; //items from XML
    private List<ModelQueue> _modQ = new List<ModelQueue>();
    private Thread t;
 
    protected void Page_Load(object sender, EventArgs e)
    {
        string conDetails = _model.ConnectMQ("QMTEST", "", 1414, "");
        TextBox1.Text = conDetails;
        _qList = ql.GetQueue();
        while (ConnectionStatus(conDetails) == true)
        {
            for (int i = 0; i < _qList.Count; i++)
            {
                t = new Thread(() => QueueHandler(i, _qList[i]));
                t.Start();
            } 
        }        
    }

    public void QueueHandler(int queuecount, ModelXMLQueue model)
    {      
        _modQ.Add(_model.ReadQueueDetails(model.GetQueueName)); //load information about queue from queuename
        string Queuename =_modQ[queuecount].GetQueueName; //contains name of loaded queue
        double MessageAge = _modQ[queuecount].GetMessageAge; //contains age of oldest message in loaded queue
        int depth = _modQ[queuecount].GetQueueDepth; //contains the number of messages in loaded queue
        bool agealert = MessageAgeChecker(MessageAge, model.GetMaxQueueAge); //false if age of oldest message in queue is more than the set max age
        Thread.Sleep(SleepTimer((int)model.GetUpdateInterval)); //sleeps the function for the time set
    }
    public bool MessageAgeChecker(double messageage, double maxage) //todo: make method check if oldest message is too old
    {
        bool age = true;
        if (messageage >= maxage)
        {
            age = false;
        }
        return age;
    }

    public bool ConnectionStatus(string status)
    {
        bool queuemanagerstatus = false;

        if (status == "Connected")
        {
            queuemanagerstatus = true;
        }

        return queuemanagerstatus;
    }

    public void AlertMessageAge() //todo: make method throw an alert in view, if messages are too old, (might be implemented into MessageAgeChecker)
    {
        
    }
    
    private int SleepTimer(double refreshrate)
    {
        int sleeper = (int)refreshrate * 60000; //one minute is 60000 milliseconds and thread.sleep uses milliseconds, with this we convert the refreshrate from the xml file (which is in minutes) to milliseconds
        return sleeper;
    }

    public void Start()
    {
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
}