using UnityEngine;

public class Health : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        MovementCharacter player = collider.GetComponent<MovementCharacter>();
        if (player)
        {
            player.Lives++;
            Destroy(gameObject);
        }
    }
}
