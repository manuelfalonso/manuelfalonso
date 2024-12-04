using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SombraStudios.Shared.TutorialSystem
{
    [CreateAssetMenu(fileName = "New Tutorial Action", menuName = "Sombra Studios/Tutorial/Wait Scene Action", order = 2)]
    public class WaitSceneAction : TutorialActionSO
    {
        [Header("Data")]
        [SerializeField]
        private int _sceneIndex;


        public override IEnumerator ExecuteAction()
        {
            if (!_active)
                yield break;

            yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == _sceneIndex);

            IsCompleted = true;
        }
    }
}
