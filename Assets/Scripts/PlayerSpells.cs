using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerSpells : MonoBehaviour
{
    [SerializeField]
    float ExplosionRadius;
    [SerializeField]
    float MaxRangeSacrifice;
    [SerializeField]
    float MinRangeSacrifice;

    private GameObject _bloodSplash;
    private GameObject _bloodFlaque;
    private GameObject _currentFlaque;
    [Inject]
    public void Construct([Inject(Id = "BloodSplash")] GameObject BloodSplash, [Inject(Id = "BloodFlaque")] GameObject BloodFlaque)
    {
        _bloodFlaque = BloodFlaque;
        _bloodSplash = BloodSplash;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputManager.Instance.OnExplosion += Explosion;
        InputManager.Instance.OnSacrifice += Sacrifice;
    }

    private bool CheckFlaque()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.tag == "Flaque")
        {
            _currentFlaque = hit.collider.gameObject;
            return true;
        }
        else { return false; }
    }
    private void Explosion()
    {
        if (CheckFlaque() != true) return;
        GameObject splashInstance = Instantiate(_bloodSplash, _currentFlaque.transform.position, _currentFlaque.transform.rotation);
        splashInstance.transform.localScale = new Vector3(3f, 3f, 0f);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_currentFlaque.transform.position, ExplosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.attachedRigidbody != null && hitCollider.tag == "Enemy")
            {
                var power = 5.0f;
                print("hitCollider");
                    hitCollider.GetComponent<Enemy>().agent.Move(power * (hitCollider.transform.position - _currentFlaque.transform.position).normalized);
                print("passé");
                //hitCollider.attachedRigidbody.AddForce(power * (hitCollider.transform.position - this.transform.position).normalized);
            }
        }
        Destroy(_currentFlaque);
    }
    private void Sacrifice()
    {
        Instantiate(_bloodFlaque, transform.position + transform.up + new Vector3(Random.Range(-MinRangeSacrifice, MaxRangeSacrifice), Random.Range(-MinRangeSacrifice, MaxRangeSacrifice), 0), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 380f)));
    }
}
