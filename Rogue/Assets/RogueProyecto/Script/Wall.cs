using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public Sprite dmgSprite;

    private SpriteRenderer spriteRenderer;  

    int hp = 4;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void DamageWall(int loss)
    {
        spriteRenderer.sprite = dmgSprite;
        hp -= loss;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
