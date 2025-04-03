using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dialogue;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;

namespace Characters
{
    public class CharacterManager : MonoBehaviour
    {
        public static CharacterManager instance { get; private set; }
        private Dictionary<string, Character> characters = new Dictionary<string, Character>();
        private CharacterConfigSO config => DialogueSystem.instance.config.characterConfigAsset;

        private const string CHARRACTER_CASTING_ID = " as ";
        private const string CHARACTER_NAME_ID = "<charname>";
        public string characterRootPathFormat => $"Characters/{CHARACTER_NAME_ID}";
        public string characterPrefabNameFormat => $"[{CHARACTER_NAME_ID}] - CharacterRoot";
        public string characterPrefabPathFormat => $"{characterRootPathFormat}/{characterPrefabNameFormat}";

        [SerializeField] private RectTransform _characterPannel = null;
        public RectTransform characterPannel => _characterPannel;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Debug.LogError("Two instances of Character Manager detected, deleting second");
                DestroyImmediate(this);
            }
        }

        public Character GetCharacter(string characterName, bool createIfDoesNotExist = false)
        {
            if (characters.ContainsKey(characterName.ToLower()))
            {
                return characters[characterName.ToLower()];
            }
            else if (createIfDoesNotExist)
            {
                return CreateCharacter(characterName);
            }

            return null;
        }

        public Character[] allCharacters => characters.Values.ToArray();

        public bool HasCharacter(string characterName) => characters.ContainsKey(characterName.ToLower());

        public CharacterConfigData GetCharacterConfig(string characterName)
        {
            return config.GetConfig(characterName);
        }


        public Character CreateCharacter(string characterName, bool revealAfterCreation = false)
        {
            if (characters.ContainsKey(characterName.ToLower()))
            {
                Debug.LogWarning($"A Character called '{characterName}' already exists. Did not create character.");
                return null;
            }

            CHARACTER_INFO info = GetCharacterInfo(characterName);

            Character character = CreateCharacterFromInfo(info);

            characters.Add(info.name.ToLower(), character);

            if (revealAfterCreation)
            {
                character.Show();
            }

            return character;
        }

        private CHARACTER_INFO GetCharacterInfo(string characterName)
        {
            CHARACTER_INFO result = new CHARACTER_INFO();

            string[] nameData = characterName.Split(CHARRACTER_CASTING_ID, System.StringSplitOptions.RemoveEmptyEntries);

            result.name = nameData[0];
            result.castingName = nameData.Length > 1 ? nameData[1] : result.name;

            result.config = config.GetConfig(result.castingName);

            result.prefab = GetPrefabForCharacter(result.castingName);

            result.rootCharacterFolder = FormatCharacterPath(characterRootPathFormat, result.castingName);
            // Debug.Log(result.prefab);

            return result;
        }

        private GameObject GetPrefabForCharacter(string characterName)
        {
            string prefabPath = FormatCharacterPath(characterPrefabPathFormat, characterName);
            return Resources.Load<GameObject>(prefabPath);
        }

        public string FormatCharacterPath(string path, string characterName) => path.Replace(CHARACTER_NAME_ID, characterName);

        private Character CreateCharacterFromInfo(CHARACTER_INFO info)
        {
            CharacterConfigData config = info.config;

            switch (config.characterType)
            {
                case Character.CharacterType.Text:
                    return new Character_Text(info.name, config);
                case Character.CharacterType.Sprite:
                case Character.CharacterType.SpriteSheet:
                    return new Character_Sprite(info.name, config, info.prefab, info.rootCharacterFolder);
                case Character.CharacterType.Live2D:
                    return new Character_Live2D(info.name, config, info.prefab, info.rootCharacterFolder);
                case Character.CharacterType.Model3D:
                    return new Character_Model3D(info.name, config, info.prefab, info.rootCharacterFolder);
            }
            return null;
        }

        public void SortCharacters()
        {
            List<Character> activeCharacters = characters.Values.Where(c => c.root.gameObject.activeInHierarchy && c.isVisible).ToList();
            List<Character> inactiveCharacters = characters.Values.Where(c => !(c.root.gameObject.activeInHierarchy && c.isVisible)).ToList();

            activeCharacters.Sort((a, b) => a.priority.CompareTo(b.priority));


            SortCharacters(activeCharacters.Concat(inactiveCharacters).ToList());
        }

        public void SortCharacters(List<Character> charactersSortingOrder)
        {
            int i = 0;
            foreach (Character character in charactersSortingOrder)
            {
                character.root.SetSiblingIndex(i++);
            }
        }

        /// <summary>
        /// Will push the characters in array to front of all chars
        /// Front will be first on array
        /// </summary> 
        /// <param name="characterNames"></param>
        public void SortCharacters(string[] characterNames)
        {
            List<Character> sortedCharacters = new List<Character>();

            sortedCharacters = characterNames
                .Select(name => GetCharacter(name))
                .Where(character => character != null)
                .ToList();

            // Debug.Log(sortedCharacters.Count);

            List<Character> remainingCharacters = characters.Values
                .Except(sortedCharacters)
                .OrderBy(character => character.priority)
                .ToList();

            sortedCharacters.Reverse();

            int startingPriority = remainingCharacters.Count > 0 ? remainingCharacters.Max(c => c.priority) : 0;
            for (int i = 0; i < sortedCharacters.Count; i++)
            {
                Character character = sortedCharacters[i];
                character.SetPriority(startingPriority + 1 + i, false);
                // Debug.Log($"{character.name} has priority {character.priority}"); 
            }



            // sortedCharacters.Concat(remainingCharacters);


            // foreach (Character character in remainingCharacters)
            // {
            //     Debug.Log($"{character.name}");
            // }

            SortCharacters(remainingCharacters.Concat(sortedCharacters).ToList());
        }

        private class CHARACTER_INFO
        {
            public string name = "";
            public string castingName = "";
            public string rootCharacterFolder = "";

            public CharacterConfigData config = null;

            public GameObject prefab;
        }
    }
}
