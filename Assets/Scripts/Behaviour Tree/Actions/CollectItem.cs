using UnityEngine;

public class CollectItem : Node
{
    private AI _agent;
    private AgentActions _agentActions;
    private InventoryController _agentInventory;
    private GameObject _itemToCollect;
    private TargetTypes _itemTargetType;
    public CollectItem(AI agent, AgentActions agentActions, Sensing agentSenses, InventoryController agentInventory, GameObject itemToCollect = null)
    {
        _agent = agent;
        _agentActions = agentActions;
        _agentInventory = agentInventory;
        _itemToCollect = itemToCollect;
    }

    public CollectItem(AI agent, AgentActions agentActions, Sensing agentSenses, InventoryController agentInventory, TargetTypes targetType)
    {
        _agent = agent;
        _agentActions = agentActions;
        _agentInventory = agentInventory;
        _itemTargetType = targetType;
    }

    public override NodeState Evaluate()
    {
        SetState(NodeState.RUNNING);

        if (_itemToCollect == null)
        {
            GameObject tmp = Util.DetermineTarget(_agent, _itemToCollect, _itemTargetType);

            if (Collect(tmp))
                return NodeState.SUCCESS;
        }
        else
        {
            if (Collect(_itemToCollect))
                return NodeState.SUCCESS;
        }


        return NodeState.FAILURE;
    }

    private bool Collect(GameObject tmp)
    {
        if (!tmp.GetComponent<Collectable>().isTaken)
        {
            _agentActions.CollectItem(tmp);

            if (!_agentInventory.HasItem(tmp.name))
            {
                return false;
            }

            return true;
        }

        return false;
    }
}
