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
            { "Main Menu", new MainMenu() }
        };

        static void Main(string[] args)
        {
            Game.Instance.OverrideMenus(menuOverrides);
            Game.Instance.Initialize();
            Game.Instance.Error.AddListener((e) => Console.WriteLine($"Error: {e.Message}"));
            // 1. Einmalig registrieren
            Game.Instance.Input.AddListener(OnInputRequest);
            Game.Instance.Start();

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
            Console.Clear();
            Game.Instance.ReRender();
            Console.WriteLine("Type:");

            if (ir.Info == "Main Menu")
            {
                MainMenuInput(ir);
            }
        }

        static void MainMenuInput(InputRequest ir)
        {
            ConsoleKeyInfo code = Console.ReadKey(true);
            switch (code.Key)
            {
                case ConsoleKey.S:
                    (ir as IntRequest).Reply(0);
                    break;
                case ConsoleKey.O:
                    (ir as IntRequest).Reply(1);
                    break;
                case ConsoleKey.E:
                    (ir as IntRequest).Reply(2);
                    break;
            }
        }
    }
}