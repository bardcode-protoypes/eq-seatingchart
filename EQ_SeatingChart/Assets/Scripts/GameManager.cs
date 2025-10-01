using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<TableController> tables;
    [SerializeField] private List<GuestCardController> guestCards;
    [SerializeField] private List<GuestWaveSO> waves;
    [SerializeField] private GuestSpawner spawner;
    [SerializeField] private RuleChecker ruleChecker;

    private int currentWaveIndex = 0;

    private void Start()
    {
        SpawnCurrentWave();
    }

    private void SpawnCurrentWave()
    {
        if (currentWaveIndex < waves.Count)
        {
            spawner.SpawnWave(waves[currentWaveIndex]);
        }
    }

    public void ValidateArrangement()
    {
        var results = ruleChecker.Validate(spawner.GetActiveCards(), tables);

        if (results.IsSuccessful)
        {
            Debug.Log("Wave Complete!");
            AdvanceWave();
        }
        else
        {
            foreach (var error in results.Errors)
                Debug.LogWarning(error);
            // Intradiegetic error note?
        }
    }

    private void AdvanceWave()
    {
        currentWaveIndex++;
        if (currentWaveIndex < waves.Count)
        {
            SpawnCurrentWave();
        }
        else
        {
            Debug.Log("All waves complete!");
        }
    }

    public void RegisterGuestCard(GuestCardController card)
    {
        if (!guestCards.Contains(card))
            guestCards.Add(card);
    }

    public void RegisterTable(TableController table)
    {
        if (!tables.Contains(table))
            tables.Add(table);
    }
}