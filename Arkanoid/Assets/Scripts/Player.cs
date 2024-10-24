using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    [SerializeField] private float moveSpeed;

    private float bounds = 6.5f;
    private bool isAutoPlaying = false;
    private Ball ball;
    private float moveInput;

    public GameObject extraBallPrefab; // Prefab de la pelota a disparar


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        ball = FindObjectOfType<Ball>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            isAutoPlaying = !isAutoPlaying;                                                                                 // Cambiar el estado de isAutoPlaying
        }

        PlayerMove();
    }

    private void PlayerMove()
    {
        if (isAutoPlaying && ball != null)
        {
            moveInput = ball.transform.position.x > transform.position.x ? 1 : -1;                                          // Calcular la dirección hacia la bola
        }
        else
        {
            moveInput = Input.GetAxisRaw("Horizontal");
        }

        Vector2 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x + (moveInput * moveSpeed * Time.deltaTime), -bounds, bounds);
        transform.position = playerPosition;
    }

    public void CreateExtraBall()
    {
        // Crea la nueva pelota justo encima del jugador
        Vector3 ballPosition = transform.position + new Vector3(0f, 0.5f, 0f);
        Instantiate(extraBallPrefab, ballPosition, Quaternion.identity); // Crea una nueva pelota
    }
}
