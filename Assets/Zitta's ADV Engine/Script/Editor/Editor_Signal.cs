using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ADV
{
    [CustomEditor(typeof(Signal), true)]
    [CanEditMultipleObjects]
    public class Editor_Signal : Editor {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (EditorApplication.isPlaying)
            {
                Signal S = (Signal)target;
                EditorGUILayout.Separator();
                EditorGUILayout.ObjectField("Source", S.Source, typeof(Card), false);
                EditorGUILayout.ObjectField("Target", S.Target, typeof(Card), false);
                EditorGUILayout.Vector2Field("Position", S.Position);
            }
        }
    }
}