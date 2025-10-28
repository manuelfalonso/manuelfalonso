#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace SombraStudios.Shared.Animations.Editor
{
    /// <summary>  
    /// Provides a custom preview for <see cref="AnimatorState"/> objects in the Unity Editor.  
    /// Consideration: The preview lenghth of the clip will always be 30 frames.
    /// </summary>  
    [CustomPreview(typeof(AnimatorState))]
    public class AnimatorStateObjectPreview : ObjectPreview
    {
        private static FieldInfo _cachedAvatarPreviewField;
        private static FieldInfo _cachedTimeControlField;
        private static FieldInfo _cachedStopTimeField;

        private UnityEditor.Editor _preview;
        private int _animationClipId;

        /// <summary>  
        /// Initializes the preview for the specified targets.  
        /// </summary>  
        /// <param name="targets">The objects to preview.</param>  
        public override void Initialize(Object[] targets)
        {
            base.Initialize(targets);

            if (targets.Length != 1 || Application.isPlaying)
                return;

            SetAnimationClipEditorFields();

            if (target is AnimatorState state)
            {
                var clip = GetAnimationClip(state);
                if (clip != null)
                {
                    _preview = UnityEditor.Editor.CreateEditor(clip);
                    _animationClipId = clip.GetInstanceID();
                }
            }
        }

        /// <summary>  
        /// Cleans up resources used by the preview.  
        /// </summary>  
        public override void Cleanup()
        {
            CleanUpPreviewEditor();
            base.Cleanup();
        }

        /// <summary>  
        /// Determines whether the object has a preview GUI.  
        /// </summary>  
        /// <returns>True if the object has a preview GUI; otherwise, false.</returns>  
        public override bool HasPreviewGUI()
        {
            return _preview != null && _preview.HasPreviewGUI();
        }

        /// <summary>  
        /// Renders the interactive preview GUI for the object.  
        /// </summary>  
        /// <param name="r">The rectangle in which to draw the preview.</param>  
        /// <param name="background">The background style for the preview.</param>  
        public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
        {
            AnimationClip currentClip = GetAnimationClip(target as AnimatorState);
            if (currentClip != null && _animationClipId != currentClip.GetInstanceID())
            {
                CleanUpPreviewEditor();
                _preview = UnityEditor.Editor.CreateEditor(currentClip);
                _animationClipId = currentClip.GetInstanceID();
            }

            if (_preview != null)
            {
                UpdateAnimationClipEditor(_preview, currentClip);
                _preview.OnInteractivePreviewGUI(r, background);
            }
        }

        /// <summary>  
        /// Retrieves the <see cref="AnimationClip"/> associated with the given <see cref="AnimatorState"/>.  
        /// </summary>  
        /// <param name="state">The animator state to retrieve the clip from.</param>  
        /// <returns>The associated animation clip, or null if none exists.</returns>  
        private AnimationClip GetAnimationClip(AnimatorState state)
        {
            return state.motion as AnimationClip;
        }

        /// <summary>  
        /// Cleans up the preview editor and releases associated resources.  
        /// </summary>  
        private void CleanUpPreviewEditor()
        {
            if (_preview != null)
            {
                Object.DestroyImmediate(_preview);
                _preview = null;
                _animationClipId = 0;
            }
        }

        private void SetAnimationClipEditorFields()
        {
            if (_cachedAvatarPreviewField != null)
                return;

            // Warning: Using reflection to access internal UnityEditor classes and fields may break in future Unity versions.
            _cachedAvatarPreviewField = System.Type.GetType("UnityEditor.AnimationClipEditor, UnityEditor")
                ?.GetField("m_AvatarPreview", BindingFlags.NonPublic | BindingFlags.Instance);
            _cachedTimeControlField = System.Type.GetType("UnityEditor.AvatarPreview, UnityEditor")
                ?.GetField("timeControl", BindingFlags.Public | BindingFlags.Instance);
            _cachedStopTimeField = System.Type.GetType("UnityEditor.TimeControl, UnityEditor")
                ?.GetField("stopTime", BindingFlags.Public | BindingFlags.Instance);
        }

        private void UpdateAnimationClipEditor(UnityEditor.Editor preview, AnimationClip currentClip)
        {
            if (_cachedAvatarPreviewField == null || _cachedTimeControlField == null || _cachedStopTimeField == null)
                return;

            var avatarPreview = _cachedAvatarPreviewField.GetValue(preview);
            var timeControl = _cachedTimeControlField.GetValue(avatarPreview);

            _cachedStopTimeField.SetValue(timeControl, currentClip.length);
        }
    }
}
#endif