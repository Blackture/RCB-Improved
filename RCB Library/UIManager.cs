using RCBLibrary;
using RCBLibrary.Events;
using RCBLibrary.Menus;
using RCBLibrary.SceneManagement;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

namespace RCBImprovedC
{
    public class UIManager
    {
        private static UIManager instance;
        public static UIManager Instance => instance;

        private List<IUIElement> uiElements;
        private string lastElement;
        private IUIElement currentElement;

        private Dictionary<string, Menu> menus;

        private Event<string> menuChanged = new Event<string>();
        private Event<IUIElement> uiElementChanged = new Event<IUIElement>();

        private bool inScene = false;

        public IUIElement CurrentElement => currentElement;
        public string LastElement => lastElement;

        public static void CreateInstance()
        {
            if (instance == null)
            {
                instance = new UIManager();
                instance.Initialize();
            }
        }

        public static void CreateInstance(params List<IUIElement> additionalElements)
        {
            if (instance == null)
            {
                instance = new UIManager();
                instance.Initialize(additionalElements);
            }
        }

        private void Initialize()
        {
            menus = new Dictionary<string, Menu>()
            {
                { "Main Menu", new MainMenu() },
                { "Settings Menu", new SettingsMenu() },
                { "Character Menu", new CharacterMenu() }
            };

            uiElements = new List<IUIElement>();
        }

        private void Initialize(params List<IUIElement> additionalElements)
        {
            menus = new Dictionary<string, Menu>()
            {
                { "Main Menu", new MainMenu() },
                { "Settings Menu", new SettingsMenu() },
                { "Character Menu", new CharacterMenu() }
            };

            uiElements = [.. additionalElements];
        }

        public void Awake(SettingsData settings)
        {
            uiElements.AddRange(menus.Values);
            (menus["Settings Menu"] as SettingsMenu)?.Initialize(settings);
            (menus["Character Menu"] as CharacterMenu)?.Initialize();
            currentElement = menus["Main Menu"];
        }

        public void ReRender()
        {
            if (currentElement != null)
            {
                currentElement.Render();
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

        private void ShowMenu(string title)
        {
            if (!menus.ContainsKey(title))
            {
                new MenuNotFoundError(title).Send();
                return;
            }
            menuChanged.Invoke(lastElement);
        }

        public void ShowLastElement()
        {
            ShowElement<IUIElement>(lastElement);
        }

        public void ShowElement(string key)
        {
            ShowElement<IUIElement>(key);
        }

        public T? ShowElement<T>(string key) where T : class, IUIElement
        {
            IUIElement? el = uiElements.Find(x => x.Key == key);
            if (el == null)
            {
                new UINotFoundError(lastElement).Send();
                return null;
            }
            if (el.Type == UI_ELEMENT_TYPE.MENU)
            {
                ShowMenu(key);
            } else if (el.Type == UI_ELEMENT_TYPE.SCENE)
            {
                inScene = true;
            }
            lastElement = currentElement.Key;
            currentElement = el;
            currentElement.Show();

            Input();
            return (T)el;
        }

        private void Input()
        {
            if (currentElement != null && currentElement is IInputable inputable)
            {
                inputable.Input();
            }
        }

        public void RegisterScene(Scene scene)
        {
            scene.Register(uiElements.Count);
            uiElements.Add(scene);
        }

        /// <summary>
        /// Registers a callback function to be invoked whenever the menu changes. The callback receives the key of the last menu (from which it changed) as a parameter.
        /// </summary>
        /// <param name="callback"></param>
        public void MenuChangedAddListener(Action<string> callback)
        {
            menuChanged.AddListener(callback);
        }


        /// <summary>
        /// Registers a callback function to be invoked whenever the menu changes. The callback receives the key of the last menu (from which it changed) as a parameter.
        /// </summary>
        /// <param name="callback"></param>
        public void ElementChangedAddListener(Action<string> callback)
        {
            menuChanged.AddListener(callback);
        }
    }
}
