using System;
using UnityEngine;

namespace EvtGraph
{
    [Serializable]
    public abstract class EvtVariable<T> : EvtEvent<T>
    {
        [SerializeField] private T value;

        public T Value
        {
            get => value;
            set
            {
                if (IsEqual(this.value, value)) return;
                this.value = value;
                Invoke(this.value);
            }
        }

        protected virtual bool IsEqual(T current, T other) => current.Equals(other);

    }
    
    [Serializable]
    public class EvtIntVariable : EvtVariable<int> {}
    
    [Serializable]
    public class EvtBoolVariable : EvtVariable<bool> {}
    
    [Serializable]
    public class EvtStringVariable : EvtVariable<string> {}

    [Serializable]
    public class EvtFloatVariable : EvtVariable<float>
    {
        protected override bool IsEqual(float current, float other) => Mathf.Abs(current - other) < Mathf.Epsilon;
    }
    
    [Serializable]
    public class EvtTransformVariable : EvtVariable<Transform> {}
}