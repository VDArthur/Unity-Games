using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    private MovementCharacter target;

    private void Start()
    {
        target = FindObjectOfType<MovementCharacter>();
    }
    void Update()
    {
        if (target == null)
        {
            transform.position = transform.position;
        }
        else
        {
            UpdateCameraPosition();
        }

    }

    // Changing the position of the camera on the screen
    void UpdateCameraPosition()
    {
        transform.position = new Vector3(
            // The position of the game object behind which we are moving
            Mathf.Clamp(target.transform.position.x, minX, maxX),
            Mathf.Clamp(target.transform.position.y, minY, maxY), transform.position.z);
    }
}