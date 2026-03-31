using UnityEngine;

public class Sacrifice : SpellSO
{
    [SerializeField]
    float MaxRangeSacrifice;
    [SerializeField]
    float MinRangeSacrifice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Spell()
    {
        //Instantiate(_bloodFlaque, transform.position + transform.up + new Vector3(Random.Range(-MinRangeSacrifice, MaxRangeSacrifice), Random.Range(-MinRangeSacrifice, MaxRangeSacrifice), 0), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 380f)));
    }
    public override void Cast(GameObject _currentFlaque)
    {
        //Instantiate(_bloodFlaque, transform.position + transform.up + new Vector3(Random.Range(-MinRangeSacrifice, MaxRangeSacrifice), Random.Range(-MinRangeSacrifice, MaxRangeSacrifice), 0), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 380f)));
    }

}