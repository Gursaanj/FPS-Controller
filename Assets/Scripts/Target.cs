using System.Collections;
using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private GameObject crackedVersion;

    [SerializeField] private float timeTillRespawn = 5f;
    [SerializeField] private float timeTillCrackCopyRemoval = 3f;
    private float health;


    private void Awake()
    {
        if (timeTillRespawn <= timeTillCrackCopyRemoval)
        {
            Debug.LogError("Target will respawn before removal");
        }
    }

    private void OnEnable()
    {
        health = maxHealth;
    }

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
        GameObject crackedCopy = Instantiate(crackedVersion, transform.position, transform.rotation);
        crackedCopy.GetComponent<CrackedCopyRemoval>().IntiateFadeOut(timeTillCrackCopyRemoval);
        TargetManager.instance.InitializeRespawn(gameObject, timeTillRespawn);
    }
}