using RCBImprovedC.Menus;
using RCBLibrary;
using RCBLibrary.Characters;
using RCBLibrary.Input;
using RCBLibrary.Input.Requests;
using RCBLibrary.Math;
using RCBLibrary.SceneManagement;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;

namespace RCBImprovedC
{
    public static class Input
    {
        public static void OnInputRequest(InputRequest ir)
        {
            if (ir == null) return;

            if (ir.Info == "Main Menu")
            {
                Handlers.MainMenu(ir);
            }
            else if (ir.Info == "Settings Menu")
            {
                Handlers.SettingsMenu(ir);
            }
            else if (ir.Info == "Character Menu")
            {
                Handlers.CharacterMenu(ir);
            }
            else if (ir.Info.StartsWith("PS-"))
            {
                Handlers.ProceduralScene(ir);
            }
        }

        public static class Maps
        {
            public static Dictionary<ConsoleKey, int> MainMenu = new Dictionary<ConsoleKey, int>()
            {
                { ConsoleKey.S, 0 },
                { ConsoleKey.O, 2 },
                { ConsoleKey.E, 3 }
            };

            public static Dictionary<ConsoleKey, int> SettingsMenu = new Dictionary<ConsoleKey, int>()
            {
                { ConsoleKey.B, 0 },
                { ConsoleKey.E, 11 },
                { ConsoleKey.Q, 12 }
            };

            public static Dictionary<ConsoleKey, int> ProceduralScene = new Dictionary<ConsoleKey, int>()
            {
                { ConsoleKey.W, 11 },
                { ConsoleKey.A, 12 },
                { ConsoleKey.S, 13 },
                { ConsoleKey.D, 14 }
            };
        }

        public static class Handlers
        {
            public static void MainMenu(InputRequest ir)
            {
                ConsoleKeyInfo code;
                do
                {
                    code = Console.ReadKey(true);
                    if (UIManager.Instance.CurrentElement.Key != "Main Menu") return;
                } while (Maps.MainMenu.ContainsKey(code.Key) == false);

                (ir as IntRequest).Reply(Maps.MainMenu[code.Key]);
            }

            public static void SettingsMenu(InputRequest ir)
            {
                ConsoleKeyInfo code;
                do
                {
                    code = Console.ReadKey(true);
                    if (UIManager.Instance.CurrentElement.Key != "Settings Menu") return;
                } while (Maps.SettingsMenu.ContainsKey(code.Key) == false);

                (ir as IntRequest).Reply(Maps.SettingsMenu[code.Key]);
            }

            public static void CharacterMenu(InputRequest ir)
            {
                if (UIManager.Instance.CurrentElement.Key != "Character Menu") return;
                Menus.CharacterMenu? cm = (UIManager.Instance.CurrentElement as Menus.CharacterMenu);
                if (cm == null) return;

                do
                {
                    ConsoleKey ck = Console.ReadKey(true).Key;
                    switch (ck)
                    {
                        case ConsoleKey.Enter:
                            cm.Active = cm.Hover;
                            break;
                        case ConsoleKey.W:
                            int hover = ((int)cm.Hover - 1);
                            if (hover <= 0) cm.Hover = (Menus.CharacterMenu.SETTING)3;
                            else cm.Hover = (Menus.CharacterMenu.SETTING)hover;
                            Output.ClearCurrentConsoleLines(0, cm.lines);
                            Console.SetCursorPosition(0, 0);
                            UIManager.Instance.ReRender();
                            break;
                        case ConsoleKey.S:
                            cm.Hover = (Menus.CharacterMenu.SETTING)((int)cm.Hover + 1);
                            if ((int)cm.Hover > 3) cm.Hover = (Menus.CharacterMenu.SETTING)1;
                            Output.ClearCurrentConsoleLines(0, cm.lines);
                            Console.SetCursorPosition(0, 0);
                            UIManager.Instance.ReRender();
                            break;
                        case ConsoleKey.B:
                            UIManager.Instance.ShowElement("Main Menu");
                            return;
                        case ConsoleKey.Spacebar:
                            Console.Clear();
                            Game.Instance.Start(GenerationScreen.GenerationCallback, MapDataRenderer.Renderer, MapDataRenderer.MoveCharacter, new RCBLibrary.Raycast.Axis.Point() { X = Console.BufferWidth, Y = Console.BufferHeight });
                            break;
                    }
                } while (cm.Active == Menus.CharacterMenu.SETTING.NONE);

                CharacterRequest cr = (ir as CharacterRequest);
                Character c = cr.Value;

                switch (cm.Active)
                {
                    case Menus.CharacterMenu.SETTING.NAME:
                        Console.WriteLine();
                        Console.Write("New Name: ");
                        string? name = Console.ReadLine();

                        if (name == null)
                        {
                            cr.Reply(c);
                            return;
                        }

                        cm.Active = Menus.CharacterMenu.SETTING.NONE;
                        c.SetName(name);
                        cr.Reply(c);
                        break;
                    case Menus.CharacterMenu.SETTING.GENDER:
                        bool isSelecting = true;
                        Console.WriteLine();
                        Vector2 pos1 = new Vector2(Console.CursorLeft, Console.CursorTop);
                        GENDER gender = c.Gender;
                        do
                        {
                            if (gender == GENDER.MALE)
                            {
                                Console.WriteLine("[A] Female      > Male < [D]");
                            }
                            else
                            {
                                Console.WriteLine("[A] > Female <      Male [D]");
                            }

                            ConsoleKey ck = Console.ReadKey(true).Key;
                            switch (ck)
                            {
                                case ConsoleKey.Enter:
                                    c.Gender = gender;
                                    isSelecting = false;
                                    break;
                                case ConsoleKey.A:
                                    gender = GENDER.FEMALE;
                                    Output.ClearCurrentConsoleLine((int)pos1.Y);
                                    Console.SetCursorPosition(0, (int)pos1.Y);
                                    break;
                                case ConsoleKey.D:
                                    gender = GENDER.MALE;
                                    Output.ClearCurrentConsoleLine((int)pos1.Y);
                                    Console.SetCursorPosition(0, (int)pos1.Y);
                                    break;
                            }
                        } while (isSelecting);

                        cm.Active = Menus.CharacterMenu.SETTING.NONE;
                        c.Gender = gender;
                        cr.Reply(c);
                        break;

                        case Menus.CharacterMenu.SETTING.COLOR:
                        isSelecting = true;
                        Console.WriteLine("\nChose your Color:\n");
                        Vector2 pos2 = new Vector2(Console.CursorLeft, Console.CursorTop);

                        if (!Enum.TryParse(c.Color.Name, out ConsoleColor color))
                        {
                            color = ConsoleColor.Magenta;
                        } 

                        List<ConsoleColor> colors = new List<ConsoleColor>()
                        {
                            ConsoleColor.Magenta,
                            ConsoleColor.Yellow,
                            ConsoleColor.Red,
                            ConsoleColor.Cyan,
                            ConsoleColor.Blue,
                            ConsoleColor.White
                        };
                        int index = colors.FindIndex(x => x == color);
                        
                        do
                        {
                            switch (color)
                            {
                                case ConsoleColor.Magenta:
                                    Console.WriteLine(">Magenta< Yellow Red Cyan Blue White");
                                    break;
                                case ConsoleColor.Yellow:
                                    Console.WriteLine("Magenta >Yellow< Red Cyan Blue White");
                                    break;
                                case ConsoleColor.Red:
                                    Console.WriteLine("Magenta Yellow >Red< Cyan Blue White");
                                    break;
                                case ConsoleColor.Cyan:
                                    Console.WriteLine("Magenta Yellow Red >Cyan< Blue White");
                                    break;
                                case ConsoleColor.Blue:
                                    Console.WriteLine("Magenta Yellow Red Cyan >Blue< White");
                                    break;
                                case ConsoleColor.White:
                                    Console.WriteLine("Magenta Yellow Red Cyan Blue >White<");
                                    break;
                            }

                            ConsoleKey ck = Console.ReadKey(true).Key;
                            switch (ck)
                            {
                                case ConsoleKey.Enter:
                                    Enum.TryParse(color.ToString(), out KnownColor kc);
                                    c.Color = Color.FromKnownColor(kc);
                                    isSelecting = false;
                                    break;
                                case ConsoleKey.A:
                                    index--;
                                    if (index < 0) index = colors.Count - 1;
                                    color = colors[index];
                                    Output.ClearCurrentConsoleLine((int)pos2.Y);
                                    Console.SetCursorPosition(0, (int)pos2.Y);
                                    break;
                                case ConsoleKey.D:
                                    index++;
                                    if (index >= colors.Count) index = 0;
                                    color = colors[index];
                                    Output.ClearCurrentConsoleLine((int)pos2.Y);
                                    Console.SetCursorPosition(0, (int)pos2.Y);
                                    break;
                            }

                        } while (isSelecting);

                        cm.Active = Menus.CharacterMenu.SETTING.NONE;
                        Output.ClearCurrentConsoleLine((int)pos2.Y);
                        Console.SetCursorPosition(0, (int)pos2.Y);
                        cr.Reply(c);
                        break;
                }

            }

            public static void ProceduralScene(InputRequest ir)
            {
                ConsoleKeyInfo code;
                do
                {
                    code = Console.ReadKey(true);
                    if (UIManager.Instance.CurrentElement.Key.StartsWith("PS-")) return;
                } while (Maps.ProceduralScene.ContainsKey(code.Key) == false);

                (ir as IntRequest).Reply(Maps.ProceduralScene[code.Key]);
            }
        }
    }
}
