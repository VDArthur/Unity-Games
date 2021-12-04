using UnityEngine;

public class IdleMonster : UnitCollider
{
    private MovementCharacter movementCharacter;

    private void Start()
    {
        movementCharacter = FindObjectOfType<MovementCharacter>();
    }
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();
        if (bullet && bullet.Parent == movementCharacter.gameObject)
        {
            ReceivDamage();
        }

        MovementCharacter player = collider.GetComponent<MovementCharacter>();
        if (player)
        {
            player.ReceivDamage();
        }
    }
}
