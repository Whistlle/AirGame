using UnityEngine;
using System.Collections;

public class LevelEdge : MonoBehaviour 
{
	//Called when something triggerer the object
	void OnTriggerEnter2D(Collider2D other) 
	{
		//If a spawn triggerer is collided with this
		if (other.name == "SpawnTriggerer")
		{
			//Spawn a proper object
			switch (other.tag)
			{	
				case "Obstacles":
                    GameGenerator.Instance.GenerateObstacles();
					break;
			}
		}
		//If a reset triggerer is collided with this
		else if (other.name == "ResetTriggerer")
		{
			//Reset the proper object
			switch (other.tag)
			{	
				case "Obstacles":
					other.transform.parent.GetComponent<Obstacles>().DeactivateChild();
                 //   PlayerManager.Instance.IsBlockedByObstacles = true;
                    GameGenerator.Instance.SleepGameObject(other.transform.parent.gameObject);
					break;
			}
		}
        else if(other.tag == "Player")
        {
            //GameOver
            GameManager.Instance.GameOver(GameOverType.OutOfEdge);
        }
	}
}
