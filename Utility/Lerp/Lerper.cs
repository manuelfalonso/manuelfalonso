using UnityEngine;

namespace SombraStudios.Shared.Utility.Lerp
{
    public class Lerper : MonoBehaviour
    {
        [SerializeField] private float _value;

        private void Start()
        {
            LerpValueData data = new LerpValueData
            {
                Start = 0,
                Target = 1,
                Duration = 1,
                Curve = AnimationCurve.EaseInOut(0, 0, 1, 1)
            };

            LerpTool.LerpValue(ref _value, data);
        }
    }
}
