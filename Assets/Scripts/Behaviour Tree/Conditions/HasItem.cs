using UnityEngine;

public class HasItem : Node
{
    private InventoryController _agentInventory;

    private string _item;

    public HasItem(InventoryController inventory, string itemName)
    {
        _agentInventory = inventory;
        _item = itemName;
    }

    public override NodeState Evaluate()
    {
        if (_agentInventory.HasItem(_item))
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}
