using UnityEngine;
using Zenject;
public class OutlineScript : MonoBehaviour
{
    public Material BaseMaterial;
    public Material MaterialOver;
    private GameObject _bloodSplash;

    [Inject]
    public void Construct([Inject(Id = "BloodSplash")] GameObject BloodSplash)
    {
        _bloodSplash = BloodSplash;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Explosion();
        }
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

    private void Explosion()
    {
        GameObject splashInstance = Instantiate(_bloodSplash,transform.position,transform.rotation);
        splashInstance.transform.localScale = new Vector3(3f,3f,0f);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, 20);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.attachedRigidbody != null && hitCollider.tag == "Enemy")
            {
                var power = 5.0f;
                print("hitCollider");
                hitCollider.GetComponent<Enemy>().agent.Move(power*(hitCollider.transform.position - this.transform.position).normalized);
                print("passé");
                //hitCollider.attachedRigidbody.AddForce(power * (hitCollider.transform.position - this.transform.position).normalized);
            }
        }
    }
}
