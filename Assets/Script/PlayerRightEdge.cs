using UnityEngine;
using System.Collections;

public class PlayerRightEdge : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other)
    {
        if(PlayerManager.Instance.IsStartRecovering
       &&  other.tag == "Ob")
            PlayerManager.Instance.IsBlockedByObstacles = true;
    }
}
