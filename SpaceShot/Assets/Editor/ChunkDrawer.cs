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
        newposition.y += 100;
        SerializedProperty data = property.FindPropertyRelative("rows");

        for (int y = 0; y < 4; y++)
        {
            SerializedProperty row = data.GetArrayElementAtIndex(y).FindPropertyRelative("row");
            newposition.height = 30f;

            if (row.arraySize != 4)
                row.arraySize = 4;

            newposition.width = 30f;

            for (int x = 0; x < 4; x++)
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
        return 150;
    }

}

