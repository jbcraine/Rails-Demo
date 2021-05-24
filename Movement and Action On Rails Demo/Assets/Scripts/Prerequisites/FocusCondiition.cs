using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCondiition : Prerequisite
{
    public override bool complete
    {
        get {return GameManager.manager.currentlyFocused;}
    }
}
