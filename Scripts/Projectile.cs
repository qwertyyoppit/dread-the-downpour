using UnityEngine;

public class Projectile
{
    public CharacterBody Owner;

    public ProjectileData Data;

    public Vector3 Origin;

    public static void FireProjectile(Vector3 origin, Vector3 direction, ProjectileData data, CharacterBody owner = null)
    {
        if (Physics.Raycast(origin, direction, out var hit))
        {
            if (hit.transform.TryGetComponent<CharacterBody>(out var body))
            {
                float falloff = GetFalloffFactor(origin, hit.point, data);
                DamageSystem.Damage(body, (owner ? owner.Damage : 10.0f) * falloff);
            }
        }
    }

    private static float GetFalloffFactor(Vector3 origin, Vector3 hit, ProjectileData data)
    {
        switch (data.Falloff)
        {
            case BulletFalloff.Linear:
                return 1 - 0.5f * (Mathf.Min(Mathf.Max(Vector3.Distance(origin, hit) - 25.0f, 0.0f), 35.0f) / 35.0f);
            case BulletFalloff.Buckshot:
                return 1 - 0.75f * (Mathf.Min(Mathf.Max(Vector3.Distance(origin, hit) - 7.0f, 0.0f), 18.0f) / 18.0f);
            default:
                return 1.0f;
        }
    }
}

//projectile stats:
//	speed

//    bullet(hitscan) damage falloff
//		none
//		linear (25m 100%, 25-60m 100-50%) most used
//		buckshot (7m 100%, 7-25m 100-25%) phase blast
//	aoe falloff
//		none
//		linear (radius=0->radius 100%->0%)
//		sweet spot (0-half 100%, half-full 25%) most used

//	projectiles can do damage by 'hitting' or passively
//		on collision
//		whenever in an update
//	projectiles can spawn projectiles
//		on collision

//	artificer ball
//		passively deals damage around
//		on hit, spawns artificer ball explosion projectile (!!!seperate!!!)
//			therefore they can have different projectile stats/falloff