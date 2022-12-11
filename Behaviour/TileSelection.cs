using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Required: Tilemap must have a Collider2D
/// </summary>
public class TileSelection : MonoBehaviour
{
    [SerializeField] private Tilemap tileMap;

    [SerializeField] private Color selectionColor = Color.green;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero, 0f);
            if (hit)
            {
                Vector3Int pos = tileMap.WorldToCell(mouseWorldPos);

                // Change tile Color
                tileMap.SetTileFlags(pos, TileFlags.None);
                tileMap.SetColor(pos, selectionColor);
            }
        }
    }
}