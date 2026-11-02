using UnityEngine;

[CreateAssetMenu(fileName = "DoubleTapSkillData", menuName = "Scriptable Objects/DoubleTapSkillData")]
public class DoubleTapSkillData : SkillData
{
    public GameObject bulletTrail;
    public ProjectileData data;
    public override void Use(CharacterBody body)
    {
        var trailObj = Instantiate(bulletTrail, body.transform.position, Quaternion.identity);
        
        trailObj.transform.forward = body.lookDir;
        var trail = trailObj.GetComponent<BulletTrailVisuals>();
        trail.StartProjectile(body.transform.position + body.lookDir*trail.Length);

        if (!data) return;
        Projectile.FireProjectile(body.transform.position, body.lookDir, data, body);
    }
}

public static class blowup
{
    public static void theworld()
    {
        Application.Quit();
    }
}