using System;
using UnityEngine;

public class CurrencyFactory : MonoBehaviour
{
    public static CurrencyFactory Instance;

    public GameObject CurrencyPrefab;
    public GameObject ManagerPrefab;
    public GameObject TeamPrefab;

	public Currency MakeCurrency(CurrencyType _type)
    {
        GameObject _newObj;
        Currency _returnCurrency;

        switch (_type)
        {
            case CurrencyType.InvestorCredit:
                _newObj = Instantiate(CurrencyPrefab);
                _returnCurrency = _newObj.AddComponent<InvestorCredit>();
                break;

            case CurrencyType.IntellectualProperty:
                _newObj = Instantiate(CurrencyPrefab);
                _returnCurrency = _newObj.AddComponent<IntellectualProperty>();
                break;

            case CurrencyType.PublicRelation:
                _newObj = Instantiate(CurrencyPrefab);
                _returnCurrency = _newObj.AddComponent<PublicRelation>();
                break;

            case CurrencyType.EmployeeMotivation:
                _newObj = Instantiate(CurrencyPrefab);
                _returnCurrency = _newObj.AddComponent<EmployeeMotivation>();
                break;

            case CurrencyType.WorkerManager:
                _newObj = Instantiate(ManagerPrefab);
                _returnCurrency = _newObj.AddComponent<WorkerManager>();
                break;

            case CurrencyType.WorkerTeam:
                _newObj = Instantiate(TeamPrefab);
                _returnCurrency = _newObj.AddComponent<WorkerTeam>();
                break;

            default:
                _newObj = Instantiate(CurrencyPrefab);
                _returnCurrency = _newObj.AddComponent<Currency>();
                Debug.LogError("Tried to give player untyped currency");
                break;
        }

        return _returnCurrency;
    }

    private void Awake()
    {
        Instance = this;
    }
}
