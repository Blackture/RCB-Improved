using RCBLibrary.Events;
using RCBLibrary.Input;
using RCBLibrary.Menus;
using System.Diagnostics;

namespace RCBLibrary
{
    public class Game
    {
        private static readonly Lazy<Game> _lazy = new Lazy<Game>(() => new Game());

        private bool active = true;
        private Menu currentMenu;
        private string lastMenu;
        private bool inGame = false;
        private SettingsData settings;

        private Event<string> menuChanged = new Event<string>();

        public Event<InputRequest> Input = new Event<InputRequest>();
        public Event<Error> Error = new Event<Error>();

        public static Game Instance => _lazy.Value;
        public bool Active => active;
        public Menu CurrentMenu => currentMenu;
        public string LastMenu => lastMenu;

        public bool InGame => inGame;

        private Dictionary<string, Menu> menus;

        public void ReRender()
        {
            if (currentMenu != null)
            {
                currentMenu.Render();
            }
            else
            {
                //ReRender Game.
            }
        }

        public void OverrideMenus(Dictionary<string, Menu> menuOverrides)
        {
            for (int i = 0; i < menus.Keys.Count; i++)
            {
                if (menuOverrides.ContainsKey(menus.Keys.ElementAt(i)))
                {
                    menus[menus.Keys.ElementAt(i)] = menuOverrides[menus.Keys.ElementAt(i)];
                }
            }
        }

        public void Initialize()
        {
            Error.AddListener(OnError);
            settings = new SettingsData()
            {
                BackgroundMusicVolume = 20,
            };

            menus = new Dictionary<string, Menu>()
            {
                { "Main Menu", new MainMenu()  },
                { "Settings Menu", new SettingsMenu() },
            };
            AudioManager.Instance.Initialize(settings);
        }

        public void Awake()
        {
            (menus["Settings Menu"] as SettingsMenu).Initialize(settings);
            currentMenu = menus["Main Menu"];
            AudioManager.Instance.PlayBackgroundMusic();
            ShowMenu("Main Menu");
        }

        /// <summary>
        /// For Main Menu input processor, do not touch.
        /// </summary>
        public void Start()
        {
            inGame = true;
        }

        public void ShowMenu(string title)
        {
            if (!menus.ContainsKey(title))
            {
                new MenuNotFoundError(title).Send();
                return;
            }
            lastMenu = currentMenu.Key;
            currentMenu = menus[title];
            menuChanged.Invoke(lastMenu);
            currentMenu.Show();
        }

        private void OnError(Error error)
        {
            Debug.WriteLine($"Error {error.Code}: {error.Title}");
            Debug.WriteLine(error.Message);
            if (active && error.Fatal)
            {
                active = false;
            }
        }

        public void Stop()
        {
            active = false;
        }

        public void ExitToMainMenu()
        {
            inGame = false;
            ShowMenu("Main Menu");
        }

        /// <summary>
        /// Save Settins
        /// </summary>
        /// <param name="settings"></param>
        public void SaveSettingsToGame(SettingsData settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// Registers a callback function to be invoked whenever the menu changes. The callback receives the key of the last menu (from which it changed) as a parameter.
        /// </summary>
        /// <param name="callback"></param>
        public void MenuChangedAddListener(Action<string> callback)
        {
            menuChanged.AddListener(callback);
        }
    }
}
