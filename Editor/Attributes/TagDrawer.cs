using UnityEditor;
using UnityEngine;

namespace PeartreeGames.Evt.Graph.Editor
{
    [CustomPropertyDrawer(typeof(TagAttribute))]
    public class TagDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            property.stringValue = EditorGUI.TagField(position, property.displayName, property.stringValue == string.Empty ? "Untagged" : property.stringValue);
        }
    }
}