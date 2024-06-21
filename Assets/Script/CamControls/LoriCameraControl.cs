using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CLoriCameraControl : MonoBehaviour
{
    // Sensitivity of mouse movement
    public float mouseSensitivity = 100f;

    // Reference to the character GameObject
    public GameObject character;

    // Distance from the character
    public float distanceFromCharacter = 12f; // Adjust this value to change the distance

    // Height offset from the character
    public float heightOffset = 5f; // Adjust this value to change the height offset

    // Reference to the RawImage UI element
    public RawImage rawImage;

    // Variables to store camera rotation
    private float yaw = 0f;

    void Update()
    {
        // Check if the mouse is over the RawImage
        bool isMouseOverUI = EventSystem.current.IsPointerOverGameObject();

        if (!isMouseOverUI)
        {
            // Get mouse input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            // Calculate camera rotation
            yaw += mouseX;

            // Rotate the camera around the character
            Quaternion rotation = Quaternion.Euler(0f, yaw, 0f);
            Vector3 position = character.transform.position - (rotation * Vector3.forward * distanceFromCharacter);
            position.y = character.transform.position.y + heightOffset; // Adjust camera height
            transform.position = position;
            transform.LookAt(character.transform.position + Vector3.up * heightOffset); // Look at character's center
        }
    }
}
