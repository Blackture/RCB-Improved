using RCBLibrary.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Entity
{
    public interface IEntity
    {
        string Id { get; }
        string Name { get; }

        Vector2 this[string uid] { get; }

        List<string> Keys { get; }

        bool Kill(string uid);

        bool KillAll();

        string Instantiate(Vector2 position);

        void Initialize(Action render);
    }
}
