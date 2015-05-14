using UnityEngine;
using System.Collections;

public class PlayerTopEgde : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ob")
            GameManager.Instance.GameOver(GameOverType.Obstacle);
    }
}
