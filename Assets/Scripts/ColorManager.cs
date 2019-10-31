using AxeMan.GameSystem.GameDataHub;
using AxeMan.GameSystem.GameDataTag;
using UnityEngine;

namespace AxeMan.GameSystem
{
    public interface IColorManager
    {
        void SetColor(SpriteRenderer renderer, ColorTag colorTag);

        void SetColor(string text, ColorTag colorTag, out string colorfulText);
    }

    public class ColorManager : MonoBehaviour, IColorManager
    {
        public void SetColor(SpriteRenderer renderer, ColorTag colorTag)
        {
            renderer.color = GetComponent<ColorData>().GetRGBAColor(colorTag);
        }

        public void SetColor(string text, ColorTag colorTag,
            out string colorfulText)
        {
            string hex = GetComponent<ColorData>().GetHexColor(colorTag);
            colorfulText = $"<color={hex}>{text}</color>";
        }
    }
}
