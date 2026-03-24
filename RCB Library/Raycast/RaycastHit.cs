using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RCBLibrary.SceneManagement;

namespace RCBLibrary.Raycast
{
    public class RaycastHit
    {
        public static bool NoBlock(int x, int y, PSC psc) //for mountains
        {
            bool NoBlock = true;
            
            if (psc.BlockedPoints.Contains(new Axis.Point() { X = x, Y = y }))
            {
                NoBlock = false;
            }

            return NoBlock;
        }
    }
}
