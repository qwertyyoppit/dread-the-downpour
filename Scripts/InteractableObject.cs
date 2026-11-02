using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    public bool CanInteract = true;

    public Action<InteractionArgs> Interacted;

    public void Interact(CharacterBody interactor)
    {
        Interacted?.Invoke(new InteractionArgs(this, interactor, !CanInteract));
    }
}
