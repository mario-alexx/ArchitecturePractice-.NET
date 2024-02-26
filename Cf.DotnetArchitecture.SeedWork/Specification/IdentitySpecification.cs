using System.Linq.Expressions;

namespace Cf.Dotnet.Architecture.Domain.SeedWork.Specification;

internal sealed class IdentitySpecification<T> : Specification<T>
{
    public override Expression<Func<T, bool>> ToExpression()
    {
        return x => true;
    }
}