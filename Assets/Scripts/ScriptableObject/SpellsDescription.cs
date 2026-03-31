using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "SpellsDescription", menuName = "Scriptable Objects/SpellsDescription")]
public class SpellsDescription : ScriptableObject
{
    public string SpellName;

    public string Description;

    public float Cooldown;
    public float Delay;

    public Sprite SpellSprite;

    public PlayerSpells script;
}
