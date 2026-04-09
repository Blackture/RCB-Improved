using NAudio.SoundFont;
using RCBLibrary.Events;
using RCBLibrary.Input;
using RCBLibrary.Raycast.Axis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RCBLibrary.SceneManagement
{
    public class ProceduralScene : Scene
    {
        private bool generated = false;
        private bool isInitialized = false;

        public MapData? mapData = null;

        public Event<string> GenerationUpdate = new Event<string>();

        public ProceduralScene(int index = -1, string? key = null) : base(index, key, true)
        {
        }

        public override void Render()
        {
            if (IsProcedural && CanRenderMap(mapData!))
            {
                OnRender.Invoke(mapData!);
            }
        }

        /// <summary>
        /// Initialize Renderer
        /// </summary>
        public void Initialize(Action<string> generationUpdateCallback = null, Point? dimensions = null)
        {
            GenerationUpdate.AddListener(generationUpdateCallback);
            if (IsProcedural && dimensions != null) PSC.Generate(dimensions.Value.X, dimensions.Value.Y, OnGenerationUpdate, OnGenerated);
            isInitialized = true;
        }

        /// <summary>
        /// Internal Show
        /// </summary>
        public override void Show()
        {
            isActive = true;
            if (isInitialized)
            {
                Render();
            }
            else
            {
                Initialize();
            }
            Debug.WriteLine("Internal Show");
        }

        public bool CanRenderMap(MapData data)
        {
            if (data == null) return false;
            if (!generated) return false;
            return true;
        }

        public virtual void LoadingRenderer(string s)
        {

        }

        public void OnGenerated(PSC psc)
        {
            MapData md = new MapData
            {
                SpawnPoint = psc.SpawnPoint,
                StonePoints = psc.StonePoints,
                BlockedPoints = psc.BlockedPoints,
                LR_StoneTriangles = psc.LR_StoneTriangles,
                BR_StoneTriangles = psc.BR_StoneTriangles,
                BL_StoneTriangles = psc.BL_StoneTriangles,
                TL_StoneTriangles = psc.TL_StoneTriangles,
                TR_StoneTriangles = psc.TR_StoneTriangles
            };
            mapData = md;
            generated = true;
            
            if (CanRenderMap(md)) OnRender.Invoke(md);
        }

        public void OnGenerationUpdate(string s)
        {
            GenerationUpdate.Invoke(s);
        }

        public override void Input()
        {
            throw new NotImplementedException();
        }

        public override void InputCallback(InputRequest ir)
        {
            throw new NotImplementedException();
        }
    }
}
