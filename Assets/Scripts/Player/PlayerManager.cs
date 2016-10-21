using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager _instance;
    public static PlayerManager instance { get { return _instance; } }

    [SerializeField]
    GameObject _playerPrefab;

    [SerializeField]
    int _maxAmountOfPlayers = -1;

    private int _currentAmountOfPlayers { get { return _currentPlayers.Count; } }
    private List<GameObject> _currentPlayers = new List<GameObject>(); 
    
    //TODO Should have event of all players (all player reset, all player remove and the like)

    void Awake()
    {
        _instance = this;
    }

    public GameObject GetNewPlayer()
    {
        GameObject newPlayer;
        if (_maxAmountOfPlayers <= -1 || _currentAmountOfPlayers < _maxAmountOfPlayers)
        {
            newPlayer = Instantiate(_playerPrefab);

        }
        else
        {
            return null;
        }
        _currentPlayers.Add(newPlayer);
        newPlayer.name = "Player" + _currentAmountOfPlayers;
        return newPlayer;
    }
}
