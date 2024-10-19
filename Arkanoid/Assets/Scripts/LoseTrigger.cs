using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    public Ball ball;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (HeartManager.Instance.heartsLeft > 0)
        {
            HeartManager.Instance.LoseHeart();
            ball.ResetBall();
        }
        else GameManager.Instance.GameOver();
    }
}
