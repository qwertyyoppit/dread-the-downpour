using System;
using UnityEngine;

[Serializable]
public class MindBehaviorRequirements
{
    public SkillSlot skillSlot;
    public SkillInfo skill;
    public bool skillReady;
    public bool equipmentReady;
    public RequiredRange userHealth;
    public float healthMin;
    public float healthMax;
    public RequiredRange targetHealth;
    public float targetHealthMin;
    public float targetHealthMax;
    public RequiredRange targetDistance;
    public float targetDistanceMin;
    public float targetDistanceMax;
    public bool targetLineOfSight;
    public bool aimTarget; // whether to retarget after behavior
    public bool grounded;
    public bool noRepetition;

    public enum RequiredRange
    {
        None,
        Less,
        More,
        Range
    }

    public bool Check(CharacterMind mind)
    {
        if (skillSlot != SkillSlot.None)
        {
            if (!mind.Body.skillLoadout[skillSlot].Info) return false;
            if (skill)
            {
                if (mind.Body.skillLoadout[skillSlot].Info != skill) return false;
            }
            if (skillReady)
            {
                if (mind.Body.skillLoadout[skillSlot].cooldown > 0) return false;
            }
        }
        else for (int i = 0; i <= (int)SkillSlot.End; i++)
        {
            if (i == (int)SkillSlot.End)
            {
                return false;
            }
            if (mind.Body.skillLoadout[i].Info == skill)
            {
                break;
            }
        }

        if (targetDistance != RequiredRange.None)
        {
            float distance = Vector3.Distance(mind.Body.transform.position, mind.target);
            bool lessThan = distance < targetDistanceMax;
            bool moreThan = distance > targetDistanceMin;
            if (targetDistance == RequiredRange.Less && !lessThan) return false;
            if (targetDistance == RequiredRange.More && !moreThan) return false;
            if (targetDistance == RequiredRange.Range && (!lessThan || !moreThan)) return false;
        }

        if (targetLineOfSight) return mind.HasLineOfSight();

        return true;
    }
}
