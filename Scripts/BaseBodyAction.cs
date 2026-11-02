using UnityEngine;

/// <summary>
/// Class for all actions a character body can perform.
/// Scriptable to allow usage of serialized assets.
/// </summary>
public abstract class BaseBodyAction : ScriptableObject
{
    public abstract void Use(CharacterBody body);
}
