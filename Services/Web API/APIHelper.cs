using UnityEngine;
using System.Net;
using System.IO;

namespace SombraStudios.Services.WebAPI
{
    /// <summary>
    /// Example API Get call
    /// </summary>
    public static class APIHelper
    {
        public static Joke GetNewJoke()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.chucknorris.io/jokes/random");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());
            string json = reader.ReadToEnd();


            return JsonUtility.FromJson<Joke>(json);
        }
    }
}
