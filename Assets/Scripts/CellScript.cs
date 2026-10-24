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
    public int intViriiCount = 50;
    float viriiCount = 50f;
    public float multiplySpeed = 1f;
    public float normalMultiplyRate = 1.2f;
    float currentMultiplyRate = 1f;
    public int maxVirii = 250;
    public TextMeshPro countText;
    void Awake()
    {
        viriiCount = intViriiCount;
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
            GameManager.gameManager.ExploreCell(this);
            GameManager.gameManager.ExploreCell(this);
            GameManager.gameManager.RemoveCell(this);
            GameManager.gameManager.DeleteCell(this);
        }
        if (intViriiCount <= Mathf.Floor(viriiCount))
        {
            UpdateVirii();
        }

    }
    void UpdateVirii()
    {
        int prevCount = intViriiCount;
        intViriiCount = Mathf.Min((int)Mathf.Floor(viriiCount), maxVirii);
        GameManager.gameManager.ChangeVirus(intViriiCount - prevCount);
        countText.SetText("{0}", intViriiCount);
        //Debug.Log(intViriiCount);
    }
    public void UpdateMultiplyRate(float factor)
    {
        currentMultiplyRate = normalMultiplyRate * factor;
    }
    void OnMouseDown()
    {
        //Debug.Log("You clicked me");
        GameManager.gameManager.selectActive(this);
    }
    public void IncrementVirii(int count)
    {
        viriiCount += count;
        countText.SetText("{0}", intViriiCount);
        //GameManager.gameManager.ChangeVirus(count);
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
            //Debug.Log("Decreasing in cell " + Id+" by" + count +" to "+ viriiCount);
            //GameManager.gameManager.ChangeVirus(-count);
            UpdateVirii();
            return true;
        }
    }
}
