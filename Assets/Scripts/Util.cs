using UnityEngine;

public static class Util
{
    public static GameObject DetermineTarget(AI agent, GameObject gameObject, TargetTypes targetTypes)
    {
        GameObject tmp = null;

        if (gameObject == null)
        {
            switch (targetTypes)
            {
                case TargetTypes.FLAG:
                    tmp = (GameObject)agent.Blackboard.GetData("PriorityFlag");
                    break;
                case TargetTypes.ENEMY:
                    tmp = (GameObject)agent.Blackboard.GetData("ClosestEnemy");
                    break;
                case TargetTypes.HEALTHKIT:
                    tmp = (GameObject)agent.Blackboard.GetData(Names.HealthKit);
                    break;
                case TargetTypes.POWERUP:
                    tmp = (GameObject)agent.Blackboard.GetData(Names.PowerUp);
                    break;
                case TargetTypes.ALLY:
                    tmp = (GameObject)agent.Blackboard.GetData("AllyWithFlag");
                    break;
                default:
                    break;
            }
        }
        else
        {
            tmp = gameObject;
        }

        return tmp;
    }
}

public enum TargetTypes { FLAG, ENEMY, HEALTHKIT, POWERUP, ALLY }
