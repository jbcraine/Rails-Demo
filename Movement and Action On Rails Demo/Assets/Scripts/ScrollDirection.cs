using System;

[Flags]
public enum ScrollDirection
{
    None = 0b_0000_0000, // 0
    Top = 0b_0000_0001, // 1
    Left = 0b_0000_0010, //2
    Right = 0b_0000_0100, //4
    Bottom = 0b_0000_1000 // 8
}