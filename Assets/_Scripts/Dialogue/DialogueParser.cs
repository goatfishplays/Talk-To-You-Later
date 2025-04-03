using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Dialogue
{
    public class DialogueParser
    {
        // private const string commandRegexPattern = "\\w*[^\\s]\\(";
        private const string commandRegexPattern = @"[\w\[\]]*[^\s]\(";
        public static DIALOGUE_LINE Parse(string rawLine)
        {
            // rawLine = rawLine.Trim();
            // Debug.Log($"Parsing line - '{rawLine}'");

            (string speaker, string dialogue, string commands) = RipContent(rawLine);

            // Debug.Log($"Speaker: '{speaker}'\n Dialogue: '{dialogue}'\n Commands: '{commands}'"); 

            return new DIALOGUE_LINE(speaker, dialogue, commands);
        }

        private static (string, string, string) RipContent(string rawLine)
        {
            string speaker = "", dialogue = "", commands = "";


            // Dialogue Region
            int dialogueStart = -1;
            int dialogueEnd = -1;
            // bool characterEscaped = false;

            for (int i = 0; i < rawLine.Length; i++)
            {
                // if (characterEscaped)
                // {
                //     characterEscaped = false; 
                //     continue;
                // }
                if (rawLine[i] == '\"')
                {
                    if (dialogueStart == -1)
                    {
                        dialogueStart = i;
                        // Debug.Log(i); 
                    }
                    else
                    {
                        dialogueEnd = i;
                        break;
                        // Debug.Log(i);
                    }
                }
                else if (rawLine[i] == '\\')
                {
                    // characterEscaped = true;
                    i++;
                    continue;
                }
            }

            // Debug.Log(rawLine.Substring(dialogueStart + 1, dialogueEnd - dialogueStart - 1));


            // Command Region
            Regex commandRegex = new Regex(commandRegexPattern);
            MatchCollection matches = commandRegex.Matches(rawLine);
            int commandStart = -1;
            foreach (Match match in matches)
            {
                if (match.Index < dialogueStart || match.Index > dialogueEnd)
                {
                    commandStart = match.Index;
                    break;
                }
            }

            if (commandStart != -1 && (dialogueStart == -1 && dialogueEnd == -1))
            {
                return ("", "", rawLine.Trim());
            }

            // figure out if dialogue or string passed as arg to command
            if (dialogueStart != -1 && dialogueEnd != -1 && (commandStart == -1 || commandStart > dialogueEnd))
            {
                // have valid dialogue
                speaker = rawLine.Substring(0, dialogueStart).Trim();
                dialogue = rawLine.Substring(dialogueStart + 1, dialogueEnd - dialogueStart - 1).Replace("\\\"", "\"");
                if (commandStart != -1)
                {
                    commands = rawLine.Substring(commandStart).Trim();
                }
            }
            else if (commandStart != -1 && dialogueStart > commandStart)
            {
                commands = rawLine.Trim();
            }
            else
            {
                // speaker = rawLine;
                dialogue = rawLine;
            }

            return (speaker, dialogue, commands);
        }
    }
}