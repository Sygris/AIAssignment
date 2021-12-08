using UnityEngine;

public class DropItem : Node
{
    private AI _agent;
    private AgentActions _agentActions;
    private InventoryController _agentInventory;
    private GameObject _itemToDrop;

    public DropItem(AI agent, AgentActions agentActions, InventoryController agentInventory, GameObject itemToDrop = null)
    {
        _agent = agent;
        _agentActions = agentActions;
        _agentInventory = agentInventory;
        _itemToDrop = itemToDrop;
    }

    public override NodeState Evaluate()
    {
        SetState(NodeState.RUNNING);

        GameObject tmp = Util.DetermineTarget(_agent, ref _itemToDrop, TargetTypes.FLAG);

        _agentActions.DropItem(tmp);

        _agent.PriorityFlag = null;

        Debug.Log("Dropped the <color=yellow>flag</color>");

        return NodeState.SUCCESS;
    }
}
