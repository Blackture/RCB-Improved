using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;
using RCBLibrary.Events;
using RCBLibrary.Input.Mappings;

namespace RCBLibrary.Input
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="I">is the input type. Not the mappping.</typeparam>
    /// <typeparam name="M">is the input mapping. Not the type.</typeparam>
    public static class Input<I,O>
    {         
        public static O In<M>(M mapping, I input) where M : IInputMapping<I,O>
        {
            return mapping.Map(input);
        }
    }
}
