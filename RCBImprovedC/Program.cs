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
            { "Main Menu", new MainMenu() },
            { "Settings Menu", new SettingsMenu() }
        };

        static void Main(string[] args)
        {
            Game.Instance.Initialize();
            UIManager.Instance.OverrideMenus(menuOverrides);
            //AudioManager.Instance.AddBackgroundMusic(bla);

            Game.Instance.Error.AddListener((e) => {
                (int left, int top) = Console.GetCursorPosition();
                Console.SetCursorPosition(0, Console.BufferHeight - 12);
                ClearCurrentConsoleLines(Console.BufferHeight - 12, Console.BufferHeight - 6);
                string result = new string('-', Console.BufferWidth);
                Console.WriteLine(result);
                Console.WriteLine($"Error {e.Code}: {e.Title}");
                Console.WriteLine(e.Message);

            });
            // 1. Einmalig registrieren
            Game.Instance.Input.AddListener(OnInputRequest);
            UIManager.Instance.MenuChangedAddListener((key) =>
            {
                int lines = 1;
                switch (key)
                {
                    case "Main Menu":
                        lines = (menuOverrides[key] as MainMenu).lines;
                        break;
                    case "Settings Menu":
                        lines = (menuOverrides[key] as SettingsMenu).lines;
                        break;
                }
                ClearCurrentConsoleLines(0, lines);
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

        static void OnInputRequest(InputRequest ir)
        {
            if (ir == null) return;

            if (ir.Info == "Main Menu")
            {
                MainMenuInput(ir);
            }
            else if (ir.Info == "Settings Menu")
            {
                SettingsMenuInput(ir);
            }
        }

        public static Dictionary<ConsoleKey, int> MainMenuInputMap = new Dictionary<ConsoleKey, int>()
        {
            { ConsoleKey.S, 0 },
            { ConsoleKey.O, 2 },
            { ConsoleKey.E, 3 }
        };

        static void MainMenuInput(InputRequest ir)
        {
            ConsoleKeyInfo code;
            do
            {
                code = Console.ReadKey(true);
                if (UIManager.Instance.CurrentElement.Key != "Main Menu") return;
            } while (MainMenuInputMap.ContainsKey(code.Key) == false);

            (ir as IntRequest).Reply(MainMenuInputMap[code.Key]);
        }

        public static Dictionary<ConsoleKey, int> SettingsMenuInputMap = new Dictionary<ConsoleKey, int>()
        {
            { ConsoleKey.B, 0 },
            { ConsoleKey.E, 11 },
            { ConsoleKey.Q, 12 }
        };

        static void SettingsMenuInput(InputRequest ir)
        {
            ConsoleKeyInfo code;
            do
            {
                code = Console.ReadKey(true);
                if (UIManager.Instance.CurrentElement.Key != "Settings Menu") return;
            } while (SettingsMenuInputMap.ContainsKey(code.Key) == false);

            (ir as IntRequest).Reply(SettingsMenuInputMap[code.Key]);
        }

        public static void ClearCurrentConsoleLine(int line)
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, line);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void ClearCurrentConsoleLines(int start, int end)
        {
            int currentLineCursor = Console.CursorTop;

            for (int line = start; line <= end; line++)
            {
                Console.SetCursorPosition(0, line);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}