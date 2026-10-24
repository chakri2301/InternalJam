using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;
    public int levelNumber = 0;
    public int Score = 0;
    public int target = 500;
    void Awake()
    {
        if (levelManager != null)
        {
            Destroy(gameObject);
            return;
        }
        levelManager = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(int next)
    {
        SceneManager.LoadScene(next);
    }
    public void SetScore(int score)
    {
        Score = score;
    }
}
