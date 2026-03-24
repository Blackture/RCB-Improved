using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCBLibrary.SceneManager.MapGeneration
{
    using Raycast.Axis;
    using RCBLibrary.SceneManagement;

    public class Disperser
    {
        public void Disperse(char Axis, Point CenterPoint, int Dispersion, PSC psc, MapGenerator mg)
        {
            switch (Axis)
            {
                case 'X':
                case 'x':
                    XDispersion(CenterPoint, Dispersion, psc, mg);
                    break;
                case 'Y':
                case 'y':
                    YDispersion(CenterPoint, Dispersion, psc, mg);
                    break;
            }
        }

        void XDispersion(Point Center, int Dispersion, PSC psc, MapGenerator mg)
        {
            if ((Center.X - Dispersion * 2) > 1 && (Center.X + Dispersion * 2) < psc.Width)
            {
                int Left = mg.generation.Next(1, Dispersion * 2);
                int Right = mg.generation.Next(1, Dispersion * 2);

                for (int i = 0; i < Left; i++)
                {
                    Point p = new Point() { X = (Center.X - i - 1), Y = Center.Y };
                    mg.FillerPoints.Add(p);
                }

                for (int i = 0; i < Right; i++)
                {
                    Point p = new Point() { X = (Center.X + i + 1), Y = Center.Y };
                    mg.FillerPoints.Add(p);
                }
            }

            else if ((Center.X - Dispersion * 2) > 1 && (Center.X + Dispersion * 2) > psc.Width)
            {
                int Left = mg.generation.Next(1, Dispersion * 2);
                int Right = mg.generation.Next(1, psc.Width - Center.X);

                for (int i = 0; i < Left; i++)
                {
                    Point p = new Point() { X = (Center.X - i - 1), Y = Center.Y };
                    mg.FillerPoints.Add(p);
                }

                for (int i = 0; i < Right; i++)
                {
                    Point p = new Point() { X = (Center.X + i + 1), Y = Center.Y };
                    mg.FillerPoints.Add(p);
                }
            }

            else if ((Center.X - Dispersion * 2) < 1 && (Center.X + Dispersion * 2) < psc.Width)
            {
                int _CenterX;
                if (Center.X - 1 <= 0)
                {
                    _CenterX = 1;
                }
                else
                {
                    _CenterX = Center.X;
                }

                int Left = mg.generation.Next(1, _CenterX);
                int ZR = psc.Width - Center.X;
                if (ZR + Center.X >= 0)
                {
                    Left = 0;
                }
                int Right = mg.generation.Next(1, Dispersion * 2);

                for (int i = 0; i < Left; i++)
                {
                    Point p = new Point() { X = (Center.X - i - 1), Y = Center.Y };
                    mg.FillerPoints.Add(p);
                }

                for (int i = 0; i < Right; i++)
                {
                    Point p = new Point() { X = (Center.X + i + 1), Y = Center.Y };
                    mg.FillerPoints.Add(p);
                }
            }

            else if ((Center.X - Dispersion * 2) == 1 && (Center.X + Dispersion * 2) < psc.Width)
            {
                int Right = mg.generation.Next(1, Dispersion * 2);

                for (int i = 0; i < Right; i++)
                {
                    Point p = new Point() { X = (Center.X + i + 1), Y = Center.Y };
                    mg.FillerPoints.Add(p);
                }
            }

            else if ((Center.X - Dispersion * 2) > 1 && (Center.X + Dispersion * 2) == psc.Width)
            {
                int Left = mg.generation.Next(1, Dispersion * 2);

                for (int i = 0; i < Left; i++)
                {
                    Point p = new Point() { X = (Center.X - i - 1), Y = Center.Y };
                    mg.FillerPoints.Add(p);
                }
            }
        }

        static void YDispersion(Point Center, int Dispersion, PSC psc, MapGenerator mg)
        {
            if ((Center.Y - Dispersion) > 1 && (Center.Y + Dispersion) < psc.Height)
            {
                int Top = mg.generation.Next(1, Dispersion);
                int Bottom = mg.generation.Next(1, Dispersion);

                for (int i = 0; i < Top; i++)
                {
                    Point p = new Point() { X = Center.X, Y = Center.Y - i - 1 };
                    mg.FillerPoints.Add(p);
                }

                for (int i = 0; i < Bottom; i++)
                {
                    Point p = new Point() { X = Center.X, Y = Center.Y + i + 1 };
                    mg.FillerPoints.Add(p);
                }
            }

            else if ((Center.Y - Dispersion) > 1 && (Center.Y + Dispersion) > psc.Height)
            {
                int Top = mg.generation.Next(1, Dispersion);
                int Bottom = mg.generation.Next(1, psc.Height - Center.Y);

                for (int i = 0; i < Top; i++)
                {
                    Point p = new Point() { X = Center.X, Y = Center.Y - i - 1 };
                    mg.FillerPoints.Add(p);
                }

                for (int i = 0; i < Bottom; i++)
                {
                    Point p = new Point() { X = Center.X, Y = Center.Y + i + 1 };
                    mg.FillerPoints.Add(p);
                }
            }

            else if ((Center.Y - Dispersion) < 1 && (Center.Y + Dispersion) < psc.Height)
            {
                int _CenterY;
                if (Center.Y - 1 <= 0)
                {
                    _CenterY = 1;
                }
                else
                {
                    _CenterY = Center.Y;
                }

                int Top = mg.generation.Next(1, _CenterY);
                int HR = psc.Height - Center.Y;
                if (HR + Center.Y >= 0)
                {
                    Top = 0;
                }

                int Bottom = mg.generation.Next(1, Dispersion);

                for (int i = 0; i < Top; i++)
                {
                    Point p = new Point() { X = Center.X, Y = (Center.Y - i - 1) };
                    mg.FillerPoints.Add(p);
                }

                for (int i = 0; i < Bottom; i++)
                {
                    Point p = new Point() { X = Center.X, Y = (Center.Y + i + 1) };
                    mg.FillerPoints.Add(p);
                }
            }

            else if ((Center.Y - Dispersion) == 1 && (Center.Y + Dispersion) < psc.Height)
            {
                int Bottom = mg.generation.Next(1, Dispersion);

                for (int i = 0; i < Bottom; i++)
                {
                    Point p = new Point() { X = Center.X, Y = Center.Y + i + 1 };
                    mg.FillerPoints.Add(p);
                }
            }

            else if ((Center.Y - Dispersion) > 1 && (Center.Y + Dispersion) == psc.Height)
            {
                int Top = mg.generation.Next(1, Dispersion);

                for (int i = 0; i < Top; i++)
                {
                    Point p = new Point() { X = Center.X, Y = Center.Y - i - 1 };
                    mg.FillerPoints.Add(p);
                }
            }
        }
    }
}
