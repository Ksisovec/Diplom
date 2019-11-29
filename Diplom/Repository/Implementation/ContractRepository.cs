
namespace Diplom.Repository.Implementation
{
    using Models;
    using Interfaces;
    public class ContractRepository : CacheBase<Contract>, IContractRepository
    {
        public ContractRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
        }
    }
}
