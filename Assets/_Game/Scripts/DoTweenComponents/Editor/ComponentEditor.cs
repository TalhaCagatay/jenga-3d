using UnityEditor;
using UnityEngine;

namespace _Game.Scripts.DoTweenComponents.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(ComponentBase), true)]
    public class ComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty useCustomStartingVectorProp;
        private SerializedProperty startingVectorProp;

        private void OnEnable()
        {
            useCustomStartingVectorProp = serializedObject.FindProperty("_useCustomStartingVector");
            startingVectorProp = serializedObject.FindProperty("_startingVector");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(useCustomStartingVectorProp);
            if (useCustomStartingVectorProp.boolValue)
            {
                EditorGUILayout.PropertyField(startingVectorProp);
            }

            if (GUILayout.Button("Re-Play"))
            {
                var componentBase = target as ComponentBase;
                componentBase.RePlayTween();
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}