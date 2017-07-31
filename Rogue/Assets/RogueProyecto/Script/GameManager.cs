using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;

    void Awake()
    {
        boardScript = GetComponent<BoardManager>();
    }

    void Start ()
    {
        InitGame();
    }
	
    void InitGame()
    {
        boardScript.SetupScene();
    }
	
	void Update ()
    {
		
	}
}
