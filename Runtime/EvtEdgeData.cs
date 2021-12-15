using System;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    [Serializable]
    public class EvtEdgeData : ISerializationCallbackReceiver
    {
        public Guid OutputId;
        public Guid InputId;
        public string portName;

        [SerializeField] private string outputIdString;
        [SerializeField] private string inputIdString;
        public void OnBeforeSerialize()
        {
            outputIdString = OutputId.ToString();
            inputIdString = InputId.ToString();
        }

        public void OnAfterDeserialize()
        {
            Guid.TryParse(outputIdString, out OutputId);
            Guid.TryParse(inputIdString, out InputId);
        }
    }
}