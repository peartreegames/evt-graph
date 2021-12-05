using UnityEngine;

namespace EvtGraph
{
    public class EvtInteractTrigger : EvtTrigger
    {
        public void Interact(GameObject interactor) => Trigger();
    }
}