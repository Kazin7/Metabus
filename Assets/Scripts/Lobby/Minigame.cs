using UnityEngine;
using UnityEngine.SceneManagement;

public class Minigame : MonoBehaviour
{
    [SerializeField] private string targetSceneName;

    private bool playerInTrigger = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && !string.IsNullOrEmpty(targetSceneName))
        {
            playerInTrigger = true;
            UIManager.Instance.ShowExplain(targetSceneName);
        }
    }
    public void StartMiniGame()
    {
        if (!string.IsNullOrEmpty(targetSceneName))
        {
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
