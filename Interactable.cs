using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public abstract class Interactable : MonoBehaviour
{
    // message displayed to player when looking at an interactable.
    public bool useEvents;
    [SerializeField]
    public string promptMessage;
    
    public virtual string OnLook()
    {
        return promptMessage;
    }

    public void BaseInteract()
    {
        if (useEvents)
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        Interact();
    }

    protected virtual void Interact()
    {
        // we won't have any code written in this function
        // this is a template function to be overridden by our subclasses
    }
}