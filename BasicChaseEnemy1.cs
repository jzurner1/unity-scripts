using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicChaseAI : BaseEnemy
{
    void Start()
    {
        // currently this script inherits from another script that has a MovementSpeed value. If you don't want to inherit, uncomment the next line and make sure you change BaseEnemy (in the class) to MonoBehaviour
        // float MovementSpeed = 5f;
    }

    void Update()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");  // get the player
        if (playerObject != null)
        {
            Vector3 playerPosition = playerObject.transform.position;
            Vector3 direction = (playerPosition - transform.position).normalized;  // vector in player's direction with magnitude 1

            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = new Vector2(direction.x, direction.y) * MovementSpeed;
            }
            else
            {
                Debug.LogError("This object does not have a Rigidbody2D!");
            }
        }
        else
        {
            Debug.Log("Could not locate player! Make sure they have the tag 'Player'!");
        }
    }
}
