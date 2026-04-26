using RCBLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Inventories
{
    public class Item : IItem
    {
        private string id;
        private string name;
        private int count;

        public string Name => name;
        public string Id => id;

        public int Count
        {
            get { return count; }
            set { count = value > 0 ? value : 0; }
        }

        public Item(string name)
        {
            this.name = name;
            id = name.SanitizeId();
            if (string.IsNullOrEmpty(name.Trim()) || string.IsNullOrEmpty(id))
            {
                new InvalidItemNameError(name).Send();
            }
        }
    }
}
