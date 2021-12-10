using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesAllyHaveFlag : Node
{
    private AI _agent;
    private AgentData _agentData;

    public DoesAllyHaveFlag(AI agent, AgentData _data)
    {
        _agent = agent;
        _agentData = _data;
    }

    public override NodeState Evaluate()
    {
        var allies = GameObject.FindGameObjectsWithTag(_agentData.gameObject.tag);

        foreach (var ally in allies)
        {
            AgentData allyData = ally.GetComponent<AgentData>();
            if (allyData.HasFriendlyFlag || allyData.HasEnemyFlag)
            {
                _agent.Blackboard.ModifyData("AllyWithFlag", ally);
                return NodeState.SUCCESS;
            }
        }

        return NodeState.FAILURE;
    }
}
