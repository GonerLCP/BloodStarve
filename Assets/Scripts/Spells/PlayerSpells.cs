using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class PlayerSpells : MonoBehaviour
{
    //public GameObject _bloodSplash { get; private set; }
    //public GameObject _bloodFlaque { get; private set; }
    public GameObject _currentFlaque;
    public SpellSO currentSpell;
    //[Inject]
    //public void Construct([Inject(Id = "BloodSplash")] GameObject BloodSplash, [Inject(Id = "BloodFlaque")] GameObject BloodFlaque)
    //{
    //    _bloodFlaque = BloodFlaque;
    //    _bloodSplash = BloodSplash;
    //}
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        InputManager.Instance.OnExplosion += Explosion;
        //InputManager.Instance.OnSacrifice += Sacrifice;
    }

    private void Explosion()
    {
        if (CheckFlaque() != true) return;
        currentSpell.Cast(_currentFlaque);
    }
    public bool CheckFlaque()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.tag == "Flaque")
        {
            _currentFlaque = hit.collider.gameObject;
            return true;
        }
        else { return false; }
    }
}


public interface SpellInterface
{
    public void Spell();
}
