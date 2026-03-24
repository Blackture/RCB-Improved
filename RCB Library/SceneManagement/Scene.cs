using RCBLibrary.Character.StatTypes;
using RCBLibrary.Events;
using RCBLibrary.Raycast.Axis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace RCBLibrary.SceneManagement
{
    public class Scene : IUIElement
    {
        private bool isProcedural = false;
        private int index;
        private readonly PSC? proceduralSceneController;
        private bool generated = false;
        private bool isActive = false;

        private string key;
        private UI_ELEMENT_TYPE type;
        public string Key => key;
        public UI_ELEMENT_TYPE Type => type;

        public MapData? mapData = null;

        public bool IsActive => isActive;
        public bool IsProcedural => isProcedural;
        public int Index => index;

        private Event<string> GenerationUpdate = new Event<string>();
        private Event<MapData> Generated = new Event<MapData>();

        public Scene(int index, string? key = null, Point? dimensions = null, bool isProcedural = false)
        {
            this.index = index;
            this.isProcedural = isProcedural;
            type = UI_ELEMENT_TYPE.SCENE;
            this.key = key ?? $"Scene {index}";

            GenerationUpdate.AddListener(LoadingRenderer);
            Generated.AddListener(RenderMap);

            if (isProcedural && dimensions != null) proceduralSceneController = PSC.Generate(dimensions.Value.X, dimensions.Value.Y, OnGenerationUpdate, OnGenerated);
        }

        public void Render()
        {
            if (IsProcedural && generated)
            {
                RenderMap(mapData!);
            }
        }

        public void Show()
        {
            isActive = true;
            Render();
            Debug.WriteLine("Internal Show");
        }

        public void RenderMap(MapData data)
        {
            if (data == null) return;
            if (!generated) return;
            RenderMapOverride(data);
        }

        public virtual void RenderMapOverride(MapData data)
        {

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

            Generated.Invoke(md);
        }

        public void OnGenerationUpdate(string s)
        {
            GenerationUpdate.Invoke(s);
        }
    }
}
