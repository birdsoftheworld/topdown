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

    public GameObject node;

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

    public Transform[] GenerateWalls(bool up, bool down, bool left, bool right, int roomNumber)
    {
        Transform[] thisRoom = new Transform[13];

        Transform tilesChild = transform.GetChild(0);
        Grid grid = GetComponent<Grid>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x % (width - 1) == 0 || y % (height - 1) == 0)
                {
                    GameObject wall;

                    // doors
                    if ((Mathf.Abs(x - width / 2) <= 1 && ((y == 0 && down) || (y == height - 1 && up)))
                        || (Mathf.Abs(y - height / 2) <= 1 && ((x == 0 && left) || (x == width - 1 && right))))
                    {
                        wall = Instantiate(level.floorTile, tilesChild);
                    }
                    else
                    {
                        wall = Instantiate(level.wallTile, tilesChild);
                    }
                    // offset of 0.5 because the tilemap is offset by that much
                    wall.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f));
                }

                //cover unaltered/close to door: new Vector2(74, 153)

                if (x == 3)
                {
                    if (up == true)
                    {
                        if (y == 9)
                        {
                            GameObject nodeC = Instantiate(node, tilesChild);
                            nodeC.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(74, 153));
                            nodeC.tag = "NodeCover";

                            thisRoom[0] = nodeC.transform;

                            nodeC.GetComponent<storeRoomVars>().integer = roomNumber;
                        }
                    }

                    if (down == true)
                    {
                        if (y == 1)
                        {
                            GameObject nodeC = Instantiate(node, tilesChild);
                            nodeC.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(74, 153));
                            nodeC.tag = "NodeCover";

                            thisRoom[10] = nodeC.transform;

                            nodeC.GetComponent<storeRoomVars>().integer = roomNumber;
                        }
                    }
                }

                if (x == 7)
                {
                    if (up == true)
                    {
                        if (y == 9)
                        {
                            GameObject nodeC = Instantiate(node, tilesChild);
                            nodeC.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(72, 153));
                            nodeC.tag = "NodeCover";

                            thisRoom[2] = nodeC.transform;

                            nodeC.GetComponent<storeRoomVars>().integer = roomNumber;
                        }
                    }

                    if (down == true)
                    {
                        if (y == 1)
                        {
                            GameObject nodeC = Instantiate(node, tilesChild);
                            nodeC.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(72, 153));
                            nodeC.tag = "NodeCover";

                            thisRoom[12] = nodeC.transform;

                            nodeC.GetComponent<storeRoomVars>().integer = roomNumber;
                        }
                    }
                }

                if (x == 1)
                {
                    if (left == true)
                    {
                        if (y == 7)
                        {
                            GameObject nodeC = Instantiate(node, tilesChild);
                            nodeC.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(73, 152));
                            nodeC.tag = "NodeCover";

                            thisRoom[3] = nodeC.transform;

                            nodeC.GetComponent<storeRoomVars>().integer = roomNumber;
                        }

                        if (y == 3)
                        {
                            GameObject nodeC = Instantiate(node, tilesChild);
                            nodeC.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(73, 154));
                            nodeC.tag = "NodeCover";

                            thisRoom[8] = nodeC.transform;

                            nodeC.GetComponent<storeRoomVars>().integer = roomNumber;
                        }

                        if (y == 5)
                        {
                            GameObject nodeE = Instantiate(node, tilesChild);
                            nodeE.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(73, 153));
                            nodeE.tag = "NodeEnter";

                            thisRoom[5] = nodeE.transform;

                            nodeE.GetComponent<storeRoomVars>().integer = roomNumber;
                        }
                    }
                }

                if (x == 9)
                {
                    if (right == true)
                    {
                        if (y == 7)
                        {
                            GameObject nodeC = Instantiate(node, tilesChild);
                            nodeC.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(73, 152));
                            nodeC.tag = "NodeCover";

                            thisRoom[4] = nodeC.transform;

                            nodeC.GetComponent<storeRoomVars>().integer = roomNumber;
                        }

                        if (y == 3)
                        {
                            GameObject nodeC = Instantiate(node, tilesChild);
                            nodeC.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(73, 154));
                            nodeC.tag = "NodeCover";

                            thisRoom[9] = nodeC.transform;

                            nodeC.GetComponent<storeRoomVars>().integer = roomNumber;
                        }

                        if (y == 5)
                        {
                            GameObject nodeE = Instantiate(node, tilesChild);
                            nodeE.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(73, 153));
                            nodeE.tag = "NodeEnter";

                            thisRoom[7] = nodeE.transform;

                            nodeE.GetComponent<storeRoomVars>().integer = roomNumber;
                        }
                    }
                }

                if (x == 5)
                {
                    if (y == 5)
                    {
                        GameObject nodeM = Instantiate(node, tilesChild);
                        nodeM.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(73, 153));
                        nodeM.tag = "NodeCenter";

                        thisRoom[6] = nodeM.transform;

                        nodeM.GetComponent<storeRoomVars>().integer = roomNumber;
                    }

                    if (up == true)
                    {
                        if (y == 9)
                        {
                            GameObject nodeE = Instantiate(node, tilesChild);
                            nodeE.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(73, 153));
                            nodeE.tag = "NodeEnter";

                            thisRoom[1] = nodeE.transform;

                            nodeE.GetComponent<storeRoomVars>().integer = roomNumber;
                        }
                    }

                    if (down == true)
                    {
                        if (y == 1)
                        {
                            GameObject nodeE = Instantiate(node, tilesChild);
                            nodeE.transform.Translate(new Vector2(x, y) * new Vector2(grid.cellSize.x, grid.cellSize.y) + new Vector2(0.5f, 0.5f) - new Vector2(73, 153));
                            nodeE.tag = "NodeEnter";

                            thisRoom[11] = nodeE.transform;

                            nodeE.GetComponent<storeRoomVars>().integer = roomNumber;
                        }
                    }
                }
            }

        }

        this.GetComponent<storeRoomVars>().integer = roomNumber;

        this.GetComponent<storeRoomVars>().upExit = up;
        this.GetComponent<storeRoomVars>().downExit = down;
        this.GetComponent<storeRoomVars>().leftExit = left;
        this.GetComponent<storeRoomVars>().rightExit = right;

        return thisRoom;

    }

    public Vector2 GetFullSize()
    {
        Grid grid = GetComponent<Grid>();
        return new Vector2(grid.cellSize.x * width, grid.cellSize.y * height);
    }
}
