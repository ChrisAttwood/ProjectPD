using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ChunkData))]
public class ChunkDrawer : PropertyDrawer
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Rect newposition = position;
        newposition.y += 256;
        SerializedProperty data = property.FindPropertyRelative("rows");

        for (int y = 0; y < 8; y++)
        {
            SerializedProperty row = data.GetArrayElementAtIndex(y).FindPropertyRelative("row");
            newposition.height = 30f;

            if (row.arraySize != 8)
                row.arraySize = 8;

            newposition.width = 30f;

            for (int x = 0; x < 8; x++)
            {
                EditorGUI.PropertyField(newposition, row.GetArrayElementAtIndex(x), GUIContent.none);
                newposition.x += newposition.width;
            }
            newposition.x = position.x;
            newposition.y -= 30f;
        }



    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 300;
    }

}

