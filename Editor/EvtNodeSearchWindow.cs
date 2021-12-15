using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace PeartreeGames.EvtGraph.Editor
{
    public class EvtNodeSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private EvtGraphView _view;
        private EvtGraphEditor _editor;
        private EvtTrigger _evtTrigger;

        public void Init(EvtGraphEditor editor, EvtGraphView view, EvtTrigger trigger)
        {
            _editor = editor;
            _view = view;
            _evtTrigger = trigger;
        }
        
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Create Element")),
                new(new GUIContent("Condition"))
                {
                    userData = CreateInstance<EvtConditionNode>(),
                    level = 1
                },
                new(new GUIContent("Reaction"))
                {
                    userData = CreateInstance<EvtReactionNode>(),
                    level = 1
                }
            };
            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            var node = (EvtNodeData) SearchTreeEntry.userData;
            node.ID = Guid.NewGuid();

            var worldMousePosition = _editor.rootVisualElement.ChangeCoordinatesTo(_editor.rootVisualElement.parent,
                context.screenMousePosition - _editor.position.position);
            var localMousePosition = _view.contentViewContainer.WorldToLocal(worldMousePosition);

            node.position = localMousePosition;
            _evtTrigger.nodes ??= new List<EvtNodeData>();
            switch (node)
            {
                case EvtConditionNode condition:
                    _view.CreateNode(condition);
                    _evtTrigger.nodes.Add(condition);
                    break;
                case EvtReactionNode reaction:
                    _view.CreateNode(reaction);
                    _evtTrigger.nodes.Add(reaction);
                    break;
            }

            return true;
        }
    }
}