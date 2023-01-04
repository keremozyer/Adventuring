using Adventuring.Architecture.Model.Entity.Interface;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System.Reflection;

namespace Adventuring.Architecture.Data.Context.Implementation.EntityFramework.Extensions;

internal static class MutableEntityTypeExtensions
{
    internal static void AddSoftDeleteQueryFilter(this IMutableEntityType entityType)
    {
        MethodInfo methodToCall = typeof(MutableEntityTypeExtensions).GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(entityType.ClrType);
        object filter = methodToCall.Invoke(null, Array.Empty<object>())!;
        entityType.SetQueryFilter((LambdaExpression)filter);
    }

    private static LambdaExpression GetSoftDeleteFilter<EntityType>() where EntityType : ISoftDeletedEntity
    {
        Expression<Func<EntityType, bool>> filter = x => x.DeletedAt == null;
        return filter;
    }
}