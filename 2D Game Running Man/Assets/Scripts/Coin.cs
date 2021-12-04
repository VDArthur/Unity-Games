using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    [SerializeField] private int numberCoins = 100;
    [SerializeField] private AudioSource coinsSound;

    private Text textComponent;
    private MovementCharacter player;

    private void Start()
    {
        textComponent = FindObjectOfType<Text>();
        coinsSound = GetComponent<AudioSource>();
    }

    IEnumerator OnTriggerEnter2D(Collider2D collider)
    {
        player = collider.GetComponent<MovementCharacter>();

        if (player)
        {
            player.SumCoins += numberCoins;
            textComponent.text = player.SumCoins.ToString();
            coinsSound.Play();
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;

            yield return new WaitForSeconds(coinsSound.clip.length);

            Destroy(gameObject);
        }
    }
}
