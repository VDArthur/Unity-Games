using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource menuSource;

    [SerializeField] private Button buttonContinue;

    private void Start()
    {
        menuSource = GetComponent<AudioSource>();
        buttonContinue = GetComponentInChildren<Button>();
        int id = PlayerPrefs.GetInt("Last_level_ID", 0);

        if (id != 0)
        {
            buttonContinue.gameObject.SetActive(true);
        }
        else
        {
            buttonContinue.gameObject.SetActive(false);
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Last_level_ID"));
        Time.timeScale = 1f;
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        menuSource.Stop();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
