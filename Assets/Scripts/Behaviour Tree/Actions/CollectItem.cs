using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : Node
{
    private AI _agent;
    private AgentActions _agentActions;
    private Sensing _agentSenses;
    private InventoryController _agentInventory;
    private GameObject _itemToCollect;

    public CollectItem(AI agent, AgentActions agentActions, Sensing agentSenses, InventoryController agentInventory, GameObject itemToCollect = null)
    {
        _agent = agent;
        _agentActions = agentActions;
        _agentSenses = agentSenses;
        _agentInventory = agentInventory;
        _itemToCollect = itemToCollect;
    }

    public override NodeState Evaluate()
    {
        SetState(NodeState.RUNNING);

        GameObject tmp = Util.DetermineTarget(_agent, ref _itemToCollect, TargetTypes.FLAG);

        if (!_agentSenses.IsItemInReach(tmp))
        {
            SetState(NodeState.FAILURE);
            return _nodeState;
        }

        _agentActions.CollectItem(tmp);

        if (!_agentInventory.HasItem(tmp.name))
        {
            SetState(NodeState.FAILURE);
            return _nodeState;
        }

        SetState(NodeState.SUCCESS);
        return _nodeState;
    }
}
