using UnityEngine;

namespace PeartreeGames.EvtGraph
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