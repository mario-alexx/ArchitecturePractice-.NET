using System.Linq.Expressions;

namespace Cf.Dotnet.Architecture.Domain.SeedWork.Specification;

public abstract class Specification<T>
{
    public static readonly Specification<T> All = new IdentitySpecification<T>();

    public bool IsSatisfiedBy(T entity)
    {
        var predicate = ToExpression().Compile();
        return predicate(entity);
    }

    public bool IsNotSatisfiedBy(T entity)
    {
        return !IsSatisfiedBy(entity);
    }

    public abstract Expression<Func<T, bool>> ToExpression();

    public Specification<T> And(Specification<T> specification)
    {
        if (this == All) return specification;

        return specification == All ? this : new AndSpecification<T>(this, specification);
    }

    public Specification<T> Or(Specification<T> specification)
    {
        if (this == All || specification == All) return All;

        return new OrSpecification<T>(this, specification);
    }

    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }

    public static Specification<T> operator &(Specification<T> lhs, Specification<T> rhs)
    {
        return lhs.And(rhs);
    }

    public static Specification<T> operator |(Specification<T> lhs, Specification<T> rhs)
    {
        return lhs.Or(rhs);
    }

    public static Specification<T> operator !(Specification<T> spec)
    {
        return spec.Not();
    }
}