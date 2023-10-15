using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;

namespace PeartreeGames.Evt.Graph.Editor
{
    public class EvtGraphEditor : EditorWindow
    {
        [MenuItem("Tools/Evt/Graph")]
        private static void ShowWindow()
        {
            var window = GetWindow<EvtGraphEditor>();
            window.titleContent = new UnityEngine.GUIContent("Evt.Graph");
            window.Show();
        }

        private void OnEnable()
        {
            Init();
            Selection.selectionChanged += Init;
            EditorApplication.playModeStateChanged += PlayModeChanged;
        }

        private void OnDisable()
        {
            rootVisualElement.Clear();
            Selection.selectionChanged -= Init;
            EditorApplication.playModeStateChanged -= PlayModeChanged;
        }

        private void PlayModeChanged(PlayModeStateChange mode)
        {
            Init();
        }

        private void Init()
        {
            var box = new Box {style = {alignItems = Align.Center}};
            box.StretchToParentSize();
            var label = new Label {style = {top = 50}};
            EvtTrigger evtTrigger = null;
            if (Selection.activeGameObject == null ||
                !Selection.activeGameObject.TryGetComponent(out evtTrigger)) label.text = "No EvtTrigger selected";
            if (PrefabStageUtility.GetCurrentPrefabStage() != null) label.text = "Cannot edit prefab Evt.Graph";
            if (Selection.count > 1) label.text = "Cannot edit multiple EvtTriggers at once";
            
            rootVisualElement.Clear();
            if (label.text != string.Empty || evtTrigger == null)
            {
                box.Add(label);
                rootVisualElement.Add(box);
                return;
            }
            var graph = new EvtGraphView(this, evtTrigger)
            {
                name = "Evt.Graph"
            };
            graph.StretchToParentSize();
            rootVisualElement.Add(graph);
        }
    }
}