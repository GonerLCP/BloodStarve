using UnityEngine;
using Zenject;
public abstract class SpellSO : ScriptableObject
{
    public string SpellName;

    public string Description;

    public float Cooldown;
    public float Delay;

    public Sprite SpellSprite;

    public GameObject _bloodSplash;
    public GameObject _bloodFlaque;
    public abstract void Cast(GameObject _currentFlaque);

}