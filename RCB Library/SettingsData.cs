using System;
using System.Collections.Generic;
using System.Text;
using RCBLibrary.Math;

namespace RCBLibrary
{
    public class SettingsData
    {
        private int backgroundMusicvolume;
        public int BackgroundMusicVolume
        {
            get { return backgroundMusicvolume; }
            set { backgroundMusicvolume = (int)Mathf.Floor(Mathf.Clamp(value,0,100)); }
        }
    }
}
