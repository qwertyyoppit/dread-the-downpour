using UnityEngine;

public abstract class InteractionAction : MonoBehaviour
{
    public InteractableObject ConnectedInteraction;

    protected virtual void OnEnable()
    {
        ConnectedInteraction.Interacted += Trigger;
    }

    protected virtual void OnDisable()
    {
        ConnectedInteraction.Interacted -= Trigger;
    }

    protected abstract void Trigger(InteractionArgs args);
}

public readonly struct InteractionArgs
{
    readonly public InteractableObject Interacted;
    readonly public CharacterBody Interactor;
    readonly public bool Canceled;
    public InteractionArgs(InteractableObject interacted, CharacterBody interactor, bool canceled = false)
    {
        Interacted = interacted;
        Interactor = interactor;
        Canceled = canceled;
    }
}
