using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RCBLibrary.SceneManager.MapGeneration
{
    using RCBLibrary.Raycast.Axis;
    using RCBLibrary.SceneManagement;

    public class MapGenerator
    {
        public Point[] Inner;
        public List<Point> FillerPoints = new List<Point>();
        public Random generation = new Random();
        
        SpaceManager spaceManager = new SpaceManager();
        IntelligentSpawn intelligentSpawn = new IntelligentSpawn();
        Disperser disperser = new Disperser();

        public void Generate(string Biom, PSC psc)
        {
            Inner = new Point[psc.Height];
            int[] Line = new int[psc.Height];
            for (int y = 0; y < psc.Height; y++) //Anwendung auf jede Zeile
            {
                Line[y] = generation.Next(1, psc.Width);
                Inner[y].X = Line[y];
                Inner[y].Y = y;
                if (y > 1)
                {
                    int _dx;
                    int dx = 10;
                    if (y <= 4)
                    {
                        int dy = y;
                        for (int i = 1; i < (dy + 1); i++)
                        {
                            if (Inner[y - i].X < Inner[y].X)
                            {
                                _dx = Inner[y].X - Inner[y - i].X;
                                if (_dx <= dx)
                                {
                                    Inner[y].X = generation.Next(1, psc.Width);
                                }
                            }
                            else if (Inner[y - 1].X > Inner[y].X)
                            {
                                _dx = Inner[y - i].X - Inner[y].X;
                                if (_dx <= dx)
                                {
                                    Inner[y].X = generation.Next(1, psc.Width);
                                }
                            }
                            else
                            {
                                Inner[y].X = generation.Next(1, psc.Width);
                            }
                            //Thread.Sleep(1);
                        }
                    }

                    if (y >= 5)
                    {
                        int dy = 5;
                        for (int i = 1; i < (dy + 1); i++)
                        {
                            if (Inner[y - i].X < Inner[y].X)
                            {
                                _dx = Inner[y].X - Inner[y - i].X;
                                if (_dx <= dx)
                                {
                                    Inner[y].X = generation.Next(1, psc.Width);
                                }
                            }
                            else if (Inner[y - 1].X > Inner[y].X)
                            {
                                _dx = Inner[y - i].X - Inner[y].X;
                                if (_dx <= dx)
                                {
                                    Inner[y].X = generation.Next(1, psc.Width);
                                }
                            }
                            else
                            {
                                Inner[y].X = generation.Next(1, psc.Width);
                            }
                            //Thread.Sleep(1);
                        }
                    }
                }
                if (y % 2 == 0)
                {
                    psc.LoadingUpdate.Invoke("Generating..");
                }
                else if (y % 3 == 0)
                {
                    Console.Clear();
                    psc.LoadingUpdate.Invoke("Generating...");
                }
                else
                {
                    Console.Clear();
                    psc.LoadingUpdate.Invoke("Generating.");
                }
            } //Generates middle points

            psc.LoadingUpdate.Invoke("Loading.");
            Spread(15, 5, psc);

            Console.Clear();
            psc.LoadingUpdate.Invoke("Loading..");
            BlockPoints(Biom, psc);

            Console.Clear();
            psc.LoadingUpdate.Invoke("Loading...");
            spaceManager.Manage(psc);

            intelligentSpawn.Player(psc);
            // IntelligentSpawn.Materials();

            psc.Loaded.Invoke(psc);
        }

        public void BlockPoints(string Biom, PSC psc)
        {
            int Count = Inner.Count<Point>();
            for (int i = 0; i < Count; i++)
            {
                if (i % 4 == 0)
                {
                    psc.AddBlock(Inner[i].X, Inner[i].Y);
                    if (Biom == "Mountains")
                    {
                        psc.StonePoints.Add(new Point() { X = Inner[i].X, Y = Inner[i].Y });
                    }
                    //Thread.Sleep(2);
                }
            }

            int FPCount = FillerPoints.Count;
            for (int i = 0; i < FPCount; i++)
            {
                psc.AddBlock(FillerPoints[i].X, FillerPoints[i].Y);
                if (Biom == "Mountains")
                {
                    psc.StonePoints.Add(new Point() { X = FillerPoints[i].X, Y = FillerPoints[i].Y });
                }
                //Thread.Sleep(2);
            }
            Console.CursorVisible = false;
        }

        public void Spread(int SpreadL, int SpreadP, PSC psc)
        {
            int Count = Inner.Count<Point>();
            for (int i = 0; i < Count; i++)
            {
                if (i % 4 == 0)
                {
                    disperser.Disperse('X', Inner[i], SpreadL, psc, this);
                    disperser.Disperse('Y', Inner[i], SpreadL, psc, this);
                    //Thread.Sleep(2);
                }
            }

            //Thread.Sleep(10);

            char Axis = ' ';
            int LinePts = FillerPoints.Count;
            for (int i = 0; i < LinePts; i++)
            {
                if (i >= 1)
                {
                    if (FillerPoints[i - 1].X == (FillerPoints[i].X - 1) && FillerPoints[i - 1].Y == FillerPoints[i].Y)
                    {
                        Axis = 'Y';
                    }
                    else if (FillerPoints[i - 1].X == (FillerPoints[i].X + 1) && FillerPoints[i - 1].Y == FillerPoints[i].Y)
                    {
                        Axis = 'y';
                    }
                    else if (FillerPoints[i - 1].Y == (FillerPoints[i].Y + 1) && FillerPoints[i - 1].X == FillerPoints[i].X)
                    {
                        Axis = 'X';
                    }
                    else if (FillerPoints[i - 1].Y == (FillerPoints[i].Y - 1) && FillerPoints[i - 1].X == FillerPoints[i].X)
                    {
                        Axis = 'x';
                    }
                }
                else if (i == 0)
                {
                    if (FillerPoints[i + 1].X == (FillerPoints[i].X - 1) && FillerPoints[i + 1].Y == FillerPoints[i].Y)
                    {
                        Axis = 'Y';
                    }
                    else if (FillerPoints[i + 1].X == (FillerPoints[i].X + 1) && FillerPoints[i + 1].Y == FillerPoints[i].Y)
                    {
                        Axis = 'y';
                    }
                    else if (FillerPoints[i + 1].Y == (FillerPoints[i].Y + 1) && FillerPoints[i + 1].X == FillerPoints[i].X)
                    {
                        Axis = 'X';
                    }
                    else if (FillerPoints[i + 1].Y == (FillerPoints[i].Y - 1) && FillerPoints[i + 1].X == FillerPoints[i].X)
                    {
                        Axis = 'x';
                    }
                }

                disperser.Disperse(Axis, FillerPoints[i], SpreadP, psc, this);
                //Thread.Sleep(2);
            }
        }
    }
}
