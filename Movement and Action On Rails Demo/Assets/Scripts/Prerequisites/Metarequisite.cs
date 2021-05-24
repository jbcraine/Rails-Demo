using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A Metarequisite is a sort of Prerequisite evaluator. It can judge id a certain number of Prerequisites have been met, and deliver a final status on the matter
public class Metarequisite : MonoBehaviour
{
    public Prerequisite[] prerequisites;
    public int prerequisitesNeededMet;
    private int prerequisitesActualMet;

    private void Start() {
        prerequisites = GetComponents<Prerequisite>();
    }

    private bool Evaluate()
    {
        prerequisitesActualMet = 0;
        foreach (Prerequisite p in prerequisites)
        {
            if (p.complete)
            {
                if (++prerequisitesActualMet >= prerequisitesNeededMet)
                    return true;
            }
        }
        return false;
    }

    public bool complete
    {
        get {return Evaluate();}
    }
}
