using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    private static int width = 11;
    private static int height = 11;

    public bool upExit = true;
    public bool downExit = true;
    public bool leftExit = true;
    public bool rightExit = true;

    private Level level;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Grid grid = GetComponent<Grid>();

        Vector3 bl = transform.position + new Vector3(grid.cellSize.x, grid.cellSize.y);
        Vector3 br = bl + new Vector3(grid.cellSize.x * (width - 2), 0);
        Vector3 tl = bl + new Vector3(0, grid.cellSize.y * (height - 2));
        Vector3 tr = bl + new Vector3(grid.cellSize.x * (width - 2), grid.cellSize.y * (height - 2));
        Gizmos.DrawLine(bl, br);
        Gizmos.DrawLine(br, tr);
        Gizmos.DrawLine(tr, tl);
        Gizmos.DrawLine(tl, bl);
    }

    public void SetLevel(Level level)
    {
        this.level = level;
    }

    public void GenerateWalls(bool up, bool down, bool left, bool right)
    {
        Transform tilesChild = transform.GetChild(0);
        Grid grid = GetComponent<Grid>();
        for (int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(x % (width - 1) == 0 || y % (height - 1) == 0)
                {
                    GameObject wall;

                    // doors
                    if((Mathf.Abs(x - width / 2) <= 1 && ((y == 0 && down) || (y == height - 1 && up)))
                        || (Mathf.Abs(y - height / 2) <= 1 && ((x == 0 && left) || (x == width - 1 && right))))
                    {
                        wall = Instantiate(level.floorTile, tilesChild);
                    } else
                    {
                        wall = Instantiate(level.wallTile, tilesChild);
                    }
                    // offset of 0.5 because the tilemap is offset by that much
                    wall.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f));
                }
            }
        }
    }

    public Vector2 GetFullSize()
    {
        Grid grid = GetComponent<Grid>();
        return new Vector2(grid.cellSize.x * width, grid.cellSize.y * height);
    }
}
