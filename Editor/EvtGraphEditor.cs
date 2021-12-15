using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.UIElements;
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

        private EvtTrigger _evtTrigger;
        public bool isLocked;

        public void Init()
        {
            
            if (isLocked) return;
            var box = new Box {style = {alignItems = Align.Center}};
            box.StretchToParentSize();
            var label = new Label() {style = {top = 50}};
            EvtTrigger evtTrigger = null;
            if (Selection.activeGameObject == null ||
                !Selection.activeGameObject.TryGetComponent<EvtTrigger>(out evtTrigger)) label.text = "No EvtTrigger selected";
            if (PrefabStageUtility.GetCurrentPrefabStage() != null) label.text = "Cannot edit prefab EvtGraph";
            if (Selection.count > 1) label.text = "Cannot edit multiple EvtTriggers at once";

            if (_evtTrigger == evtTrigger) return;
            _evtTrigger = evtTrigger;
            
            rootVisualElement.Clear();
            if (label.text != string.Empty)
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