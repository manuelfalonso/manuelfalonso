using UnityEngine;

namespace SombraStudios.Shared.Animations.AnimatorParameterReference
{
    /// <summary>
    /// ScriptableObject that holds a reference to an Animator Controller parameter.
    /// </summary>
    [CreateAssetMenu(fileName = "NewAnimatorParameterReferenceSO",
        menuName = "Sombra Studios/Animations/AnimatorParameterReference")]
    public class AnimatorParameterReferenceSO : ScriptableObject
    {
#if UNITY_EDITOR
        /// <summary>
        /// The Animator Controller associated with this parameter reference.
        /// </summary>
        public UnityEditor.Animations.AnimatorController AnimatorController;
#endif
        /// <summary>
        /// The type of the Animator parameter (e.g., Float, Int, Bool, Trigger).
        /// </summary>
        public AnimatorControllerParameterType ParameterType;

        /// <summary>
        /// The name of the Animator parameter.
        /// </summary>
        [SerializeField] private string _parameterName;

        /// <summary>
        /// Gets the name of the Animator parameter.
        /// </summary>
        public string ParameterName { get => _parameterName; set => _parameterName = value; }

        /// <summary>
        /// Gets the hash of the Animator parameter name for optimized lookups.
        /// </summary>
        public int ParameterHash => Animator.StringToHash(_parameterName);
    }
}
