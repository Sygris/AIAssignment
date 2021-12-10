using UnityEngine;

public class Flee : Node
{
    private AgentActions _agentActions;
    private Sensing _agentSenses;
    private Node _moveToNode;
    private Node _consumeItem;
    private float _fleeSafeDistance = 10f;

    public Flee(AgentActions agentActions, Sensing agentSenses, Node moveToNode, Node consumeItem)
    {
        _agentActions = agentActions;
        _agentSenses = agentSenses;
        _moveToNode = moveToNode;
        _consumeItem = consumeItem;
    }

    public override NodeState Evaluate()
    {
        _moveToNode.Evaluate();

        // Calls Branch Use/Collect Item branch
        _consumeItem.Evaluate();

        if (Vector3.Distance(_agentSenses.GetNearestEnemyInView().transform.position, _agentActions.gameObject.transform.position) > _fleeSafeDistance)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }
}
