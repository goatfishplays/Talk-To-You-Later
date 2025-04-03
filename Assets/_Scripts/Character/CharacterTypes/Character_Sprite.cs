using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Characters
{
    public class Character_Sprite : Character
    {
        private const string SPRITE_RENDERERS_PARENT_NAME = "Renderers";
        private const string SPRITESHEET_DEFAULT_SHEETNAME = "Default";
        private const char SPRITESHEET_TEX_SPRITE_DELIMITER = '/';
        private CanvasGroup rootCG => root.GetComponent<CanvasGroup>();
        private const float FADE_SPEED = 3f;
        public override bool isVisible
        {
            get { return isRevealing || rootCG.alpha == 1; }
            set { rootCG.alpha = value ? 1 : 0; }
        }

        public List<CharacterSpriteLayer> layers = new List<CharacterSpriteLayer>();
        string artAssetsDirectory = "";

        public Character_Sprite(string name, CharacterConfigData config, GameObject prefab, string rootAssetsFolder) : base(name, config, prefab)
        {
            rootCG.alpha = ENABLE_ON_START ? 1 : 0;
            artAssetsDirectory = rootAssetsFolder + "/Images";
            GetLayers();
            // Show(); 
            Debug.Log($"Created Sprite Character: '{name}'");
        }

        private void GetLayers()
        {
            Transform rendererRoot = animator.transform.Find(SPRITE_RENDERERS_PARENT_NAME);

            if (rendererRoot == null)
            {
                return;
            }

            for (int i = 0; i < rendererRoot.transform.childCount; i++)
            {
                Transform child = rendererRoot.transform.GetChild(i);

                Image rendererImage = child.GetComponentInChildren<Image>();

                if (rendererImage != null)
                {
                    CharacterSpriteLayer layer = new CharacterSpriteLayer(rendererImage, i);
                    layers.Add(layer);
                    // child.name = $"Layer: {i}"; 
                }
            }
        }

        public void SetSprite(Sprite sprite, int layer = 0)
        {
            layers[layer].SetSprite(sprite);
        }

        public Sprite GetSprite(string spriteName)
        {
            if (config.characterType == CharacterType.SpriteSheet)
            {
                string[] data = spriteName.Split(SPRITESHEET_TEX_SPRITE_DELIMITER);
                string textureName;

                if (data.Length == 2)
                {
                    textureName = data[0];
                    spriteName = data[1];
                }
                else
                {
                    textureName = SPRITESHEET_DEFAULT_SHEETNAME;
                }

                Sprite[] spriteArray = Resources.LoadAll<Sprite>($"{artAssetsDirectory}/{textureName}");
                if (spriteArray.Length == 0)
                {
                    Debug.LogWarning($"Character '{name}' does not have a default art asset called '{textureName}'");
                }

                return Array.Find(spriteArray, sprite => sprite.name == spriteName);
            }
            else
            {
                return Resources.Load<Sprite>($"{artAssetsDirectory}/{spriteName}");
            }
        }

        public Coroutine TransitionSprite(Sprite sprite, int layer = 0, float speed = 1)
        {
            CharacterSpriteLayer spriteLayer = layers[layer];

            return spriteLayer.TransitionSprite(sprite, speed);
        }

        public override IEnumerator ShowingOrHiding(bool show, float speedMultiplier = 1f)
        {
            float targetAlpha = show ? 1f : 0;
            CanvasGroup self = rootCG;

            while (self.alpha != targetAlpha)
            {
                self.alpha = Mathf.MoveTowards(self.alpha, targetAlpha, speedMultiplier * FADE_SPEED * Time.deltaTime);
                yield return null;
            }

            co_hiding = null;
            co_revealing = null;
        }

        public override void SetColor(Color color)
        {
            base.SetColor(color);

            color = displayColor;

            foreach (CharacterSpriteLayer layer in layers)
            {
                layer.StopChangingColor();
                layer.SetColor(color);
            }
        }

        public override IEnumerator ChangingColor(Color color, float speed)
        {
            foreach (CharacterSpriteLayer layer in layers)
            {
                layer.TransitionColor(color, speed);
            }
            yield return null;

            while (layers.Any(l => l.isChangingColor))
            {
                yield return null;
            }

            co_changingColor = null;
        }

        public override IEnumerator Highlighting(bool highlight, float speedMultiplier, bool immediate = false)
        {
            Color targetColor = displayColor;

            foreach (CharacterSpriteLayer layer in layers)
            {
                if (immediate)
                {
                    layer.SetColor(displayColor);
                }
                else
                {
                    layer.TransitionColor(targetColor, speedMultiplier);
                }
            }

            yield return null;

            while (layers.Any(l => l.isChangingColor))
            {
                yield return null;
            }

            co_highlighting = null;
        }

        public override IEnumerator FaceDirection(bool faceLeft, float speedMultiplier, bool immediate)
        {
            foreach (CharacterSpriteLayer layer in layers)
            {
                if (faceLeft)
                {
                    layer.FaceLeft(speedMultiplier, immediate);
                }
                else
                {
                    layer.FaceRight(speedMultiplier, immediate);
                }
            }

            yield return null;

            while (layers.Any(l => l.isFlipping))
            {
                yield return null;
            }

            co_flipping = null;
        }

        public override void OnReceiveCastingExpression(int layer, string expression)
        {
            Sprite sprite = GetSprite(expression);

            if (sprite == null)
            {
                Debug.LogWarning($"Sprite '{expression}' could not be found for character '{name}'");
                return;
            }

            TransitionSprite(sprite, layer);
        }
    }
}