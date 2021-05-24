using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //Begin by disabling the Interactable component
    private void Awake() {
        this.enabled = false;
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with " + name);
    }
}
