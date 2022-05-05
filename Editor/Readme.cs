using System;
using UnityEngine;

/// <summary>
/// SO used for the Editor Readme Script
/// It supports Title, icon, headers, body, format, boxes, bullet list and links
/// Credits to: rich-joslin-unity
/// </summary>
[CreateAssetMenu(fileName = "New Readme", menuName = "Readme file", order = 51)]
public class Readme : ScriptableObject
{
    public Header header;
    public Section[] sections;

    [Serializable]
    public class Header
    {
        public string title;
        public Texture2D icon;
    }

    [Serializable]
    public class Section
    {
        public string subHeader1;
        public string subHeader2;
        public string subHeader3;
        public string body;
        public FontFormat bodyFormat;
        public string boxCallout;
        public BulletItemLevel1[] bulletList;
        public LinkListItem[] linkList;
    }

    [Serializable]
    public class BulletItemLevel1
    {
        public string body;
        public FontFormat bodyFormat;
        public BulletItemLevel2[] bulletList;
        public LinkListItem[] linkList;
    }

    [Serializable]
    public class BulletItemLevel2
    {
        public string body;
        public FontFormat bodyFormat;
        public BulletItemLevel3[] bulletList;
    }

    [Serializable]
    public class BulletItemLevel3
    {
        public string body;
        public FontFormat bodyFormat;
        public string[] bulletList;
    }

    [Serializable]
    public class LinkListItem
    {
        public string linkText;
        public string url;
    }

    [Serializable]
    public enum FontFormat
    {
        Regular,
        Bold,
        Italic,
    }
}