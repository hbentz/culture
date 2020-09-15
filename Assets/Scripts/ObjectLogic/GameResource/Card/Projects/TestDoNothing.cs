using System;

public class TestDoNothing : Project
{
    public void DoWhenPlayed(Card card, Player player)
    {
        card.DoWhenPlayed();
    }
}
