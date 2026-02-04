using UnityEngine;
using Zenject;
public class OutlineScript : MonoBehaviour
{
    [SerializeField]
    Material BaseMaterial;
    [SerializeField]
    Material MaterialOver;

    private GameObject _bloodSplash;
    OutlineScript.Factory factory;

    [Inject]
    public void Construct([Inject(Id = "BloodSplash")] GameObject BloodSplash, OutlineScript.Factory Factory)
    {
        _bloodSplash = BloodSplash;
        factory = Factory;
    }
    private void OnMouseOver()
    {
        if (GetComponent<SpriteRenderer>().material == MaterialOver)
        {
            return;
        }
        GetComponent<SpriteRenderer>().material = MaterialOver;
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().material = BaseMaterial;
    }
    public class Factory : PlaceholderFactory<OutlineScript>
    {
    }
}
