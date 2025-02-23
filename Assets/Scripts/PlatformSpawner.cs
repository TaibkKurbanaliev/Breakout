using System;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private int _rows;
    [SerializeField] private int _columns;
    [SerializeField] private float _xOffset;
    [SerializeField] private float _yOffset;
    [SerializeField] private Platform _prefab;

    public event Action<int> PlatformDestroyed;

    private void Start()
    {
        for (int i = 0; i < _columns; i++)
        {
            for (int j = 0; j < _rows; j++)
            {
                var pos = transform.position;
                var platform = Instantiate(_prefab, new Vector3(pos.x + _xOffset * j, pos.y - _yOffset * i), _prefab.transform.rotation);
                platform.Died += PlatformDied;
            }
        }
    }

    private void PlatformDied(int points)
    {
        PlatformDestroyed?.Invoke(points);
    }
}
