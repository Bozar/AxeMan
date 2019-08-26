using System.IO;
using System.Xml.Linq;
using UnityEngine;

namespace AxeMan.GameSystem.SaveLoadGameFile
{
    public interface ISaveLoadXML
    {
        XElement Load(string file, string directory);

        void Save(XElement xElement, string file, string directory);
    }

    public class SaveLoadXML : MonoBehaviour, ISaveLoadXML
    {
        public XElement Load(string file, string directory)
        {
            string path = Path.Combine(directory, file);

            if (File.Exists(path))
            {
                return XElement.Load(path);
            }
            throw new FileNotFoundException();
        }

        public void Save(XElement xElement, string file, string directory)
        {
            string path = Path.Combine(directory, file);
            xElement.Save(path);
        }

        private void Start()
        {
            string file = "setting.xml";
            string directory = "Build";

            XElement xElement = Load(file, directory);
            string data = xElement.Element("Language").Element("User").Value;
            Debug.Log(data);

            xElement.Element("Language").Element("User").SetValue("Russian");
            Save(xElement, file, directory);
        }
    }
}
