using System;
using System.Collections.Generic;
using UnityEngine;

namespace EvtGraph
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
        [SerializeField] protected List<T> items;
    }
}