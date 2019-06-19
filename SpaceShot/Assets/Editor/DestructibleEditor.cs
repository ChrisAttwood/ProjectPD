using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Destructible))]
public class DestructibleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Build Collider, don't use on blocks"))
        {
            Destructible collider = (Destructible)target;
            collider.ReBuildCollider();
        }
        base.OnInspectorGUI();
    }
}
