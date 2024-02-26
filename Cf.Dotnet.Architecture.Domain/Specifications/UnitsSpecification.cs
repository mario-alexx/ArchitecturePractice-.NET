using System.Linq.Expressions;
using Cf.Dotnet.Architecture.Domain.SeedWork.Specification;

namespace Cf.Dotnet.Architecture.Domain.Specifications;

/// <summary>
///     Especificación para validar la cantidad de unidades de un ítem.
/// </summary>
public class UnitsSpecification : Specification<int>
{
    // Definición de constantes para los límites de las unidades.
    private const int MinUnits = 0; // Mínimo número de unidades permitidas.
    private const int MaxUnits = 15; // Máximo número de unidades permitidas.

    /// <summary>
    ///     Construye la expresión de especificación para validar las unidades.
    /// </summary>
    /// <returns>Una expresión que representa los criterios de la especificación.</returns>
    /// <remarks>
    ///     Esta expresión se utilizará para determinar si un número de unidades dado cumple con los criterios de la
    ///     especificación.
    /// </remarks>
    public override Expression<Func<int, bool>> ToExpression()
    {
        // Expresión que valida que las unidades sean mayores que el mínimo y menores o iguales que el máximo.
        return units => units > MinUnits && units <= MaxUnits;
    }
}