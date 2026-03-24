using RCBLibrary.Events;
using RCBLibrary.Raycast.Axis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.SceneManagement
{
    public class Scene
    {
        private bool isProcedural = false;
        private int index;
        private PSC proceduralSceneController;
        public MapData? mapData = null;

        public bool IsProcedural => isProcedural;
        public int Index => index;

        public Event<string> GenerationUpdate = new Event<string>();
        public Event<MapData> Generated = new Event<MapData>();

        public Scene(int index, Point dimensions, bool isProcedural = false)
        {
            this.index = index;
            this.isProcedural = isProcedural;
            if (isProcedural) proceduralSceneController = PSC.Generate(dimensions.X, dimensions.Y, OnGenerationUpdate, OnGenerated);
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
            Generated.Invoke(md);
        }

        public void OnGenerationUpdate(string s)
        {
            GenerationUpdate.Invoke(s);
        }
    }
}
