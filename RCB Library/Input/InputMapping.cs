using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Input
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="O">Output</typeparam>
    public interface IInputMapping<I,O>
    {
        public abstract O Map(I input);
    }
}
