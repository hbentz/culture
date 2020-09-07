using System.Collections.Generic;

public class ResourceTray : GameBoard
{
	public int MaximumTotalCurrency;
	public List<Currency> InvestorCredits;
	public List<Currency> IntellectualPropertys;
	public List<Currency> PublicRelations;
	public List<Currency> EmployeeMotivations;
	public List<Currency> Managers;
	public List<Currency> Teams;

	public int CurrentTotalCurrency {
		get
		{
			// Add up all the items in all the lengths of the monetary currency lists to get this number
			return InvestorCredits.Count + IntellectualPropertys.Count + PublicRelations.Count + EmployeeMotivations.Count;
		}
	}
}