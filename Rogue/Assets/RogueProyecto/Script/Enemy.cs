using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    public int playerDamage;

    bool skipMove;
    Transform target;
    Animator animator;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        base.Awake();
    }

    protected override void Start ()
    {
        GameManager.instance.AddEnemyToList(this);
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
	}

    protected override void AttempMove(int xDir, int yDir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }
        base.AttempMove(xDir, yDir);
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
        AttempMove(xDir, yDir);
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
