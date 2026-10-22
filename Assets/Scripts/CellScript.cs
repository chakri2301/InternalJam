using TMPro;
using UnityEngine;

public class CellScript : MonoBehaviour
{
    public static int counter = 0;
    public int Id
    {
        get;
        private set;
    }
    int intViriiCount = 50;
    float viriiCount = 50f;
    public float multiplySpeed = 1f;
    public float normalMultiplyRate = 1.2f;
    float currentMultiplyRate = 1f;
    public int maxVirii = 500;
    public TextMeshPro countText;
    void Awake()
    {
        currentMultiplyRate = normalMultiplyRate;
    }
    void Start()
    {
        Id = counter;
        counter++;
        InvokeRepeating(nameof(ViriiTick), 2, 2);
    }
    void ViriiTick()
    {
        viriiCount += viriiCount * multiplySpeed * currentMultiplyRate;
        if (viriiCount > maxVirii)
        {
            viriiCount = maxVirii;
            return;
        }
        if (intViriiCount <= Mathf.Floor(viriiCount))
        {
            UpdateVirii();
        }

    }
    void UpdateVirii()
    {
        intViriiCount = Mathf.Min((int)Mathf.Floor(viriiCount), maxVirii);
        countText.SetText("{0}", intViriiCount);
        Debug.Log(intViriiCount);
    }
    public void UpdateMultiplyRate(float factor)
    {
        currentMultiplyRate = normalMultiplyRate * factor;
    }
    void OnMouseDown()
    {
        Debug.Log("You clicked me");
        GameManager.gameManager.selectActive(this);
    }
    public void IncrementVirii(int count)
    {
        viriiCount += count;
        countText.SetText("{0}", intViriiCount);
    }
    public bool DecrementVirii(int count)
    {
        if (viriiCount - count < 0)
        {
            return false;
        }
        else
        {
            viriiCount -= count;
            UpdateVirii();
            return true;
        }
    }
}
