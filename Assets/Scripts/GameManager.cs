using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Ball _ballPrefab;

    private Ball _currentBall;
    private void Start()
    {
        _currentBall = Instantiate(_ballPrefab);
        _currentBall.Died += OnBallDied;
    }

    private void OnDisable()
    {
        _currentBall.Died -= OnBallDied;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _currentBall.EnableMoving();
        }

    }

    private void OnBallDied()
    {
        if (_player.IsDied)
            return;

        _currentBall = Instantiate(_ballPrefab);
        _currentBall.Died += OnBallDied;
    }
}
