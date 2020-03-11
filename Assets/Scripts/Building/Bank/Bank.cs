using System.Collections.Generic;
using Enums;
using JetBrains.Annotations;
using UnityEngine;
using Zenject_Signals;

namespace BuildingPackage
{
    public class Bank : Building, iBank
    {
        private List<LoanData> loans;
        
        private void Awake()
        {
            buildingData = new BuildingData
            {
                buildingType = BuildingType.BANK,
                name = name,
                workers = 0,
                maxHitPoints = 1000000,
                currentHitPoints = 1000000,
                upgradePrice = 0,
                workPlacesLimit = 0,
                moneyPerSec = 0
            };
            loans = new List<LoanData>();
            this.IsBuying = true;
            stateController.CurrentState = BuildingState.EMPTY;
        }

        public void TakeLoan(int amount)
        {
            if(!CompanyHaveLoan(company))
            {
                LoanData loan = new LoanData
                {
                    loan_Amount = amount,
                    company = company
                };
                loans.Add(loan);
                company.CurrentBudget += amount;
            }
            else
            {
                var loanData = GetLoanData(company);
                var data = loanData.Value;
                data.loan_Amount += amount;
                company.CurrentBudget += amount;
            }
        }

        public void RepayLoan(int amount)
        {
                var loanData = GetLoanData(company);
                if (loanData.HasValue)
                {
                    var data = loanData.Value;
                    data.repay_Amount += amount;
                    company.CurrentBudget -= amount;
                }
                else
                {
                    Debug.Log(company.name + " dont have a Loan.");
                }
        }

        [CanBeNull]
        private LoanData? GetLoanData(Company targetCompany)
        {
            foreach (var loan in loans)
            {
                if (loan.company == company) return loan;
            }
            return null;
        }

        private bool CompanyHaveLoan(Company company)
        {
            foreach (var loan in loans)
            {
                if (loan.company == company) return true;
            }
            return false;
        }
    }
}