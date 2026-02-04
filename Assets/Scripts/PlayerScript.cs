using ModestTree;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;
using Zenject.SpaceFighter;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _arrow;
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    float _arrowRadius;
    [SerializeField]
    float _radius;

    public Vector3 center;
    public LayerMask layerMask;
    public float speed;

    Rigidbody2D rb;
    
    Vector2 velocity;
    Enemy.Factory enemyFactory;

    float health = 100f;
    float invinsibilityDuration = 1f;
    bool invincible=false;
    

    float angle;



    [Inject]
    public void Construct(Enemy.Factory _enemyFactory)
    {
        enemyFactory = _enemyFactory;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InputManager.Instance.OnAttack += PlayerAttack;
        InputManager.Instance.OnCreateEnemy += EnemyCreate;
    }
    private void OnDisable()
    {
        InputManager.Instance.OnAttack -= PlayerAttack;
        InputManager.Instance.OnCreateEnemy -= EnemyCreate;
    }

    // Update is called once per frame
    void Update()
    {
        velocity.y = Input.GetAxis("Vertical"); 
        velocity.x = Input.GetAxis("Horizontal");
        velocity = velocity * speed + (velocity.normalized * Time.deltaTime * speed);
        rb.linearVelocity = velocity;

        RotateArrow();
        if (velocity != Vector2.zero) { rb.MoveRotation(Quaternion.LookRotation(transform.forward, velocity)); }
    }

    void EnemyCreate()
    {
        enemyFactory.Create();

    }
    void PlayerAttack()
    {
        center = Vector3.RotateTowards(center, transform.up, 3.0f, 0.0f);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_arrow.transform.position, _radius, layerMask);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.GetComponent<Enemy>().Spillingblood();
            Debug.Log("uii");
        }
    }

    void RotateArrow()
    {
        Vector2 dir = ((Vector2)this.transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition)).normalized;
        Vector2 orbitPos = (Vector2)this.transform.position + dir * _arrowRadius;
        _arrow.transform.position = orbitPos;

        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle += 90f;

        _arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);
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
        while (invincible == true)
        {
            _spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            _spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        _spriteRenderer.color = Color.white;
    }

    private void OnDrawGizmosSelected()
    {
        center = Vector3.RotateTowards(center, transform.up, 3.0f, 0.0f);
        Gizmos.DrawSphere(_arrow.transform.position, _radius);
    }
}
