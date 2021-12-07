using System;
using UnityEngine;

namespace EvtGraph
{
    public class EvtObject : ScriptableObject
    {
        [SerializeField] private EvtEvent evt;
        public virtual void Subscribe(Action listener) => evt.Subscribe(listener);
        public virtual void Unsubscribe(Action listener) => evt.Unsubscribe(listener);
    }

    public abstract class EvtObject<T> : EvtObject
    {
        protected abstract EvtVariable<T> Variable { get; }
        public override void Subscribe(Action listener) => Variable.Subscribe(listener);
        public override void Unsubscribe(Action listener) => Variable.Unsubscribe(listener);
        public void Subscribe(Action<T> listener) => Variable.Subscribe(listener);
        public void Unsubscribe(Action<T> listener) => Variable.Unsubscribe(listener);

        public T Value
        {
            get => Variable.Value;
            set => Variable.Value = value;
        }
        
    }
}