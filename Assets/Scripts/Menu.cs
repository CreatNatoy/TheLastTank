using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Text _textBestScore;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Score"))
            PlayerPrefs.SetInt("Score", 0);
        _textBestScore.text = "Best score: " + PlayerPrefs.GetInt("Score").ToString(); 
    }

    public void ButtonStart()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f; 
    }

    public void ButtonExit()
    {
        Application.Quit(); 
    }

    public void ButtonDeleteKeys()
    {
        PlayerPrefs.DeleteAll(); 
    }

    public void OnPause(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void ButtonPlay(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1f; 
    }

}
