using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Input.Errors
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InvalidIntegerInputError<T> : Error
    {
        string erroredInput = "";

        public InvalidIntegerInputError(T input) : base(101, "Invalid Action Input", "The type or value did not fit the expected format.")
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
