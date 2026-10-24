using UnityEngine;

public class HelpScript : MonoBehaviour
{
    public void LoadScene(int num)
    {
        LevelManager.levelManager.LoadScene(num);
    }
}
