using UnityEngine;

namespace SombraStudios.Shared.Utility.TimeScale
{
    public class TimeScaleClient : MonoBehaviour
    {
        [SerializeField] private AnimationCurveValueSO _curve;


        private void Start()
        {
            Invoke(nameof(UpdateTimeScale), 2f);
        }


        private void UpdateTimeScale()
        {
            TimeScaleManager.Instance.SetTimeScale(_curve);
        }
    }
}
