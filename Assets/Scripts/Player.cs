using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
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
        if (IsDied)
        {
            return;
        }

        NumberOfLives--;
        LivesChanged?.Invoke();
    }
}
