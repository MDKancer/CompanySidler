namespace Buildings.Bank
{
    public interface iBank
    {
        void TakeLoan(int amount);

        void RepayLoan(int amount);
    }
}