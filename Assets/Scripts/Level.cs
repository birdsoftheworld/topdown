using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("Map settings")]
    [Tooltip("The size roughly determines how many rooms will be generated per level")]
    public int mapSize;
    [Tooltip("The map rooms are all the rooms that can be generated on a floor")]
    public List<GameObject> mapRooms;
    [Tooltip("Whether the map will generate as a straight line or a more complex shape")]
    public bool straightLine;

    [Header("Tiles")]
    public GameObject floorTile;
    public GameObject wallTile;

    private bool[][] occupiedRooms;

    private void Start()
    {
        occupiedRooms = new bool[mapSize * 2 + 1][];
        for(int i = 0; i < occupiedRooms.Length; i++)
        {
            occupiedRooms[i] = new bool[mapSize * 2 + 1];
        }

        Vector2 position = new Vector2(mapSize, mapSize);

        int nRooms = 2 * mapSize;
        for(int i = 0; i < nRooms; i++)
        {
            List<Vector2> directions = new List<Vector2>();

            int x = (int)position.x;
            int y = (int)position.y;

            occupiedRooms[x][y] = true;

            Vector2[] dirs = {
                Vector2.up, Vector2.down, Vector2.left, Vector2.right
            };

            foreach(Vector2 dir in dirs)
            {
                Vector2 offset = position + dir;
                if(IsValid(offset) && !IsRoom(offset) && GetNeighbors(offset) < (straightLine ? 2 : 3))
                {
                    directions.Add(dir);
                }
            }

            if(directions.Count <= 0)
            {
                break;
            }

            position += directions[Random.Range(0, directions.Count)];
        }

        GenerateRooms();
    }

    private void GenerateRooms()
    {
        Vector2 origin = new Vector2(mapSize, mapSize);
        for(int x = 0; x < occupiedRooms.Length; x++)
        {
            for(int y = 0; y < occupiedRooms[0].Length; y++)
            {
                Vector2 pos = new Vector2(x, y);
                if(!occupiedRooms[x][y])
                {
                    continue;
                }

                Vector2[] dirs = {
                    Vector2.up, Vector2.down, Vector2.left, Vector2.right
                };

                bool[] doors = new bool[4];

                for (int i = 0; i < dirs.Length; i++)
                {
                    Vector2 dir = dirs[i];
                    Vector2 offset = pos + dir;
                    if (IsValid(offset) && IsRoom(offset))
                    {
                        doors[i] = true;
                    } else
                    {
                        doors[i] = false;
                    }
                }

                List<GameObject> possible = PossibleRooms(doors);

                GameObject randomRoom = possible[Random.Range(0, possible.Count)];
                GameObject instance = Instantiate(randomRoom, transform);
                Room room = instance.GetComponent<Room>();
                room.SetLevel(this);

                room.GenerateWalls(doors[0], doors[1], doors[2], doors[3]);

                room.transform.Translate(room.GetFullSize() * (pos - origin));
            }
        }
    }

    private List<GameObject> PossibleRooms(bool[] doors)
    {
        List<GameObject> possible = new List<GameObject>();

        foreach(GameObject obj in mapRooms)
        {
            Room room = obj.GetComponent<Room>();
            bool[] needed = { room.upExit, room.downExit, room.leftExit, room.rightExit };

            bool invalid = false;
            for (int i = 0; i < needed.Length; i++)
            {
                if (!needed[i] && doors[i])
                {
                    invalid = true;
                    break;
                }
            }

            if(!invalid)
            {
                possible.Add(obj);
            }
        }

        return possible;
    }

    private bool IsValid(Vector2 location)
    {
        return location.x >= 0 && location.y >= 0 && location.x < occupiedRooms.Length && location.y < occupiedRooms.Length;
    }

    private bool IsRoom(Vector2 location)
    {
        return occupiedRooms[(int)location.x][(int)location.y];
    }

    private int GetNeighbors(Vector2 location)
    {
        Vector2[] dirs = {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right
        };

        int neighbors = 0;
        foreach (Vector2 dir in dirs)
        {
            Vector2 offset = location + dir;
            if (!IsValid(offset) || IsRoom(offset))
            {
                neighbors++;
            }
        }

        return neighbors;
    }

    private bool GetNeighborsAreSurrounded(Vector2 location)
    {
        Vector2[] dirs = {
            Vector2.up, Vector2.down, Vector2.left, Vector2.right
        };

        foreach (Vector2 dir in dirs)
        {
            Vector2 offset = location + dir;
            if (IsValid(offset) && GetNeighbors(offset) == 3)
            {
                return true;
            }
        }
        return false;
    }
}
