using Characters;
using TMPro;
using UnityEngine;

namespace Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue System Config", menuName = "Dialogue System/Dialogue Config Asset")]
    public class DialogueSystemConfigurationSO : ScriptableObject
    {
        public CharacterConfigSO characterConfigAsset;
        public Color defaultTextColor = Color.white;
        public TMP_FontAsset defaultFont;
    }
}
