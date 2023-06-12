using System.Text;
using UnityEngine;

namespace SombraStudios.Utility
{
    /// <summary>
    /// Util class to return a string with the selected Encoding
    /// </summary>
    public class StringEncodingUtil : MonoBehaviour
    {
        private static StringEncodingUtil instance;
        public static StringEncodingUtil Instance { get => instance; }

        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                // The GameObject will persist across multiple scenes.
                DontDestroyOnLoad(gameObject);
            }
        }

        void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }

        public string Encode(string stringToEncode, Encoding encoding)
        {
            // Convert the string into a byte array.
            byte[] bytes = Encoding.Unicode.GetBytes(stringToEncode);

            // Perform the conversion from one encoding to the other.
            byte[] encodingBytes = Encoding.Convert(Encoding.Unicode, encoding, bytes);

            // Convert the new byte[] into a char[] and then into a string.
            char[] utfChars =
                new char[encoding.GetCharCount(encodingBytes, 0, encodingBytes.Length)];
            encoding.GetChars(encodingBytes, 0, encodingBytes.Length, utfChars, 0);
            string stringEncoded = new string(utfChars);

            return stringEncoded;
        }
    }
}
