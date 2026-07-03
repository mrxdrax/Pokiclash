using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PowerPanelSpawner : MonoBehaviour
{
    [Header("Power Prefabs (Total 5)")]
    public List<GameObject> powerPrefabs;

    [Header("Spawn Points (2 Only)")]
    public Transform slotA;
    public Transform slotB;

    private void OnEnable()
    {
        SpawnRandomPowers();
    }

    void SpawnRandomPowers()
    {
        // Safety check (bakchodi rokne ke liye)
        if (powerPrefabs.Count < 2)
        {
            Debug.LogError("Kam se kam 2 power prefabs chahiye bhai");
            return;
        }

        // Purane power hatao (agar panel dubara khula)
        ClearSlot(slotA);
        ClearSlot(slotB);

        // Random selection
        int firstIndex = Random.Range(0, powerPrefabs.Count);
        int secondIndex;

        do
        {
            secondIndex = Random.Range(0, powerPrefabs.Count);
        }
        while (secondIndex == firstIndex);

        // Spawn powers
        GameObject power1 = Instantiate(powerPrefabs[firstIndex], slotA.position, Quaternion.identity, slotA);
SetupButton(power1);

GameObject power2 = Instantiate(powerPrefabs[secondIndex], slotB.position, Quaternion.identity, slotB);
SetupButton(power2);
    }
    void ClearSlot(Transform slot)
    {
        if (slot.childCount > 0)
        {
            for (int i = slot.childCount - 1; i >= 0; i--)
            {
                Destroy(slot.GetChild(i).gameObject);
            }
        }
    }
void SetupButton(GameObject obj)
{
    Button btn = obj.GetComponent<Button>();
    PowerButton power = obj.GetComponent<PowerButton>();

    if (btn == null || power == null)
        return;

    btn.onClick.RemoveAllListeners();

    switch (power.powerType)
    {
        case PowerType.Blade:
            btn.onClick.AddListener(GameManager.Instance.AddOrbitBlade);
            break;

        case PowerType.Area:
            btn.onClick.AddListener(GameManager.Instance.IncreaseKillArea);
            break;

        case PowerType.Bomb:
            btn.onClick.AddListener(GameManager.Instance.DropBombs);
            break;

        case PowerType.ClearScreen:
            btn.onClick.AddListener(GameManager.Instance.ClearScreen);
            break;

        case PowerType.MaxHealth:
            btn.onClick.AddListener(GameManager.Instance.maxhealth);
            break;
    }
}
}
