using _Game.Scripts.View.Views.Gameplay;
using UnityEditor;
using UnityEditor.UI;

namespace _Game.Scripts.View.Editor
{
    [CustomEditor(typeof(FocusButton))]
    public class FocusButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            var focusButton = (FocusButton)target;

            focusButton.FocusStackText.text = EditorGUILayout.TextField("Focus Text", focusButton.FocusStackText.text);
            
            base.OnInspectorGUI();
        }
    }
}