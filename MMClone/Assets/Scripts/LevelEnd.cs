using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPlayerEntered;
    private void OnTriggerEnter2D(Collider2D col)
    {
        _onPlayerEntered?.Invoke();
        int index = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        print($"{SceneManager.GetActiveScene().buildIndex + 1} % {SceneManager.sceneCountInBuildSettings}");
        print(index);
        SceneManager.LoadScene(index);
    }
}
