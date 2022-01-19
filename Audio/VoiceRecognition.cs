using System;
using System.Text;
using UnityEngine;
using UnityEngine.Windows.Speech;

/// <summary>
/// Class to detect voice recognition through a list of keywords
/// When recognized take an action
/// </summary>
public class VoiceRecognition : MonoBehaviour
{
    [SerializeField]
    private bool m_debugMode = true;

    [Header("Keywords")]
    [SerializeField]
    private string[] m_Keywords;

    private KeywordRecognizer m_Recognizer;

    private string phraseRecognized;

    void Start()
    {
        m_Recognizer = new KeywordRecognizer(m_Keywords);
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognizedLog;
        m_Recognizer.OnPhraseRecognized += OnPhraseRecognizedUI;
        StartRecognizer();
    }

    private void OnGUI()
    {
        if (m_debugMode)
        {
            GUILayout.BeginArea(new Rect(25, 25, 200, 200));

            if (GUILayout.Button("Start Voice Recognition"))
            {
                StartRecognizer();
            }

            GUILayout.Label("Keyword list: ");
            foreach (var keyword in m_Keywords)
            {
                GUILayout.Label(keyword);
            }

            GUILayout.Label("\nKeyword Recognized: ");
            GUILayout.Label(phraseRecognized);

            if (GUILayout.Button("Stop Voice Recognition"))
            {
                StopRecognizer();
            }

            GUILayout.EndArea();
        }
    }

    private void OnPhraseRecognizedLog(PhraseRecognizedEventArgs args)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} ({1}){2}", args.text, args.confidence, Environment.NewLine);
        builder.AppendFormat("\tTimestamp: {0}{1}", args.phraseStartTime, Environment.NewLine);
        builder.AppendFormat("\tDuration: {0} seconds{1}", args.phraseDuration.TotalSeconds, Environment.NewLine);
        Debug.Log(builder.ToString());
    }

    private void OnPhraseRecognizedUI(PhraseRecognizedEventArgs args)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("{0} (confidence: {1})", args.text, args.confidence);
        phraseRecognized = builder.ToString();
    }

    private void StartRecognizer()
    {
        m_Recognizer.Start();
    }

    private void StopRecognizer()
    {
        m_Recognizer.Stop();
    }

    private void OnDisable()
    {
        m_Recognizer.Dispose();
    }
}
