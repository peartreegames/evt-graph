using UnityEngine;

namespace EvtGraph
{
    [CreateAssetMenu(fileName = "string_", menuName = "Evt/StringObject", order = 0)]
    public class EvtStringObject : EvtObject<string>
    {
        [SerializeField] private EvtStringVariable stringVariable;
        protected override EvtVariable<string> Variable => stringVariable;
    }
}