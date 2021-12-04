using UnityEngine;

public class LivesBar : MonoBehaviour
{
    Transform[] hearts = new Transform[5];

    private MovementCharacter player;

    private void Awake()
    {
        player = FindObjectOfType<MovementCharacter>();
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i] = transform.GetChild(i);
        }
    }

    public void Refresh()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.Lives)
            {
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                hearts[i].gameObject.SetActive(false);
            }
        }
    }
}
