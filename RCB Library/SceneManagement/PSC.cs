using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Text;
using RCBLibrary.Events;
using RCBLibrary.Raycast.Axis;
using RCBLibrary.SceneManager.MapGeneration;

namespace RCBLibrary.SceneManagement
{
    /// <summary>
    /// Procedural Scene Controller
    /// </summary>
    public class PSC
    {
        public int Width, Height;

        public Event<string> LoadingUpdate = new Event<string>();
        public Event<PSC> Loaded = new Event<PSC>();

        public Point SpawnPoint;
        public List<Point> BlockedPoints = new List<Point>();
        public List<Point> StonePoints = new List<Point>();
        public List<Point> BR_StoneTriangles = new List<Point>(); //Bottom Right
        public List<Point> TR_StoneTriangles = new List<Point>(); //Top Right
        public List<Point> BL_StoneTriangles = new List<Point>(); //Bottom Left
        public List<Point> TL_StoneTriangles = new List<Point>(); //Top Left
        public List<Point> LR_StoneTriangles = new List<Point>(); //Left Right (Bottom/Top)
        public List<Point> spawnablePoints = new List<Point>();
        //public static List<Player.UI.IItem> Stone = new List<Player.UI.IItem>();

        public void AddBlock(int X, int Y)
        {
            BlockedPoints.Add(new Point { X = X, Y = Y });
        }

        public static PSC Generate(int height, int width, Action<string> loadingUpdateCallback = null, Action<PSC> loadedCallbacks = null)
        {
            MapGenerator mg = new MapGenerator();
            PSC psc = new PSC();
            psc.Height = height;
            psc.Width = width;
            if (loadingUpdateCallback != null)
            {
                psc.LoadingUpdate.AddListener(loadingUpdateCallback);
            }
            if (loadedCallbacks != null)
            {
                psc.Loaded.AddListener(loadedCallbacks);
            }

            mg.Generate("Mountains", psc);
            return psc;
        }
    }
}
