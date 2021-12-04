using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverBar : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text text;

    private MovementCharacter player;
    void Start()
    {
        image.enabled = false;
        text.enabled = false;
        player = FindObjectOfType<MovementCharacter>();
    }

    void Update()
    {
        if (player.Lives <= 0)
        {
            image.enabled = true;
            text.enabled = true;
        }
    }
}
