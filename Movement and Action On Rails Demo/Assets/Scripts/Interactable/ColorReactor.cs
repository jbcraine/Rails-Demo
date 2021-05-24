using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorReactor : StateReactor
{
    public Color active;
    public Color inactive;
    MeshRenderer mesh;

    protected override void Awake()
    {
        base.Awake();
        mesh = GetComponentInChildren<MeshRenderer>();
        React();
    }

    public override void React()
    {
        if (switcher.state)
        {
            mesh.material.color = active;
        }
        else
        {
            mesh.material.color = inactive;
        }
    }
}
