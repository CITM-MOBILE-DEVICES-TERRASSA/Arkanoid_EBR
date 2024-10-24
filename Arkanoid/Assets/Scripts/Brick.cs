using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int health = 1;

    public Color brickColor = Color.white;

    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = brickColor;
    }

    public void TakeDamage()
    {
        health--;
        AudioManager.Instance.PlaySFX(1);

        if (health <= 0)
        {
            Destroy(gameObject);
            BrickManager.Instance.BrickDestroyed(gameObject);
        }
        else BrickManager.Instance.CheckColor(gameObject);
    }
}
