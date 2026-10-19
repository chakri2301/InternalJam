using TMPro;
using UnityEngine;

public class CellScript : MonoBehaviour
{
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
        InvokeRepeating(nameof(UpdateVirii), 2, 2);
    }
    void UpdateVirii()
    {
        viriiCount += viriiCount * multiplySpeed * currentMultiplyRate;
        if (intViriiCount <= Mathf.Floor(viriiCount))
        {
            intViriiCount = Mathf.Min((int)Mathf.Floor(viriiCount), maxVirii);
            countText.SetText("{0}", intViriiCount);
        }
        Debug.Log(intViriiCount);
    }
    public void UpdateMultiplyRate(float factor)
    {
        currentMultiplyRate = normalMultiplyRate * factor;
    }
}
