using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;
using Dialogue;
using TMPro;

namespace TESTING
{
    public class TestCharacters : MonoBehaviour
    {
        public TMP_FontAsset tempfont;
        // Start is called before the first frame update
        void Start()
        {
            // Character character = CharacterManager.instance.CreateCharacter("Testerman");
            // Character character4 = CharacterManager.instance.CreateCharacter("Testerwoman");
            // Character character3 = CharacterManager.instance.CreateCharacter("Testerdog");

            StartCoroutine(Test());
            // Character fs2 = CharacterManager.instance.CreateCharacter("Female Student 2");
        }
        private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);
        IEnumerator Test()
        {
            Character_Sprite guard1 = CreateCharacter("Guard1 as Generic") as Character_Sprite;
            Character_Sprite guard2 = CreateCharacter("Guard2 as Generic") as Character_Sprite;
            Character_Sprite Raelin = CreateCharacter("Raelin") as Character_Sprite;
            Character_Sprite FS2 = CreateCharacter("Female Student 2") as Character_Sprite;
            guard2.SetColor(Color.red);


            // guard2.isVisible = false;

            guard1.SetPosition(new Vector2(0.2f, 0f));
            guard2.SetPosition(new Vector2(0.3f, 0f));
            Raelin.SetPosition(new Vector2(0.4f, 0f));


            // yield return new WaitForSeconds(1);
            guard2.SetPriority(999);
            // yield return new WaitForSeconds(1);
            // guard1.SetPriority(1);
            // yield return new WaitForSeconds(1);
            // guard2.SetPriority(3); 

            Raelin.Animate("Hop");
            yield return new WaitForSeconds(1);
            Raelin.Animate("Shiver", true);
            CharacterManager.instance.SortCharacters(new string[] { "Guard1", "Raelin" });
            yield return new WaitForSeconds(1);
            guard1.Animate("Shiver", true);
            yield return new WaitForSeconds(0.1f);
            guard2.Animate("Shiver", true);
            yield return new WaitForSeconds(0.1f);
            FS2.Animate("Shiver", true);
            Raelin.Animate("Hop");
            CharacterManager.instance.SortCharacters();
            yield return new WaitForSeconds(1);
            CharacterManager.instance.SortCharacters(new string[] { "Guard2", "Female Student 2", "Guard1", "Raelin" });


            // Sprite FS2Sprite = FS2.GetSprite("female student 2 - happy");

            // Sprite RaelinSprite = Raelin.GetSprite("3"); 

            // guard1.SetPosition(new Vector2(0.5f, 0.5f)); 
            // guard2.SetPosition(new Vector2(0, 0.1f));
            // Raelin.SetPosition(new Vector2(2f, 1f));
            // Raelin.SetPosition(new Vector2(1f, 1f));
            // FS2.SetPosition(new Vector2(1, 1f));
            // yield return new WaitForSeconds(1);
            // yield return Raelin.Flip();
            // yield return new WaitForSeconds(1);
            // yield return Raelin.Flip(0.5f);
            // yield return new WaitForSeconds(1);
            // yield return Raelin.Flip(immediate: true);
            // yield return new WaitForSeconds(1);
            // yield return Raelin.FaceRight();
            // yield return new WaitForSeconds(1);
            // yield return Raelin.FaceLeft();
            // yield return new WaitForSeconds(1);
            // yield return Raelin.FaceRight(.3f);
            // yield return new WaitForSeconds(1);
            // yield return Raelin.FaceLeft(.3f);
            // yield return new WaitForSeconds(1);
            // yield return Raelin.FaceRight(.3f, true);
            // yield return new WaitForSeconds(1);
            // yield return Raelin.FaceLeft(.3f, true);
            // yield return new WaitForSeconds(1);

            // yield return new WaitForSeconds(1);

            // yield return Raelin.UnHighlight();

            // yield return new WaitForSeconds(1);

            // yield return Raelin.TransitionColor(Color.red);

            // yield return new WaitForSeconds(1);

            // yield return Raelin.Highlight();

            // yield return new WaitForSeconds(1);

            // yield return Raelin.TransitionColor(Color.white);

            // yield return Raelin.TransitionColor(Color.red);
            // yield return Raelin.TransitionColor(Color.white);
            // yield return Raelin.TransitionColor(Color.red, 0.5f);
            // yield return Raelin.TransitionColor(Color.white, 0.5f);

            yield return null;

            // guard1.Show(); 
            // yield return new WaitForSeconds(1);

            // Sprite face = Raelin.GetSprite("A_Blush");
            // Sprite body = Raelin.GetSprite("B2");
            // Raelin.TransitionSprite(body);
            // // Raelin.SetColor(Color.red);
            // Raelin.layers[1].SetColor(Color.red);
            // yield return Raelin.TransitionSprite(face, 1, .3f);

            // yield return new WaitForSeconds(1);

            // face = Raelin.GetSprite("B_Scold");
            // body = Raelin.GetSprite("A2");
            // Raelin.TransitionSprite(body);
            // Raelin.TransitionSprite(face, 1);


            // guard1.Show();
            // yield return guard2.Show();
            // Raelin.Show();
            // FS2.Show();

            // yield return new WaitForSeconds(1f);
            // FS2.SetSprite(FS2Sprite);

            // yield return Raelin.MoveToPosition(new Vector2(0.25f, 0.75f));
            // yield return Raelin.MoveToPosition(new Vector2(0.75f, 0.25f));
            // yield return Raelin.MoveToPosition(new Vector2(0.25f, 0.75f), smooth: true);
            // yield return Raelin.MoveToPosition(new Vector2(0.75f, 0.25f), smooth: true);

            // guard2.SetDialogueColor(Color.cyan);
            // FS2.SetDialogueColor(Color.red);

            // yield return guard1.Say("Pain");
            // yield return guard2.Say("Pain2");
            // yield return FS2.Say("Pain3");


            yield return null;

            //     yield return new WaitForSeconds(1f);
            //     Character Raelin = CharacterManager.instance.CreateCharacter("Raelin");

            //     yield return new WaitForSeconds(1f);

            //     yield return Raelin.Hide();

            //     yield return new WaitForSeconds(0.5f);

            //     yield return Raelin.Show();

            //     yield return Raelin.Say("Hello");
            // Character character2 = CharacterManager.instance.CreateCharacter("Testerwoman");
            // Character dog = CharacterManager.instance.CreateCharacter("Testerdog");
            // Character frog = CharacterManager.instance.CreateCharacter("Testerfrog");

            // List<string> lines = new List<string>() {
            //     "I am no narrator Narrator \"Hi!\"",
            //     "This is a line.",
            //     "And another.",
            //     "And a last one."
            // };

            // yield return dog.Say(lines);

            // lines = new List<string>() {
            //     "I speak for the trees",
            //     "This is another line.",
            //     "And another.",
            //     "And a another last one."
            // };

            // yield return character2.Say(lines);

            // character2.SetNameColor(Color.red);
            // character2.SetDialogueColor(Color.green);
            // character2.SetNameFont(tempfont);
            // character2.SetDialogueFont(tempfont);

            // yield return character2.Say(lines);

            // character2.ResetConfigurationData();

            // yield return character2.Say(lines);

            // lines = new List<string>() {
            //     "I am a frog",
            //     "Just a frog"
            // };

            // yield return frog.Say(lines);

            // Debug.Log("Finished");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}
