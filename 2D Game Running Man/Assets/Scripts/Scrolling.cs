using UnityEngine;

public class Scrolling : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private Vector3 lastCameraPosition = Vector3.zero;

    [SerializeField] private float speed;
    [SerializeField] private float offsetX;
    private float direction;

    void Start()
    {
        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        direction = (lastCameraPosition.x - cameraTransform.position.x) * speed * Time.deltaTime;

        transform.position = new Vector2(transform.position.x + direction, transform.position.y);

        if (transform.position.x - cameraTransform.position.x < -offsetX)
        {
            transform.position = new Vector2(cameraTransform.position.x + offsetX, transform.position.y);
        }
        else if (transform.position.x - cameraTransform.position.x > offsetX)
        {
            transform.position = new Vector2(cameraTransform.position.x - offsetX, transform.position.y);
        }
        lastCameraPosition = cameraTransform.position;
    }
}
