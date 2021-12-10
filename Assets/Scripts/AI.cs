using System.Collections.Generic;
using UnityEngine;

/*****************************************************************************************************************************
 * Write your core AI code in this file here. The private variable 'agentScript' contains all the agents actions which are listed
 * below. Ensure your code it clear and organised and commented.
 *
 * Unity Tags
 * public static class Tags
 * public const string BlueTeam = "Blue Team";	The tag assigned to blue team members.
 * public const string RedTeam = "Red Team";	The tag assigned to red team members.
 * public const string Collectable = "Collectable";	The tag assigned to collectable items (health kit and power up).
 * public const string Flag = "Flag";	The tag assigned to the flags, blue or red.
 * 
 * Unity GameObject names
 * public static class Names
 * public const string PowerUp = "Power Up";	Power up name
 * public const string HealthKit = "Health Kit";	Health kit name.
 * public const string BlueFlag = "Blue Flag";	The blue teams flag name.
 * public const string RedFlag = "Red Flag";	The red teams flag name.
 * public const string RedBase = "Red Base";    The red teams base name.
 * public const string BlueBase = "Blue Base";  The blue teams base name.
 * public const string BlueTeamMember1 = "Blue Team Member 1";	Blue team member 1 name.
 * public const string BlueTeamMember2 = "Blue Team Member 2";	Blue team member 2 name.
 * public const string BlueTeamMember3 = "Blue Team Member 3";	Blue team member 3 name.
 * public const string RedTeamMember1 = "Red Team Member 1";	Red team member 1 name.
 * public const string RedTeamMember2 = "Red Team Member 2";	Red team member 2 name.
 * public const string RedTeamMember3 = "Red Team Member 3";	Red team member 3 name.
 * 
 * _agentData properties and public variables
 * public bool IsPoweredUp;	Have we powered up, true if we’re powered up, false otherwise.
 * public int CurrentHitPoints;	Our current hit points as an integer
 * public bool HasFriendlyFlag;	True if we have collected our own flag
 * public bool HasEnemyFlag;	True if we have collected the enemy flag
 * public GameObject FriendlyBase; The friendly base GameObject
 * public GameObject EnemyBase;    The enemy base GameObject
 * public int FriendlyScore; The friendly teams score
 * public int EnemyScore;       The enemy teams score
 * 
 * _agentActions methods
 * public bool MoveTo(GameObject target)	Move towards a target object. Takes a GameObject representing the target object as a parameter. Returns true if the location is on the navmesh, false otherwise.
 * public bool MoveTo(Vector3 target)	Move towards a target location. Takes a Vector3 representing the destination as a parameter. Returns true if the location is on the navmesh, false otherwise.
 * public bool MoveToRandomLocation()	Move to a random location on the level, returns true if the location is on the navmesh, false otherwise.
 * public void CollectItem(GameObject item)	Pick up an item from the level which is within reach of the agent and put it in the inventory. Takes a GameObject representing the item as a parameter.
 * public void DropItem(GameObject item)	Drop an inventory item or the flag at the agents’ location. Takes a GameObject representing the item as a parameter.
 * public void UseItem(GameObject item)	Use an item in the inventory (currently only health kit or power up). Takes a GameObject representing the item to use as a parameter.
 * public void AttackEnemy(GameObject enemy)	Attack the enemy if they are close enough. ). Takes a GameObject representing the enemy as a parameter.
 * public void Flee(GameObject enemy)	Move in the opposite direction to the enemy. Takes a GameObject representing the enemy as a parameter.
 * 
 * _agentSenses properties and methods
 * public List<GameObject> GetObjectsInViewByTag(string tag)	Return a list of objects with the same tag. Takes a string representing the Unity tag. Returns null if no objects with the specified tag are in view.
 * public GameObject GetObjectInViewByName(string name)	Returns a specific GameObject by name in view range. Takes a string representing the objects Unity name as a parameter. Returns null if named object is not in view.
 * public List<GameObject> GetObjectsInView()	Returns a list of objects within view range. Returns null if no objects are in view.
 * public List<GameObject> GetCollectablesInView()	Returns a list of objects with the tag Collectable within view range. Returns null if no collectable objects are in view.
 * public List<GameObject> GetFriendliesInView()	Returns a list of friendly team AI agents within view range. Returns null if no friendlies are in view.
 * public List<GameObject> GetEnemiesInView()	Returns a list of enemy team AI agents within view range. Returns null if no enemies are in view.
 * public GameObject GetNearestEnemyInView()   Returns the nearest enemy AI in view to the agent. Returns null if no enemies are in view.
 * public bool IsItemInReach(GameObject item)	Checks if we are close enough to a specific collectible item to pick it up), returns true if the object is close enough, false otherwise.
 * public bool IsInAttackRange(GameObject target)	Check if we're with attacking range of the target), returns true if the target is in range, false otherwise.
 * public Vector3 GetVectorToTarget(GameObject target)  Return a normalised vector pointing to the target GameObject
 * public Vector3 GetVectorToTarget(Vector3 target)     Return a normalised vector pointing to the target vector
 * public Vector3 GetFleeVectorFromTarget(GameObject target)    Return a normalised vector pointing away from the target GameObject
 * public Vector3 GetFleeVectorFromTarget(Vector3 target)   Return a normalised vector pointing away from the target vector
 * 
 * _agentInventory properties and methods
 * public bool AddItem(GameObject item)	Adds an item to the inventory if there's enough room (max capacity is 'Constants.InventorySize'), returns true if the item has been successfully added to the inventory, false otherwise.
 * public GameObject GetItem(string itemName)	Retrieves an item from the inventory as a GameObject, returns null if the item is not in the inventory.
 * public bool HasItem(string itemName)	Checks if an item is stored in the inventory, returns true if the item is in the inventory, false otherwise.
 * 
 * You can use the game objects name to access a GameObject from the sensing system. Thereafter all methods require the GameObject as a parameter.
 * 
*****************************************************************************************************************************/

/// <summary>
/// Implement your AI script here, you can put everything in this file, or better still, break your code up into multiple files
/// and call your script here in the Update method. This class includes all the data members you need to control your AI agent
/// get information about the world, manage the AI inventory and access essential information about your AI.
///
/// You may use any AI algorithm you like, but please try to write your code properly and professionaly and don't use code obtained from
/// other sources, including the labs.
///
/// See the assessment brief for more details
/// </summary>
public class AI : MonoBehaviour
{
    #region Variables already given
    // Gives access to important data about the AI agent (see above)
    private AgentData _agentData;
    // Gives access to the agent senses
    private Sensing _agentSenses;
    // gives access to the agents inventory
    private InventoryController _agentInventory;
    // This is the script containing the AI agents actions
    // e.g. agentScript.MoveTo(enemy);
    private AgentActions _agentActions;
    #endregion

    #region Variables addded by Tiago Antunes Boa Vista
    // Root node of the main Tree
    private Node _topNode;
    private Node _attackTree;

    // Blackboard
    private Blackboard _blackboard;
    public Blackboard Blackboard { get { return _blackboard; } }
    private GameObject _friendlyBase;
    #endregion

    void Start()
    {
        // Initialise the accessable script components
        _agentData = GetComponent<AgentData>();
        _agentActions = GetComponent<AgentActions>();
        _agentSenses = GetComponentInChildren<Sensing>();
        _agentInventory = GetComponentInChildren<InventoryController>();

        InitialiseBehaviourTree();
    }

    void Update()
    {
        _topNode.Evaluate();
        UpdateBlackboard();
    }

    private void InitialiseBehaviourTree()
    {
        #region Blackboard
        _blackboard = new Blackboard();

        _blackboard.AddData(_agentData.EnemyFlagName, GameObject.Find(_agentData.EnemyFlagName));
        _blackboard.AddData(_agentData.FriendlyFlagName, GameObject.Find(_agentData.FriendlyFlagName));
        _blackboard.AddData(_agentData.EnemyBase.name, GameObject.Find(_agentData.EnemyBase.name));
        _blackboard.AddData(_agentData.FriendlyBase.name, GameObject.Find(_agentData.FriendlyBase.name));
        _blackboard.AddData(Names.HealthKit, GameObject.Find(Names.HealthKit));
        _blackboard.AddData(Names.PowerUp, GameObject.Find(Names.PowerUp));
        _blackboard.AddData("AllyWithFlag", null);
        // TODO: Create an enum that stores these strings so there are no "magic strings" in the project and prevent errors
        _blackboard.AddData("PriorityFlag", null);
        _blackboard.AddData("ClosestEnemy", null);

        _friendlyBase = (GameObject)_blackboard.GetData(_agentData.FriendlyBase.name);

        #endregion
        Selector collectUseItemSelector = new Selector();
        Sequence healthKitSequence = new Sequence();
        Selector useCollectHealthKit = new Selector();
        Sequence useHealthKit = new Sequence();
        Sequence collectHealthKit = new Sequence();
        Sequence collectPowerUp = new Sequence();

        healthKitSequence.AddChild(new IsHealthLessThan(_agentData.HealthThreshold, _agentData));
        healthKitSequence.AddChild(useCollectHealthKit);
        useCollectHealthKit.AddChild(useHealthKit);
        useCollectHealthKit.AddChild(collectHealthKit);

        useHealthKit.AddChild(new HasItem(_agentInventory, Names.HealthKit));
        useHealthKit.AddChild(new UseItem(this, _agentActions, _agentInventory, Names.HealthKit));

        collectHealthKit.AddChild(new MoveToPosition(this, _agentActions, null, _agentData.PickUpRange, TargetTypes.HEALTHKIT));
        collectHealthKit.AddChild(new CollectItem(this, _agentActions, _agentSenses, _agentInventory, TargetTypes.HEALTHKIT));
        collectHealthKit.AddChild(new UseItem(this, _agentActions, _agentInventory, Names.HealthKit));

        collectPowerUp.AddChild(new Inverter(new HasItem(_agentInventory, Names.PowerUp)));
        collectPowerUp.AddChild(new MoveToPosition(this, _agentActions, null, _agentData.PickUpRange, TargetTypes.POWERUP));
        collectPowerUp.AddChild(new CollectItem(this, _agentActions, _agentSenses, _agentInventory, TargetTypes.POWERUP));

        collectUseItemSelector.AddChild(healthKitSequence);
        collectUseItemSelector.AddChild(collectPowerUp);

        #region Attack Tree
        Node hasEncounterEnemy = new HasEnconterEnemy(this, _agentSenses);
        Node shouldFlee = new ShouldFlee(this, _agentData, _agentSenses);
        Node flee = new Flee(_agentActions, _agentSenses, new MoveToPosition(this, _agentActions, _friendlyBase, 2.5f), collectUseItemSelector);
        Node hasPowerUp = new HasItem(_agentInventory, Names.PowerUp);
        Node usePowerUp = new UseItem(this, _agentActions, _agentInventory, Names.PowerUp);
        Node getClose = new MoveToPosition(this, _agentActions, null, _agentData.AttackRange, TargetTypes.ENEMY);
        Node attack = new Attack(this, _agentActions);
        Node doIHaveBlueFlag = new Inverter(new HasItem(_agentInventory, Names.BlueFlag));
        Node doIHaveRedFlag = new Inverter(new HasItem(_agentInventory, Names.RedFlag));
        Node amITooCloseToEnemyFlag = new Inverter(new ComparePositions(this, null, 12f));


        Sequence fleeSequence = new Sequence(new List<Node> { shouldFlee, flee });
        Sequence attackSequence = new Sequence(new List<Node> { getClose, attack });
        Sequence attackWithPowerUpSequence = new Sequence(new List<Node> { hasPowerUp, usePowerUp, attackSequence });
        Selector mainSelector = new Selector(new List<Node> { fleeSequence, attackWithPowerUpSequence, attackSequence });
        Sequence mainSequence = new Sequence(new List<Node> { hasEncounterEnemy, doIHaveBlueFlag, doIHaveRedFlag, amITooCloseToEnemyFlag, mainSelector });

        _attackTree = mainSequence;
        #endregion

        Selector root = new Selector();

        Sequence returnFlag = new Sequence();
        Sequence dropFlag = new Sequence();

        Sequence captureFlag = new Sequence();

        returnFlag.AddChild(new IsCarryingFlag(_agentInventory));
        returnFlag.AddChild(dropFlag);

        dropFlag.AddChild(new MoveToPosition(this, _agentActions, _friendlyBase, 2f));
        dropFlag.AddChild(new ComparePositions(this, _friendlyBase, 2f));
        dropFlag.AddChild(new DropItem(this, _agentActions));

        Selector attackSelector = new Selector();
        Sequence captureFlagSequence = new Sequence();
        attackSelector.AddChild(_attackTree);
        attackSelector.AddChild(captureFlagSequence);

        captureFlag.AddChild(new IsAnyFlagOutsideFriendlyBase(_friendlyBase, this, _agentData, _blackboard));
        captureFlag.AddChild(attackSelector);
        captureFlagSequence.AddChild(new MoveToPosition(this, _agentActions));
        captureFlagSequence.AddChild(new ComparePositions(this));
        captureFlagSequence.AddChild(new CollectItem(this, _agentActions, _agentSenses, _agentInventory));

        Selector defendSelector = new Selector();
        Sequence defendBaseSequence = new Sequence();
        Selector defendBaseSelector = new Selector();
        Sequence defendAllySequence = new Sequence();
        Selector defendAllySelector = new Selector();

        defendSelector.AddChild(defendBaseSequence);
        defendBaseSequence.AddChild(new AreBothFlagsInFriendlyBase(_friendlyBase, this, _agentData, Blackboard));
        defendBaseSequence.AddChild(defendBaseSelector);

        defendBaseSelector.AddChild(_attackTree);
        defendBaseSelector.AddChild(new MoveToPosition(this, _agentActions, _friendlyBase, 2f));

        defendSelector.AddChild(defendAllySequence);
        defendAllySequence.AddChild(new DoesAllyHaveFlag(this, _agentData));
        defendAllySequence.AddChild(defendAllySelector);
        defendAllySelector.AddChild(_attackTree);
        defendAllySelector.AddChild(new MoveToPosition(this, _agentActions, null, 4f, TargetTypes.ALLY));

        root.AddChild(returnFlag);
        root.AddChild(captureFlag);
        root.AddChild(collectUseItemSelector);
        root.AddChild(defendSelector);

        _topNode = root;
    }

    public void UpdateBlackboard()
    {
        _blackboard.ModifyData(Names.HealthKit, _agentSenses.GetNearestObjectInViewByName(Names.HealthKit));

        _blackboard.ModifyData(Names.PowerUp, _agentSenses.GetNearestObjectInViewByName(Names.PowerUp));
    }
}