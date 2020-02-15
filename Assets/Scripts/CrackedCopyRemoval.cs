using System.Collections;
using UnityEngine;

public class CrackedCopyRemoval : MonoBehaviour
{
    public void IntiateFadeOut(float removalTime)
    {
        StartCoroutine(FadeOut(removalTime));
    }

    private IEnumerator FadeOut(float removalTime)
    {
        yield return new WaitForSeconds(removalTime);
        Destroy(gameObject);
    }
}
