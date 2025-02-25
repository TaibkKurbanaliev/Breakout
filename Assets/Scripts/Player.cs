using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private const string HighScoreKey = "HighScore";

    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private PlatformSpawner _spawner;
    [field:SerializeField] public int Score {  get; private set; }
    [field: SerializeField] public int NumberOfLives { get; private set; } = 3;

    private Rigidbody2D _rb;

    public event Action ScoreChanged;
    public event Action LivesChanged;

    public bool IsDied => NumberOfLives == 0;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _spawner.PlatformDestroyed += OnPlatformDestroyed;
    }

    private void OnDisable()
    {
        _spawner.PlatformDestroyed -= OnPlatformDestroyed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var input = Input.GetAxis("Horizontal");
       
        var currentPos = new Vector2(transform.position.x, transform.position.y);
        _rb.MovePosition(currentPos + Vector2.right * _speed * input * Time.fixedDeltaTime);
    }

    public void DecreaseLive()
    {
        NumberOfLives--;
        LivesChanged?.Invoke();

        if (IsDied)
        {
            if (PlayerPrefs.HasKey(HighScoreKey))
            {
                var previousHighScore = PlayerPrefs.GetInt(HighScoreKey);
                if (previousHighScore < Score)
                {
                    PlayerPrefs.SetInt(HighScoreKey, Score);
                }
            }
            else
            {
                PlayerPrefs.SetInt(HighScoreKey, Score);
            }

            PlayerPrefs.Save();
            return;
        }
    }
    
    private void OnPlatformDestroyed(int points)
    {
        Score += points;
        ScoreChanged?.Invoke();
    }
}
