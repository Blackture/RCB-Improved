using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Inventories
{
    public interface IItem
    {
        string Name { get; }
        string Id { get; }
        int Count { get; set; }
    }
}
