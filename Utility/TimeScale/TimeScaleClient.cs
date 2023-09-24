using SombraStudios.Utility;
using SombraStudios.Utility.TimeScale;
using UnityEngine;

namespace SombraStudios
{
    public class TimeScaleClient : MonoBehaviour
    {
        [SerializeField] private AnimationCurveValue _curve;


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
