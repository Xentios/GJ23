using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ADV
{
    [CustomEditor(typeof(Library))]
    [CanEditMultipleObjects]
    public class Editor_Library : Editor {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Apply"))
            {
                Library L = (Library)target;
                Undo.RecordObject(L, "Apply");
                L.EditorIni();
                PrefabUtility.RecordPrefabInstancePropertyModifications(L);
            }
        }
    }
}