using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBM.WMQ;

namespace _20_11_2014_MQ_Test
{
    class MQTest
    {
        MQQueueManager _mqManager;
        private int OpenInputOptions = MQC.MQOO_INPUT_SHARED | MQC.MQOO_BROWSE | MQC.MQOO_INQUIRE | MQC.MQOO_FAIL_IF_QUIESCING;

        public MQTest()
        {

        }

        public string ConnectMQ(string mqManagerName, string mqServerIP, int mqServerPort, string mqChanelName)
        {
            string strReturn = "";
            string transport = "";
            string transportInfo = "";

            try
            {
                Hashtable connectionPrpoerties = new Hashtable();
                
                //server
                if (mqServerIP == "" || mqChanelName =="")
                {
                    transport = MQC.TRANSPORT_MQSERIES_BINDINGS;

                    transportInfo = MQC.TRANSPORT_MQSERIES_BINDINGS;
                    
                    connectionPrpoerties.Add(MQC.TRANSPORT_PROPERTY, transport);
                }
                else //client
                {
                    transport = MQC.TRANSPORT_MQSERIES_CLIENT;
                    transportInfo = MQC.TRANSPORT_MQSERIES_CLIENT;
                    //connect as client

                    connectionPrpoerties.Add(MQC.TRANSPORT_PROPERTY, transport);
                    connectionPrpoerties.Add(MQC.HOST_NAME_PROPERTY, mqServerIP);
                    if (mqServerPort != -1)
                    {
                        connectionPrpoerties.Add(MQC.PORT_PROPERTY, mqServerPort);
                    }
                    connectionPrpoerties.Add(MQC.CHANNEL_PROPERTY, mqChanelName);
                }
                _mqManager = new MQQueueManager(mqManagerName, connectionPrpoerties);
                strReturn = "Connected succesfully";
            }
            catch (MQException exp)
            {
                strReturn = "Exception: " + transportInfo + "\n" + exp.Message;
                
            }
            return strReturn;
        }

        public MQQueue OpenQueueConn(string q)
        {
            MQQueue queue = _mqManager.AccessQueue(q, OpenInputOptions); //MQC.MQOO_INPUT_SHARED + MQC.MQOO_FAIL_IF_QUIESCING
            string retOpen = "Connection open";
            return queue;
        }

        public string CloseQueueConn(string q)
        {
            MQQueue queue = _mqManager.AccessQueue(q, OpenInputOptions);
            MQMessage message = new MQMessage();
            MQGetMessageOptions gmo = new MQGetMessageOptions();
            queue.Get(message, gmo);
            queue.Close();
            string close = "connection to " + q + " closed";
            return close;
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

        public string WriteMsg(string stringputMsg, string q)
        {
            string strReturn = "";

            try
            {
                MQQueue queue = _mqManager.AccessQueue(q, MQC.MQOO_OUTPUT + MQC.MQOO_FAIL_IF_QUIESCING);
                MQMessage queueMessage = new MQMessage();
                queueMessage.WriteString(stringputMsg);
                queueMessage.Format = MQC.MQFMT_STRING;
                MQPutMessageOptions queuePutMessageOptions = new MQPutMessageOptions();
                queue.Put(queueMessage, queuePutMessageOptions);
                queue.Close();
                strReturn = "Message put to queue successfully";
            }
            catch (MQException MQexp)
            {
                strReturn = "Exception: " + MQexp.Message;
                throw;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
            }
            return strReturn;
        }

        public string ReadMsg(string q)
        {
            
            string strReturn = "";
            string output = ""; //skal den her bruges?

            try
            {
                MQQueue queue = _mqManager.AccessQueue(q, OpenInputOptions);
                MQMessage message = new MQMessage();
                MQGetMessageOptions gmo = new MQGetMessageOptions();

                gmo.Options = MQC.MQGMO_BROWSE_NEXT;
                
                output = message.ReadString(message.MessageLength);
                DateTime datetime = message.PutDateTime;
                strReturn = "Message get from queue successfully "+ datetime + message;
            }
            catch (MQException MQexp)
            {
                strReturn = "Exception: " + MQexp.Message;
            }
            catch (Exception exp)
            {
                strReturn = "Exception: " + exp.Message;
            }
            return strReturn;
        }

    }
}
