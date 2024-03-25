using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    [Header("Grid Settings")]
    public int width = 10;
    public int height = 10;
    public float roomWidth = 16f;
    public float roomHeight = 16f;

    [Header("Room Prefabs")]
    public GameObject room1Entrance;
    public GameObject room2Entrances;
    public GameObject room3Entrances;
    public GameObject room4Entrances;

    private GameObject[,] dungeonGrid;

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        dungeonGrid = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject roomInstance = ChooseRandomRoom(x, y);
                dungeonGrid[x, y] = roomInstance;
            }
        }
    }

    GameObject ChooseRandomRoom(int x, int y)
    {
        Vector3 position = CalculateWorldPosition(x, y);
        GameObject roomPrefab = SelectRandomRoomPrefab();
        GameObject roomInstance = Instantiate(roomPrefab, position, Quaternion.identity);
        return roomInstance;
    }

    Vector3 CalculateWorldPosition(int gridX, int gridY)
    {
        float worldX = gridX * roomWidth;
        float worldY = gridY * roomHeight;
        return new Vector3(worldX, worldY, 0); // Assuming a 2D game on the XY plane
    }

    GameObject SelectRandomRoomPrefab()
    {
        int rand = Random.Range(1, 5); // Random.Range is inclusive for min, exclusive for max
        switch (rand)
        {
            case 1:
                return room1Entrance;
            case 2:
                return room2Entrances;
            case 3:
                return room3Entrances;
            case 4:
                return room4Entrances;
            default:
                return room1Entrance;
        }
    }
}
