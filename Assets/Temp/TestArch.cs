using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Dialogue;

namespace TESTING
{
    public class TestArch : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        string[] lines = new string[5] {
            "Welcome to 2025 and a happy newyear to all the fools who thought that was enough",
            "We have flying cars and We have dying stars",
            "This is a line of text about a line of text",
            "We have everything on Earth but we still greed for more",
            "And all the way to Mars"
        };


        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            // architect.buildMethod = TextArchitect.BuildMethod.instant;
            architect.buildMethod = TextArchitect.BuildMethod.fade;
            architect.speed = .5f;
        }

        // Update is called once per frame
        void Update()
        {

            if (bm != architect.buildMethod)
            {
                architect.buildMethod = bm;
                architect.Stop();
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                architect.Stop();
            }

            string longLine = "This is a line about a line about a line about a line, in the first like it was just a line, but after the second it was a line about a line, and the third it was a line about a line about a line, now we are here...";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (architect.isBuilding)
                {
                    if (!architect.hurryUp)
                    {
                        architect.hurryUp = true;
                    }
                    else
                    {
                        architect.ForceComplete();
                    }
                }
                else
                {
                    // architect.Build(lines[Random.Range(0, lines.Length)]);
                    architect.Build(longLine);
                }
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Build(longLine);
                // architect.Append(lines[Random.Range(0, lines.Length)]);
            }
        }
    }

}
