using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Characters
{

    public class Character_Text : Character
    {
        public Character_Text(string name, CharacterConfigData config) : base(name, config, null)
        {
            Debug.Log($"Created Text Character: '{name}'");
        }
    }
}