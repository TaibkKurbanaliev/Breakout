using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    [SerializeField] TextMeshProUGUI _score;

    private void Start()
    {
        _score.text = PlayerPrefs.HasKey(HighScoreKey) ? PlayerPrefs.GetInt(HighScoreKey).ToString() : "0";
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene(1);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
