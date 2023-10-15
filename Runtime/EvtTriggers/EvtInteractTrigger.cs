using UnityEngine;

namespace PeartreeGames.Evt.Graph
{
    public class EvtInteractTrigger : EvtTrigger
    {
        public void Interact(GameObject interactor) => Trigger();
    }
}