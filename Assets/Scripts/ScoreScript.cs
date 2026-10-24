using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI ScoreTest;
    void Start()
    {
        if (LevelManager.levelManager.target == 0 || LevelManager.levelManager.Score < LevelManager.levelManager.target)
        {
            ScoreTest.SetText("Game Over\n Score: {0}", LevelManager.levelManager.Score);
        }
        else
        {
            ScoreTest.SetText("Level {0} Completed", LevelManager.levelManager.levelNumber);
        }
    }
    public void LoadScene(int idx)
    {
        LevelManager.levelManager.LoadScene(idx);
    }
}
