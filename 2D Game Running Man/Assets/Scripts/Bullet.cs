using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private SpriteRenderer sprite;

    //We get movement from the outside
    private Vector3 direction;
    public Vector3 Direction
    {
        set { direction = value; }
    }

    //Get the parent from the outside and return
    private GameObject parent;
    public GameObject Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    //We get the color from the outside
    public Color Color
    {
        set { sprite.color = value; }
    }

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        //Delete the bullet object every 1.5 sec.
        Destroy(gameObject, 1.5f);
    }

    void Update()
    {
        //Set bullet movement
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    /// <summary>
    /// Remove bullet when it hits an object (monster or player)
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit.gameObject != parent)
        {
            Destroy(gameObject);
        }
    }
}
