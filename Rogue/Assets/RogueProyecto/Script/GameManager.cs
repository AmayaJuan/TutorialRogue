using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public BoardManager boardScript;
    [HideInInspector] public bool playersTurn = true;
    public int playerFoodPoints = 100;
    public float turnDelay = .1f;

    bool enemiesMoving;
    List<Enemy> enemies = new List<Enemy>();

    void Awake()
    {
        if (GameManager.instance == null)
            GameManager.instance = this;
        else if (GameManager.instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
    }

    void Start ()
    {
        InitGame();
    }

    void Update()
    {
        if (playersTurn || enemiesMoving)
            return;

        StartCoroutine(MoveEnemies());
    }

    void InitGame()
    {
        enemies.Clear();
        boardScript.SetupScene(3);
    }
	
	public void GameOver()
    {
        enabled = false;
    }

    IEnumerator MoveEnemies()
    {
        enemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);

        if (enemies.Count == 0)
            yield return new WaitForSeconds(turnDelay);

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MoveEnemy();
            yield return new WaitForSeconds(enemies[i].moveTime);
        }
        playersTurn = true;
        enemiesMoving = false;
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }
}
