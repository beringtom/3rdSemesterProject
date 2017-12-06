using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardRelay
{
    class SensorData
    {
        public string Room;
        public string Time;
        public string CardID;

        public SensorData(string room, string time, string cardId)
        {
            Room = room;
            Time = time;
            CardID = cardId;
        }
    }
}
