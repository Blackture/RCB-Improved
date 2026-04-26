using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Inventories
{
    public class Inventory
    {
        private List<IItem> inventory = new List<IItem>();

        public void AddItem(IItem item)
        {
            inventory.Add(item);
        }

        public void RemoveItem(IItem item)
        {
            inventory.Remove(item);
        }

        public void Clear()
        {
            inventory.Clear();
        }

        public bool Contains(IItem item)
        {
            return inventory.Contains(item);
        }

        public bool Contains(string itemId)
        {
            return inventory.Exists(i => i.Id == itemId);
        }
        
        public IItem? GetItem(string itemId)
        {
            return inventory.Find(i => i.Id == itemId);
        }
    }
}
