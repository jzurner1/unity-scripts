using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonGenerator : MonoBehaviour
{
    public Tilemap tilemap;
    public Tile floorTile;
    public Tile wallTile;

    public int dungeonWidth = 100;
    public int dungeonHeight = 100;
    public int minRoomSize = 5;
    public int maxLeafSize = 20;

    public RectInt testRoom = new RectInt(0, 0, 10, 10);
    void Start()
    {
        GenerateDungeon();
    }


    public void CreateRoomFromRectInt(RectInt room)
    {
        for (int x = room.xMin; x < room.xMax; x++)
        {
            for (int y = room.yMin; y < room.yMax; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), floorTile);
            }
        }

        for (int x = room.xMin - 1; x <= room.xMax; x++)
        {
            tilemap.SetTile(new Vector3Int(x, room.yMin - 1, 0), wallTile);
            tilemap.SetTile(new Vector3Int(x, room.yMax, 0), wallTile);
        }
        for (int y = room.yMin - 1; y <= room.yMax; y++)
        {
            tilemap.SetTile(new Vector3Int(room.xMin - 1, y, 0), wallTile);
            tilemap.SetTile(new Vector3Int(room.xMax, y, 0), wallTile);
        }
    }


    public void SplitNode(BSPNode node, int minRoomSize, int maxLeafSize)
    {
        if (node.IsLeaf())
        {
            bool splitH = Random.value > 0.5f;  // if random number over .5, split horizontally, otherwise split vertically
            if (node.bounds.width / node.bounds.height >= 1.25f)  // if too wide, split vertically
            {
                splitH = false;
            }
            else if (node.bounds.height / node.bounds.width >= 1.25f)  // if too tall, split horizontally
            {
                splitH = true;
            }

            int max = (splitH ? node.bounds.height : node.bounds.width) - minRoomSize;
            if (max <= minRoomSize)  // if room is too small to split further
            {
                return;
            }

            int split = Random.Range(minRoomSize, max);  // choose a split position
            if (splitH)
            {
                // Horizontal split
                node.leftChild = new BSPNode(new RectInt(node.bounds.x, node.bounds.y, node.bounds.width, split));
                node.rightChild = new BSPNode(new RectInt(node.bounds.x, node.bounds.y + split, node.bounds.width, node.bounds.height - split));
            }
            else
            {
                // Vertical split
                node.leftChild = new BSPNode(new RectInt(node.bounds.x, node.bounds.y, split, node.bounds.height));
                node.rightChild = new BSPNode(new RectInt(node.bounds.x + split, node.bounds.y, node.bounds.width - split, node.bounds.height));
            }

            SplitNode(node.leftChild, minRoomSize, maxLeafSize);
            SplitNode(node.rightChild, minRoomSize, maxLeafSize);
        }
    }

    void GenerateDungeon()
    {
        BSPNode root = new BSPNode(new RectInt(0, 0, dungeonWidth, dungeonHeight));
        SplitNode(root, minRoomSize, maxLeafSize);
        DrawDungeon(root);
    }

    void DrawDungeon(BSPNode node)
    {
        if (node == null) return;
        
        if (node.IsLeaf())
        {
            CreateRoomFromRectInt(node.bounds);
        } else
        {
            DrawDungeon(node.leftChild);
            DrawDungeon(node.rightChild);
        }
    }

}


public class BSPNode
{
    public BSPNode leftChild;
    public BSPNode rightChild;
    public RectInt bounds;
    public RectInt room;  // room within bounds

    public BSPNode(RectInt bounds)
    {
        this.bounds = bounds;
    }

    public bool IsLeaf() => leftChild == null && rightChild == null;

}

