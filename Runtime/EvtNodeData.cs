using System;
using System.Collections.Generic;
using UnityEngine;

namespace PeartreeGames.Evt.Graph
{
    public abstract class EvtNodeData : ScriptableObject, ISerializationCallbackReceiver
    {
        public Guid ID;
        public Vector2 position;

        public abstract void Execute(EvtTrigger trigger);

        [SerializeField] private string idString;
        public void OnBeforeSerialize() => idString = ID.ToString();
        public void OnAfterDeserialize() => Guid.TryParse(idString, out ID);
    }

    public abstract class EvtNodeData<T> : EvtNodeData where T : EvtNodeItemData
    {
        [HideInInspector]
        [SerializeField] public List<T> items;

        private void OnValidate()
        {
            items.RemoveAll(item => item == null);
            items.TrimExcess();
        }
    }
}