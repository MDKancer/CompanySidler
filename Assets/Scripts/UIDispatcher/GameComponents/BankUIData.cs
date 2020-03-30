using Building.Bank;
using TMPro;
using UIDispatcher.GameComponents.UIBuildingContent;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject.SceneContext.Signals;

namespace UIDispatcher.GameComponents
{
    public class BankUIData<T> where T : Bank
    {
        private T bank;
        private SignalBus signalBus;
        private UIData uiData;
        private ProceduralUiElements proceduralUiElements;
        private MonoBehaviour monoBehaviour;
        public BankUIData(SignalBus signalBus,MonoBehaviour monoBehaviour,ref UIData uiData, Building.Building bank)
        {
            this.uiData = uiData;
            this.proceduralUiElements = new ProceduralUiElements();
            this.bank = bank as T;
            
            this.signalBus = signalBus;
            this.monoBehaviour = monoBehaviour;
        }
        public void SetBankInteractions()
        {
            uiData.buyBtn.gameObject.SetActive(false);
            uiData.upgradeBtn.gameObject.SetActive(false);
            uiData.stateBtn.gameObject.SetActive(false);
            SetTakeLoanInteractions();
            SetRepayLoanInteractions();
        }

        private void SetTakeLoanInteractions()
        {
            Button takeLoan = proceduralUiElements.CreateButton(
                uiData.employeeLayout.rectTransform,
                "Take Loan"
                );
            
            takeLoan.onClick.AddListener(() =>
            {
                bank.TakeLoan( 1000);
                this.signalBus.Fire(new UpdateUIWindow());
            });   
            //it should be in another List 
            uiData.AddEmployeesApplyButton(takeLoan, takeLoan.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
        }
        private void SetRepayLoanInteractions()
        {
            Button repayLoan = proceduralUiElements.CreateButton(
                uiData.employeeLayout.rectTransform,
                "Repay Loan"
            );
            
            repayLoan.onClick.AddListener(() =>
            {
                bank.RepayLoan( 1000);
                this.signalBus.Fire(new UpdateUIWindow());
            });   
            //it should be in another List 
            uiData.AddEmployeesApplyButton(repayLoan, repayLoan.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
        }
        
        ~BankUIData()
        {
            this.signalBus.Fire(new UpdateUIWindow());
            uiData.buyBtn.gameObject.SetActive(true);
            uiData.upgradeBtn.gameObject.SetActive(true);
            uiData.stateBtn.gameObject.SetActive(true);
        }
        
        
    }
}