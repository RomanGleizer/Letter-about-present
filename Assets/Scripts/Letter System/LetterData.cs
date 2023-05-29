using System;

[Serializable]
public class LetterData
{
    public bool[] IsLettersWereRead = new bool[15];
    public bool[] IsLettersWereSkiped = new bool[15];
    public int ReadIndex = 0;
    public int SkipIndex = 0;
}