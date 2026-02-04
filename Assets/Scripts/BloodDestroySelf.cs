using UnityEngine;
using System.Collections;

public class BloodDestroySelf : MonoBehaviour
{
    public float SecondsToDestroying;

    private void Awake()
    {
        StartCoroutine(DestroySelfCoroutine(SecondsToDestroying));
    }

    IEnumerator DestroySelfCoroutine(float secondsToDestroying)
    {
        yield return new WaitForSeconds(secondsToDestroying);
        Destroy(gameObject);
    }
}
