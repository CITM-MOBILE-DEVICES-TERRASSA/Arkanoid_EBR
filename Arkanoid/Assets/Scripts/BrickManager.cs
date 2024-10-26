using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    public static BrickManager Instance; // Crear al BrickManager como singleton para evitar tener varios

    public List<Brick> brickTypes;
    public GameObject powerUpBallPrefab; // Prefab de la pelota que caerá

    private int bricksLeft;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        bricksLeft = bricks.Length;

        foreach (GameObject brick in bricks)
        {
            RandomBrickType(brick);
        }
    }

    private void RandomBrickType(GameObject brick)
    {
        Brick randomBrick = brickTypes[Random.Range(0, brickTypes.Count)];
        Brick brickComponent = brick.GetComponent<Brick>();

        if (brickComponent != null)
        {
            brickComponent.health = randomBrick.health;

            SpriteRenderer spriteRenderer = brick.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = GetColorByHealth(brickComponent.health);
            }
        }
    }

    public void CheckColor(GameObject brick)
    {
        Brick brickComponent = brick.GetComponent<Brick>();
        SpriteRenderer spriteRenderer = brick.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = GetColorByHealth(brickComponent.health);
        }
    }

    private Color GetColorByHealth(int health)
    {
        if (health == 1)
        {
            return Color.white;
        }
        else if (health == 2)
        {
            return Color.yellow;
        }
        else if (health == 3)
        {
            return Color.cyan;
        }
        else if(health >= 4)
        {
            return Color.magenta;
        }
        return Color.white; // Color default
    }

    public void BrickDestroyed(GameObject brick)
    {
        bricksLeft--;
        ScoreManager.Instance.AddScore(5);

        // Aquí decides si se debe generar una pelota de poder
        if (Random.Range(0f, 1f) < 0.5f) // 50% de probabilidad de generar
        {
            SpawnPowerUpBall(brick.transform.position);
        }

        if (bricksLeft <= 0)
        {
            GameManager.Instance.LoadNextLevel(); // Si no quedan bloques carga el siguiente nivel
        }
    }

    private void SpawnPowerUpBall(Vector3 position)
    {
        Instantiate(powerUpBallPrefab, position, Quaternion.identity);
    }
}
