using UnityEngine;

public class LawnMower : MonoBehaviour
{
    [SerializeField] private float speed = 15f; 
    private bool isActivated = false;
    [SerializeField] private Rigidbody2D rb;

    void OnTriggerEnter2D(Collider2D other)
    {
        ZombieBase zombie = other.GetComponent<ZombieBase>();

        if (zombie != null)
        {
            if (!isActivated)
            {
                Activate();
            }
            zombie.TakeDamage(999999);
        }
    }
    private void Activate()
    {
        isActivated = true;
        rb.velocity = new Vector2(speed, 0);
        SoundManager.Instance.PlaySFX(FX.lawnmowerClip);
        Destroy(gameObject, 5f);
    }
}