using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public float temperature = 97f;
    public TextMeshProUGUI tempText;
    public GameObject virus;
    public GameObject cell;
    List<VirusScript> virii = new List<VirusScript>();
    List<CellScript> cells = new List<CellScript>();
    public float minCellGap = 3f;
    CellScript selectedCell;
    public GameObject cellSelector;
    public CellScript startCell;
    void Start()
    {
        cells.Add(startCell);
        selectedCell = startCell;
        gameManager = this;
        updateTemp(temperature);
    }
    void updateTemp(float tmp)
    {
        temperature = tmp;
        float tmpMult = temperature * (-0.10309278f) + 11f;
        foreach (CellScript cs in cells)
        {
            cs.UpdateMultiplyRate(tmpMult);
        }
        tempText.text = "T:" + temperature + "\u00b0F";
    }

    VirusScript GetFreeVirus()
    {
        VirusScript selectedVirus = null;
        foreach (VirusScript vs in virii)
        {
            if (!vs.isActive)
            {
                selectedVirus = vs;
                break;
            }
        }
        if (selectedVirus == null)
        {
            GameObject newVirus = Instantiate(virus);
            if (newVirus.TryGetComponent(out VirusScript vs))
            {
                selectedVirus = vs;
                virii.Add(vs);
            }
        }
        return selectedVirus;
    }
    public void selectActive(CellScript cs)
    {
        if (selectedCell != null)
        {
            if (selectedCell.DecrementVirii(50))
            {
                GetFreeVirus().TeleportToForm(selectedCell, cs, 50);
            }
        }
        selectedCell = cs;
        cellSelector.transform.position = selectedCell.transform.position;
    }
    public void ExploreCell()
    {
        if (selectedCell == null || !selectedCell.DecrementVirii(50)) return;
        Vector3 newCellPosition = new Vector3();
        bool foundPosition = false;
        int tryCount = 0;
        while (!foundPosition && tryCount < 10)
        {
            tryCount++;
            newCellPosition = (Vector3)Random.insideUnitCircle * 10;
            newCellPosition += selectedCell.transform.position;
            foundPosition = true;
            foreach (CellScript cs in cells)
            {
                if (Vector3.Distance(cs.transform.position, newCellPosition) < minCellGap)
                {
                    foundPosition = false;
                    break;
                }
            }
        }
        CellScript newCellScript = CreateCell(newCellPosition);
        GetFreeVirus().TeleportToForm(selectedCell, newCellScript, 0);
    }
    CellScript CreateCell(Vector3 position)
    {
        GameObject newCell = Instantiate(cell, position, transform.rotation);
        if (newCell.TryGetComponent(out CellScript cs))
        {
            cells.Add(cs);
            cs.UpdateMultiplyRate(temperature * (-0.10309278f) + 11f);
            Debug.Log("Temperature multiplier " + (temperature * (-0.10309278f) + 11f));
            return cs;
        }
        return null;
    }

}
