using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Spells/Explosion")]
public class Explosion : SpellSO
{
    public override void Cast(GameObject _currentFlaque)
    {
        GameObject splashInstance = Instantiate(_bloodSplash, _currentFlaque.transform.position, _currentFlaque.transform.rotation);
        splashInstance.transform.localScale = new Vector3(3f, 3f, 0f);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_currentFlaque.transform.position, 10f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.attachedRigidbody != null && hitCollider.tag == "Enemy")
            {
                var power = 5.0f;
                Console.Write("hitCollider");
                hitCollider.GetComponent<Enemy>().agent.Move(power * (hitCollider.transform.position - _currentFlaque.transform.position).normalized);
                Console.Write("passé");
                //hitCollider.attachedRigidbody.AddForce(power * (hitCollider.transform.position - this.transform.position).normalized);
            }
        }
        Destroy(_currentFlaque);
    }
}
