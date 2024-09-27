#if DOTWEEN
using DG.DOTweenEditor;
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Editor
{

    /// <summary>
    /// Editor script to preview and test a Tween
    /// </summary>
    [CustomEditor(typeof(DoTweenTester), true)]
    public class DoTweenTesterEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DoTweenTester myTarget = (DoTweenTester)target;

            if (GUILayout.Button("Preview"))
            {
                if (!DOTweenEditorPreview.isPreviewing)
                {
                    Preview();
                }
                else
                {
                    DOTweenEditorPreview.Stop(true, false);
                    Preview();
                }
            }

            if (GUILayout.Button("Restart"))
            {
                DOTweenEditorPreview.Stop(true, false);
            }

            void Preview()
            {
                DOTweenEditorPreview.PrepareTweenForPreview(
                    myTarget.GetTween());
                DOTweenEditorPreview.Start();
            }
        }
    }
}
#endif