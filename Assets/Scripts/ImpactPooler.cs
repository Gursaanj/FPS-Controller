using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactPooler : MonoBehaviour
{
    public static ImpactPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    [HideInInspector]
    public List<GameObject> pooledImpactCollisions;

    [SerializeField] GameObject impactCollisionToPool;
    [SerializeField] int pooledAmount;

    private void Start()
    {
        pooledImpactCollisions = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject collision = (GameObject)Instantiate(impactCollisionToPool, transform);
            collision.SetActive(false);
            pooledImpactCollisions.Add(collision);
        }
    }

    public GameObject GetPooledImpactCollision()
    {
        for (int i = 0, count = pooledImpactCollisions.Count; i < count; i++)
        {
            GameObject impactCollision = pooledImpactCollisions[i];
            if (!impactCollision.activeInHierarchy)
            {
                pooledImpactCollisions.RemoveAt(i);
                return impactCollision;
            }
        }

        GameObject collision = (GameObject)Instantiate(impactCollisionToPool);
        collision.SetActive(false);
        return collision;
    }

    public void AddImpactCollisionToPool(GameObject impactCollision)
    {
        impactCollision.SetActive(false);
        pooledImpactCollisions.Add(impactCollision);
    }
}
