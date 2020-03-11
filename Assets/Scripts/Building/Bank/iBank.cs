using JetBrains.Annotations;

namespace BuildingPackage
{
    public interface iBank
    {
        void TakeLoan(int amount);

        void RepayLoan(int amount);
    }
}