using RCBLibrary.Raycast.Axis;
using RCBLibrary.SceneManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBImprovedC
{
    public class MapDataRenderer
    {
        public static void Renderer(MapData data)
        {
            if (data.biome == RCBLibrary.BIOME.MOUNTAIN)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            int StoneCount = data.StonePoints.Count;
            for (int i = 0; i < StoneCount; i++)
            {
                Console.SetCursorPosition(data.StonePoints[i].X - 1, data.StonePoints[i].Y);
                Console.Write('\u2588');
            }

            int StoneBRTriPoints = data.BR_StoneTriangles.Count;
            for (int i = 0; i < StoneBRTriPoints; i++)
            {
                Console.SetCursorPosition(data.BR_StoneTriangles[i].X - 1, data.BR_StoneTriangles[i].Y);
                Console.Write('\u2592');
            }

            int StoneBLTriPoints = data.BL_StoneTriangles.Count;
            for (int i = 0; i < StoneBLTriPoints; i++)
            {
                Console.SetCursorPosition(data.BL_StoneTriangles[i].X - 1, data.BL_StoneTriangles[i].Y);
                Console.Write('\u2591');
            }

            int StoneTRTriPoints = data.TR_StoneTriangles.Count;
            for (int i = 0; i < StoneTRTriPoints; i++)
            {
                Console.SetCursorPosition(data.TR_StoneTriangles[i].X - 1, data.TR_StoneTriangles[i].Y);
                Console.Write('\u2591');
            }

            int StoneTLTriPoints = data.TL_StoneTriangles.Count;
            for (int i = 0; i < StoneTLTriPoints; i++)
            {
                Console.SetCursorPosition(data.TL_StoneTriangles[i].X - 1, data.TL_StoneTriangles[i].Y);
                Console.Write('\u2592');
            }

            int StoneLRTriPoints = data.LR_StoneTriangles.Count;
            for (int i = 0; i < StoneLRTriPoints; i++)
            {
                Console.SetCursorPosition(data.LR_StoneTriangles[i].X - 1, data.LR_StoneTriangles[i].Y);
                Console.Write('\u2593');
            }
        }

        public static void MoveCharacter(MapData data)
        {

        }
    }
}
