using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MovingObject
{
    public Text foodText;
    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    public float restartLevelDelay = 1f;

    private int food;
    private Animator animator;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        base.Awake();
    }

    protected override void Start()
    {
        food = GameManager.instance.playerFoodPoints;
        foodText.text = "Food: " + food;
        base.Start();
    }

    void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;
    }

    void CheckIfGameOver()
    {
        if (food <= 0)
            GameManager.instance.GameOver();
    }

    protected override void AttemptMove(int xDir, int yDir)
    {
        food--;
        foodText.text = "Food: " + food;
        base.AttemptMove(xDir, yDir);
        CheckIfGameOver();
        GameManager.instance.playersTurn = false;
    }

    void Update()
    {
        if (!GameManager.instance.playersTurn || GameManager.instance.doingSetup)
            return;

        int horizontal;
        int vertical;
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");
        if (horizontal != 0)
            vertical = 0;
        if (horizontal != 0 || vertical != 0)
            AttemptMove(horizontal, vertical);
    }

    protected override void OnCantMove(GameObject go)
    {
        Wall hitWall = go.GetComponent<Wall>();
        if (hitWall != null)
        {
            hitWall.DamageWall(wallDamage);
            animator.SetTrigger("PlayerChop");
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoseFood(int loss)
    {
        food -= loss;
        foodText.text = "-" + loss + " Food: " + food;
        animator.SetTrigger("PlayerHit");
        CheckIfGameOver();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Exit"))
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.CompareTag("Food"))
        {
            food += pointsPerFood;
            foodText.text = "+" + pointsPerFood + " Food: " + food;
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Soda"))
        {
            food += pointsPerSoda;
            foodText.text = "+" + pointsPerSoda + " Food: " + food;
            other.gameObject.SetActive(false);
        }
    }
}
