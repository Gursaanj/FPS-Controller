using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolerManager : MonoBehaviour
{
    public static PoolerManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public class TargetManager
    {
        [Header("Target Pooler")]

        [SerializeField] private GameObject plane;
        [SerializeField] private GameObject target;
        [SerializeField] private int pooledAmountOfTargets = 3;

        private const float heightAbovePlane = 20f;

        private float xDimensionsPlane;
        private float zDimensionsPlane;
        private List<GameObject> targets = new List<GameObject>();

        public float HeightAbovePlane
        {
            get
            {
                return heightAbovePlane;
            }
        }

        private void Start()
        {

            Bounds planeBounds = plane.GetComponent<MeshRenderer>().bounds;

            xDimensionsPlane = planeBounds.size.x / 2.2f;
            zDimensionsPlane = planeBounds.size.z / 2.2f;

            Vector3 position;
            for (int i = 0; i < pooledAmountOfTargets; i++)
            {
                position = NewSpawnPosition();
                targets.Add(Instantiate(target, position, Quaternion.identity, PoolerManager.instance.transform));
            }
        }

        private Vector3 NewSpawnPosition()
        {
            float xPosition = Random.Range(-xDimensionsPlane, xDimensionsPlane);
            float zPosition = Random.Range(-zDimensionsPlane, zDimensionsPlane);
            Vector3 targetPosition = new Vector3(xPosition, heightAbovePlane, zPosition);
            return targetPosition;
        }
   
        public IEnumerator Respawn(GameObject target, float respawnTime)
        {
            yield return new WaitForSeconds(respawnTime);
            target.transform.position = NewSpawnPosition();
            target.SetActive(true);
        }
    }

    public class ImpactEffectManager
    {
        [Header("Impact Pooler")]
        [SerializeField] GameObject impactCollisionToPool;
        [SerializeField] int pooledAmount = 4;

        private List<GameObject> pooledImpactCollisions;

        private void Start()
        {
            pooledImpactCollisions = new List<GameObject>();

            for (int i = 0; i < pooledAmount; i++)
            {
                GameObject collision = (GameObject)Instantiate(impactCollisionToPool, PoolerManager.instance.transform);
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
}
