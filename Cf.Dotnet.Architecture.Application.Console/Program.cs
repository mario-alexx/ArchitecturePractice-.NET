using Cf.Dotnet.Architecture.Domain.Entities;
using Cf.Dotnet.Database;
using Cf.DotnetArchitecture.SeedWork;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// Creación del constructor de la aplicación (builder) usando la clase Host.
var builder = Host.CreateApplicationBuilder(args);

// Configuración del sistema de logging para que utilice la salida de la consola.
// Esto permite registrar información útil durante la ejecución de la aplicación.
builder.Logging.AddConsole();

// Configuración de los servicios de la aplicación.
// Esto incluye la base de datos, los repositorios y la unidad de trabajo (UnitOfWork).
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddUnitOfWork();

// Construcción del host de la aplicación.
// El host es responsable de la gestión y la configuración de los servicios.
using var host = builder.Build();

// Obtención del repositorio de órdenes y del servicio de logging desde el contenedor de servicios.
var orderRepo = host.Services.GetRequiredService<IRepository<Order>>();
var logger = host.Services.GetRequiredService<ILogger<Program>>();

// Conversión del primer argumento de la línea de comandos a un entero para obtener el ID de la orden.
var id = Convert.ToInt32(Environment.GetCommandLineArgs()[1]);

// Búsqueda asíncrona de la orden por su ID y posterior cancelación de la misma.
var order = await orderRepo.FindAsync(id);
order.Cancel();

// Guardado de los cambios en la base de datos mediante la unidad de trabajo.
orderRepo.Update(order);
await orderRepo.UnitOfWork.SaveChangesAsync();

// Registro en el log de la cancelación de la orden.
logger.LogInformation("Order {OrderId} cancelled", order.Id);