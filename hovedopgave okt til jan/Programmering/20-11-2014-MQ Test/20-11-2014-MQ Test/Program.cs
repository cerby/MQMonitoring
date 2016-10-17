using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.WMQ;

namespace _20_11_2014_MQ_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MQTest test = new MQTest();

            Console.WriteLine(test.ConnectMQ("QMTEST","", 1414, ""));
            //Console.WriteLine(test.OpenQueueConn("TestLocalQueue"));           
            Console.WriteLine(test.ReadMsg("TEST.QUEUE"));
            //win prof bruger adgang til mqm
        }
    }
}
