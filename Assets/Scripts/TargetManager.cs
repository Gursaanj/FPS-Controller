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

    private void Awake()
    {
        if (instance == null)
        {
            instance = null;
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

        Transform targetTransform = target.transform;

        Vector3 targetPosition;
        for (int i = 0; i < pooledAmountOfTargets; i++)
        {
            float xPosition = Random.Range(-xDimensionsPlane, xDimensionsPlane);
            float zPosition = Random.Range(-zDimensionsPlane, zDimensionsPlane);

            targetPosition = new Vector3(xPosition, heightAbovePlane, zPosition);
            targetTransform.position = targetPosition;
            targets.Add(Instantiate(target, targetTransform));
        }
    }
}
