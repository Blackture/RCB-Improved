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
        STR,
        CHARACTER
    }

    public enum STAT_TYPE
    {
        NONE,
        INT,
        FLOAT,
        ELEMENT,
        BOOL
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

    public enum ELEMENT
    {
        Fire,
        Water,
        Earth,
        Air
    }

    public enum UI_ELEMENT_TYPE {
        MENU,
        SCENE
    }
}
