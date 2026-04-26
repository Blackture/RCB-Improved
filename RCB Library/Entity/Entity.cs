using RCBLibrary.Events;
using RCBLibrary.Math;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RCBLibrary.Entity
{
    public class Entity : IEntity
    {
        private string id = "";
        private string name = "";
        private Event render = new Event();

        public string Name => name;
        public string Id => id;

        public Vector2 this[string uid] 
        {
            get { return entities[uid]; }
        }

        public List<string> Keys => entities.Keys.ToList();

        private Dictionary<string, Vector2> entities = new Dictionary<string, Vector2>();

        public Entity(string name)
        {
            this.name = name;
            id = "";
            id = name.SanitizeId();
            if (string.IsNullOrEmpty(name.Trim()) || string.IsNullOrEmpty(id))
            {
                new InvalidEntityNameError(name).Send();
            }
        }

        public void Initialize(Action render)
        {
            this.render += render;
        }

        public string Instantiate(Vector2 position)
        {
            string uid = Guid.NewGuid().ToString();
            entities.Add(uid, position);
            render.Invoke();
            return uid;
        }

        public bool Kill(string uid)
        {
            return EntityManager.Instance.KillEntity(uid, id);
        }

        public bool KillAll()
        {
            return EntityManager.Instance.KillAll(id);
        }
    }
}
