using RCBImprovedC;
using RCBLibrary.Math;
using RCBLibrary.Raycast.Axis;
using RCBLibrary.SceneManagement;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RCBLibrary.Entity
{
    public class EntityManager
    {
        private static EntityManager instance;
        public static EntityManager Instance => instance;

        public static int entitySpread = 10;

        public static void CreateInstance()
        {
            if (instance == null)
            {
                instance = new EntityManager();
            }
        }

        private List<IEntity> registeredEntities = new List<IEntity>();

        public bool RegisterEntity(IEntity entity)
        {
            if (entity == null) return false;
            if (registeredEntities.Exists(e => e.Id == entity.Id))
            {
                new EntityAlreadyExistsError(entity.Name).Send();
                return false;
            }
            registeredEntities.Add(entity);
            return true;
        }

        public bool RegisterEntity(IEntity entity, Action render)
        {
            if (entity == null) return false;
            if (registeredEntities.Exists(e => e.Id == entity.Id))
            {
                new EntityAlreadyExistsError(entity.Name).Send();
                return false;
            }
            entity.Initialize(render);
            registeredEntities.Add(entity);
            return true;
        }

        public void OverrideRenderer(string id, Action render)
        {
            IEntity? entity = GetEntity(id);
            if (entity != null)
            {
                entity.Initialize(render);
            }
        }

        public void OverrideRenderers(Action render, params string[] ids)
        {
            foreach (string id in ids)
            {
                OverrideRenderer(id, render);
            }
        }

        public IEntity? GetEntity(string id)
        {
            return registeredEntities.Find(e => e.Id == id);
        }

        private ProceduralScene? CurrentProceduralScene()
        {
            return UIManager.Instance.CurrentElement as ProceduralScene;
        }

        public bool KillEntity(string uid, string id)
        {
            ProceduralScene? scene = CurrentProceduralScene();
            if (scene != null)
            {
                MapData? data = scene.mapData;
                if (data == null) return false;
                List<IEntity>? entities = data.entities;
                if (entities != null)
                {
                    IEntity? entity = entities.Find(x => x.Id == id);
                    if (entity == null) return false;
                    Point blockedPoint = new Point() { X = (int)entity[uid].X, Y = (int)entity[uid].Y };
                    data.BlockedPoints.RemoveAll(x => x == blockedPoint);
                    return true;
                }
            }
            return false;
        }

        public bool KillAll(string id)
        {
            ProceduralScene? scene = CurrentProceduralScene();
            if (scene != null)
            {
                MapData? data = scene.mapData;
                if (data == null) return false;
                List<IEntity>? entities = data.entities;
                if (entities != null)
                {
                    IEntity? entity = entities.Find(x => x.Id == id);
                    if (entity == null) return false;
                    foreach (string k in entity.Keys)
                    {
                        Point blockedPoint = new Point() { X = (int)entity[k].X, Y = (int)entity[k].Y };
                        data.BlockedPoints.RemoveAll(x => x == blockedPoint);
                    }
                    data.entities.Remove(entity);
                    return true;
                }
            }
            return false;
        }


        public bool SummonEntity(IEntity entity, Vector2 position)
        {
            if (!ValidPosition(position)) return false;
            if (entity == null) return false;
            string uid = entity.Instantiate(position);
            ProceduralScene? scene = CurrentProceduralScene();
            if (scene != null)
            {
                if (entity is ICollectible && IsTooClose(uid, entity.Id)) return false;
                scene.mapData?.entities.Add(entity);
                scene.mapData?.BlockedPoints.Add(new Point() { X = (int)entity[uid].X, Y = (int)entity[uid].Y });
            }
            return true;
        }

        public bool ValidPosition(Vector2 position)
        {
            ProceduralScene? scene = CurrentProceduralScene();
            if (scene != null)
            {
                bool? valid = scene.mapData?.spawnablePoints.Contains(new Point() { X = (int)position.X, Y = (int)position.Y });
                if (valid != null && valid == true)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsTooClose(string uid, string id)
        {
            List<ICollectible> collectibles = GetCollectibles();

            IEntity? main = GetEntity(id);
            Vector2? pos = main?[uid];

            if (main != null)
            {
                foreach (ICollectible collectible in collectibles)
                {
                    IEntity? c = (collectible as IEntity);
                    string[] keys = c?.Keys.ToArray() ?? [];

                    foreach (string ueid in keys)
                    {
                        if (!(c[ueid] == null || pos == null))
                        {
                            float d = Vector2.Distance(c[ueid], pos);
                            if (d < entitySpread) return true;
                        }
                    }
                }
            }
            return false;
        }

        public List<ICollectible> GetCollectibles()
        {
            return (UIManager.Instance.CurrentElement as ProceduralScene)?.mapData?.entities.OfType<ICollectible>().ToList() ?? new List<ICollectible>();
        }
    }
}
