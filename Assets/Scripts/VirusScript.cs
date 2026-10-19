using System.Collections;
using UnityEngine;

public class VirusScript : MonoBehaviour
{
    public bool isActive = false;
    public float teleportTime = 2f;
    public void TeleportToForm(CellScript from, CellScript to)
    {
        StartCoroutine(StartTeleport(from.transform.position, to.transform.position));
    }
    IEnumerator StartTeleport(Vector3 start, Vector3 end)
    {
        float timer = 0f;
        start += Vector3.forward; end += Vector3.forward;
        while (timer < teleportTime)
        {
            timer += teleportTime;
            transform.position = Vector3.Lerp(start, end, timer / teleportTime);
            yield return null;
        }
        yield break;
    }
}
