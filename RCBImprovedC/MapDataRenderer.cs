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

        static bool TrySetMapCursor(Point point)
        {
            int left = point.X - 1;
            int top = FlipY(point.Y);

            if (left < 0 || left >= Console.BufferWidth) return false;
            if (top < 0 || top >= Console.BufferHeight) return false;

            Console.SetCursorPosition(left, top);
            return true;
        }

        static void DrawPoint(Point point, char value)
        {
            if (!TrySetMapCursor(point)) return;

            Console.Write(value);
        }

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
                DrawPoint(data.Map.StonePoints[i], '\u2588');
            }

            int StoneBRTriPoints = data.Map.BR_StoneTriangles.Count;
            for (int i = 0; i < StoneBRTriPoints; i++)
            {
                DrawPoint(data.Map.BR_StoneTriangles[i], '\u2592');
            }

            int StoneBLTriPoints = data.Map.BL_StoneTriangles.Count;
            for (int i = 0; i < StoneBLTriPoints; i++)
            {
                DrawPoint(data.Map.BL_StoneTriangles[i], '\u2591');
            }

            int StoneTRTriPoints = data.Map.TR_StoneTriangles.Count;
            for (int i = 0; i < StoneTRTriPoints; i++)
            {
                DrawPoint(data.Map.TR_StoneTriangles[i], '\u2591');
            }

            int StoneTLTriPoints = data.Map.TL_StoneTriangles.Count;
            for (int i = 0; i < StoneTLTriPoints; i++)
            {
                DrawPoint(data.Map.TL_StoneTriangles[i], '\u2592');
            }

            int StoneLRTriPoints = data.Map.LR_StoneTriangles.Count;
            for (int i = 0; i < StoneLRTriPoints; i++)
            {
                DrawPoint(data.Map.LR_StoneTriangles[i], '\u2593');
            }

            if (!TrySetMapCursor(new Point((int)data.Map.character.Position.X, (int)data.Map.character.Position.Y))) return;
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
            DrawPoint(new Point((int)data.OldPosition.X, (int)data.OldPosition.Y), ' ');
            if (!TrySetMapCursor(new Point((int)data.NewPosition.X, (int)data.NewPosition.Y))) return;
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
