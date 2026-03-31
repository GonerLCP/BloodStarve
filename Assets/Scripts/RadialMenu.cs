using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    public int NumberOfSpells;
    public PlayerSpells _playerSpells; //a injecter
    public Vector3 position;
    public float radius;
    public GameObject tempToInstantiate;
    public List<GameObject> ListInstatiatedObjects;
    public Canvas RadialCanva;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        InputManager.Instance.OnRadialMenu +=InstantiateObjects;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InstantiateObjects()
    {
        DestroyAllObjects();
        float nextAngle = 2 * Mathf.PI / NumberOfSpells;
        float angle = Mathf.PI/2;
        for (int i = 0; i < NumberOfSpells; i++) 
        {
            float x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x + Mathf.Cos(angle)*radius;
            float y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y + Mathf.Sin(angle)*radius;
            GameObject temp = GameObject.Instantiate(tempToInstantiate, new Vector2(x,y), Quaternion.identity, RadialCanva.transform);
            ListInstatiatedObjects.Add(temp);
            angle += nextAngle;
        }
    }

    void DestroyAllObjects()
    {
        for (int i = 0; i < ListInstatiatedObjects.Count; i++)
        {
            Destroy(ListInstatiatedObjects[i]);
        }
        ListInstatiatedObjects.Clear();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(position, radius);
        Gizmos.color = Color.yellow;
    }
}
