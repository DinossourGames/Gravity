using LiteNetLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gravity.Assets.Scripts
{
    public class Packet : INetSerializable
    {

        List<Player> players;



        public void Deserialize(NetDataReader reader)
        {
            throw new NotImplementedException();
        }

        public void Serialize(NetDataWriter writer)
        {
            throw new NotImplementedException();
        }
    }
}
