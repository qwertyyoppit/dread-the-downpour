using System;

[Serializable]
public class SkillLoadout
{
    public SkillPair PrimarySkill = new(SkillSlot.Primary);
    public SkillPair SecondarySkill = new(SkillSlot.Secondary);
    public SkillPair UtilitySkill = new(SkillSlot.Utility);
    public SkillPair SpecialSkill = new(SkillSlot.Special);

    public SkillPair this[int i]
    {
        get => this[(SkillSlot)i];
    }

    public SkillPair this[SkillSlot i]
    {
        get => i switch
        {
            SkillSlot.None => null,
            SkillSlot.End => null,
            SkillSlot.Primary => PrimarySkill,
            SkillSlot.Secondary => SecondarySkill,
            SkillSlot.Utility => UtilitySkill,
            SkillSlot.Special => SpecialSkill,
            _ => throw new NotImplementedException()
        };
    }
}

[Serializable]
public class SkillPair
{
    readonly public SkillSlot Slot;
    public SkillInfo Info;
    public SkillData Data => Info.SkillData;

    public float cooldown;

    public SkillPair(SkillSlot slot)
    {
        Slot = slot;
    }
}