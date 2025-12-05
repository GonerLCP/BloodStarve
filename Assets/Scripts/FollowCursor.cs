using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    public GameObject mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mask.transform.position = Input.mousePosition;
    }
}
