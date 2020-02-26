using System.Collections.Generic;
using Enums;
using JetBrains.Annotations;
using UnityEngine;

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
            
            stateController.CurrentState = BuildingState.EMPTY;
        }

        private void Start()
        {
            stateController.CurrentState = BuildingState.WORK;
        }

        public void TakeLoan([NotNull]Company company,int amount)
        {
            if(!CompanyHaveLoan(company))
            {
                LoanData loan = new LoanData
                {
                    loan_Amount = amount,
                    company = company
                };
                loans.Add(loan);
            }
            else
            {
                var loanData = GetLoanData(company);
                var data = loanData.Value;
                data.loan_Amount += amount;
            }
        }

        public void RepayLoan([NotNull]Company company,int amount)
        {
                var loanData = GetLoanData(company);
                if (loanData.HasValue)
                {
                    var data = loanData.Value;
                    data.repay_Amount += amount;
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