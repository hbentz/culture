using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : GameResource
{
    // The card type of this card
    public CardType EnumCardType;
    public CardReference.CardName CardName;
    public bool IsPrivateDomain;

	// The possible currencies this card can host
	public List<CurrencyType> HostableResources;

	// The lists of the currently hosted currencies
	public List<Currency> InvestorCredits;
	public List<Currency> IntellectualPropertys;
	public List<Currency> PublicRelations;
	public List<Currency> EmployeeMotivations;
	public List<Currency> WorkerManagers;
	public List<Currency> WorkerTeams;

    // Actions this card does
    public Action<Card, Player> PlayEffect;
    public Action<Card, Player> DestroyEffect;
    public Action<Card, Player> ActivateEffect;
    public Action<Card, Player> ManageEffect;
    public Action<Card, Player> StopManageEffect;
    public Action<Card, Player> DevelopEffect;
    public Action<Card, Player> DisableEffect;

    public Action<int> RoundStartEffect;
    public Action<Phase> PhaseStartEffect;
    public Action<int> TurnStartEffect;
    public Action<int> TurnEndEffect;
    public Action<Phase> PhaseEndEffect;
    public Action<int> RoundEndEffect;

    public abstract void DoWhenPlayed(Card card, Player player);
}
