using BootManager;
using Enums;
using UnityEngine;

 [CreateAssetMenu (fileName = "CompanyData", menuName = "ScriptableObjects/CompanyData", order = 1)]
public class CompanyData : ScriptableObject
{
    public string nameCompany;
    public bool haveInitialBudget;
    public bool hasLoan; // Credit from the Bank
    public int loan_Amount;
    public OfficesForBegin officesForBegin;
    public void ParametersByDefault()
    {
            nameCompany = null;
            haveInitialBudget = false;
            hasLoan = false;
            loan_Amount = 0;
    }

    public void SetHaveInitialBudget(bool value)
    {
        haveInitialBudget = value;
    }
    public void SetHasLoan(bool value)
    {
        hasLoan = value;
    }

    public void SetCompanyName(string name)
    {
        nameCompany = name;
    }
    
}
