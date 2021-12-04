using UnityEngine;

public class MovementMonster : UnitCollider
{
    [SerializeField] private float speed = 2f;

    private Vector3 direction;

    void Start()
    {
        direction = transform.right;
    }

    void Update()
    {
        Move();
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }

    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + direction + (-transform.up * 0.6f), .2f);

        if (colliders.Length < 1)
        {
            direction *= -1f;
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
}
