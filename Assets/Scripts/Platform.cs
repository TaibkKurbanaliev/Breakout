using System;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private int _health = 1;
    [SerializeField] private int _points = 1;

    public event Action<int> Died;

    public void Hit()
    {
        _health--;

        if (_health < 0 )
        {
            Died?.Invoke(_points);
            Destroy(gameObject);
        }
    }
}
