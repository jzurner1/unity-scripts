using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
To use, create `Rotation` and `ResetRotation` keybinds
Attach to a player object, generally with camera attached
*/

public class BasicPlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    public float playerSpeed = 5.0f;

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
		
        // Construct a movement vector that's relative to the character's orientation but constrained to the XZ plane
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f).normalized * playerSpeed * Time.deltaTime;
        movement = Quaternion.Euler(0, transform.eulerAngles.y, 0) * (transform.TransformDirection(movement)); // Ensure movement is aligned with the character's rotation on the Y axis
		
        transform.Translate(movement, Space.World);
		
        // Rotation
        float rotationInput = Input.GetAxisRaw("Rotation");
        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
		
        transform.Rotate(0, 0, rotationAmount, Space.World);
		
        // Reset rotation
        float rotationResetInput = Input.GetAxisRaw("ResetRotation");
        if (rotationResetInput != 0)
        {
            Vector3 newRotation = new Vector3(0, 0, 0); // Reset to default rotation. Adjust if your default rotation differs.
            transform.eulerAngles = newRotation;
        }
    }
}
