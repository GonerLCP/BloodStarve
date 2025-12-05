using UnityEngine;
using System.Collections;

public class BloodDestroySelf : MonoBehaviour
{
    public float SecondsToDestroying;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
