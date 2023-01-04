using Adventuring.Architecture.Data.Repository.Implementation.EntityFramework;
using Adventuring.Contexts.AdventureManager.Data.Repository.Database.EntityFramework.Context;
using Adventuring.Contexts.AdventureManager.Model.Entity.Adventure;

namespace Adventuring.Contexts.AdventureManager.Data.Repository.Database.EntityFramework.Repositories;

public class AdventureTreeRepository : BaseSoftDeleteRepository<AdventureManagerDataContext, AdventureTree>
{
    public AdventureTreeRepository(AdventureManagerDataContext dataContext, Architecture.Container.ActiveUser.Interface.IActiveUser activeUser) : base(dataContext, activeUser) { }
}