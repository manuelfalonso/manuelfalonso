using System;
using UnityEngine;
using UnityEngine.Events;

namespace SombraStudios.Shared.Animations
{
    /// <summary>
    /// Will receive triggers from the <see cref="AnimationEventActionBegin"/> and 
    /// <see cref="AnimationEventActionFinished"/> classes, and forward them to Unity Events.
    /// </summary>
    public class OnAnimationEvent : MonoBehaviour, IAnimationEventActionBegin, IAnimationEventActionFinished
    {
        [Serializable]
        private struct ActionEvent
        {
            /// <summary>
            /// A label identifying the animation that has finished.
            /// </summary>
            [Tooltip("A label identifying the animation that has finished.")]
            public string Label;
            /// <summary>
            /// A Unity Event to be invoked when the animation event is triggered.
            /// </summary>
            [Tooltip("A Unity Event to be invoked when the animation event is triggered.")]
            public UnityEvent Action;
        }

        [SerializeField]
        private ActionEvent[] _actionBeginEvents;

        [SerializeField]
        private ActionEvent[] _actionEndEvents;


        /// <inheritdoc />
        public void ActionBegin(string label)
        {
            if (_actionBeginEvents == null)
                return;

            foreach (var currentAction in _actionBeginEvents)
            {
                if (currentAction.Label == label)
                    currentAction.Action.Invoke();
            }
        }

        /// <inheritdoc />
        public void ActionFinished(string label)
        {
            if (_actionEndEvents == null)
                return;

            foreach (var currentAction in _actionEndEvents)
            {
                if (currentAction.Label == label)
                    currentAction.Action.Invoke();
            }
        }
    }
}
