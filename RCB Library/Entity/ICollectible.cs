using RCBLibrary.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Entity
{
    public interface ICollectible
    {
        public void Collect(string uid);
        public void IsInReach(string uid, Character player);
    }
}
