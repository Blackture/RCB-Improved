using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public class SettingsData
    {
        private int backgroundMusicvolume;
        public int BackgroundMusicVolume
        {
            get { return backgroundMusicvolume; }
            set { backgroundMusicvolume = Math.Clamp(value,0,100); }
        }
    }
}
