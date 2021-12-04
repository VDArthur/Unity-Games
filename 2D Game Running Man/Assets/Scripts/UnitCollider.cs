using UnityEngine;

public class UnitCollider : Unit
{
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();

        if (unit && unit is MovementCharacter)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < .7f)
            {
                Debug.Log(unit.gameObject.name);
                ReceivDamage();
            }
            else
            {
                unit.ReceivDamage();
            }
        }
    }
}
