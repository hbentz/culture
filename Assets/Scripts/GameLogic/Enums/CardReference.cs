using System.Collections.Generic;
using System;

public static class CardReference
{
    public enum CardName
    {
        TestDoNothing,
    }

    public static Dictionary<CardName, Type> CardLookup = new Dictionary<CardName, Type>()
    {
        {CardName.TestDoNothing, typeof(TestDoNothing)}
    };
}
