using RCBLibrary.Events;
using RCBLibrary.Input;
using RCBLibrary.Menus;
using System.Diagnostics;

namespace RCBLibrary
{
    public class Game
    {
        private static readonly Lazy<Game> _lazy = new Lazy<Game>(() => new Game());

        protected Menu currentMenu;

        public static Game Instance => _lazy.Value;

        public Event<Error> Error = new Event<Error>();
        public Event<InputRequest> Input = new Event<InputRequest>();

        private bool active = true;
        public bool Active => active;
        public Menu CurrentMenu => currentMenu;

        private Dictionary<string, Menu> menus = new Dictionary<string, Menu>()
        {
            { "Main Menu", new MainMenu() }
        };

        public Game()
        {

        }

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
            currentMenu = menus["Main Menu"];
            AudioManager.Instance.Initialize();
        }

        public void Start()
        {
            AudioManager.Instance.PlayBackgroundMusic();
            menus["Main Menu"].Show();
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
    }
}
