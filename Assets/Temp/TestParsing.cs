using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;

namespace TESTING
{
    public class TestParsing : MonoBehaviour
    {
        // [SerializeField] private TextAsset file;
        // Start is called before the first frame update
        void Start()
        {
            // string line = "Speaker \"Dialogue \\\"Goes\\\" Here\" Command(arguments here)";
            // DialogueParser.Parse(line);
            SendFileToParse();
        }

        void SendFileToParse()
        {
            List<string> lines = FileManager.ReadTextAsset("testFile");
            foreach (string line in lines)
            {
                if (line == string.Empty)
                {
                    continue;
                }
                DIALOGUE_LINE dl = DialogueParser.Parse(line);
            }
        }
    }

}
