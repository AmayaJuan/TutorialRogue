using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public LayerMask blockingLayer;
    public float moveTime = .1f;

    float movementSpeed;
    BoxCollider2D boxCollider;
    Rigidbody2D rb;

    protected virtual void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start ()
    {
        movementSpeed = 1f / moveTime;
	}

    protected IEnumerator SmoothMovement(Vector2 end)
    {
        float remainingDistance = Vector2.Distance(rb.position, end);

        while (remainingDistance > float.Epsilon)
        {
            Vector2 newPosition = Vector2.MoveTowards(rb.position, end, movementSpeed * Time.deltaTime);
            rb.MovePosition(newPosition);
            remainingDistance = Vector2.Distance(rb.position, end);
            yield return null;
        }
    }
	
	protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);
        boxCollider.enabled = true;

        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;
    }

    protected abstract void OnCantMove(GameObject go);

    protected virtual void AttempMove(int xDir, int yDir)
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);
        if (canMove)
            return;

        OnCantMove(hit.transform.gameObject);
    }
}
