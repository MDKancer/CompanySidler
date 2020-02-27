using JetBrains.Annotations;

namespace BuildingPackage
{
    public interface iBank
    {
        void TakeLoan([NotNull]Company company,int amount);

        void RepayLoan([NotNull]Company company,int amount);
    }
}