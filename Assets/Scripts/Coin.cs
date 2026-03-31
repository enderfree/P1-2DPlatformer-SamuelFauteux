using UnityEngine;

public class Coin : MonoBehaviour
{
    public delegate void CoinPickup(); // I'm using C# event because they are more efficient at run time.
    public static event CoinPickup OnPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player playerScript))
        {
            OnPickup();
            Destroy(gameObject);
        }
    }
}
