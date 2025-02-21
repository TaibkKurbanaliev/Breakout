using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private LayerMask _playerMask;

    private Rigidbody2D _rb;
    private Vector2 _targetDir = Vector2.down;
    private bool _isMoving = false;
    private Player _player;

    public event Action Died;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = FindAnyObjectByType<Player>();
    }

    public void FixedUpdate()
    {
        if (!_isMoving)
            return;

        Move();
    }

    private void Move()
    {
        var currentPos = new Vector2(transform.position.x, transform.position.y);
        _rb.MovePosition(currentPos + _targetDir * _speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((1 << collision.gameObject.layer & _playerMask) != 0)
        {
            _targetDir = Vector2.Reflect(_targetDir, collision.relativeVelocity).normalized;
            return;
        }

        _targetDir = Vector2.Reflect(_targetDir, new Vector2(collision.transform.right.x * Mathf.Sign(_rb.linearVelocityX),
                                                             collision.transform.right.y * Mathf.Sign(_rb.linearVelocityY))).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player.DecreaseLive();
        Died?.Invoke();
        Destroy(gameObject);
    }

    public void EnableMoving()
    {
        _isMoving = true;
    }
}
