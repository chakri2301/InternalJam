using UnityEngine;

public class ImmuneCellScript : MonoBehaviour
{
    Vector3 startPos = new Vector3();
    Vector3 targetPos;
    public float time = 2f;
    float timer = 0f;
    CellScript target;
    public Vector3 offset;
    void Start()
    {
        target = GameManager.gameManager.GetRandomCell();
        //startPos = target.transform.position + (Vector3)Random.insideUnitCircle * 7f;
        startPos = Camera.main.transform.position  + offset - 3* Vector3.forward;
        targetPos = target.transform.position;
        transform.position = startPos;
    }
    void Update()
    {
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(startPos, targetPos, timer / time);
        if (timer > time)
        {
            //Destroy(target.gameObject);
            GameManager.gameManager.DeleteCell(target);
            Destroy(gameObject);
        }
    }
}
