using System;
using UnityEngine;

[Serializable]
public class MindBehaviorAction
{
    public MoveType moveType;
    public TargetType targetType;
    public AimType aimType;
    public SkillSlot skillSlot;
    public SkillInfo skill;
    public bool sprint;
    public float behaviorTimer;
    public bool fireEquipment;

    public enum MoveType
    {
        None,
        Move,
        Strafe
    }
    public enum TargetType
    {
        Custom,
        Enemy,
        Friend,
        Leader
    }
    public enum AimType
    {
        None,
        Target,
        Enemy,
        Leader,
        MoveDirection
    }

    public void Start(CharacterMind mind)
    {
        //behaviorTimer should keep firing for x duration even when out of reach, basically ignoring distance, health, etc. requirements
        if (aimType == AimType.Target) mind.Body.Look(mind.target - mind.Body.transform.position);
        if (moveType == MoveType.Move) mind.moveDir = (mind.target - mind.Body.transform.position);
        else if (moveType == MoveType.Strafe) mind.Body.Move(Vector3.Cross(mind.target - mind.Body.transform.position, mind.Body.transform.up));
        if (skillSlot != SkillSlot.None && (!skill || mind.Body.skillLoadout[skillSlot].Info == skill)) mind.Body.UseSkill(skillSlot);
        else if (skill)
        {
            for (SkillSlot i = 0; i < SkillSlot.End; i++)
            {
                if (mind.Body.skillLoadout[i].Info == skill) mind.Body.UseSkill(i);
            }
        }
    }
}