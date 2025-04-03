using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;

namespace TESTING
{
    public class TestDialogueFiles : MonoBehaviour
    {
        [SerializeField] private TextAsset file;
        // Start is called before the first frame update
        void Start()
        {
            // string line = "Speaker \"Dialogue \\\"Goes\\\" Here\" Command(arguments here)";
            // DialogueParser.Parse(line);
            StartConversation();
        }

        void StartConversation()
        {
            List<string> lines = FileManager.ReadTextAsset(file);

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                // Debug.Log($"Segmenting line '{line}'"); 
                DIALOGUE_LINE dl = DialogueParser.Parse(line);

                // int i = 0;
                // foreach (DL_DIALOGUE_DATA.DIALOGUE_SEGMENT segment in dlLine.dialogue.segments)
                // {
                //     Debug.Log($"Segment [{i++}] = '{segment.dialogue}' [signal={segment.startSignal.ToString()}{(segment.signalDelay > 0 ? $" {segment.signalDelay}" : "")}]");
                // }
                // if (dl.hasSpeaker)
                // {

                //     Debug.Log($"{dl.speaker.name} as [{(dl.speaker.castName != string.Empty ? dl.speaker.castName : dl.speaker.name)}] at {dl.speaker.castPosition}");

                //     foreach ((int l, string ex) in dl.speaker.CastExpressions)
                //     {
                //         Debug.Log($"[Layer[{l}] = '{ex}']");
                //     }
                // }
                // if (dl.hasCommands)
                // {

                //     for (int i = 0; i < dl.commandData.commands.Count; i++)
                //     {
                //         DL_COMMAND_DATA.Command command = dl.commandData.commands[i];
                //         Debug.Log($"Command [{i}] '{command.name}' has arguments [{string.Join(", ", command.arguments)}]");
                //     }
                // }
            }

            DialogueSystem.instance.Say(lines);
        }
    }

}