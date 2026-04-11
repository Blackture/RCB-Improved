using NAudio.SoundFont;
using RCBImprovedC;
using RCBLibrary.Characters;
using RCBLibrary.Events;
using RCBLibrary.Input;
using RCBLibrary.Input.Errors;
using RCBLibrary.Input.Requests;
using RCBLibrary.Math;
using RCBLibrary.Menus;
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
        private BIOME biome;

        public MapData? mapData = null;

        public Event<string> GenerationUpdate = new Event<string>();
        public Event<MapData> CharacterMoved = new Event<MapData>();

        public ProceduralScene(BIOME? biome = null, int index = -1, string? key = null) : base(index, key, true)
        {
            this.biome = biome ?? (BIOME)new Random().Next(Enum.GetValues(typeof(BIOME)).Length);
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
            if (IsProcedural && dimensions != null) PSC.Generate(dimensions.Value.Y, dimensions.Value.X, OnGenerationUpdate, OnGenerated);
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
                character = (UIManager.Instance.GetElement("Character Menu") as CharacterMenu)?.Character ?? new Character("Example"),
                StonePoints = psc.StonePoints,
                BlockedPoints = psc.BlockedPoints,
                LR_StoneTriangles = psc.LR_StoneTriangles,
                BR_StoneTriangles = psc.BR_StoneTriangles,
                BL_StoneTriangles = psc.BL_StoneTriangles,
                TL_StoneTriangles = psc.TL_StoneTriangles,
                TR_StoneTriangles = psc.TR_StoneTriangles,
                mapSize = new Point() { X = psc.Width, Y = psc.Height }
            };
            md.character.Position = psc.SpawnPoint;
            md.biome = biome;
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
            if (UIManager.Instance.CurrentElement != this) return;

            IntRequest r = new IntRequest($"PS-{Index}");
            r.Subscribe(InputCallback);
            r.Send();
        }

        public override void InputCallback(InputRequest ir)
        {
            if (UIManager.Instance.CurrentElement != this) return;

            if (ir == null) return;
            int? i = (ir as IntRequest)?.Value;

            if (i == null)
            {
                (new InvalidIntegerInputError<int?>(i)).Send();
            }

            ProcessInput((int)i);
        }

        private void ProcessInput(int input)
        {
            PS_INPUT setting = (PS_INPUT)Mathf.Floor(input / 10f);

            switch (setting)
            { 
                case PS_INPUT.MOVEMENT:
                    int i = input % 10;
                    mapData!.lastCharacterPosition = mapData.character.Position;
                    switch (i)
                    {
                        case 1:
                            // W ------------------------------
                            if (mapData!.character.Position.Y + 1 >= mapData!.mapSize.Y)
                            mapData!.character.Position.Y++;
                            break;
                        case 2:
                            // A ------------------------------
                            if (mapData!.character.Position.Y - 1 >= 0)
                                mapData!.character.Position.Y--;
                            break;
                        case 3:
                            // S ------------------------------
                            if (mapData!.character.Position.X - 1 >= 0)
                                mapData!.character.Position.X--;
                            break;
                        case 4:
                            // D ------------------------------
                            if (mapData!.character.Position.X + 1 >= mapData!.mapSize.X)
                                mapData!.character.Position.X--;
                            break;
                    }
                    CharacterMoved.Invoke(mapData!);
                    break;
            }

            Input();
        }


    }
}
