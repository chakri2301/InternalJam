using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public static GameManager gameManager;
    public float temperature = 97f;
    public TextMeshProUGUI tempText;
    public GameObject virus;
    public GameObject cell;
    List<VirusScript> virii;
    List<CellScript> cells = new List<CellScript>();
    void TeleportVirii()
    {
        
    }
    void Start()
    {
        CreateCell();
        //gameManager = this;
        updateTemp(100f);
    }
    void updateViriiMultipler()
    {
        float tmpMult = temperature*(-0.10309278f) + 11f;
        foreach (CellScript cs in cells)
        {
            cs.UpdateMultiplyRate(tmpMult);
        }
    }
    void updateTemp(float tmp)
    {
        temperature = tmp;
        updateViriiMultipler();
        tempText.text =  "T:"+ temperature +"\u00b0F";
    }
    void CreateCell()
    {
        GameObject newCell = Instantiate(cell);
        if(newCell.TryGetComponent(out CellScript cs))
        {
            cells.Add(cs);
        }
    }
}
