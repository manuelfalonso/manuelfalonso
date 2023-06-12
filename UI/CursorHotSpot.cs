using UnityEngine;

namespace SombraStudios.UI
{
    /// <summary>
    /// Simple script to set the hot Spot and textture of a Cursor
    /// </summary>
    public class CursorHotSpot : MonoBehaviour
    {
        public enum HotSpotPosition 
        { 
            UpLeft, 
            UpCenter, 
            UpRight, 
            Left, 
            Center, 
            Right, 
            DownLeft, 
            DownCenter,
            DownRight
        };

        [SerializeField] private Texture2D _cursorTex;
        [SerializeField] private HotSpotPosition _hotSpotPosition;

        void Start()
        {
            InitializeCursor();
        }

        private void InitializeCursor()
        {
            int xspot = 0;
            int yspot = 0;
            CursorMode mode = CursorMode.ForceSoftware;

            switch (_hotSpotPosition)
            {
                case HotSpotPosition.UpLeft:
                    xspot = 0;
                    yspot = 0;
                    break;
                case HotSpotPosition.UpCenter:
                    xspot = _cursorTex.width / 2;
                    yspot = 0;
                    break;
                case HotSpotPosition.UpRight:
                    xspot = _cursorTex.width;
                    yspot = 0;
                    break;
                case HotSpotPosition.Left:
                    xspot = 0;
                    yspot = _cursorTex.height / 2;
                    break;
                case HotSpotPosition.Center:
                    xspot = _cursorTex.width / 2;
                    yspot = _cursorTex.height / 2;
                    break;
                case HotSpotPosition.Right:
                    xspot = _cursorTex.width;
                    yspot = _cursorTex.height / 2;
                    break;
                case HotSpotPosition.DownLeft:
                    xspot = 0;
                    yspot = _cursorTex.height;
                    break;
                case HotSpotPosition.DownCenter:
                    xspot = _cursorTex.width / 2;
                    yspot = _cursorTex.height;
                    break;
                case HotSpotPosition.DownRight:
                    xspot = _cursorTex.width;
                    yspot = _cursorTex.height;
                    break;
            }

            Vector2 hotSpot = new Vector2(xspot, yspot);
            Cursor.SetCursor(_cursorTex, hotSpot, mode);
        }
    }
}
