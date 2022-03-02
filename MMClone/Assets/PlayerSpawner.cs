using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    private int _nextIndex;
    [SerializeField] private GameObject[] _playerPrefabs;

    public void CreateNextPlayer()
    {
        GameObject go = Instantiate(_playerPrefabs[_nextIndex++], transform.position, Quaternion.identity);
    }
}
