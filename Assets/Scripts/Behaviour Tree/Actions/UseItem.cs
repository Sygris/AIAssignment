public class UseItem : Node
{
    private AI _agent;
    private AgentActions _agentActions;
    private InventoryController _agentInventory;
    private string _itemToUse;

    public UseItem(AI agent, AgentActions agentActions, InventoryController agentInventory, string itemToUse)
    {
        _agent = agent;
        _agentActions = agentActions;
        _agentInventory = agentInventory;
        _itemToUse = itemToUse;
    }

    public override NodeState Evaluate()
    {
        switch (_itemToUse)
        {
            case Names.PowerUp:
                _agentActions.UseItem(_agentInventory.GetItem(Names.PowerUp));
                break;
            case Names.HealthKit:
                _agentActions.UseItem(_agentInventory.GetItem(Names.HealthKit));
                break;
            default:
                break;
        }

        return NodeState.SUCCESS;
    }
}
