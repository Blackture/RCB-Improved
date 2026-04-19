using System;
using System.Collections.Generic;
using System.Text;
using RCBLibrary.Math;

namespace RCBLibrary.SceneManagement
{
    public class MoveEventArgs : EventArgs
    {
        public Vector2 OldPosition { get; }
        public Vector2 NewPosition { get; }
        public MapData Map { get;  }

        public MoveEventArgs(Vector2 oldPosition, Vector2 newPosition, MapData map)
        {
            OldPosition = oldPosition;
            NewPosition = newPosition;
            Map = map;
        }
    }
}
