using RCBLibrary.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary.Input.Requests
{
    public class CharacterRequest : InputRequest
    {
        // Request for a character class. Inlcuding info such as name, etc.
        // Request for an action like move etc is an ActionRequest
        public Character? Value = null;
        public CharacterRequest(Character? character = null, string info = "") : base(INPUT_TYPE.CHARACTER, info)
        {
            if (character != null) Value = character;
        }

        public void Reply(Character input)
        {
            if (input is Character c)
            {
                Value = c;
                Reply();
            }
        }
    }
}
