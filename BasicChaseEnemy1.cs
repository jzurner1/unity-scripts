using UnityEngine;

public class BasicChaseAI : BaseEnemy
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component missing from object.");
        }

        // currently this script inherits from another script that has a MovementSpeed value. If you don't want to inherit, uncomment the next line and make sure you change BaseEnemy (in the class) to MonoBehaviour
        // float MovementSpeed = 5f;
    }

    void Update()
    {
        AlignRotationWithPlayer();
        RepelFromOtherEntities();  // see BasicEntityRepulsion1.cs
        ChasePlayer();
    }

    void AlignRotationWithPlayer()  // rotate to match player
    {
        transform.rotation = Quaternion.Euler(0, 0, PlayerMovement.CurrentRotationZ);
    }

    void ChasePlayer()  // locate player and call MoveTowards
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            MoveTowards(playerObject.transform.position);
        }
        else
        {
            Debug.LogWarning("Could not locate player! Make sure they have the tag 'Player'.");
        }
    }

    void MoveTowards(Vector3 targetPosition)  // actual movement
    {
        if (rb == null) return;

        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x, direction.y) * MovementSpeed;
    }
}
