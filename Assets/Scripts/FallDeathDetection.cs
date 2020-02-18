using UnityEngine;

public class FallDeathDetection : MonoBehaviour
{
    private Vector3 playerSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        GameObject collidedGameObject = other.gameObject;

        if (collidedGameObject.tag == "Target")
        {
            TargetManager.instance.InitializeRespawn(collidedGameObject);
        }

        if (collidedGameObject.tag == "Player")
        {
            playerSpawnPoint = collidedGameObject.GetComponent<PlayerMovement>().SpawnPoint;
            collidedGameObject.SetActive(false);
            collidedGameObject.transform.position = playerSpawnPoint;
            collidedGameObject.SetActive(true);
        }
    }
}
