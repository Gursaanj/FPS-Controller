using System.Collections;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private float muzzleLightIntensity = 5f;
    [SerializeField] private Light muzzleFlashLight;
    [SerializeField] private float pushBackForce = 30f;
    [SerializeField] private float fireRate = 15f;
    private ParticleSystem impactEffect;
    private float nextTimeToFire = 0f;


    private void Start()
    {
        muzzleFlashLight.intensity = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        muzzleFlashLight.intensity = muzzleLightIntensity;
        muzzleFlash.Play();
        muzzleFlashLight.intensity = 0f;
        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * pushBackForce);
            }

            GameObject impactCollision = ImpactPooler.Instance.GetPooledImpactCollision();

            if (impactCollision != null)
            {
                impactEffect = impactCollision.GetComponent<ParticleSystem>();
                impactCollision.transform.position = hit.point;
                impactCollision.transform.rotation = Quaternion.LookRotation(hit.normal);
                impactCollision.SetActive(true);
                StartCoroutine(PoolImpactCollision(impactCollision));
            }
        }
    }

    private IEnumerator PoolImpactCollision(GameObject impactCollision)
    {
        yield return new WaitForSeconds(impactEffect.main.duration);
        ImpactPooler.Instance.AddImpactCollisionToPool(impactCollision);
    }
}
