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
        private static void AddFillerPoint(MapGenerator mg, PSC psc, int x, int y)
        {
            if (x < 1 || x >= psc.Width) return;
            if (y < 0 || y >= psc.Height) return;

            mg.FillerPoints.Add(new Point() { X = x, Y = y });
        }

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
                    AddFillerPoint(mg, psc, Center.X - i - 1, Center.Y);
                }

                for (int i = 0; i < Right; i++)
                {
                    AddFillerPoint(mg, psc, Center.X + i + 1, Center.Y);
                }
            }

            else if ((Center.X - Dispersion * 2) > 1 && (Center.X + Dispersion * 2) > psc.Width)
            {
                int Left = mg.generation.Next(1, Dispersion * 2);
                int Right = mg.generation.Next(1, psc.Width - Center.X);

                for (int i = 0; i < Left; i++)
                {
                    AddFillerPoint(mg, psc, Center.X - i - 1, Center.Y);
                }

                for (int i = 0; i < Right; i++)
                {
                    AddFillerPoint(mg, psc, Center.X + i + 1, Center.Y);
                }
            }

            else if ((Center.X - Dispersion * 2) < 1 && (Center.X + Dispersion * 2) < psc.Width)
            {
                int availableLeft = Center.X - 1;
                int Left = availableLeft > 0 ? mg.generation.Next(1, System.Math.Min(availableLeft, Dispersion * 2) + 1) : 0;
                int Right = mg.generation.Next(1, Dispersion * 2);

                for (int i = 0; i < Left; i++)
                {
                    AddFillerPoint(mg, psc, Center.X - i - 1, Center.Y);
                }

                for (int i = 0; i < Right; i++)
                {
                    AddFillerPoint(mg, psc, Center.X + i + 1, Center.Y);
                }
            }

            else if ((Center.X - Dispersion * 2) == 1 && (Center.X + Dispersion * 2) < psc.Width)
            {
                int Right = mg.generation.Next(1, Dispersion * 2);

                for (int i = 0; i < Right; i++)
                {
                    AddFillerPoint(mg, psc, Center.X + i + 1, Center.Y);
                }
            }

            else if ((Center.X - Dispersion * 2) > 1 && (Center.X + Dispersion * 2) == psc.Width)
            {
                int Left = mg.generation.Next(1, Dispersion * 2);

                for (int i = 0; i < Left; i++)
                {
                    AddFillerPoint(mg, psc, Center.X - i - 1, Center.Y);
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
                    AddFillerPoint(mg, psc, Center.X, Center.Y - i - 1);
                }

                for (int i = 0; i < Bottom; i++)
                {
                    AddFillerPoint(mg, psc, Center.X, Center.Y + i + 1);
                }
            }

            else if ((Center.Y - Dispersion) > 1 && (Center.Y + Dispersion) > psc.Height)
            {
                int Top = mg.generation.Next(1, Dispersion);
                int Bottom = mg.generation.Next(1, psc.Height - Center.Y);

                for (int i = 0; i < Top; i++)
                {
                    AddFillerPoint(mg, psc, Center.X, Center.Y - i - 1);
                }

                for (int i = 0; i < Bottom; i++)
                {
                    AddFillerPoint(mg, psc, Center.X, Center.Y + i + 1);
                }
            }

            else if ((Center.Y - Dispersion) < 1 && (Center.Y + Dispersion) < psc.Height)
            {
                int availableTop = Center.Y - 1;
                int Top = availableTop > 0 ? mg.generation.Next(1, System.Math.Min(availableTop, Dispersion) + 1) : 0;

                int Bottom = mg.generation.Next(1, Dispersion);

                for (int i = 0; i < Top; i++)
                {
                    AddFillerPoint(mg, psc, Center.X, Center.Y - i - 1);
                }

                for (int i = 0; i < Bottom; i++)
                {
                    AddFillerPoint(mg, psc, Center.X, Center.Y + i + 1);
                }
            }

            else if ((Center.Y - Dispersion) == 1 && (Center.Y + Dispersion) < psc.Height)
            {
                int Bottom = mg.generation.Next(1, Dispersion);

                for (int i = 0; i < Bottom; i++)
                {
                    AddFillerPoint(mg, psc, Center.X, Center.Y + i + 1);
                }
            }

            else if ((Center.Y - Dispersion) > 1 && (Center.Y + Dispersion) == psc.Height)
            {
                int Top = mg.generation.Next(1, Dispersion);

                for (int i = 0; i < Top; i++)
                {
                    AddFillerPoint(mg, psc, Center.X, Center.Y - i - 1);
                }
            }
        }
    }
}
