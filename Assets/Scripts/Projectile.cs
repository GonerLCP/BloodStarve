using UnityEngine;
using Zenject;
public class Projectile : MonoBehaviour
{
    PlayerScript _player;
    Vector3 velocity = Vector3.zero;
    public float speed;
    public bool update;
    public bool velocityb;
    public bool pos;

    [Inject]
    public void Construct(PlayerScript player)
    {
        _player = player;
    }
    private void Awake()
    {
        velocity = _player.transform.position - this.transform.position;
        transform.rotation = Quaternion.LookRotation(transform.forward, _player.transform.position);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.rotation = Quaternion.LookRotation(transform.forward, _player.transform.position);
        velocity = _player.transform.transform.position - this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.forward, _player.transform.position);
        if (update == false) { return; }
        transform.position += velocity * speed* Time.deltaTime;
    }
}
