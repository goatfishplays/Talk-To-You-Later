using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Commands
{
    public abstract class CMD_DatabaseExtension
    {
        /// <summary>
        /// Note all commands must be static to enable the automatic adding to the database
        /// Still can have static commands to access non static items(use singletons or GameObject.Find("NAME") or smthn)
        /// </summary> 
        /// <param name="database"></param>
        public static void Extend(CommandDatabase database) { }

        public static CommandParameters ConvertDataToParameters(string[] data, int startingIndex = 0) => new CommandParameters(data, startingIndex);
    }
}