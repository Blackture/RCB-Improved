using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Input.Errors
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InvalidActionMappingError<T> : Error
    {
        string erroredInput = "";

        public InvalidActionMappingError(T input) : base(100, "Invalid Action Mapping", "The type or value did not fit the expected format.")
        {
            erroredInput = input?.ToString() ?? "Unreadable Input";
        }

        public override void Send()
        {
            message += $"\nReading: \"${erroredInput}\"";
            base.Send();
        }
    }
}
