using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCBLibrary.SceneManager.MapGeneration
{
    //using Player;
    using Raycast.Axis;
    using RCBLibrary.SceneManagement;
    using System.Net.Security;

    public class IntelligentSpawn
    {
        public int x;
        public int y;
        public List<Point> FFPoints = new List<Point>();
        public void Player(PSC psc)
        {
            Point SpawnPoint = new Point();
            bool End = false;
            do
            {
                if (End == false)
                {
                    if (FFPoints.Count != 0)
                    {
                        for (int Y = 1; Y < psc.Height - 1; Y++)
                        {
                            for (int X = 1; X < psc.Width - 1; X++)
                            {
                                Point T = new Point() { X = X, Y = Y };
                                if (!FFPoints.Contains(T) || !psc.BlockedPoints.Contains(T))
                                {
                                    x = T.X;
                                    y = T.Y;
                                    FFPoints.Clear();
                                    return;
                                }
                            }
                        }
                    }
                    if (x == 0 && y == 0)
                    {
                        if (psc.BlockedPoints.Contains(new Point() { X = x, Y = y }))
                        {
                            for (int Y = 1; Y < psc.Height; Y++)
                            {
                                for (int X = 1; X < psc.Width; X++)
                                {
                                    Point Checker = new Point() { Y = Y, X = X };
                                    if (psc.BlockedPoints.Contains(Checker))
                                    {
                                        x = Checker.X;
                                        y = Checker.Y;
                                    }
                                }
                            }
                        }
                        else
                        {
                            x = 1;
                            y = 1;
                        }
                    }
                    FloodFill(x, y, psc);
                    if (FFPoints.Count >= 50)
                    {
                        Random rnd = new Random();
                        int rndInt = rnd.Next(0, FFPoints.Count);
                        SpawnPoint = FFPoints[rndInt];
                        End = true;
                    }
                }
            } while (End == false);

            psc.SpawnPoint = SpawnPoint;
        }

        public void FloodFill(int X, int Y, PSC psc)
        {
            if (psc.BlockedPoints.Contains(new Point() { X = X, Y = Y }) || FFPoints.Contains(new Point() { X = X, Y = Y }))
                return;
            if (X < 1 || X >= psc.Width)
                return;
            if (Y < 1 || Y >= psc.Width)
                return;

            FFPoints.Add(new Point() { X = X, Y = Y });

            FloodFill(X + 1, Y, psc);
            FloodFill(X - 1, Y, psc);
            FloodFill(Y + 1, X, psc);
            FloodFill(Y - 1, X, psc);
        }

        public void Materials()
        {
            Random rnd = new Random();
            int ItemsToSpawn = rnd.Next(10, 50);
            for (int i = 0; i < ItemsToSpawn; i++)
            {
                //PSC.StonePoints.Add(new Player.UI.Item("Stone", rnd.Next(1, 5), "collectable", "Mountains;None;None;None", FFPoints[rnd.Next(0, FFPoints.Count)]));
            }
            
        }
    }
}
