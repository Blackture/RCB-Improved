using RCBLibrary;
using RCBLibrary.Raycast.Axis;
using RCBLibrary.SceneManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RCBImprovedC
{
    public class MapDataRenderer
    {
        static int FlipY(int y) => (Console.BufferHeight - 1) - y;

        public static void Renderer(MapDataEventArgs data)
        {
            if (data.Map.biome == BIOME.MOUNTAIN)
            {
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
            }

            int StoneCount = data.Map.StonePoints.Count;
            for (int i = 0; i < StoneCount; i++)
            {
                Console.SetCursorPosition(data.Map.StonePoints[i].X - 1, FlipY(data.Map.StonePoints[i].Y));
                Console.Write('\u2588');
            }

            int StoneBRTriPoints = data.Map.BR_StoneTriangles.Count;
            for (int i = 0; i < StoneBRTriPoints; i++)
            {
                Console.SetCursorPosition(data.Map.BR_StoneTriangles[i].X - 1, FlipY(data.Map.BR_StoneTriangles[i].Y));
                Console.Write('\u2592');
            }

            int StoneBLTriPoints = data.Map.BL_StoneTriangles.Count;
            for (int i = 0; i < StoneBLTriPoints; i++)
            {
                Console.SetCursorPosition(data.Map.BL_StoneTriangles[i].X - 1, FlipY(data.Map.BL_StoneTriangles[i].Y));
                Console.Write('\u2591');
            }

            int StoneTRTriPoints = data.Map.TR_StoneTriangles.Count;
            for (int i = 0; i < StoneTRTriPoints; i++)
            {
                Console.SetCursorPosition(data.Map.TR_StoneTriangles[i].X - 1, FlipY(data.Map.TR_StoneTriangles[i].Y));
                Console.Write('\u2591');
            }

            int StoneTLTriPoints = data.Map.TL_StoneTriangles.Count;
            for (int i = 0; i < StoneTLTriPoints; i++)
            {
                Console.SetCursorPosition(data.Map.TL_StoneTriangles[i].X - 1, FlipY(data.Map.TL_StoneTriangles[i].Y));
                Console.Write('\u2592');
            }

            int StoneLRTriPoints = data.Map.LR_StoneTriangles.Count;
            for (int i = 0; i < StoneLRTriPoints; i++)
            {
                Console.SetCursorPosition(data.Map.LR_StoneTriangles[i].X - 1, FlipY(data.Map.LR_StoneTriangles[i].Y));
                Console.Write('\u2593');
            }

            Console.SetCursorPosition((int)data.Map.character.Position.X, FlipY((int)data.Map.character.Position.Y));
            Console.ForegroundColor = Enum.TryParse(data.Map.character.Color.ToKnownColor().ToString(), out ConsoleColor AvatarColor) ? AvatarColor : Console.ForegroundColor;
            if (data.Map.character.Gender == GENDER.FEMALE)
            {
                Console.Write("\u2640");
            }
            else if (data.Map.character.Gender == GENDER.MALE)
            {
                Console.Write("\u2642");
            }
        }

        public static void MoveCharacter(MoveEventArgs data)
        {
            Console.SetCursorPosition((int)data.OldPosition.X - 1, FlipY((int)data.OldPosition.Y));
            Console.Write(' ');
            Console.SetCursorPosition((int)data.NewPosition.X - 1, FlipY((int)data.NewPosition.Y));
            Console.ForegroundColor = Enum.TryParse(data.Map.character.Color.ToKnownColor().ToString(), out ConsoleColor AvatarColor) ? AvatarColor : Console.ForegroundColor;
            if (data.Map.character.Gender == GENDER.FEMALE)
            {
                Console.Write("\u2640");
            }
            else if (data.Map.character.Gender == GENDER.MALE)
            {
                Console.Write("\u2642");
            }
        }
    }
}
