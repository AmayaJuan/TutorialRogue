using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [HideInInspector]public bool playersTurn = true;
    public bool doingSetup;
    public BoardMananager boardScript;
    public int playerFoodPoints = 100;
    public float turnDelay = .1f;
    public float levelStartDelay = 2f;

    private GameObject levelImage;
    private Text levelText;
    private bool enemiesMoving;
    private List<Enemy> enemies = new List<Enemy>();
    private int level = 0;

    void Awake()
    {
        if (GameManager.instance == null)
            GameManager.instance = this;
        else if (GameManager.instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardMananager>();
    }

    void InitGame()
    {
        doingSetup = true;
        levelImage = GameObject.Find("LevelImage");
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        levelText.text = "Day " + level;
        levelImage.SetActive(true);
        enemies.Clear();
        boardScript.SetupScene(level);
        Invoke("HideLevelImage", levelStartDelay);
    }

    void HideLevelImage()
    {
        levelImage.SetActive(false);
        doingSetup = false;
    }

    public void GameOver()
    {
        levelText.text = "After" + level + " days, yoou starved.";
        levelImage.SetActive(true);
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

    void Update()
    {
        if (playersTurn || enemiesMoving || doingSetup)
            return;
        StartCoroutine(MoveEnemies());
    }

    public void AddEnemyToList(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        level++;
        InitGame();
    }
}
