
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float health = 50f;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            TargetDestroyed();
        }
    }

    private void TargetDestroyed()
    {
        Destroy(gameObject);
    }
}
