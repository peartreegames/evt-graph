using System.Collections;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public class EvtInstantiateReaction : EvtReaction
    {
        public new static string DisplayName => "GameObject/Instantiate";
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform spawn;
        [SerializeField] private Transform parent;
        [SerializeField] private int quantity = 1;
        [SerializeField] private float delayPerSpawn;
        public override IEnumerator React(EvtTrigger trigger)
        {
            for (var i = 0; i < quantity; i++)
            {
                Instantiate(prefab, spawn.position, spawn.rotation, parent);
                yield return new WaitForSeconds(delayPerSpawn);
            }
        }
    }
}