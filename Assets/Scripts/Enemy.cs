using UnityEngine;
using Zenject;
using System.Collections;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    PlayerScript _player;
    public float Speed;
    float _health = 20;
    public float damageOutput;
    public GameObject bloodSpill;
    private GameObject _bloodFlaque;
    private Rigidbody2D _rb2d;
    public LayerMask ObstacleMask;

    public NavMeshAgent agent;

    [Inject]
    public void Construct( PlayerScript player, [Inject(Id = "BloodFlaque")] GameObject BloodFlaque)
    {
        _player = player;
        _bloodFlaque = BloodFlaque;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(_player.transform.position);
    }

    public void Spillingblood()
    {
        //animation ahah on perd du sang
        _health -= 10;
        if (_health <= 0)
        {
            Death();
            return;
        }
        Instantiate(bloodSpill, transform.position,transform.rotation);
        Instantiate(_bloodFlaque, transform.position+ 5*transform.forward, Quaternion.Euler(0.0f,0.0f, Random.Range(0.0f, 380f)));
    }
    void Death()
    {
        GameObject newSpill = Instantiate(bloodSpill, transform.position, transform.rotation);
        newSpill.transform.localScale = new Vector3(Random.Range(1f, 2.5f), Random.Range(1f, 2.5f), newSpill.transform.localScale.z);
        GameObject newFlaque = Instantiate(_bloodFlaque, transform.position + 5 * transform.forward, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 380f)));
        newFlaque.transform.localScale = new Vector3(Random.Range(1f, 2.5f), Random.Range(1f, 2.5f), newFlaque.transform.localScale.z);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag =="Player")
        {
            collision.GetComponent<PlayerScript>().RecievingDamage(damageOutput);
        }
    }
    IEnumerator AttackAfterDelay(float attackDelay)
    {
        yield return new WaitForSeconds(attackDelay);
        var radius = 1.5f;

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Player")
            {
                hitCollider.GetComponent<PlayerScript>().RecievingDamage(damageOutput) ;
                Debug.Log("uii");
            }
        }
    }
    public class Factory : PlaceholderFactory<Enemy>
    {
    }
}
