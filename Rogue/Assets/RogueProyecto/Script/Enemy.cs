using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    public int playerDamage;

    private Animator animator;
    private Transform target;
    private bool skipMove;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        base.Awake();
    }

    protected  override void Start()
    {
        GameManager.instance.AddEnemyToList(this);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
    }

    protected override void AttemptMove(int xDir, int yDir)
    {
        if(skipMove)
        {
            skipMove = false;
            return;
        }
        base.AttemptMove(xDir, yDir);
        skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;
        if (Math.Abs(target.position.x - transform.position.x) < float.Epsilon)
            yDir = target.position.y > transform.position.y ? 1 : -1;
        else
            xDir = target.position.x > transform.position.y ? 1 : -1;
    
        AttemptMove(xDir, yDir);
    }

    protected override void OnCantMove(GameObject go)
    {
        Player hitPlayer = go.GetComponent<Player>();
        if (hitPlayer != null)
        {
            hitPlayer.LoseFood(playerDamage);
            animator.SetTrigger("EnemyAttack");
        }
    }
}
