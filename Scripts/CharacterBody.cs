using System;
using UnityEngine;

public abstract class CharacterBody : MonoBehaviour
{
    public float MaxHealth = 100.0f;
    public float Health = 100.0f;
    public float HealthRegenRate = 1.0f;
    public float Damage = 10.0f;
    public float CooldownRate = 1.0f;
    public float Speed = 10.0f;
    public float Acceleration = 10.0f;
    public float JumpForce = 5.0f;
    public float LookSpeed = 6.0f;

    public Vector3 lookDir;

    public Rigidbody Rb;
    public BoxCollider Collider;

    public SkillLoadout skillLoadout;

    public Action<CharacterBody> Died;

    //public Armature;

    private void Awake()
    {
        if (!Rb) Rb = GetComponent<Rigidbody>();
        if (!Collider) Collider = GetComponent<BoxCollider>();

        lookDir = transform.forward;
    }

    private void Update()
    {
        if (!enabled) return;

        Health += HealthRegenRate * Time.deltaTime;
        Health = Mathf.Min(Health, MaxHealth);

        for (SkillSlot slot = 0; slot < SkillSlot.End; slot++)
        {
            skillLoadout[slot].cooldown = Mathf.Max(skillLoadout[slot].cooldown - Time.deltaTime, 0.0f);
        }
    }

    public void Look(Vector3 direction)
    {
        if (!enabled) return;

        lookDir = Vector3.RotateTowards(lookDir, direction.normalized, LookSpeed * Time.deltaTime, float.MaxValue);
    }

    public virtual void Move(Vector3 direction)
    {
        if (!enabled) return;

        direction.y = 0;
        direction.Normalize();
        var move = new Vector3(direction.x, 0, direction.z);
        move *= Speed;
        var horzdir = transform.forward;
        horzdir.y = 0;
        direction = direction.sqrMagnitude != 0 ? direction : transform.forward;
        horzdir = Vector3.RotateTowards(horzdir, direction, LookSpeed * Mathf.PI * Time.deltaTime, 1);
        transform.forward = new Vector3(horzdir.x, transform.forward.y, horzdir.z).normalized;

        Rb.linearVelocity = new(move.x, Rb.linearVelocity.y, move.z);
    }
    //Move jump to child abstract class for walking characters? or to an interface to combine actions? or a list of a new Action class maybe yeah maybe...
    public virtual void Jump()
    {
        if (!enabled) return;

        Rb.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
    }

    public void UseSkill(SkillSlot slot)
    {
        if (!enabled) return;

        if (skillLoadout[slot].cooldown > 0.0f) return;
        skillLoadout[slot].Data.Use(this);
        skillLoadout[slot].cooldown = skillLoadout[slot].Data.UseCooldown * CooldownRate;
    }

    public virtual void Die() 
    {
        if (!enabled) return;
        enabled = false;
        Died?.Invoke(this);
    }
}