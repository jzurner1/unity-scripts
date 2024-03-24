using UnityEngine;

/*
To use, create `Rotation` and `ResetRotation` keybinds
Attach to a player object, generally with camera attached
*/

public class PlayerMovement : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    public float playerSpeed = 5.0f;

    public static float CurrentRotationZ { get; private set; }

    void Update()
    {
        ProcessMovement();
        ProcessRotation();
        ProcessRotationReset();
    }

    void ProcessMovement()  // check for movement input
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0.0f).normalized * playerSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);
    }

    void ProcessRotation()  // check for rotation input
    {
        float rotationInput = Input.GetAxisRaw("Rotation");
        float rotationAmount = rotationInput * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, -rotationAmount);
        CurrentRotationZ = transform.eulerAngles.z;
    }

    void ProcessRotationReset()  // rotation reset key
    {
        float rotationResetInput = Input.GetAxisRaw("ResetRotation");
        if (rotationResetInput != 0)
        {
            Vector3 newRotation = new Vector3(0, 0, 0);
            transform.eulerAngles = newRotation;
        }
    }
}
