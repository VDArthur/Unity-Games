using UnityEngine;
using System.Collections;

public class ShootMonster : UnitCollider
{
    [SerializeField] private float rate = 2.0f;
    //Set the color of the bullets
    [SerializeField] private Color bulletColor = Color.white;
    [SerializeField] Transform player;

    private Bullet bullet;
    private SpriteRenderer sprite;

    private void Awake()
    {
        bullet = Resources.Load<Bullet>("Bullet/Bullet");
        sprite = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<Transform>(player);
    }

    void Start()
    {
        if (player != null)
        {
            StartCoroutine(Shoot());
        }

    }

    void Update()
    {

    }

    IEnumerator Shoot()
    {
        while (true)
        {
            Vector3 position = transform.position;
            Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
            newBullet.Parent = gameObject;
            newBullet.Color = bulletColor;

            sprite.flipX = player.position.x > position.x;

            if (player.position.x < position.x)
            {
                newBullet.Direction = -newBullet.transform.right;
            }
            else
            {
                newBullet.Direction = newBullet.transform.right;
            }

            yield return new WaitForSeconds(rate);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }
}
