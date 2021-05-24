using System;
public delegate void OnResponseSelect(ResponseSelectArgs r);

public class ResponseSelectArgs : EventArgs
{
    public int npcResponseID {get; private set;}

    public ResponseSelectArgs(int r)
    {
        npcResponseID = r;
    }
}

