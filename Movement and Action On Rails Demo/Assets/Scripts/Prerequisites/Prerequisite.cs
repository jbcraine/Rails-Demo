using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Prerequisites keep track of a condition and evaluate it to True/False. Other classes guide decisions by the use of Prerequisites.
public abstract class Prerequisite : MonoBehaviour
{
    public virtual bool complete
    {
        get {return false;}
    }
}
