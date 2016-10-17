using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    
    Dictionary<string, data> mydic = new Dictionary<string, data>();
    string inner = "";
    Thread th;
    Class1 c = new Class1();
    public List<data> dl = new List<data>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            dl.Add(new data("navn", 4));
            dl.Add(new data("navn1", 5));
            dl.Add(new data("navn2", 3));
            dl.Add(new data("navn3", 3));
            
        }

        foreach (data d in dl)
        {
            th = new Thread(() => RunThread(d));
            th.Start();
            TimeSpan ts = TimeSpan.FromSeconds(0.2);
            Thread.Sleep(ts);
        }
        
        foreach (KeyValuePair<string,data> t in mydic)
        {
            if (t.Value.forgammel)
            {
                inner += "den er god nok: "+ t.Value.navn;
            }
            else
            {
                inner += "den er for gammel: "+t.Value.navn;
            }
            inner += "<br />";

        }
        div.InnerHtml = inner;
        //th.Join();
        
    }
    public void RunThread(data d)
    {
        int tal = 3;
        
        lock (this)
        {
            if (tal >= d.antal)
            {
                d.SetForGammet = true;
                mydic.Add(d.navn, d);
            }
            else
            {
                d.SetForGammet = false;
                mydic.Add(d.navn, d);
            }
        }
        
        
        
    }
}