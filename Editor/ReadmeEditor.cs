#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace SombraStudios.Shared.Editor
{
    /// <summary>
    /// Editor script for the readme scriptable object
    /// Credits to: rich-joslin-unity
    /// </summary>
    [CustomEditor(typeof(ReadmeSO))]
    [InitializeOnLoad]
    public class ReadmeEditor : UnityEditor.Editor
    {
        ReadmeSO _readmeSO;
        const float k_SectionSpacer = 16f;
        const float k_HeaderSpacer = 8f;
        const float k_BulletItemSpacer = 2f;
        const float k_BulletLevel1LabelWidth = 25;
        const float k_BulletLevel2LabelWidth = 40;
        const float k_BulletLevel3LabelWidth = 55;
        const float k_BulletLevel4LabelWidth = 70;
        bool m_StylesInitialized;

        const string k_ShowedReadmeSessionStateName = "ReadmeEditor.showedReadme";
        const string k_DefaultReadmeLabel = "StartHereReadme";

        [SerializeField] GUIStyle m_BodyStyle;
        [SerializeField] GUIStyle m_HeaderTitleStyle;
        [SerializeField] GUIStyle m_SubHeader1Style;
        [SerializeField] GUIStyle m_SubHeader2Style;
        [SerializeField] GUIStyle m_SubHeader3Style;
        [SerializeField] GUIStyle m_BoldStyle;
        [SerializeField] GUIStyle m_ItalicsStyle;
        [SerializeField] GUIStyle m_LinkStyle;

        void OnEnable()
        {
            _readmeSO = target as ReadmeSO;
        }

        static ReadmeEditor()
        {
            EditorApplication.delayCall += SelectReadmeAutomatically;
        }

        static void SelectReadmeAutomatically()
        {
            if (!SessionState.GetBool(k_ShowedReadmeSessionStateName, false))
            {
                SelectReadme();
                SessionState.SetBool(k_ShowedReadmeSessionStateName, true);
            }
        }

        static void SelectReadme()
        {
            var ids = AssetDatabase.FindAssets($"l:{k_DefaultReadmeLabel}");

            if (ids.Length > 0)
            {
                var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(ids[0]));
                Selection.objects = new[] { readmeObject };
            }
            else
            {
                Debug.Log($"Couldn't find a default README asset with the \"{k_DefaultReadmeLabel}\" label.");
            }
        }

        void InitStyles()
        {
            if (m_StylesInitialized)
            {
                return;
            }

            m_BodyStyle = new GUIStyle(EditorStyles.label)
            {
                wordWrap = true,
                fontSize = 14
            };

            m_HeaderTitleStyle = new GUIStyle(m_BodyStyle)
            {
                fontSize = 26
            };

            m_SubHeader1Style = new GUIStyle(m_BodyStyle)
            {
                fontSize = 20,
                fontStyle = FontStyle.Bold
            };

            m_SubHeader2Style = new GUIStyle(m_BodyStyle)
            {
                fontSize = 18,
                fontStyle = FontStyle.Bold
            };

            m_SubHeader3Style = new GUIStyle(m_BodyStyle)
            {
                fontSize = 16,
                fontStyle = FontStyle.Italic
            };

            m_BoldStyle = new GUIStyle(m_BodyStyle)
            {
                fontStyle = FontStyle.Bold
            };

            m_ItalicsStyle = new GUIStyle(m_BodyStyle)
            {
                fontStyle = FontStyle.Italic
            };

            m_LinkStyle = new GUIStyle(EditorStyles.linkLabel)
            {
                fontSize = 14
            };

            m_StylesInitialized = true;
        }

        protected override void OnHeaderGUI()
        {
            InitStyles();

            Texture2D icon = null;
            var title = "";
            var iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth / 3f - 20f, 128f);

            if (_readmeSO != null && _readmeSO.header != null)
            {
                icon = _readmeSO.header.icon;
                title = _readmeSO.header.title;
            }

            using (new EditorGUILayout.HorizontalScope("In BigTitle"))
            {
                if (icon != null)
                {
                    GUILayout.Label(icon, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth));
                }
                GUILayout.Label(title, m_HeaderTitleStyle);
            }
        }

        public override void OnInspectorGUI()
        {
            InitStyles();

            if (_readmeSO == null || _readmeSO.sections == null)
            {
                return;
            }

            foreach (var section in _readmeSO.sections)
            {
                if (!string.IsNullOrEmpty(section.subHeader1))
                {
                    DisplayFormattedHeaderText(section.subHeader1, m_SubHeader1Style);
                }

                if (!string.IsNullOrEmpty(section.subHeader2))
                {
                    DisplayFormattedHeaderText(section.subHeader2, m_SubHeader2Style);
                }

                if (!string.IsNullOrEmpty(section.subHeader3))
                {
                    DisplayFormattedHeaderText(section.subHeader3, m_SubHeader3Style);
                }

                if (!string.IsNullOrEmpty(section.body))
                {
                    DisplayFormattedBodyText(section.body, section.bodyFormat);
                }

                if (!string.IsNullOrEmpty(section.boxCallout))
                {
                    DisplayFormattedBoxCallout(section.boxCallout);
                }

                if (section.bulletList != null && section.bulletList.Length > 0)
                {
                    DisplayFormattedBulletItemLevel1List(section.bulletList);
                }

                if (section.linkList != null && section.linkList.Length > 0)
                {
                    DisplayFormattedLinkList(section.linkList);
                }

                GUILayout.Space(k_SectionSpacer);
            }
        }

        void DisplayFormattedHeaderText(string headerText, GUIStyle style)
        {
            GUILayout.Label(headerText, style);
            GUILayout.Space(k_HeaderSpacer);
        }

        void DisplayFormattedBodyText(string text, ReadmeSO.FontFormat format)
        {
            GUILayout.Label(text, GetStyle(format));
        }

        void DisplayFormattedBoxCallout(string text)
        {
            EditorGUILayout.HelpBox(text, MessageType.Info);
        }

        void DisplayFormattedBulletItemLevel1List(ReadmeSO.BulletItemLevel1[] list)
        {
            var previousLabelWidth = StartListFormatting(k_BulletLevel1LabelWidth);

            foreach (var item in list)
            {
                DisplayFormattedBulletListItem(item.body, item.bodyFormat);

                if (item.bulletList != null && item.bulletList.Length > 0)
                {
                    DisplayFormattedBulletItemLevel2List(item.bulletList);
                }

                if (item.linkList != null && item.linkList.Length > 0)
                {
                    DisplayFormattedLinkList(item.linkList);
                }
            }

            EndListFormatting(previousLabelWidth);
        }

        void DisplayFormattedBulletItemLevel2List(ReadmeSO.BulletItemLevel2[] list)
        {
            var previousLabelWidth = StartListFormatting(k_BulletLevel2LabelWidth);

            foreach (var item in list)
            {
                DisplayFormattedBulletListItem(item.body, item.bodyFormat);

                if (item.bulletList != null && item.bulletList.Length > 0)
                {
                    DisplayFormattedBulletItemLevel3List(item.bulletList);
                }
            }

            EndListFormatting(previousLabelWidth);
        }

        void DisplayFormattedBulletItemLevel3List(ReadmeSO.BulletItemLevel3[] list)
        {
            var previousLabelWidth = StartListFormatting(k_BulletLevel3LabelWidth);

            foreach (var item in list)
            {
                DisplayFormattedBulletListItem(item.body, item.bodyFormat);

                if (item.bulletList != null && item.bulletList.Length > 0)
                {
                    DisplayFormattedBulletItemLevel4List(item.bulletList);
                }
            }

            EndListFormatting(previousLabelWidth);
        }

        void DisplayFormattedBulletItemLevel4List(string[] list)
        {
            var previousLabelWidth = StartListFormatting(k_BulletLevel4LabelWidth);

            foreach (var item in list)
            {
                DisplayFormattedBulletListItem(item, ReadmeSO.FontFormat.Regular);
            }

            EndListFormatting(previousLabelWidth);
        }

        void DisplayFormattedBulletListItem(string text, ReadmeSO.FontFormat format)
        {
            EditorGUILayout.LabelField("*", text, GetStyle(format));

            GUILayout.Space(k_BulletItemSpacer);
        }

        void DisplayFormattedLinkList(ReadmeSO.LinkListItem[] list)
        {
            var previousLabelWidth = StartListFormatting(k_BulletLevel1LabelWidth);

            foreach (var item in list)
            {
                DisplayFormattedLinkListItem(item.linkText, item.url);
            }

            EndListFormatting(previousLabelWidth);
        }

        void DisplayFormattedLinkListItem(string text, string url)
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.PrefixLabel("*");

                if (GUILayout.Button(text, m_LinkStyle))
                {
                    Application.OpenURL(url);
                }
            }
            GUILayout.Space(k_BulletItemSpacer);
        }

        float StartListFormatting(float labelWidth)
        {
            EditorGUI.indentLevel++;
            var previousLabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = labelWidth;
            return previousLabelWidth;
        }

        void EndListFormatting(float previousLabelWidth)
        {
            EditorGUIUtility.labelWidth = previousLabelWidth;
            EditorGUI.indentLevel--;
        }

        GUIStyle GetStyle(ReadmeSO.FontFormat format)
        {
            return format switch
            {
                ReadmeSO.FontFormat.Bold => m_BoldStyle,
                ReadmeSO.FontFormat.Italic => m_ItalicsStyle,
                _ => m_BodyStyle
            };
        }
    }
}
#endif