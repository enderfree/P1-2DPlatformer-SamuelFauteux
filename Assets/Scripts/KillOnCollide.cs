using UnityEngine;

public class KillOnCollide : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidingGameObject = collision.collider.gameObject;
        
        if (collidingGameObject.TryGetComponent<Player>(out Player playerScript))
        { // should be multiplayer compatible
            playerScript.KillPlayer();
        }
    }
}
