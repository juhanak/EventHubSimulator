using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using ServiceBusClientLib;

namespace SimpleSender
{
    public class SimpleAppServiceBus
    {
        ServiceBusClient _client;

        private static SimpleAppServiceBus _instance;

        public static SimpleAppServiceBus Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SimpleAppServiceBus();
                }

                return _instance;
            }
        }

        public SimpleAppServiceBus()
        {
            _client = new ServiceBusClient();
        }

        public Task SendAsync(int value)
        {
            return Task.Run(() =>
            {
                _client.SendData("{\"PlayerId\":\"00000000 - 0000 - 0000 - 0000 - 000000000001\",\"Value\":"+ value+"}");
            });
        }
    }
}
