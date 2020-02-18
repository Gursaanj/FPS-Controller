using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance = null;

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

    private void Start()
    { 

        Bounds planeBounds = plane.GetComponent<MeshRenderer>().bounds;

        xDimensionsPlane = planeBounds.size.x / 2.2f;
        zDimensionsPlane = planeBounds.size.z / 2.2f;

        Vector3 position;
        for (int i = 0; i < pooledAmountOfTargets; i++)
        {
            position = NewSpawnPosition();
            targets.Add(Instantiate(target, position, Quaternion.identity, transform));
        }
    }

    private Vector3 NewSpawnPosition()
    {
        float xPosition = Random.Range(-xDimensionsPlane, xDimensionsPlane);
        float zPosition = Random.Range(-zDimensionsPlane, zDimensionsPlane);
        Vector3 targetPosition = new Vector3(xPosition, heightAbovePlane, zPosition);
        return targetPosition;
    }

    public void InitializeRespawn(GameObject target, float respawnTime)
    {
        StartCoroutine(Respawn(target, respawnTime));
    }

    private IEnumerator Respawn(GameObject target, float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        target.transform.position = NewSpawnPosition();
        target.SetActive(true);
    }
}
