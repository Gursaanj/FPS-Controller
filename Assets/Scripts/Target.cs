using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private GameObject crackedVersion;

    [SerializeField] private float timeTillRespawn = 5f;
    [SerializeField] private float timeTillCrackCopyRemoval = 3f;
    private float health;

    private void Start()
    {
        health = maxHealth;

        if (timeTillRespawn <= timeTillCrackCopyRemoval)
        {
            Debug.LogError("Target will respawn before removal");
        }
    }

    public void TakeDamage(float amount)
    {
        maxHealth -= amount;
        if (maxHealth <= 0f)
        {
            TargetDestroyed();
        }
    }

    private void TargetDestroyed()
    {
        GameObject crackedCopy = Instantiate(crackedVersion, transform.position, transform.rotation);
        crackedCopy.GetComponent<CrackedCopyRemoval>().IntiateFadeOut(timeTillCrackCopyRemoval);
        Destroy(gameObject);
    }
}
