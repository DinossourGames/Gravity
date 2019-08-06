
using LiteNetLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravity.Assets.Scripts
{
    public class NetworkManager
    {
        public static bool NetworkType { get; set; } // false = client  true = server

        private NetManager Client;
        private NetManager Server;


        public void Initialize(bool netType)
        {
            EventBasedNetListener listener = new EventBasedNetListener();

            NetworkType = netType;
            if (netType)
            {
                Server = new NetManager(listener);
                Server.Start(13137);
            }
            else
            {
                Client = new NetManager(listener);
                Client.Start();
            }

        }

        public void Update()
        {
            var connection = NetworkType ? Server : Client;
            connection.PollEvents();
        }

    }
}
