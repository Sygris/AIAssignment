public class UseItem : Node
{
    private AgentActions _agentActions;
    private InventoryController _agentInventory;
    private string _itemToUse;

    public UseItem(AgentActions agentActions, InventoryController agentInventory, string itemToUse)
    {
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
