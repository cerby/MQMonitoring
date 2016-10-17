using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Class1
{

    
    public void setlist()
    {
        
    }
     
}
public class data
{
    public string navn;
    public int antal;
    public bool forgammel;
    public data(string n, int i)
    {
        this.navn = n;
        this.antal = i;
    }
    public bool SetForGammet
    {
        set { forgammel = value; }
    }
}