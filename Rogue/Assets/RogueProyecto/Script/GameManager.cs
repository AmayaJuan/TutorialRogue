 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardMananager boardScript;

    void Awake()
    {
        boardScript = GetComponent<BoardMananager>();
    }

    void Start ()
    {
        InitGame();
    }

    void InitGame()
    {
        boardScript.SetupScene();
    }
}
