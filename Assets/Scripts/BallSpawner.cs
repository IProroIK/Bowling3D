using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _player;
    [SerializeField] private GameC _gameC;
    [SerializeField] private Gate _gate;
    

    private void OnTriggerEnter(Collider other)
    {
        if(_gameC.balls.Count > _gate.CheckOperetion(_gameC.balls.Count))
        {
            BallsDestroy(_gate.CheckOperetion(_gameC.balls.Count), _gameC.balls);
        }
        else
        {
            SpawnPlayer(_gate.CheckOperetion(_gameC.balls.Count));
        }
        _gate.DeactivateSecondDoor();
        Destroy(this);
    }

    public void SpawnPlayer(int size)
    {
        for (int i = 0; i < size; i++)
        {
            GameObject newPlayer = Instantiate(_playerPrefab, PlayerPosition(), Quaternion.identity);
            newPlayer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            newPlayer.transform.DOScale(1, 0.2f);
            _gameC.balls.Add(newPlayer);
        }
    }
    private Vector3 PlayerPosition()
    {
        Vector3 pos = Random.insideUnitSphere * 5;
        Vector3 newPos = transform.position + pos;
        newPos.y = 0.5f;
        return newPos;
    }

    private void BallsDestroy(int amount, List<GameObject> balls)
    {
        for(int i = balls.Count - 1; i >= amount; i--)
        {
            Destroy(balls[i]);
        }
    }
}
