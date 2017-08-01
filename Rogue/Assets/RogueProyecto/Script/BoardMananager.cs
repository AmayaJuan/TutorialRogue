using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMananager : MonoBehaviour
{
    public GameObject[] floorTiles, outerWallTiles, wallTiles, foodTiles, enemyTiles;//Losetas
    public int columns = 8;
    public int rows = 8;

    private Transform boardHolder;

	public void SetupScene()
    {
        BoardSetup();
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
