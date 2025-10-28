using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SombraStudios.Shared.Systems.Events
{
    [CreateAssetMenu(
        fileName = "WaitSceneAction", 
        menuName = "Sombra Studios/Systems/Events/Wait Scene Action", 
        order = 12)]
    public class WaitSceneActionSO : EventActionSO
    {
        [Header("Data")]
        [SerializeField] private int _sceneIndex;

        public override void PauseAction(bool pause) { }

        public override IEnumerator StartAction()
        {
            if (!_active)
                yield break;

            yield return new WaitUntil(() => SceneManager.GetActiveScene().buildIndex == _sceneIndex);

            IsCompleted = true;
        }
    }
}
