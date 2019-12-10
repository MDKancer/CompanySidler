using UnityEngine;

[CreateAssetMenu (fileName = "CompanyData", menuName = "ScriptableObjects/CompanyData", order = 1)]
public class CompanyData : ScriptableObject
{
    public string nameCompany;
    
    /// <summary>
    /// Gibts ein Eigenenkapital oder nicht.
    /// </summary>
    public bool haveInitialBudget;
    
    /// <summary>
    /// Ist ein Kredit vom Bank genommen oder nicht
    /// </summary>
    public bool hasLoan;
    /// <summary>
    /// die Summe des Kredits
    /// </summary>
    public int loan_Amount;
    /// <summary>
    /// Alle Büros was man braucht für Anfang.
    /// </summary>
    public BasicsOffice basicOffices;
    public void ResetData()
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

    public void SetCompanyName(string name = "Company")
    {
        nameCompany = name;
    }
    
}
