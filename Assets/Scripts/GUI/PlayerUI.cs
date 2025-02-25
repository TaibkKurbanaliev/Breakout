using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _lives;
    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _endGame;

    private void Awake()
    {
        _player.ScoreChanged += OnPlayerScoreChanged;
        _player.LivesChanged += OnPlayerLivesChanged;
        _score.text = _player.Score.ToString();
        _lives.text = _player.NumberOfLives.ToString();
        _menu.SetActive(false);
        _endGame.SetActive(false);
        _pauseButton.SetActive(true);
    }

    private void OnPlayerScoreChanged()
    {
        _score.text = _player.Score.ToString();
    }

    private void OnPlayerLivesChanged()
    {
        _lives.text = _player.NumberOfLives.ToString();

        if (_player.NumberOfLives == 0)
            ShowEndGame();
    }

    public void OnPauseClick()
    {
        _menu.SetActive(true);
        _pauseButton.SetActive(false);
        Time.timeScale = 0.0f;
    }

    public void OnPlayClick()
    {
        _menu.SetActive(false);
        _pauseButton.SetActive(true);
        Time.timeScale = 1.0f;
    }

    public void OnRestartClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(1);
    }

    public void OnExitClick()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    private void ShowEndGame()
    {
        _pauseButton.SetActive(false);
        _endGame.SetActive(true);
        Time.timeScale = 0f;
    }
}
