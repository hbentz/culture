using System;
using System.Collections.Generic;
using System.Reflection;

public class BasicAction
{
    // Make this a single instance
    private BasicAction()
    {
        GivePlayerCurrency = givePlayerCurrency;
    }

    private static BasicAction instance = null;
    public static BasicAction Instance
    {
        // Get the current instance of this class if there is one, otherwise create it
        get
        {
            if (instance == null) instance = new BasicAction();
            return instance;
        }
    }

    // Actions are public so they can be altered by cards
    public Action<Player, CurrencyType, int> GivePlayerCurrency;

    private void givePlayerCurrency(Player player, CurrencyType currency, int count)
    {
        // Get the player's currency board
        Dictionary<CurrencyType, List<Currency>> _playerCurrency = ((ResourceTray)player.PrivateDomain.NestedBoards[BoardType.ResourceTray][0]).HostedCurrency;
        
        // And add count new currecny objects to it
        for (int i = 0; i < count; i++)
        {
            Currency _newCurrency = CurrencyFactory.Instance.MakeCurrency(currency);
            _playerCurrency[currency].Add(_newCurrency);
        }
    }
}