using RCBImprovedC;
using RCBLibrary.Characters;
using RCBLibrary.Events;
using RCBLibrary.Inventories;
using RCBLibrary.Math;
using RCBLibrary.SceneManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RCBLibrary.Entity.Entities
{
    public class ItemEntity : Entity, ICollectible
    {
        private IItem? item;
        public IItem? Item => item;

        private Event<string> onInReach = new Event<string>();

        public ItemEntity(IItem item) : base("Item")
        {
            SetItem(item);
        }

        public void SetItem(IItem item)
        {
            if (item == null) return;
            this.item = item;
        }

        public void Collect(string uid)
        {
            if (item == null) return;
            if (UIManager.Instance.CurrentElement.Key.StartsWith("PS"))
            {
                Inventory? inventory = (UIManager.Instance.CurrentElement as ProceduralScene)?.mapData?.character.Inventory;
                if (inventory != null)
                {
                    if (inventory.Contains(item.Id))
                    {
                        inventory.GetItem(item.Id)?.Count += item.Count;
                    }
                    else
                    {
                        inventory.AddItem(item);
                    }
                    Kill(uid);
                }
            }
        }

        private bool CheckReach(string uid, Character player)
        {
            if (this[uid] == null) return false;
            float d = Mathf.Floor(Vector2.Distance(this[uid], player.Position));
            return Mathf.Approximately(d, 1, float.Epsilon);
        }

        public void IsInReach(string uid, Character player)
        {
            if (CheckReach(uid, player))
            {
                onInReach.Invoke(uid);
            }
        }

        public void Initialize(Action<string> interactRender, Action render)
        {
            Initialize(render);
            onInReach.AddListener(interactRender);
        }
    }
}
