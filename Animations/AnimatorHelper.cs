using System.Collections;
using UnityEngine;

namespace SombraStudios.Shared.Animations
{
    /// <summary>
    /// Helper class for working with Unity's Animator component.
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class AnimatorHelper : MonoBehaviour
    {
        [SerializeField] private Animator _myAnimator;
        [SerializeField] private int _mainLayer;

        /// <summary>
        /// Gets the Animator component attached to the game object.
        /// </summary>
        public Animator MyAnimator => _myAnimator;
        /// <summary>
        /// Gets the position delta of the Animator.
        /// </summary>
        public Vector3 DeltaPosition => MyAnimator.deltaPosition;

        private AnimationClip[] _animationClips;
        private bool _canUnpause = true;
        private AnimatorParamData[] _animatorParameters;


        #region Unity Methods
        public void Awake()
        {
            if (!TryGetComponent(out _myAnimator))
            {
                Debug.LogError("Could not get Animator component");
            }
        }

        private void Start()
        {
            PopulateClips();
            GetAllAnimatorParameters();
        }
        #endregion


        #region Getter Methods
        /// <summary>
        /// Gets the value of a float parameter in the Animator.
        /// </summary>
        /// <param name="id">The hash ID of the parameter.</param>
        /// <returns>The value of the float parameter.</returns>
        public float GetFloatParameter(int id)
        {
            return MyAnimator.GetFloat(id);
        }

        /// <summary>
        /// Gets the value of a float parameter in the Animator.
        /// </summary>
        /// <param name="id">The name of the parameter.</param>
        /// <returns>The value of the float parameter.</returns>
        public float GetFloatParameter(string id)
        {
            return GetFloatParameter(Animator.StringToHash(id));
        }

        /// <summary>
        /// Gets the value of an integer parameter in the Animator.
        /// </summary>
        /// <param name="id">The hash ID of the parameter.</param>
        /// <returns>The value of the integer parameter.</returns>
        public int GetIntegerParameter(int id)
        {
            return MyAnimator.GetInteger(id);
        }

        /// <summary>
        /// Gets the value of an integer parameter in the Animator.
        /// </summary>
        /// <param name="id">The name of the parameter.</param>
        /// <returns>The value of the integer parameter.</returns>
        public int GetIntegerParameter(string id)
        {
            return GetIntegerParameter(Animator.StringToHash(id));
        }

        /// <summary>
        /// Gets the value of a boolean parameter in the Animator.
        /// </summary>
        /// <param name="id">The hash ID of the parameter.</param>
        /// <returns>The value of the boolean parameter.</returns>
        public bool GetBoolParameter(int id)
        {
            return MyAnimator.GetBool(id);
        }

        /// <summary>
        /// Gets the value of a boolean parameter in the Animator.
        /// </summary>
        /// <param name="id">The name of the parameter.</param>
        /// <returns>The value of the boolean parameter.</returns>
        public bool GetBoolParameter(string id)
        {
            return GetBoolParameter(Animator.StringToHash(id));
        }
        #endregion


        #region Setter Methods
        /// <summary>
        /// Sets the value of a float parameter in the Animator.
        /// </summary>
        /// <param name="id">The hash ID of the parameter.</param>
        /// <param name="value">The value to set.</param>
        public void SetFloatParameter(int id, float value)
        {
            MyAnimator.SetFloat(id, value);
        }

        /// <summary>
        /// Sets the value of a float parameter in the Animator.
        /// </summary>
        /// <param name="id">The name of the parameter.</param>
        /// <param name="value">The value to set.</param>
        public void SetFloatParameter(string id, float value)
        {
            SetFloatParameter(Animator.StringToHash(id), value);
        }

        /// <summary>
        /// Sets the value of an integer parameter in the Animator.
        /// </summary>
        /// <param name="id">The hash ID of the parameter.</param>
        /// <param name="value">The value to set.</param>
        public void SetIntegerParameter(int id, int value)
        {
            MyAnimator.SetInteger(id, value);
        }

        /// <summary>
        /// Sets the value of an integer parameter in the Animator.
        /// </summary>
        /// <param name="id">The name of the parameter.</param>
        /// <param name="value">The value to set.</param>
        public void SetIntegerParameter(string id, int value)
        {
            SetIntegerParameter(Animator.StringToHash(id), value);
        }

        /// <summary>
        /// Sets the value of a boolean parameter in the Animator.
        /// </summary>
        /// <param name="id">The hash ID of the parameter.</param>
        /// <param name="value">The value to set.</param>
        public void SetBoolParameter(int id, bool value)
        {
            MyAnimator.SetBool(id, value);
        }

        /// <summary>
        /// Sets the value of a boolean parameter in the Animator.
        /// </summary>
        /// <param name="id">The name of the parameter.</param>
        /// <param name="value">The value to set.</param>
        public void SetBoolParameter(string id, bool value)
        {
            SetBoolParameter(Animator.StringToHash(id), value);
        }

        /// <summary>
        /// Sets a trigger parameter in the Animator.
        /// </summary>
        /// <param name="id">The hash ID of the parameter.</param>
        public void SetTriggerParameter(int id)
        {
            MyAnimator.SetTrigger(id);
        }

        /// <summary>
        /// Sets a trigger parameter in the Animator.
        /// </summary>
        /// <param name="id">The name of the parameter.</param>
        public void SetTriggerParameter(string id)
        {
            SetTriggerParameter(Animator.StringToHash(id));
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Checks if the Animator has a parameter with the specified hash.
        /// </summary>
        /// <remarks>WARNING! This causes a lot of garbage</remarks>
        /// <param name="param">The hash of the parameter.</param>
        /// <returns>True if the Animator has the parameter, false otherwise.</returns>
        public bool HasParameter(int param)
        {   
            for (short i = 0; i < MyAnimator.parameters.Length; i++)
            {
                if (MyAnimator.parameters[i].nameHash == param)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the Animator has a parameter with the specified name.
        /// </summary>
        /// <param name="param">The name of the parameter.</param>
        /// <returns>True if the Animator has the parameter, false otherwise.</returns>
        public bool HasParameter(string param)
        {
            return HasParameter(Animator.StringToHash(param));
        }

        /// <summary>
        /// Sets the speed of the Animator.
        /// </summary>
        /// <param name="targetSpeed">The target speed.</param>
        public void SetAnimatorSpeed(float targetSpeed)
        {
            _myAnimator.speed = targetSpeed;
        }

        /// <summary>
        /// Gradually slows down the Animator over a specified duration.
        /// </summary>
        /// <param name="slowdownDuration">The duration over which to slow down the Animator.</param>
        public void PauseAnimator(float slowdownDuration)
        {
            _canUnpause = false;
            StartCoroutine(PauseAnimatorCoroutine(slowdownDuration));
        }

        /// <summary>
        /// Gradually speeds up the Animator over a specified duration.
        /// </summary>
        /// <param name="slowdownDuration">The duration over which to speed up the Animator.</param>
        public void UnpauseAnimator(float slowdownDuration)
        {
            // If the animator is already unpaused, return
            if (_myAnimator.speed >= 1f) { return; }
            ResetAllTriggers();
            StartCoroutine(UnpauseAnimatorCoroutine(slowdownDuration));
        }

        /// <summary>
        /// Resets all trigger parameters in the Animator.
        /// </summary>
        public void ResetAllTriggers()
        {
            foreach (var param in _myAnimator.parameters)
            {
                if (param.type != AnimatorControllerParameterType.Trigger) { continue; }
                _myAnimator.ResetTrigger(param.name);
            }
        }

        /// <summary>
        /// Tries to get the duration of an animation.
        /// </summary>
        /// <param name="animationName">The name of the animation.</param>
        /// <param name="duration">The duration of the animation.</param>
        /// <returns>True if the duration could be retrieved, false otherwise.</returns>
        public bool TryGetAnimationDuration(string animationName, out float duration)
        {
            duration = 0f;

            AnimationClip targetClip = System.Array.Find(_animationClips, clip => clip.name == animationName);
            if (targetClip == null)
            {
                Debug.Log("Could not get animation duration");
                return false;
            }

            duration = targetClip.length;
            return true;
        }

        /// <summary>
        /// Resets all parameters in the Animator to their initial values.
        /// </summary>
        public void ResetBehaviour()
        {
            ResetAnimatorParameters();
        }

        /// <summary>
        /// Gets the weight of a layer in the Animator.
        /// </summary>
        /// <param name="layerIndex">The index of the layer.</param>
        /// <returns>The weight of the layer.</returns>
        public float GetLayerWeight(int layerIndex)
        {
            return _myAnimator.GetLayerWeight(layerIndex);
        }

        /// <summary>
        /// Sets the weight of a layer in the Animator.
        /// </summary>
        /// <param name="index">The index of the layer.</param>
        /// <param name="weight">The weight of the layer.</param>
        public void SetLayerWeight(int index, float weight)
        {
            _myAnimator.SetLayerWeight(index, weight);
        }

        /// <summary>
        /// Gets the current state information of the main layer in the Animator.
        /// </summary>
        /// <returns>The current state information of the main layer.</returns>
        public AnimatorStateInfo GetCurrentAnimatorStateInfoMainLayer()
        {
            return _myAnimator.GetCurrentAnimatorStateInfo(_mainLayer);
        }

        /// <summary>
        /// Gets the next state information of the main layer in the Animator.
        /// </summary>
        /// <returns>The next state information of the main layer.</returns>
        public AnimatorStateInfo GetNextAnimatorStateInfoMainLayer()
        {
            return _myAnimator.GetNextAnimatorStateInfo(_mainLayer);
        }

        /// <summary>
        /// Gets the current state information of a layer in the Animator.
        /// </summary>
        /// <param name="layerIndex">The index of the layer.</param>
        /// <returns>The current state information of the layer.</returns>
        public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex)
        {
            return _myAnimator.GetCurrentAnimatorStateInfo(layerIndex);
        }

        /// <summary>
        /// Gets the next state information of a layer in the Animator.
        /// </summary>
        /// <param name="layerIndex">The index of the layer.</param>
        /// <returns>The next state information of the layer.</returns>
        public AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex)
        {
            return _myAnimator.GetNextAnimatorStateInfo(layerIndex);
        }

        /// <summary>
        /// Applies the built-in root motion of the Animator.
        /// </summary>
        public void ApplyBuiltinRootMotion()
        {
            _myAnimator.ApplyBuiltinRootMotion();
        }

        /// <summary>
        /// Checks if the main layer in the Animator is in transition.
        /// </summary>
        /// <returns>True if the main layer is in transition, false otherwise.</returns>
        public bool IsInTransitionMainLayer()
        {
            return _myAnimator.IsInTransition(_mainLayer);
        }

        /// <summary>
        /// Checks if a layer in the Animator is in transition.
        /// </summary>
        /// <param name="layerIndex">The index of the layer.</param>
        /// <returns>True if the layer is in transition, false otherwise.</returns>
        public bool IsInTransition(int layerIndex)
        {
            return _myAnimator.IsInTransition(layerIndex);
        }

        /// <summary>
        /// Plays a state in the Animator.
        /// </summary>
        /// <param name="stateName">The name of the state.</param>
        /// <param name="layer">The layer to play the state on.</param>
        /// <param name="normalizedTime">The normalized time at which to start the state.</param>
        public void PlayState(string stateName, int layer, float normalizedTime)
        {
            _myAnimator.Play(stateName, layer, normalizedTime);
        }
        #endregion


        #region Coroutines
        /// <summary>
        /// Coroutine to gradually slow down the Animator over a specified duration.
        /// </summary>
        /// <param name="slowdownDuration">The duration over which to slow down the Animator.</param>
        private IEnumerator PauseAnimatorCoroutine(float slowdownDuration)
        {
            var elapsedTime = slowdownDuration;

            while (elapsedTime > 0f)
            {
                elapsedTime -= Time.deltaTime;
                _myAnimator.speed = Mathf.Clamp01(elapsedTime / slowdownDuration);
                yield return null;
            }

            _myAnimator.speed = 0f;
            _canUnpause = true;
        }

        /// <summary>
        /// Coroutine to gradually speed up the Animator over a specified duration.
        /// </summary>
        /// <param name="slowdownDuration">The duration over which to speed up the Animator.</param>
        private IEnumerator UnpauseAnimatorCoroutine(float slowdownDuration)
        {
            while (!_canUnpause) { yield return null; }

            var elapsedTime = 0f;

            while (elapsedTime < slowdownDuration)
            {
                elapsedTime += Time.deltaTime;
                _myAnimator.speed = Mathf.Clamp01(elapsedTime / slowdownDuration);
                yield return null;
            }

            _myAnimator.speed = 1f;
        }
        #endregion


        #region Public Animation Event
        /// <summary>
        /// Sets a boolean parameter in the Animator using an AnimationEvent.
        /// </summary>
        /// <param name="animEvent">The AnimationEvent containing the parameter data.</param>
        public void SetBoolByAnim(AnimationEvent animEvent)
        {
            _myAnimator.SetBool(animEvent.stringParameter, animEvent.intParameter != 0);
        }

        /// <summary>
        /// Sets a float parameter in the Animator using an AnimationEvent.
        /// </summary>
        /// <param name="animEvent">The AnimationEvent containing the parameter data.</param>
        public void SetFloatByAnim(AnimationEvent animEvent)
        {
            _myAnimator.SetFloat(animEvent.stringParameter, animEvent.floatParameter);
        }

        /// <summary>
        /// Adds a value to a float parameter in the Animator using an AnimationEvent.
        /// </summary>
        /// <param name="animEvent">The AnimationEvent containing the parameter data.</param>
        public void SetFloatByAnimAdditive(AnimationEvent animEvent)
        {
            var amount = GetFloatParameter(animEvent.stringParameter) + animEvent.floatParameter;
            _myAnimator.SetFloat(animEvent.stringParameter, amount);
        }

        /// <summary>
        /// Sets an integer parameter in the Animator using an AnimationEvent.
        /// </summary>
        /// <param name="animEvent">The AnimationEvent containing the parameter data.</param>
        public void SetIntegerByAnim(AnimationEvent animEvent)
        {
            _myAnimator.SetInteger(animEvent.stringParameter, animEvent.intParameter);
        }

        /// <summary>
        /// Adds a value to an integer parameter in the Animator using an AnimationEvent.
        /// </summary>
        /// <param name="animEvent">The AnimationEvent containing the parameter data.</param>
        public void SetIntegerByAnimAdditive(AnimationEvent animEvent)
        {
            var amount = GetIntegerParameter(animEvent.stringParameter) + animEvent.intParameter;
            _myAnimator.SetInteger(animEvent.stringParameter, amount);
        }

        /// <summary>
        /// Sets a trigger parameter in the Animator using an AnimationEvent.
        /// </summary>
        /// <param name="animEvent">The AnimationEvent containing the parameter data.</param>
        public void SetTriggerByAnim(AnimationEvent animEvent)
        {
            _myAnimator.SetTrigger(animEvent.stringParameter);
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Populates the animation clips from the animator's runtime controller.
        /// </summary>
        private void PopulateClips()
        {
            _animationClips = _myAnimator.runtimeAnimatorController.animationClips;
        }

        /// <summary>
        /// Gets all parameters from the Animator and stores them in the _animatorParameters array.
        /// </summary>
        private void GetAllAnimatorParameters()
        {
            // Get the list of parameters from the Animator
            AnimatorControllerParameter[] parameters = MyAnimator.parameters;

            // Initialize the array
            _animatorParameters = new AnimatorParamData[parameters.Length];

            // Iterate through the parameters and store their names and values
            for (int i = 0; i < parameters.Length; i++)
            {
                string paramName = parameters[i].name;
                // Store the parameter data in the array
                _animatorParameters[i] = new AnimatorParamData(GetParameterValue(paramName, i), i, paramName);
            }
        }

        /// <summary>
        /// Gets the value of a specific parameter from the Animator.
        /// </summary>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="paramIndex">The index of the parameter.</param>
        /// <returns>The value of the parameter.</returns>
        private object GetParameterValue(string paramName, int paramIndex)
        {
            // Determine the type of the parameter and get its value
            AnimatorControllerParameterType paramType = _myAnimator.GetParameter(paramIndex).type;

            return paramType switch
            {
                AnimatorControllerParameterType.Float => _myAnimator.GetFloat(paramName),
                AnimatorControllerParameterType.Int => _myAnimator.GetInteger(paramName),
                AnimatorControllerParameterType.Bool => _myAnimator.GetBool(paramName),
                AnimatorControllerParameterType.Trigger => false, // Triggers have no values, so we set it to false
                _ => null, // Unsupported parameter type
            };
        }

        /// <summary>
        /// Resets all parameters in the Animator to their initial values.
        /// </summary>
        public void ResetAnimatorParameters()
        {
            foreach (var paramData in _animatorParameters)
            {
                // Set the parameter back to its initial value
                SetParameterValueToOriginalValue(paramData);
            }
        }

        /// <summary>
        /// Sets a specific parameter in the Animator back to its original value.
        /// </summary>
        /// <param name="data">The data of the parameter to reset.</param>
        private void SetParameterValueToOriginalValue(AnimatorParamData data)
        {
            //Debug.Log($"Setting {data.ParamName} to {data.ParamValue}");
            // Determine the type of the parameter and set its value
            AnimatorControllerParameterType paramType = _myAnimator.GetParameter(data.Index).type;

            switch (paramType)
            {
                case AnimatorControllerParameterType.Float:
                    _myAnimator.SetFloat(data.ParamName, (float)data.ParamValue);
                    break;

                case AnimatorControllerParameterType.Int:
                    _myAnimator.SetInteger(data.ParamName, (int)data.ParamValue);
                    break;

                case AnimatorControllerParameterType.Bool:
                    _myAnimator.SetBool(data.ParamName, (bool)data.ParamValue);
                    break;

                case AnimatorControllerParameterType.Trigger:
                    _myAnimator.ResetTrigger(data.ParamName);
                    break;

                default:
                    // Unsupported parameter type
                    break;
            }
        }
        #endregion

        /// <summary>
        /// Struct for storing data about an Animator parameter.
        /// </summary>
        private struct AnimatorParamData
        {
            public object ParamValue { get; private set; }
            public int Index { get; private set; }
            public string ParamName { get; private set; }

            /// <summary>
            /// Constructor for the AnimatorParamData struct.
            /// </summary>
            /// <param name="paramValue">The initial value of the parameter.</param>
            /// <param name="index">The index of the parameter.</param>
            /// <param name="paramName">The name of the parameter.</param>
            public AnimatorParamData(object paramValue, int index, string paramName)
            {
                ParamValue = paramValue;
                Index = index;
                ParamName = paramName;
            }
        }
    }
}
