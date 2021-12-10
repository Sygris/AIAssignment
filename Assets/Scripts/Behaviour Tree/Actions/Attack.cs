using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Node
{
    private AI _agent;
    private AgentActions _agentActions;
    private List<GameObject> _listOfBases = new List<GameObject>();

    public Attack(AI agent, AgentActions agentActions)
    {
        _agent = agent;
        _agentActions = agentActions;

        // Loops through every base. With more time I would create a static variable that would keep record of how many bases are
        // (I like to make my code so if in the future I want more bases per scene I could use the same code without changing anything)
        foreach (var item in Object.FindObjectsOfType<SetScore>())
        {
            _listOfBases.Add(item.gameObject);
        }
    }

    public override NodeState Evaluate()
    {
        SetState(NodeState.RUNNING);

        // If close to the priority flag cancel attack
        GameObject priorityFlag = (GameObject)_agent.Blackboard.GetData("PriorityFlag");
        if (Vector3.Distance(_agent.transform.position, priorityFlag.transform.position) >= 10f)
        {
            _agentActions.AttackEnemy((GameObject)_agent.Blackboard.GetData("ClosestEnemy"));

            // Killed the enemy
            if (_agent.Blackboard.GetData("ClosestEnemy") == null)
            {
                SetState(NodeState.SUCCESS);
            }
        }
        else
        {
            SetState(NodeState.FAILURE);
        }


        return _nodeState;
    }
}
