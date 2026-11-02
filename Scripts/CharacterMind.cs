using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CharacterMind : MonoBehaviour
{
    public CharacterBody Body;

    public List<MindBehaviorData> Behaviors;

    public Vector3 target;
    public Vector3 targetLookDir;
    public CharacterBody targetBody;
    public float TargetRate = 1.0f;
    protected float targetCooldown;

    public Vector3 moveDir;
    protected bool jump;

    protected virtual void Awake()
    {
        if (!Body) GetComponent<CharacterBody>();
    }

    private void Update()
    {
        if (!Body) return;
        MindUpdate();
        //eventually move all update functions into here, add variables for all functions like moveDir
        targetLookDir = (target - transform.position).normalized;
        Body.Look(targetLookDir);
        Body.Move(new Vector3(moveDir.x, 0, moveDir.z));
        if (targetCooldown >= TargetRate)
        {
            Target();
            targetCooldown = 0.0f;
        }
        targetCooldown += Time.deltaTime;
    }

    protected abstract void MindUpdate();

    public abstract void Target();

    public bool HasLineOfSight()
    {
        if (targetBody)
        {
            var hits = Physics.RaycastAll(Body.transform.position, Body.lookDir);
            if (hits.Length < 1) return false;
            RaycastHit closest = hits[0];
            
            foreach (var hit in hits)
            {
                if (hit.transform.GetComponent<CharacterBody>() == Body) continue;
                if (Vector3.Distance(hit.point, Body.transform.position) < Vector3.Distance(closest.point, Body.transform.position))
                {
                    closest = hit;
                }
            }
            if (closest.transform.TryGetComponent<CharacterBody>(out var hitbody))
            {
                return hitbody == targetBody;
            }
        }
        else
        {
            Physics.Raycast(transform.position, Body.lookDir, out var hit);
            return hit.point == target;
        }
        return false;
    }
}
