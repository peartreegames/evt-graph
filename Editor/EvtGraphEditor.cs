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
            rootVisualElement.Clear();
            var box = new Box {style = {alignItems = Align.Center}};
            box.StretchToParentSize();
            if (Selection.activeGameObject == null ||
                !Selection.activeGameObject.TryGetComponent<EvtTrigger>(out var evtTrigger))
            {

                var label = new Label("No EvtTrigger selected") {style = {top = 50}};
                box.Add(label);
                rootVisualElement.Add(box);
                return;
            }

            if (PrefabStageUtility.GetCurrentPrefabStage() != null)
            {
                var label = new Label("Cannot edit prefab EvtGraph") {style = {top = 50}};
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