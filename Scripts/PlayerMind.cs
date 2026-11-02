using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMind : CharacterMind
{
    public float InteractionRange = 5.0f;

    private InputSystem_Actions input;

    protected override void Awake()
    {
        base.Awake();
        input = new();
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    protected override void MindUpdate()
    {
        if (input.Player.Interact.WasPressedThisFrame())
        {
            var iObj = FindBestInteraction();
            if (iObj)
            {
                iObj.Interact(Body);
            }
        }
        var movement = input.Player.Move.ReadValue<Vector2>();
        var cam = Camera.main;
        moveDir = cam.transform.forward * movement.y + cam.transform.right * movement.x;

        if (input.Player.Jump.WasPressedThisFrame())
        {
            Body.Jump();
        }
        if (input.Player.Attack.IsPressed())
        {
            Body.UseSkill(SkillSlot.Primary);
        }
        if (input.Player.Secondary.IsPressed())
        {
            Body.UseSkill(SkillSlot.Secondary);
        }
    }
    public override void Target()
    {
        var cam = Camera.main;
        var camHits = Physics.RaycastAll(cam.transform.position, cam.transform.forward);
        Vector3 camPoint = cam.transform.forward * cam.farClipPlane;
        foreach (var camHit in camHits)
        {
            if (camHit.collider.gameObject == gameObject) continue;
            camPoint = camHit.point;
        }
        target = Physics.Raycast(transform.position, (camPoint - transform.position).normalized, out var hit)
            ? hit.point
            : camPoint;
    }

    public InteractableObject FindBestInteraction()
    {
        var cam = Camera.main;

        var camRay = new Ray(cam.transform.position, cam.transform.forward);
        var results = Physics.RaycastAll(camRay, float.PositiveInfinity, LayerMask.GetMask("Default"), QueryTriggerInteraction.Collide);

        var intObjects = new List<InteractableObject>();
        foreach (var hit in results)
        {
            if (hit.collider.isTrigger
                && Vector3.Distance(hit.point, transform.position) <= InteractionRange
                && hit.collider.TryGetComponent<InteractableObject>(out var iobj))
            {
                intObjects.Add(iobj);
            }
        }
        intObjects.OrderByDescending(iObj => Vector3.Dot(cam.transform.forward, (iObj.transform.position - cam.transform.position).normalized));
        foreach (var iObj in intObjects)
        {
            if (iObj.CanInteract) return iObj;
        }
        return null;
    }
}
