using System;
using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Utility Class for taking a screenshot from the editor using a menu item.
/// </summary>
[ExecuteInEditMode]
public class ScreenshotTaker : MonoBehaviour
{
    [MenuItem("Sombra Studios/Tools/Take Screenshot")]
    public static void TakeScreenshot()
    {
        if (!Directory.Exists("Screenshots"))
            Directory.CreateDirectory("Screenshots");

        ScreenCapture.CaptureScreenshot(string.Format("Screenshots/{0}.png", 
            DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss")));

        Debug.Log("Screenshot saved at: " + Directory.GetCurrentDirectory() + "\\Screenshots");
    }
}
