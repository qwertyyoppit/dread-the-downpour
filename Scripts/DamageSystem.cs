using UnityEngine;

public static class DamageSystem
{
    public static void Damage(CharacterBody body, float damage)
    {
        body.Health -= damage;
        if (body.Health <= 0) body.Die();
        Debug.Log($"{damage} dealt to {body} now with {body.Health}");
    }
}
