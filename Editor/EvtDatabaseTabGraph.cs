using System.Collections.Generic;
using System.Linq;
using PeartreeGames.Evt.Variables.Editor;
using UnityEngine;
using UnityEngine.UIElements;

namespace PeartreeGames.Evt.Graph.Editor
{
    public class EvtDatabaseTabGraph : EvtDatabaseTab<EvtTrigger>
    {
        public override string Name => "Graph";
        public override VisualElement CreatePage()
        {
            Page = EvtDatabase.CreatePage(GetTriggersInScene, OnSelectionIndexChange);
            Page.StretchToParentSize();
            return Page;
        }

        private void OnSelectionIndexChange(IEnumerable<int> indices)
        {
            var right = Page.Q("Right");
            right.Clear();
            if (indices == null) return;
            var list = Page.Q<ListView>("List");
            foreach (var i in indices)
            {
                if (list.itemsSource[i] == null) continue;
                right.Add(CreateGraphView(list.itemsSource[i] as EvtTrigger));
                break;
            }
        }

        private VisualElement CreateGraphView(EvtTrigger evtTrigger)
        {
            var box = new Box {style = {alignItems = Align.Center}};
            box.StretchToParentSize();
            var label = new Label {style = {top = 50}};
            var graph = new EvtGraphView(EvtDatabase.Instance, evtTrigger)
            {
                name = "Evt.Graph"
            };
            graph.StretchToParentSize();
            return graph;
        }

        private static List<EvtTrigger> GetTriggersInScene() =>
            Object.FindObjectsOfType<EvtTrigger>(true).ToList();
    }
}