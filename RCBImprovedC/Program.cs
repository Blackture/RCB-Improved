using RCBLibrary;
using RCBLibrary.Input;
using RCBLibrary.Input.Requests;
using RCBLibrary.Menus;
using System.Diagnostics;

namespace RCBImprovedC
{
    public class Program
    {
        public static Dictionary<string, Menu> menuOverrides = new Dictionary<string, Menu>()
        {
            { "Main Menu", new Menus.MainMenu() },
            { "Settings Menu", new Menus.SettingsMenu() },
            { "Character Menu", new Menus.CharacterMenu()  }
        };

        static void Main(string[] args)
        {
            Game.Instance.Initialize();
            UIManager.Instance.OverrideMenus(menuOverrides);
            //AudioManager.Instance.AddBackgroundMusic(bla);

            Game.Instance.Error.AddListener((e) => {
                (int left, int top) = Console.GetCursorPosition();
                Console.SetCursorPosition(0, Console.BufferHeight - 12);
                Output.ClearCurrentConsoleLines(Console.BufferHeight - 12, Console.BufferHeight - 6);
                string result = new string('-', Console.BufferWidth);
                Console.WriteLine(result);
                Console.WriteLine($"Error {e.Code}: {e.Title}");
                Console.WriteLine(e.Message);

            });
            // 1. Einmalig registrieren
            Game.Instance.Input.AddListener(Input.OnInputRequest);
            UIManager.Instance.MenuChangedAddListener((key) =>
            {
                int lines = 1;
                switch (key)
                {
                    case "Main Menu":
                        lines = (menuOverrides[key] as Menus.MainMenu).lines;
                        break;
                    case "Settings Menu":
                        lines = (menuOverrides[key] as Menus.SettingsMenu).lines;
                        break;
                    case "Character Menu":
                        lines = (menuOverrides[key] as Menus.CharacterMenu).lines;
                        break;
                }
                Output.ClearCurrentConsoleLines(0, lines);
                Console.SetCursorPosition(0, 0);
            });

            Game.Instance.Awake();

            // 2. Das Programm am Leben erhalten, solange das Spiel läuft
            while (Game.Instance.Active)
            {
                // Verhindert 100% CPU-Last, lässt den Hintergrund-Threads Zeit
                Thread.Sleep(50);
            }
        }
    }
}