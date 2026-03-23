using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public enum INPUT_MAPPING
    {
        Action,
        MainMenu
    }

    public enum INPUT_TYPE
    {
        INT,
        STR
    }

    public enum MAIN_MENU_INPUT
    {
        Singleplayer = 0,
        Online = 1,
        Settings = 2,
        Exit = 3
    }

    public enum SETTINGS_MENU_INPUT
    {
        Back = 0,
        BackgroundMusicVolume = 1,
        Controls = 2,
    }
}
