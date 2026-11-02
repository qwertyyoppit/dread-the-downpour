using UnityEngine;

[CreateAssetMenu(fileName = "MindBehaviorData", menuName = "Scriptable Objects/MindBehaviorData")]
public class MindBehaviorData : ScriptableObject
{
    public string BehaviorName;

    public MindBehaviorRequirements Requirements;
    public MindBehaviorAction Action;
}
