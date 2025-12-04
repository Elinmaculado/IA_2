using System;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;
using Random = UnityEngine.Random;

public class CaveGenerator : MonoBehaviour
{
    [Header("Map Settings")]
    public int width = 64;
    public int height = 64;

    [Header("Cave Automata")] 
    public int smoothIteration = 3;

    [Header("Visualization")] 
    public GameObject wallPrefab;
    public GameObject floorPrefab;
    public float cellSize = 1;
    public float fillPercent = 45;

    private int[,] map;

    private void Start()
    {
        GenerateMap();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GenerateMap();
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            SmoothMap();
            RenderMap();
        }
    }

    private void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();
        for (int i = 0; i < smoothIteration; i++)
        {
            SmoothMap();
        }
        RenderMap();
    }

    private void RandomFillMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                {
                    map[x, y] = 1;
                }
                else
                {
                    map[x, y] = (Random.Range(0,100) < fillPercent) ? 1 : 0;
                }
            }
        }
    }

    private void SmoothMap()
    {
        int[,] newMap = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int neighborCount = GetSurroundingWallCount(x, y);

                if (neighborCount  > 4)
                {
                    newMap[x, y] = 1;
                }
                else if (neighborCount < 4)
                {
                    newMap[x, y] = 0;
                }
                else
                {
                    newMap[x, y] = map[x, y];
                }
            }
        }
        
        map = newMap;
    }

    private int GetSurroundingWallCount(int gridX, int gridY)
    {
        int wallCount = 0;
        for (int nX = gridX - 1; nX <= gridX + 1; nX++)
        {
            for (int nY = gridY - 1; nY <= gridY + 1; nY++)
            {
                if (nX >= 0 && nX < width && nY >= 0 && nY < height)
                {
                    if (nX != gridX || nY != gridY)
                    {
                        wallCount += map[nX, nY];
                    }
                }
                else
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }
    
    private void RenderMap()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x * cellSize, y * cellSize, 0);

                if (map[x, y] == 1)
                {
                    Instantiate(wallPrefab, pos, wallPrefab.transform.rotation, transform);
                }
                else
                {
                    Instantiate(floorPrefab, pos, floorPrefab.transform.rotation, transform);
                    
                }
            }
        }
        
    }
}
