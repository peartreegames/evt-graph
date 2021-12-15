using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public class EvtInteractTrigger : EvtTrigger
    {
        public void Interact(GameObject interactor) => Trigger();
    }
}