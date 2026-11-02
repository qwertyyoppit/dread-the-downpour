using UnityEngine;

public class BaseAIMind : CharacterMind
{
    protected override void MindUpdate()
    {
        target = targetBody ? targetBody.transform.position : moveDir + Body.transform.position;

        foreach (var behavior in Behaviors)
        {
            if (!behavior.Requirements.Check(this)) continue;
            behavior.Action.Start(this);
            break;
        }
    }

    public override void Target()
    {
        foreach (var body in FindObjectsByType<CharacterBody>())
        {
            if (body == Body) continue;
            if (body.GetType() == Body.GetType()) continue; //temporary testing thing, obviously replace with factions
            if (Vector3.Distance(body.transform.position, transform.position) > 50.0f) continue;
            targetBody = body;
            break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Body.transform.position, Body.transform.position + Body.lookDir * 10.0f);
    }
}
