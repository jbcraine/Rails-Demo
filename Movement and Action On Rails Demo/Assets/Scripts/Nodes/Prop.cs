using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public Location propLocation;
    private Collider coll;
    private Interactable interactable;

    // Start is called before the first frame update
    protected void Awake()
    {
        coll = GetComponent<Collider>();
        interactable = GetComponent<Interactable>();
    }

    protected void OnMouseDown() {
        interactable.Interact();
    }

}
