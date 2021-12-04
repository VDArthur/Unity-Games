using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    private MovementCharacter player;
    private void Start()
    {
        player = FindObjectOfType<MovementCharacter>();
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LoadMenu()
    {
        PlayerPrefs.SetInt("Last_level_ID", SceneManager.GetActiveScene().buildIndex);
        Destroy(player.gameObject);
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void QuitMenu()
    {
        Application.Quit();
    }

}
