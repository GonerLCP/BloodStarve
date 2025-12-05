using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class EnemyHitboxEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Enemy hitbox = (Enemy)target;

        if (GUILayout.Button("Create a Hitbox"))
        {
            
        }
        if (GUILayout.Button("Generate Hitboxes"))
        {
            
        }
    }
}
