using System.Collections;

namespace EvtGraph
{
    public abstract class EvtReaction : EvtNodeItemData
    {
        public abstract IEnumerator React(EvtTrigger trigger);
    }
}