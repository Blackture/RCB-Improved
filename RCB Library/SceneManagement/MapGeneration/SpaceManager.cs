using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RCBLibrary.SceneManager.MapGeneration
{
    using Raycast;
    using Raycast.Axis;
    using RCBLibrary.SceneManagement;

    public class SpaceManager
    {
        [STAThread]
        public void Manage(PSC psc)
        {
            //Create Intelligent Fill up here
            FillUp(psc);
            Shaping(psc);
        }

        public void FillUp(PSC psc)
        {
            for (int Y = 1; Y < psc.Height; Y++)
            {
                for (int X = 1; X < psc.Width; X++)
                {
                    Point Current = new Point() { X = X, Y = Y };
                    Point CheckerRight = new Point() { X = X + 1, Y = Y };
                    Point CheckerLeft = new Point() { X = X - 1, Y = Y };
                    Point CheckerTop = new Point() { X = X, Y = Y - 1 };
                    Point CheckerBottom = new Point() { X = X, Y = Y + 1 };
                    if (!psc.BlockedPoints.Contains(Current))
                    {
                        if (psc.BlockedPoints.Contains(CheckerLeft) && psc.BlockedPoints.Contains(CheckerTop) && psc.BlockedPoints.Contains(CheckerBottom) && psc.BlockedPoints.Contains(CheckerRight))
                        {
                            //when its a 1x1 gap
                            psc.BlockedPoints.Add(Current);
                            psc.StonePoints.Add(Current);
                        }
                        if (psc.BlockedPoints.Contains(CheckerLeft) && psc.BlockedPoints.Contains(CheckerTop) && psc.BlockedPoints.Contains(CheckerBottom))
                        {
                            Point CheckerRight_Y1Up = new Point() { X = X + 1, Y = Y - 1 };
                            Point CheckerRight_Y1Down = new Point() { X = X + 1, Y = Y + 1 };
                            Point Checker2xRight = new Point() { X = X + 2, Y = Y };
                            if (!psc.BlockedPoints.Contains(CheckerRight))
                            {
                                int CheckX = X + 2;
                                if (psc.BlockedPoints.Contains(CheckerRight_Y1Up) && psc.BlockedPoints.Contains(CheckerRight_Y1Down) && psc.BlockedPoints.Contains(Checker2xRight))
                                {
                                    //when its a 2x1 Gap
                                    psc.BlockedPoints.Add(Current);
                                    psc.BlockedPoints.Add(CheckerRight);
                                    psc.StonePoints.Add(Current);
                                    psc.StonePoints.Add(CheckerRight);
                                }
                                else if (!psc.BlockedPoints.Contains(Checker2xRight) && psc.BlockedPoints.Contains(CheckerRight_Y1Up) && psc.BlockedPoints.Contains(CheckerRight_Y1Down))
                                {
                                    Point[] WhileChecker = new Point[4];
                                    WhileChecker[0] = new Point() { X = CheckX, Y = Y };
                                    WhileChecker[1] = new Point() { X = CheckX, Y = Y + 1 };
                                    WhileChecker[2] = new Point() { X = CheckX, Y = Y - 1 };
                                    WhileChecker[3] = new Point() { X = CheckX + 1, Y = Y };
                                    while (CheckX < psc.Width - 1 && !psc.BlockedPoints.Contains(WhileChecker[0]) && psc.BlockedPoints.Contains(WhileChecker[1]) && psc.BlockedPoints.Contains(WhileChecker[2]))
                                    {
                                        if (psc.BlockedPoints.Contains(WhileChecker[3]))
                                        {
                                            psc.BlockedPoints.Add(Current);
                                            psc.BlockedPoints.Add(Checker2xRight);
                                            psc.BlockedPoints.Add(CheckerRight);
                                            psc.StonePoints.Add(Current);
                                            psc.StonePoints.Add(Checker2xRight);
                                            psc.StonePoints.Add(CheckerRight);
                                        }

                                        CheckX++;
                                        //at least
                                        WhileChecker[0] = new Point() { X = CheckX, Y = Y };
                                        WhileChecker[1] = new Point() { X = CheckX, Y = Y + 1 };
                                        WhileChecker[2] = new Point() { X = CheckX, Y = Y - 1 };
                                        WhileChecker[3] = new Point() { X = CheckX + 1, Y = Y };
                                    }
                                }
                            }
                        }
                    }
                    if (X % 3 == 0)
                    {
                        psc.LoadingUpdate.Invoke("Optimizing map...");
                    }
                    else if (X % 2 == 0)
                    {
                        psc.LoadingUpdate.Invoke("Optimizing map..");
                    }
                    else
                    {
                        psc.LoadingUpdate.Invoke("Optimizing map.");
                    }
                }
                if (Y % 3 == 0)
                {
                    psc.LoadingUpdate.Invoke("Processing map...");
                }
                else if (Y % 2 == 0)
                {
                    psc.LoadingUpdate.Invoke("Processing map..");
                }
                else
                {
                    psc.LoadingUpdate.Invoke("Processing map.");
                }
            }
        }

        public void Shaping(PSC psc)
        {
            for (int Y = 1; Y < psc.Height; Y++)
            {
                for (int X = 1; X < psc.Height; X++)
                {
                    Point Current = new Point() { X = X, Y = Y };
                    Point CheckerRight = new Point() { X = X + 1, Y = Y };
                    Point CheckerLeft = new Point() { X = X - 1, Y = Y };
                    Point CheckerTop = new Point() { X = X, Y = Y - 1 };
                    Point CheckerBottom = new Point() { X = X, Y = Y + 1 };

                    if (!psc.StonePoints.Contains(Current) && !psc.StonePoints.Contains(CheckerTop) && !psc.StonePoints.Contains(CheckerLeft) && psc.StonePoints.Contains(CheckerBottom) && psc.StonePoints.Contains(CheckerRight))
                    {
                        psc.BlockedPoints.Add(Current);
                        psc.BR_StoneTriangles.Add(Current);
                    }   
                    else if (!psc.StonePoints.Contains(Current) && !psc.StonePoints.Contains(CheckerTop) && psc.StonePoints.Contains(CheckerLeft) && psc.StonePoints.Contains(CheckerBottom) && !psc.StonePoints.Contains(CheckerRight))
                    {
                        psc.BlockedPoints.Add(Current);
                        psc.BL_StoneTriangles.Add(Current);
                    }
                    else if (!psc.StonePoints.Contains(Current) && psc.StonePoints.Contains(CheckerTop) && !psc.StonePoints.Contains(CheckerLeft) && !psc.StonePoints.Contains(CheckerBottom) && psc.StonePoints.Contains(CheckerRight))
                    {
                        psc.BlockedPoints.Add(Current);
                        psc.TR_StoneTriangles.Add(Current);
                    }
                    else if (!psc.StonePoints.Contains(Current) && psc.StonePoints.Contains(CheckerTop) && psc.StonePoints.Contains(CheckerLeft) && !psc.StonePoints.Contains(CheckerBottom) && !psc.StonePoints.Contains(CheckerRight))
                    {
                        psc.BlockedPoints.Add(Current);
                        psc.TL_StoneTriangles.Add(Current);
                    }
                    else if (!psc.StonePoints.Contains(Current) && psc.StonePoints.Contains(CheckerTop) && psc.StonePoints.Contains(CheckerLeft) && !psc.StonePoints.Contains(CheckerBottom) && psc.StonePoints.Contains(CheckerRight))
                    {
                        psc.BlockedPoints.Add(Current);
                        psc.LR_StoneTriangles.Add(Current);
                    }
                    else if (!psc.StonePoints.Contains(Current) && !psc.StonePoints.Contains(CheckerTop) && psc.StonePoints.Contains(CheckerLeft) && psc.StonePoints.Contains(CheckerBottom) && psc.StonePoints.Contains(CheckerRight))
                    {
                        psc.BlockedPoints.Add(Current);
                        psc.LR_StoneTriangles.Add(Current);
                    }
                    else if (!psc.StonePoints.Contains(Current) && psc.StonePoints.Contains(CheckerTop) && psc.StonePoints.Contains(CheckerLeft) && psc.StonePoints.Contains(CheckerBottom) && !psc.StonePoints.Contains(CheckerRight))
                    {
                        psc.BlockedPoints.Add(Current);
                        psc.LR_StoneTriangles.Add(Current);
                    }
                    else if (!psc.StonePoints.Contains(Current) && psc.StonePoints.Contains(CheckerTop) && !psc.StonePoints.Contains(CheckerLeft) && psc.StonePoints.Contains(CheckerBottom) && psc.StonePoints.Contains(CheckerRight))
                    {
                        psc.BlockedPoints.Add(Current);
                        psc.LR_StoneTriangles.Add(Current);
                    }
                }
                
                if (Y % 2 == 0)
                {
                    psc.LoadingUpdate.Invoke("Improving world shape..");
                }
                else
                {
                    psc.LoadingUpdate.Invoke("Improving world shape.");
                }
            }
        }
    }
}
