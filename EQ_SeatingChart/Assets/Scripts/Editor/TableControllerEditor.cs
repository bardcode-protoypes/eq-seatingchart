#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TableController))]
public class TableControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        TableController controller = (TableController)target;

        EditorGUILayout.Space();
        if (GUILayout.Button("Build Table"))
        {
            // Mark scene dirty so Unity knows it changed
            Undo.RegisterCompleteObjectUndo(controller.gameObject, "Rebuild Table");

            // Clean up all old seats
            var children = new System.Collections.Generic.List<GameObject>();
            foreach (Transform child in controller.transform)
                children.Add(child.gameObject);

            foreach (var go in children)
                if (go != null)
                    Undo.DestroyObjectImmediate(go);

            // Now build fresh
            controller.BuildTable();

            // Force scene view + inspector to refresh
            EditorUtility.SetDirty(controller);

        }
        
    }
}
#endif