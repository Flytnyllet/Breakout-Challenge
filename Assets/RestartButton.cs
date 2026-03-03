using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public void RestartGame()
    {
        UIManager.Instance.isGameOver = false;
        UIManager.Instance.isGameWon = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
