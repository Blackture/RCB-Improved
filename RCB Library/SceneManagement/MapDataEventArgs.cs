using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.SceneManagement
{
    public class MapDataEventArgs : EventArgs
    {
        public MapData Map { get; }
        public MapDataEventArgs(MapData map)
        {
            Map = map;
        }
    }
}
