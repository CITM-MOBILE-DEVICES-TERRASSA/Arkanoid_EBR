using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private float bounds = 6.5f;


    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        Vector2 playerPosition = transform.position;
        playerPosition.x = Mathf.Clamp(playerPosition.x + (moveInput * moveSpeed * Time.deltaTime), -bounds, bounds);
        transform.position = playerPosition;
    }
}
