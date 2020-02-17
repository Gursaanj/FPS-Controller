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

    public Action onTargetDestroyed;

    private void Start()
    {
        health = maxHealth;

        if (timeTillRespawn <= timeTillCrackCopyRemoval)
        {
            Debug.LogError("Target will respawn before removal");
        }
    }

    public void Respawn()
    {
        health = maxHealth;
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
        CrackedCopyRemoval crack = crackedCopy.GetComponent<CrackedCopyRemoval>();
        crack.IntiateFadeOut(timeTillCrackCopyRemoval);
        gameObject.SetActive(false);
        //onTargetDestroyed?.Invoke();
        TargetManager.instance.InitializeRespawn(timeTillRespawn);
    }   
}