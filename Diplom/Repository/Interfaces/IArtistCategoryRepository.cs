
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Repository.Interfaces
{
    using Diplom.Repository;
    using Models;

    public interface IArtistCategoryRepository : IRepository<ArtistCategory>, ICache<ArtistCategory>
    {
    }
}
