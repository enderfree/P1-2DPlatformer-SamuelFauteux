using TMPro;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text gold;
    [SerializeField] private TMP_Text time;

    private int goldCount = 0;
    private float timer = 0f;

    private void OnEnable()
    {
        Coin.OnPickup += UpdateGold;
    }

    private void OnDisable()
    {
        Coin.OnPickup -= UpdateGold;
    }

    private void LateUpdate()
    {
        timer += Time.deltaTime;
        time.text = ((int)timer).ToString().PadLeft(3, '0');
    }

    // Event consumers
    private void UpdateGold()
    {
        gold.text = ++goldCount + "$";
    }
}
