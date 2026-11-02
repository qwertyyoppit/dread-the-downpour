using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    public float UseCooldown = 1.0f;

    public abstract void Use(CharacterBody body);
}
