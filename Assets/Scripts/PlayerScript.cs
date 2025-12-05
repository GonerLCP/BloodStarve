using UnityEngine;
using Zenject;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    Vector2 velocity;
    Enemy.Factory enemyFactory;
    public Vector3 center;
    public float radius;
    public LayerMask layerMask;
    float health = 100f;
    float invinsibilityDuration = 1f;
    bool invincible=false;
    public SpriteRenderer spriteRenderer;
    private GameObject _bloodSplash;
    private GameObject _bloodFlaque;

    public float max;
    public float min;

    [Inject]
    public void Construct(Enemy.Factory _enemyFactory, [Inject(Id = "BloodFlaque")] GameObject BloodFlaque)
    {
        enemyFactory = _enemyFactory;
        _bloodFlaque = BloodFlaque; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity.y = Input.GetAxis("Vertical"); 
        velocity.x = Input.GetAxis("Horizontal");
        velocity = velocity*speed + (velocity.normalized*Time.deltaTime*speed);
        rb.linearVelocity = velocity;

        if (Input.GetKeyDown(KeyCode.E))
        {
            enemyFactory.Create();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerAttack();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Sacrifice();
        }
        //player.rotation = Quaternion.LookRotation(velocity,Vector3.up);
        //player.rotation = Quaternion.Euler(0, 0, Vector2.Angle(player.up, velocity));
        if (velocity != Vector2.zero) { rb.MoveRotation(Quaternion.LookRotation(transform.forward, velocity)); }
    }

    void PlayerAttack()
    {
        center = Vector3.RotateTowards(center, transform.up, 3.0f, 0.0f);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, radius, layerMask);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.GetComponent<Enemy>().Spillingblood();
            Debug.Log("uii");
        }
    }

    private void OnDrawGizmosSelected()
    {
        center = Vector3.RotateTowards(center, transform.up,3.0f,0.0f);
        Gizmos.DrawSphere(this.transform.position+center, radius);
    }

    public void RecievingDamage(float damageDealt)
    {
        if (invincible == false)
        {
            health = health - damageDealt;
        }
        invincible = true;
        print(health);
        StartCoroutine(DesactivatingCollider());
    }

    IEnumerator DesactivatingCollider()//Enlève le collider pour être invincible et surtout rettriger le OnTrigerEnter
    {
        StartCoroutine(invicilityAnimation());
        GetComponent<CapsuleCollider2D>().enabled = false;
        yield return new WaitForSeconds(invinsibilityDuration);
        GetComponent<CapsuleCollider2D>().enabled = true;
        invincible = false;
    }

    IEnumerator invicilityAnimation() //Change couleur du sprite
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        while (invincible == true)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.color = Color.white;
    }

    private void Sacrifice()
    {
        Instantiate(_bloodFlaque,transform.position + transform.up + new Vector3(Random.Range(-min, max), Random.Range(-min, max),0), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 380f)));
    }
}
