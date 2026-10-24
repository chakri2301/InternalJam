//using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public float temperature = 97f;
    public GameObject virus;
    public GameObject cell;
    List<VirusScript> virii = new List<VirusScript>();
    List<CellScript> cells = new List<CellScript>();
    [Header("Virus counter")]
    public TextMeshProUGUI VirusText;
    public TextMeshProUGUI MaxVirusText;
    int virusCount = 50;
    int maxVirusCount = 50;
    [Header("Human Cell")]
    public float minCellGap = 3f;
    public float maxCellGap = 5f;
    CellScript selectedCell;
    public GameObject cellSelector;
    public CellScript startCell;
    [Header("Wave")]
    public TextMeshProUGUI tempText;
    public float tempWaveTime = 3f;
    public AnimationCurve tempCurve;
    public Transform ImmuneCellSpawnPoint;
    public GameObject ImmuneCell;
    public TextMeshProUGUI targetText;
    void Awake()
    {
        cells.Add(startCell);
        selectedCell = startCell;
        gameManager = this;
        updateTemp(temperature);
    }
    void Start()
    {
        if (LevelManager.levelManager.target == 0)
        {
            targetText.SetText("   ");
        }
        else
        {
            targetText.SetText("Target:{0}", LevelManager.levelManager.target);
        }
        MaxVirusText.SetText("Max Attained:{0}", maxVirusCount);
        InvokeRepeating(nameof(WaveAttack), 10, 10);
        //Instantiate(ImmuneCell, ImmuneCellSpawnPoint.position, transform.rotation);
    }
    void updateTemp(float tmp)
    {
        temperature = tmp;
        float tmpMult = temperature * (-0.10309278f) + 11f;
        foreach (CellScript cs in cells)
        {
            cs.UpdateMultiplyRate(tmpMult);
        }
        //tempText.text = "T:" + temperature + "\u00b0F";
        tempText.SetText("T:{0:2}\u00b0F", temperature);
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
        // if (selectedCell != null)
        // {
        //     if (selectedCell.DecrementVirii(50))
        //     {
        //         GetFreeVirus().TeleportToForm(selectedCell, cs, 50);
        //     }
        // }
        selectedCell = cs;
        cellSelector.transform.position = selectedCell.transform.position;
    }
    public void OnExploreClicked()
    {
        ExploreCell(null);
    }
    public void ExploreCell(CellScript fromCell = null)
    {
        if (fromCell == null)
        {
            fromCell = selectedCell;
        }
        if (fromCell == null || fromCell.intViriiCount < 50) return;
        fromCell.DecrementVirii(25);
        Vector3 newCellPosition = new Vector3();
        bool foundPosition = false;
        int tryCount = 0;
        while (!foundPosition && tryCount < 10)
        {
            tryCount++;
            newCellPosition = (Vector3)Random.insideUnitCircle * maxCellGap;
            newCellPosition += fromCell.transform.position;
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
        GetFreeVirus().TeleportToForm(fromCell, newCellScript, 25);
    }
    CellScript CreateCell(Vector3 position)
    {
        GameObject newCell = Instantiate(cell, position, transform.rotation);
        if (newCell.TryGetComponent(out CellScript cs))
        {
            cells.Add(cs);
            cs.UpdateMultiplyRate(temperature * (-0.10309278f) + 11f);
            //Debug.Log("Temperature multiplier " + (temperature * (-0.10309278f) + 11f));
            return cs;
        }
        return null;
    }
    public CellScript GetRandomCell()
    {
        int idx = Mathf.FloorToInt(Random.value * cells.Count);
        if (idx == cells.Count)
        {
            idx = 0;
        }
        CellScript returnedCell = cells[idx];
        RemoveCell(returnedCell);
        return returnedCell;
    }
    public void RemoveCell(CellScript cs)
    {
        cells.Remove(cs);
    }
    public void WaveAttack()
    {
        if (Random.value <= 0.5f)
        {
            StartCoroutine(TemperatureRoutine());
        }
        else
        {
            if (cells.Count > 3)
            {
                int immuneCells = Mathf.CeilToInt(cells.Count * 0.2f);
                for (int i = 0; i < immuneCells; i++)
                {
                    Instantiate(ImmuneCell, ImmuneCellSpawnPoint.position, transform.rotation);
                }
            }
        }
    }
    IEnumerator TemperatureRoutine()
    {
        float timer = 0f;
        float changeTimer = 0f;
        while (timer < tempWaveTime)
        {
            timer += Time.deltaTime;
            changeTimer += Time.deltaTime;
            if (changeTimer > 0.1f)
            {
                updateTemp(97f + tempCurve.Evaluate(timer / tempWaveTime) * 10f);
                changeTimer = 0f;
            }
            yield return null;
        }
        yield break;
    }
    public void DeleteCell(CellScript cs)
    {
        if (cs == null)
        {
            return;
        }
        ChangeVirus(-cs.intViriiCount);
        Destroy(cs.gameObject);
        if (cells.Count <= 0)
        {
            Debug.Log("GameOver");
        }
    }
    public void ChangeVirus(int change)
    {
        //Debug.Log("[GameManager]changing count from " + virusCount + " by " + change);
        virusCount += change;
        if (maxVirusCount < virusCount)
        {
            maxVirusCount = virusCount;
            MaxVirusText.SetText("Max Attained:{0}", maxVirusCount);
        }
        VirusText.SetText("Virii: {0}", virusCount);
        if (LevelManager.levelManager.target == 0) return;
        if (LevelManager.levelManager.target < virusCount)
        {
            LevelManager.levelManager.Score = virusCount;
            LevelManager.levelManager.LoadScene(4);
        }
    }
    public void LoadScene(int idx)
    {
        LevelManager.levelManager.Score = maxVirusCount;
        LevelManager.levelManager.LoadScene(idx);
    }
}
