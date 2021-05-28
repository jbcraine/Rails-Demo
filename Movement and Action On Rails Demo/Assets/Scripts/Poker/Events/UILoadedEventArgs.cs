using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UILoadedEventArgs : EventArgs
{
    public RectTransform container {get; private set;}

    public UILoadedEventArgs(RectTransform r)
    {
        container = r;
    }
}
