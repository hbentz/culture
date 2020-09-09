using System.Collections.Generic;

public class ResourceTray : GameBoard
{
	public int MaximumTotalCurrency;
	public List<Currency> InvestorCredits = new List<Currency>();
	public List<Currency> IntellectualPropertys = new List<Currency>();
	public List<Currency> PublicRelations = new List<Currency>();
	public List<Currency> EmployeeMotivations = new List<Currency>();
	public List<Currency> WorkerManagers = new List<Currency>();
	public List<Currency> WorkerTeams = new List<Currency>();

	public Dictionary<CurrencyType, List<Currency>> HostedCurrency;

	public int CurrentTotalCurrency {
		get
		{
			// Add up all the items in all the lengths of the monetary currency lists to get this number
			return InvestorCredits.Count + IntellectualPropertys.Count + PublicRelations.Count + EmployeeMotivations.Count;
		}
	}

    private void Awake()
    {
		HostedCurrency = new Dictionary<CurrencyType, List<Currency>>()
		{
			{ CurrencyType.InvestorCredit, InvestorCredits },
			{ CurrencyType.IntellectualProperty, IntellectualPropertys },
			{ CurrencyType.PublicRelation, PublicRelations },
			{ CurrencyType.EmployeeMotivation, EmployeeMotivations },
			{ CurrencyType.WorkerManager, WorkerManagers },
			{ CurrencyType.WorkerTeam, WorkerTeams },
		};
	}
}