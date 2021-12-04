using UnityEngine;

public class Unit : MonoBehaviour
{
    public virtual void ReceivDamage()
    {
        Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
