using System.Collections.Generic;
using UnityEngine;

public abstract class Card : GameResource
{
    public CardType EnumCardType;
	public List<CurrencyType> HostableResources;

	public List<Currency> InvestorCredits;
	public List<Currency> IntellectualPropertys;
	public List<Currency> PublicRelations;
	public List<Currency> EmployeeMotivations;
	public List<Currency> Managers;
	public List<Currency> Teams;
}
