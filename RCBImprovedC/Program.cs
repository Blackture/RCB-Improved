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
            Game.Instance.OverrideMenus(menuOverrides);
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
            Game.Instance.MenuChangedAddListener((key) =>
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

        static void MainMenuInput(InputRequest ir)
        {
            ConsoleKeyInfo code = Console.ReadKey(true);
            if (Game.Instance.CurrentMenu.Key != "Main Menu") return;
            switch (code.Key)
            {
                case ConsoleKey.S:
                    (ir as IntRequest).Reply(0);
                    break;
                case ConsoleKey.O:
                    (ir as IntRequest).Reply(2);
                    break;
                case ConsoleKey.E:
                    (ir as IntRequest).Reply(3);
                    break;
            }
        }

        static void SettingsMenuInput(InputRequest ir)
        {
            ConsoleKeyInfo code = Console.ReadKey(true);
            if (Game.Instance.CurrentMenu.Key != "Settings Menu") return;
            switch (code.Key)
            {
                case ConsoleKey.B:
                    (ir as IntRequest).Reply(0);
                    break;
                case ConsoleKey.E:
                    (ir as IntRequest).Reply(11);
                    break;
                case ConsoleKey.Q:
                    (ir as IntRequest).Reply(12);
                    break;
            }
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