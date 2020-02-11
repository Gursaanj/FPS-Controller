using UnityEngine;

public class Rifle : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private float muzzleLightIntensity = 5f;
    [SerializeField] private Light muzzleFlashLight;
    [SerializeField] private GameObject impactCollision;
    [SerializeField] private float pushBackForce = 30f;
    [SerializeField] private float fireRate = 15f;
    private ParticleSystem impactEffect;
    private float nextTimeToFire = 0f;


    private void Start()
    {
        impactEffect = impactCollision.GetComponent<ParticleSystem>();
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
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * pushBackForce);
            }

            GameObject impact = Instantiate(impactCollision, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, impactEffect.main.duration);
        }
    }
}
