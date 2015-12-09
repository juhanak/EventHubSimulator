using Amqp;
using Amqp.Framing;
using SimpleSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusClientLib
{
    public class ServiceBusClient
    {
        string _eventHubNamespace = "";
        string _eventHubName = "";
        string _saPolicyName = "";
        string _saKey = "";
        string _partitionkey = "partition";

        Amqp.Address _address;
        Amqp.Connection _connection = null;
        Amqp.Session _session;
        Amqp.SenderLink _senderlink;

        public ServiceBusClient()
        {
            _eventHubNamespace = EventHubSettings.EventHubNamespace;
            _eventHubName = EventHubSettings.EventHubName;
            _saPolicyName = EventHubSettings.PolicyName;
            _saKey = EventHubSettings.Key;

            _address = new Amqp.Address(
                string.Format("{0}.servicebus.windows.net", _eventHubNamespace),
                5671, _saPolicyName, _saKey);
        }

        void Connect()
        {
           _connection = new Amqp.Connection(_address);
           _session = new Amqp.Session(_connection);
           _senderlink = new Amqp.SenderLink(_session,
                string.Format("send-link:{0}", _saPolicyName), _eventHubName);
        }

        public void SendData(string data)
        {
            if(_connection == null)
            {
                Connect();
            }

            Amqp.Message message = new Amqp.Message()
            {
                BodySection = new Amqp.Framing.Data()
                {
                    Binary = System.Text.Encoding.UTF8.GetBytes(data)
                }
            };

            ManualResetEvent acked = new ManualResetEvent(false);
            message.MessageAnnotations = new Amqp.Framing.MessageAnnotations();
            message.MessageAnnotations[new Amqp.Types.Symbol("x-opt-partition-key")] =
               string.Format("pk:", _partitionkey);

            try { 
            _senderlink.Send(message);
            } catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Sending failed");
                _connection = null;
            }
        }
    }
}
