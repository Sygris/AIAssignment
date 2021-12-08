using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node
{
    private AI _agent;
    private AgentActions _agentActions;

    public Attack(AI agent, AgentActions agentActions)
    {
        _agent = agent;
        _agentActions = agentActions;
    }

    public override NodeState Evaluate()
    {
        SetState(NodeState.RUNNING);

        _agentActions.AttackEnemy(_agent.ClosestEnemy);

        if (_agent.ClosestEnemy == null)
        {
            SetState(NodeState.SUCCESS);
        }

        return _nodeState;
    }
}
