using UnityEngine;

public class MouseFollowing : MonoBehaviour
{
    public GameObject mask;

    // Update is called once per frame
    void Update()
    {
        Vector3 p = Input.mousePosition;
        Vector3 pos = Camera.main.ScreenToWorldPoint(p);
        this.transform.position = pos;
        mask.transform.position = Input.mousePosition;
        //transform.position = Input.mousePosition;
    }
}
