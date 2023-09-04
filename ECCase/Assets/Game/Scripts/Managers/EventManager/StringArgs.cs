using System;
 
public class StringArgs : EventArgs
{
    public string value;

    public StringArgs(string v)
    {
        this.value = v;
    }
} 
