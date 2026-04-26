using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Inventories
{
    public static class ItemManager
    {
        private static List<IItem> items = new List<IItem>()
        {
            new Item("Pebble")
        };

        public static void RegisterItem(IItem item)
        {
            if (item == null) return;
            if (items.Exists(i => i.Id == item.Id))
            {
                new ItemAlreadyExistsError(item.Name).Send();
                return;
            }
            items.Add(item);
        }

        public static void RegisterItems(params IItem[] items)
        {
            foreach (IItem item in items)
            {
                RegisterItem(item);
            }
        }

        public static bool IsRegistered(string id)
        {
            return items.Exists(x => x.Id == id);
        }

        public static bool IsRegistered(IItem item)
        {
            return IsRegistered(item.Id);
        }
    }
}
