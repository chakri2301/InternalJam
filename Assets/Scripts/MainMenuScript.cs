using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public void LoadScene(int idx)
    {
        LevelManager.levelManager.LoadScene(idx);
    }
}
