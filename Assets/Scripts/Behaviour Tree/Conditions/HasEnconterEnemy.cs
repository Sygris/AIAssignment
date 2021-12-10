using UnityEngine;

public class HasEnconterEnemy : Node
{
    private AI _agent;
    private Sensing _agentSenses;

    private float _ignoreDistance = 15f;

    public HasEnconterEnemy(AI agent, Sensing agentSenses)
    {
        _agent = agent;
        _agentSenses = agentSenses;
    }

    public override NodeState Evaluate()
    {
        _agent.Blackboard.ModifyData("ClosestEnemy", _agentSenses.GetNearestEnemyInView());

        GameObject flag = (GameObject)_agent.Blackboard.GetData("PriorityFlag");

        // If the flag is not null and the AI is close to the priority flag ignore the enemy
        if (flag != null)
        {
            if (Vector3.Distance(_agent.transform.position, flag.transform.position) <= _ignoreDistance)
            {
                return NodeState.FAILURE;
            }
        }

        return _agentSenses.GetEnemiesInView().Count > 0 ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
