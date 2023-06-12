using System.Globalization;
using System.IO;
using UnityEngine;

namespace SombraStudios.Utility
{
    /// <summary>
    /// Class that creates a text file at the root proyect folder with the string lines
    /// converted from Hex code Colors into RGBA ready to paste them in a Swatch Color file.
    /// </summary>
    public class HexToRGBA : MonoBehaviour
    {
        [SerializeField] Color sampleColor;

        [Header("Input\nBegins with #, one code by line.")]
        [SerializeField] [Multiline] string multipleLineColors;
        [Space(10)]
        [Header("Output\nFile found at root proyect folder.")]
        [SerializeField] string fileName = "ColorListFile";

        string fileFormat = ".txt";
        string colorResult;

        // Start is called before the first frame update
        void Start()
        {
            colorResult = string.Empty;
            ConvertMultipleHexToRGBA();
            SaveTxtFile();
            Debug.Log("Create a new Swatches by clicking the Sample Color -> 3 dots ->" + 
                " 'Create New Library...'. Then click 'Reveal Current Library Location' " +
                "at the same previous location and edit the swatch just created. " + 
                "At the botton just copy the content created by the script. NOTE: " +
                "At line 15 erase the []. Should be: 'm_Presets:'");
        }

        private void ConvertMultipleHexToRGBA()
        {
            using (StringReader reader = new StringReader(multipleLineColors))
            {
                string line = string.Empty;
                do
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        Color rgbaColor;

                        if (ColorUtility.TryParseHtmlString(line, out rgbaColor))
                        {
                            colorResult += ColorToString(rgbaColor);
                        }
                        else
                        {
                            Debug.LogError("Failed to convert");
                        }
                    }

                } while (line != null);
            }
        }

        private void SaveTxtFile()
        {
            File.WriteAllText(fileName + fileFormat, colorResult);
        }

        string ColorToString(Color color)
        {
            return "  - m_Name: \r    m_Color: {r: " + color.r.ToString(CultureInfo.InvariantCulture) 
                + ", g: " + color.g.ToString(CultureInfo.InvariantCulture) 
                + ", b: " + color.b.ToString(CultureInfo.InvariantCulture)
                + ", a: " + color.a.ToString(CultureInfo.InvariantCulture) + "}\r";
        }
    }
}
