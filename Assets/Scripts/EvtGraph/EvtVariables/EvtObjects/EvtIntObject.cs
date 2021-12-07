using UnityEngine;

namespace EvtGraph
{
    [CreateAssetMenu(fileName = "int_", menuName = "Evt/IntObject", order = 0)]
    public class EvtIntObject : EvtObject<int>
    {
        [SerializeField] private EvtIntVariable intVariable;
        protected override EvtVariable<int> Variable => intVariable;
    }
}