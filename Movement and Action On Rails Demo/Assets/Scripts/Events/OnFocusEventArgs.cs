using System;

public class OnFocusEventArgs : EventArgs
{
    public bool focusSet {get; private set;}

    public OnFocusEventArgs(bool set)
    {
        focusSet = set;
    }
}
