using RCBImprovedC;
using RCBLibrary.Raycast.Axis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.SceneManagement
{
    public class SceneManager
    {
        private static SceneManager instance = null;
        public static SceneManager Instance
        {
            get
            {
                instance ??= new SceneManager();
                return instance;
            }
        }

        private List<Scene> scenes = new List<Scene>();

        public SceneManager() { }

        public static void RegisterScene(Scene scene)
        {
            UIManager.Instance.RegisterScene(scene);
        }

        public string CreateProceduralScene(Action<MapDataEventArgs> render, string key = null, Action<string> generationUpdateCallback = null, Point? dimensions = null)
        {
            ProceduralScene ps = new ProceduralScene(index: scenes.Count, key: key); 
            RegisterScene(ps);
            ps.Initialize(generationUpdateCallback, dimensions);
            ps.OnRender.AddListener(render);
            return ps.Key;
        }
    }
}
