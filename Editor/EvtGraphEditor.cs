using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.UIElements;

namespace PeartreeGames.EvtGraph.Editor
{
    public class EvtGraphEditor : EditorWindow
    {
        [MenuItem("Evt/Graph")]
        private static void ShowWindow()
        {
            var window = GetWindow<EvtGraphEditor>();
            window.titleContent = new UnityEngine.GUIContent("EvtGraph");
            window.Show();
        }

        private void OnEnable()
        {
            Init();
            Selection.selectionChanged += Init;
            EditorApplication.playModeStateChanged += PlayModeChanged;
            EditorApplication.hierarchyChanged += Init;
        }

        private void OnDisable()
        {
            rootVisualElement.Clear();
            Selection.selectionChanged -= Init;
            EditorApplication.playModeStateChanged -= PlayModeChanged;
            EditorApplication.hierarchyChanged -= Init;
        }

        private void PlayModeChanged(PlayModeStateChange mode)
        {
            Init();
        }

        private void Init()
        {
            var box = new Box {style = {alignItems = Align.Center}};
            box.StretchToParentSize();
            var label = new Label() {style = {top = 50}};
            EvtTrigger evtTrigger = null;
            if (Selection.activeGameObject == null ||
                !Selection.activeGameObject.TryGetComponent(out evtTrigger)) label.text = "No EvtTrigger selected";
            if (PrefabStageUtility.GetCurrentPrefabStage() != null) label.text = "Cannot edit prefab EvtGraph";
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
                name = "EvtGraph"
            };
            graph.StretchToParentSize();
            rootVisualElement.Add(graph);
        }
    }
}