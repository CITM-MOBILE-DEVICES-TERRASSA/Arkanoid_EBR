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

    public GameObject extraBallPrefab;

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
            isAutoPlaying = !isAutoPlaying; // Cambiar el estado de isAutoPlaying
        }

        PlayerMove();
    }

    private void PlayerMove()
    {
        if (isAutoPlaying && ball != null)
        {
            // Mover automáticamente hacia la bola
            moveInput = ball.transform.position.x > transform.position.x ? 1 : -1;
        }
        else if (Input.touchCount > 0) // Detectar toques en pantalla (para móvil)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            // Comparar la posición del toque con la del jugador para determinar la dirección
            if (touchPosition.x > transform.position.x)
            {
                moveInput = 1; // Mover hacia la derecha
            }
            else if (touchPosition.x < transform.position.x)
            {
                moveInput = -1; // Mover hacia la izquierda
            }
        }
        else
        {
            // Teclas de dirección en caso de que esté en computadora
            moveInput = Input.GetAxisRaw("Horizontal");
        }

        // Actualizar la posición del jugador
        Vector2 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x + (moveInput * moveSpeed * Time.deltaTime), -bounds, bounds);
        transform.position = playerPosition;
    }

    public void CreateExtraBall()
    {
        // Crea la nueva pelota justo encima del jugador
        Vector3 ballPosition = transform.position + new Vector3(0f, 0.5f, 0f);
        Instantiate(extraBallPrefab, ballPosition, Quaternion.identity);
    }
}