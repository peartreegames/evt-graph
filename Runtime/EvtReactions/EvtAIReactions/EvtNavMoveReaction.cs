using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace PeartreeGames.EvtGraph
{
    public class EvtNavMoveReaction : EvtReaction
    {
        public new static string DisplayName => "AI/NavAgentMove";
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Transform destination;
        [SerializeField] private float distanceThreshold = 0.1f;
        public override IEnumerator React(EvtTrigger trigger)
        {
            agent.destination = destination.position;
            while (Vector3.Distance(agent.transform.position, destination.position) > distanceThreshold)
                yield return null;
        }
    }
}