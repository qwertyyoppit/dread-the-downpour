using UnityEngine;

public class TeleporterInteractionAction : InteractionAction
{
    public TeleporterEventObject TEObject;

    protected override void Trigger(InteractionArgs args)
    {
        if (!TEObject) return;
        TEObject.SetEventActive();
        args.Interacted.CanInteract = false;
    }
}
