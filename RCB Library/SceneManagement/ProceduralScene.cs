using NAudio.SoundFont;
using RCBImprovedC;
using RCBLibrary.Characters;
using RCBLibrary.Entity;
using RCBLibrary.Events;
using RCBLibrary.Input;
using RCBLibrary.Input.Errors;
using RCBLibrary.Input.Requests;
using RCBLibrary.Math;
using RCBLibrary.Menus;
using RCBLibrary.Raycast;
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
        public Event<MoveEventArgs> CharacterMoved = new Event<MoveEventArgs>();

        public ProceduralScene(BIOME? biome = null, int index = -1, string? key = null) : base(index, key, true)
        {
            this.biome = biome ?? (BIOME)new Random().Next(Enum.GetValues(typeof(BIOME)).Length);
        }

        public override void Render()
        {
            if (IsProcedural && CanRenderMap(mapData!))
            {
                OnRender.Invoke(new MapDataEventArgs(mapData!));
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
                lastCharacterPosition = psc.SpawnPoint,
                StonePoints = psc.StonePoints,
                BlockedPoints = psc.BlockedPoints,
                LR_StoneTriangles = psc.LR_StoneTriangles,
                BR_StoneTriangles = psc.BR_StoneTriangles,
                BL_StoneTriangles = psc.BL_StoneTriangles,
                TL_StoneTriangles = psc.TL_StoneTriangles,
                TR_StoneTriangles = psc.TR_StoneTriangles,
                mapSize = new Point() { X = psc.Width, Y = psc.Height },
                spawnablePoints = psc.spawnablePoints
            };
            md.character.Position = psc.SpawnPoint;
            md.biome = biome;
            mapData = md;
            generated = true;
            
            if (CanRenderMap(md)) OnRender.Invoke(new MapDataEventArgs(md));
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
            PS_INPUT psIn = (PS_INPUT)(input / 10);
            if (psIn != PS_INPUT.MOVEMENT) return;

            int direction = input % 10;
            Vector2 oldPos = mapData!.character.Position; // Capture BEFORE change
            Vector2 nextPos = new Vector2(oldPos.X, oldPos.Y); ;

            switch (direction)
            {
                case 1: nextPos.Y++; break;
                case 2: nextPos.X--; break;
                case 3: nextPos.Y--; break;
                case 4: nextPos.X++; break;
            }

            // Bounds and Collision Check
            if (nextPos.X >= 0 && nextPos.X < mapData.mapSize.X &&
                nextPos.Y >= 0 && nextPos.Y < mapData.mapSize.Y)
            {
                if (RaycastHit.NoBlock((int)nextPos.X, (int)nextPos.Y, mapData))
                {
                    // Update the state
                    mapData.character.Position = nextPos;

                    // Broadcast the snapshot!
                    CharacterMoved?.Invoke(new MoveEventArgs(oldPos, nextPos, mapData));
                    foreach (ICollectible c in EntityManager.Instance.GetCollectibles())
                    {
                        IEntity? e = c as IEntity;
                        if (e != null)
                        {
                            foreach (string uid in e.Keys)
                            {
                                c.IsInReach(uid, mapData.character);
                            }
                        }
                    }
                }
            }

            Input(); // Recurse for next input
        }

    }
}
