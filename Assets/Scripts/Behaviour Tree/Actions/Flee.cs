public class Flee : Node
{
    private AgentActions _agentActions;
    private Sensing _agentSenses;

    public Flee(AgentActions agentActions, Sensing agentSenses)
    {
        _agentActions = agentActions;
        _agentSenses = agentSenses;
    }

    public override NodeState Evaluate()
    {
        _agentActions.Flee(_agentSenses.GetNearestEnemyInView());

        return NodeState.RUNNING;
    }
}
