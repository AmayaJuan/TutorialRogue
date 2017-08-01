 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public BoardMananager boardScript;

    void Awake()
    {
        if (GameManager.instance == null)
            GameManager.instance = this;
        else if (GameManager.instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardMananager>();
    }

    void Start ()
    {
        InitGame();
    }

    void InitGame()
    {
        boardScript.SetupScene(3);
    }
}
