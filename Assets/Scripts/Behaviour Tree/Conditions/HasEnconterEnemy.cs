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
        _agent.ClosestEnemy = _agentSenses.GetNearestEnemyInView();
        return _agentSenses.GetEnemiesInView().Count > 0 ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
