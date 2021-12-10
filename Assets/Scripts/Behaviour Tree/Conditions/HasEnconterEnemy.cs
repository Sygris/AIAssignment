using UnityEngine;

public class HasEnconterEnemy : Node
{
    private AI _agent;
    private Sensing _agentSenses;

    public HasEnconterEnemy(AI agent, Sensing agentSenses)
    {
        _agent = agent;
        _agentSenses = agentSenses;
    }

    public override NodeState Evaluate()
    {
        _agent.Blackboard.ModifyData("ClosestEnemy", _agentSenses.GetNearestEnemyInView());

        GameObject flag = (GameObject)_agent.Blackboard.GetData("PriorityFlag");

        if (Vector3.Distance(_agent.transform.position, flag.transform.position) <= 1f)
        {
            return NodeState.FAILURE;
        }

        return _agentSenses.GetEnemiesInView().Count > 0 ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
