using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private TextMeshProUGUI _lives;

    private void Awake()
    {
        _player.ScoreChanged += OnPlayerScoreChanged;
        _player.LivesChanged += OnPlayerLivesChanged;
        _score.text = _player.Score.ToString();
        _lives.text = _player.NumberOfLives.ToString();
    }

    private void OnPlayerScoreChanged()
    {
        _score.text = _player.Score.ToString();
    }

    private void OnPlayerLivesChanged()
    {
        _lives.text = _player.NumberOfLives.ToString();
    }
}
