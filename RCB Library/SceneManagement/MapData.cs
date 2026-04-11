using RCBLibrary.Events;
using RCBLibrary.Math;
using RCBLibrary.Raycast.Axis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.SceneManagement
{
    public class MapData
    {
        public BIOME biome;
        public Point SpawnPoint;
        public Characters.Character character;
        public Vector2 lastCharacterPosition;
        public Point mapSize;
        public List<Point> BlockedPoints = new List<Point>();
        public List<Point> StonePoints = new List<Point>();
        public List<Point> BR_StoneTriangles = new List<Point>(); //Bottom Right
        public List<Point> TR_StoneTriangles = new List<Point>(); //Top Right
        public List<Point> BL_StoneTriangles = new List<Point>(); //Bottom Left
        public List<Point> TL_StoneTriangles = new List<Point>(); //Top Left
        public List<Point> LR_StoneTriangles = new List<Point>(); //Left Right (Bottom/Top)
    }
}
