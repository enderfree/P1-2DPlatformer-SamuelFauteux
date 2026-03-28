using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text gold;

    private int goldCount = 0;

    private void OnEnable()
    {
        Coin.OnPickup += UpdateGold;
    }

    private void OnDisable()
    {
        Coin.OnPickup -= UpdateGold;
    }

    // Event consumers
    private void UpdateGold()
    {
        gold.text = ++goldCount + "$";
    }
}
