using System.Collections;
using UnityEngine;

public class VirusScript : MonoBehaviour
{
    public bool isActive = false;
    public float teleportTime = 2f;
    public delegate void VirusNotify();
    public event VirusNotify OnEnd;
    // void Start()
    // {
    //     gameObject.SetActive(false);
    // }
    public bool TeleportToForm(CellScript from, CellScript to, int count)
    {
        if (isActive)
        {
            return false;
        }
        Debug.Log("From " + from.Id + " to " + to.Id);
        gameObject.SetActive(true);
        StartCoroutine(StartTeleport(from.transform.position, to.transform.position, to, count));
        return true;
    }
    IEnumerator StartTeleport(Vector3 start, Vector3 end, CellScript to, int count)
    {
        float timer = 0f;
        start += Vector3.forward; end += Vector3.forward;
        isActive = true;
        while (timer < teleportTime)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, timer / teleportTime);
            yield return null;
        }
        isActive = false;
        gameObject.SetActive(false);
        OnEnd?.Invoke();
        to.IncrementVirii(count);
        yield break;
    }
}
