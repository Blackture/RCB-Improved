using RCBLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBImprovedC
{
    public class SettingsMenu : RCBLibrary.Menus.SettingsMenu
    {
        public int lines = 4;

        public override void Render()
        {
            Console.WriteLine("Settings Menu");
            Console.WriteLine();
            Console.WriteLine($"Volume: Q < {SettingsData.BackgroundMusicVolume.ToString("D3")}% > E");
            Console.WriteLine("Back [B]");
        }

        public override void OnSettingsDataChanged()
        {
            if (Game.Instance.CurrentMenu != this) return;
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, 2);
            Console.Write($"Volume: Q < {SettingsData.BackgroundMusicVolume.ToString("D3")}% > E");
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
