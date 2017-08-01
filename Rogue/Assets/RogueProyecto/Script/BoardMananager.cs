using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMananager : MonoBehaviour
{
    public GameObject[] floorTiles, outerWallTiles, wallTiles, foodTiles, enemyTiles;//Losetas
    public GameObject exit;
    public int columns = 8;
    public int rows = 8;

    private List<Vector2> gridPosition = new List<Vector2>();
    private Transform boardHolder, titleHolder;

    void InitializeList()
    {
        gridPosition.Clear();
        for (int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows -1; y++)
                gridPosition.Add(new Vector2(x, y));
        }
    }

    Vector2 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPosition.Count);
        Vector2 randomPosition = gridPosition[randomIndex];
        gridPosition.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] titleArray, int min, int max)
    {
        titleHolder = new GameObject("Titles").transform;
        int objectCount = Random.Range(min, max + 1);
        for (int i = 0; i < objectCount; i++)
        {
            Vector2 randomPosition = RandomPosition();
            GameObject titleChoice = GetRandomInArray(titleArray);
            GameObject instanceT = Instantiate(titleChoice, randomPosition, Quaternion.identity);
            instanceT.transform.SetParent(titleHolder);
        }
    }

	public void SetupScene(int level)
    {
        BoardSetup();
        InitializeList();
        LayoutObjectAtRandom(wallTiles, 5, 9);
        LayoutObjectAtRandom(foodTiles, 1, 5);
        int enemyCount = (int)Mathf.Log(level, 2);
        LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, new Vector2(columns - 1, rows - 1), Quaternion.identity);
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = GetRandomInArray(floorTiles);

                if (x == -1 || y == -1 || x == columns || y == rows)
                    toInstantiate = GetRandomInArray(outerWallTiles); ;
                GameObject instance = Instantiate(toInstantiate, new Vector2(x, y), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    GameObject GetRandomInArray(GameObject[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}
