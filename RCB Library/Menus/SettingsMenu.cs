using RCBLibrary.Events;
using RCBLibrary.Input;
using RCBLibrary.Input.Errors;
using RCBLibrary.Input.Requests;
using RCBLibrary.Math;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RCBLibrary.Menus
{
    public class SettingsMenu : Menu, IMenu
    {
        private Event settingsDataChanged = new Event();
        private SettingsData settingsData;

        public SettingsData SettingsData => settingsData;

        public SettingsMenu() : base("Settings Menu")
        {
        }

        public void Initialize(SettingsData settingsData)
        {
            this.settingsData = settingsData;
            settingsDataChanged.AddListener(OnSettingsDataChanged);
        }

        public override void Render()
        {
            Debug.WriteLine("Library Settings Menu Render");
        }

        public override void MenuInput()
        {
            if (Game.Instance.CurrentMenu != this) return;

            IntRequest r = new IntRequest("Settings Menu");
            r.Subscribe(MenuInputCallback);
            r.Send();
        }

        protected override void MenuInputCallback(InputRequest ir)
        {
            if (Game.Instance.CurrentMenu != this) return;

            if (ir == null) return;
            int? i = (ir as IntRequest)?.Value;

            if (i == null)
            {
                (new InvalidIntegerInputError<int?>(i)).Send();
            }

            ProcessInput((int)i);
        }

        protected override void ProcessInput(int input)
        {
            SETTINGS_MENU_INPUT setting = (SETTINGS_MENU_INPUT)Mathf.Floor(input / 10f);

            switch (setting)
            {
                case SETTINGS_MENU_INPUT.Back:
                    Game.Instance.ShowMenu(Game.Instance.LastMenu);
                    break;
                case SETTINGS_MENU_INPUT.BackgroundMusicVolume:
                    int i = input % 10;
                    if (i == 1)
                    {
                        settingsData.BackgroundMusicVolume++;
                        AudioManager.Instance.SetBackgroundMusicVolume(settingsData.BackgroundMusicVolume);
                    }
                    else
                    {
                        settingsData.BackgroundMusicVolume--;
                        AudioManager.Instance.SetBackgroundMusicVolume(settingsData.BackgroundMusicVolume);
                    }
                    break;
                case SETTINGS_MENU_INPUT.Controls:
                    break;
            }

            settingsDataChanged.Invoke();
            MenuInput();
        }

        public virtual void OnSettingsDataChanged()
        {
        }
    }
}
