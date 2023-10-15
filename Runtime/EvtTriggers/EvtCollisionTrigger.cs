using UnityEngine;

namespace PeartreeGames.Evt.Graph
{
    public class EvtCollisionTrigger : EvtTrigger
    {
        [Tag]
        [SerializeField] private string targetTag;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(targetTag)) Trigger();
        }
    }
}