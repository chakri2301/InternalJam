using UnityEngine;

public class LevelSelectScript : MonoBehaviour
{
    public void SelectLevel(int idx)
    {
        LevelManager.levelManager.target = idx * 500;
        LevelManager.levelManager.levelNumber = idx;
        LevelManager.levelManager.LoadScene(3);
    }
    public void LoadScene(int idx)
    {
        LevelManager.levelManager.LoadScene(idx);
    }
}
