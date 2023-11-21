using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace SombraStudios.Shared.Utility
{
    /// <summary>
    /// Script for getting image from web/local location
    /// </summary>
    public class GetRemoteTexture : MonoBehaviour
    {
        [SerializeField] string _imageUrl;
        [SerializeField] Material _material;
        Texture2D _texture;

        // Start is called before the first frame update
        async void Start()
        {
            _texture = await GetTexture(_imageUrl);
            _material.mainTexture = _texture;
        }

        void OnDestroy() => Dispose();
        public void Dispose() => Object.Destroy(_texture);// memory released, leak otherwise

        public static async Task<Texture2D> GetTexture(string url)
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
            {
                // begin request:
                var asyncOp = www.SendWebRequest();

                // await until it's done: 
                while (asyncOp.isDone == false)
                    await Task.Delay(1000 / 30);//30 hertz

                // read results:
                if (www.result != UnityWebRequest.Result.Success)
                {
                    // log error:
    #if DEBUG
                    Debug.Log($"{www.error}, URL:{www.url}");
    #endif

                    // nothing to return on error:
                    return null;
                }
                else
                {
                    // return valid results:
                    return DownloadHandlerTexture.GetContent(www);
                }
            }
        }
    }
}
