
using Gravity.Scenes;
using LiteNetLib;
using LiteNetLib.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravity.Assets.Scripts
{
    public class NetworkManager
    {
        public bool NetworkType { get; set; } = true;// false = client  true = server
        public string Hostname { get; set; }

        private NetManager Client;
        private NetManager Server;

        NetPeer peer;
        private EventBasedNetListener listenerClient;
        private EventBasedNetListener listenerServer;

        List<NetPeer> connections;
        private NetDataWriter dataWriter;

        public NetworkManager(bool server)
        {
            NetworkType = server;
            connections = new List<NetPeer>();
            dataWriter = new NetDataWriter();
            Initialize();
        }

        public NetworkManager()
        {
        }

        public void Update()
        {
            var connection = NetworkType ? Server : Client;
            connection.PollEvents();
            dataWriter.Reset();

            var pck = new Packet
            {
                Components = GameScene.components
            };
            //var p = (Player)pck.Components.Find(i => i.ID == GameScene.player.ID);
            //p.isMain = false;


            dataWriter.Put(Serialize(pck));

            if (!NetworkType)
                peer.Send(dataWriter, DeliveryMethod.ReliableOrdered);
            else
                Server.SendToAll(dataWriter, DeliveryMethod.ReliableOrdered);


        }

        public void Initialize()
        {

            if (NetworkType)
            {
                listenerServer = new EventBasedNetListener();
                Server = new NetManager(listenerServer);
                var a = Server.Start(13137);
                if (a)
                    Debug.WriteLine($"Server Started at port {Server.LocalPort}");

                
                //register listeners
                listenerServer.ConnectionRequestEvent += ListenerServer_ConnectionRequestEvent;
                listenerServer.PeerConnectedEvent += ListenerServer_PeerConnectedEvent;
                listenerServer.NetworkReceiveEvent += ListenerServer_NetworkReceiveEvent;
            }
            else
            {
                listenerClient = new EventBasedNetListener();

                Client = new NetManager(listenerClient);
                var b = Client.Start();
                if (b)
                    Debug.WriteLine($"Client Started");


                if (!string.IsNullOrEmpty(Hostname))
                    peer = Client.Connect(Hostname, 13137, "DinossourGames");
                else
                    peer = Client.Connect("localhost", 13137, "DinossourGames");

                listenerClient.NetworkReceiveEvent += ListenerClient_NetworkReceiveEvent;
            }

        }

        private void ListenerClient_NetworkReceiveEvent(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            var packet = new Packet();
            packet = Deserialize(reader.GetString());

            foreach (var item in packet.Components)
            {
                if (item == GameScene.player)
                    continue;

                var obj = GameScene.components.Find(i => i.ID == item.ID);
                if (obj != null)
                    obj = item;
                else
                    GameScene.components.Add(item);

            }
        }

        #region  Listeners

        private void ListenerServer_NetworkReceiveEvent(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            var packet = new Packet();
            packet = Deserialize(reader.GetString());

            foreach (var item in packet.Components)
            {
                if (item == GameScene.player)
                    continue;

                var obj = GameScene.components.Find(i => i.ID == item.ID);
                if (obj != null)
                    obj = item;
                else
                    GameScene.components.Add(item);

            }
        }

        private void ListenerServer_PeerConnectedEvent(NetPeer peer)
        {
            connections.Add(peer);
        }


        private void ListenerServer_ConnectionRequestEvent(ConnectionRequest request)
        {
            if (Server.PeersCount <= 20)
                request.AcceptIfKey("DinossourGames");
            else
                request.Reject();
        }


        #endregion

        public static Packet Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<Packet>(json);
        }

        public static string Serialize(Packet packet)
        {
            return JsonConvert.SerializeObject(packet);
        }

    }
}
