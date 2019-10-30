using AxeMan.GameSystem.GameDataTag;
using AxeMan.GameSystem.InitializeGameWorld;
using AxeMan.GameSystem.SaveLoadGameFile;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem.GameDataHub
{
    public interface IColorData
    {
        string GetHexColor(ColorTag colorTag);

        Color GetRGBAColor(ColorTag colorTag);
    }

    public class ColorData : MonoBehaviour, IColorData
    {
        private string alphaTag;
        private string blueTag;
        private string greenTag;
        private Dictionary<ColorTag, string> hexColor;
        private string hexTag;
        private string redTag;
        private Dictionary<ColorTag, Color> rgbaColor;
        private string rgbaTag;
        private XElement xmlFile;

        public string GetHexColor(ColorTag colorTag)
        {
            if (!hexColor.ContainsKey(colorTag))
            {
                string color
                    = (string)xmlFile
                    .Element(colorTag.ToString())
                    .Element(hexTag);

                hexColor[colorTag] = "#" + color;
            }
            return hexColor[colorTag];
        }

        public Color GetRGBAColor(ColorTag colorTag)
        {
            if (!rgbaColor.ContainsKey(colorTag))
            {
                XElement e
                    = xmlFile
                    .Element(colorTag.ToString())
                    .Element(rgbaTag);

                byte r = (byte)(int)e.Element(redTag);
                byte g = (byte)(int)e.Element(greenTag);
                byte b = (byte)(int)e.Element(blueTag);
                byte a = (byte)(int)e.Element(alphaTag);

                rgbaColor[colorTag] = new Color32(r, g, b, a);
            }
            return rgbaColor[colorTag];
        }

        private void Awake()
        {
            rgbaColor = new Dictionary<ColorTag, Color>();
            hexColor = new Dictionary<ColorTag, string>();

            hexTag = "HexColor";
            rgbaTag = "RGBAColor";
            redTag = "Red";
            greenTag = "Green";
            blueTag = "Blue";
            alphaTag = "Alpha";
        }

        private void ColorData_LoadingGameData(object sender, EventArgs e)
        {
            string file = "colorScheme.xml";
            string directory = "Data";
            xmlFile = GetComponent<SaveLoadXML>().Load(file, directory);
        }

        private void Start()
        {
            GetComponent<InitializeStartScreen>().LoadingGameData
                += ColorData_LoadingGameData;
        }
    }
}
