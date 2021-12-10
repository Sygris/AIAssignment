using UnityEngine;

public class DropItem : Node
{
    private AI _agent;
    private AgentActions _agentActions;
    private GameObject _itemToDrop;

    public DropItem(AI agent, AgentActions agentActions, GameObject itemToDrop = null)
    {
        _agent = agent;
        _agentActions = agentActions;
        _itemToDrop = itemToDrop;
    }

    public override NodeState Evaluate()
    {
        SetState(NodeState.RUNNING);

        // If no item was specified the AI will drop the flag 
        if (_itemToDrop == null)
        {
            GameObject tmp = Util.DetermineTarget(_agent, _itemToDrop, TargetTypes.FLAG);

            _agentActions.DropItem(tmp);
            _agent.Blackboard.ModifyData("PriorityFlag", null);
            return NodeState.SUCCESS;
        }

        _agentActions.DropItem(_itemToDrop);
        return NodeState.SUCCESS;
    }
}
