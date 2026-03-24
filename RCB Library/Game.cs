using RCBImprovedC;
using RCBLibrary.Character;
using RCBLibrary.Events;
using RCBLibrary.Input;
using RCBLibrary.Menus;
using System.Collections;
using System.Diagnostics;

namespace RCBLibrary
{
    public class Game
    {
        private static readonly Lazy<Game> _lazy = new Lazy<Game>(() => new Game());

        private bool active = true;

        private bool inGame = false;
        private SettingsData settings;

        public Event<InputRequest> Input = new Event<InputRequest>();
        public Event<Error> Error = new Event<Error>();

        public static Game Instance => _lazy.Value;
        public bool Active => active;

        public bool InGame => inGame;

        public void Initialize()
        {
            Error.AddListener(OnError);
            settings = new SettingsData()
            {
                BackgroundMusicVolume = 20,
            };

            UIManager.CreateInstance();
        }

        public void Initialize(Stat[] stats = null, IUIElement[] elements = null)
        {
            Error.AddListener(OnError);
            Stats.Initialize(stats);
            settings = new SettingsData()
            {
                BackgroundMusicVolume = 20,
            };
            UIManager.CreateInstance(elements.ToList());
        }

        public void Awake()
        {
            AudioManager.Instance.Awake(settings);
            UIManager.Instance.Awake(settings);

            AudioManager.Instance.PlayBackgroundMusic();
            UIManager.Instance.ShowElement("Main Menu");
        }

        /// <summary>
        /// For Main Menu input processor, do not touch.
        /// </summary>
        public void Start()
        {
            inGame = true;
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
            UIManager.Instance.ShowElement("Main Menu");
        }

        /// <summary>
        /// Save Settins
        /// </summary>
        /// <param name="settings"></param>
        public void SaveSettingsToGame(SettingsData settings)
        {
            this.settings = settings;
        }
    }
}
