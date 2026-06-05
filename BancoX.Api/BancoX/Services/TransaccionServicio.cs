using BancoX.Models;

namespace BancoX.Services;

/// <summary>
/// Servicio en memoria para la gestion de transacciones bancarias.
/// </summary>
public class TransaccionServicio
{
    private readonly List<Transaccion> transacciones;
    private readonly CuentaServicio cuentaServicio;
    private int siguienteId;

    /// <summary>
    /// Inicializa el servicio con datos de ejemplo de transacciones.
    /// </summary>
    public TransaccionServicio(CuentaServicio cuentaServicio)
    {
        this.cuentaServicio = cuentaServicio;

        transacciones =
        [
            new Transaccion
            {
                Id = 1,
                CuentaOrigenId = 1,
                CuentaDestinoId = null,
                Tipo = "deposito",
                Monto = 500.00m,
                Fecha = new DateTime(2026, 2, 1, 12, 0, 0, DateTimeKind.Utc),
                Descripcion = "Deposito inicial"
            },
            new Transaccion
            {
                Id = 2,
                CuentaOrigenId = 2,
                CuentaDestinoId = null,
                Tipo = "retiro",
                Monto = 250.00m,
                Fecha = new DateTime(2026, 2, 2, 9, 15, 0, DateTimeKind.Utc),
                Descripcion = "Retiro en cajero"
            },
            new Transaccion
            {
                Id = 3,
                CuentaOrigenId = 1,
                CuentaDestinoId = 2,
                Tipo = "transferencia",
                Monto = 120.50m,
                Fecha = new DateTime(2026, 2, 3, 16, 45, 0, DateTimeKind.Utc),
                Descripcion = "Transferencia entre cuentas"
            }
        ];

        siguienteId = transacciones.Max(t => t.Id) + 1;
    }

    /// <summary>
    /// Obtiene la lista completa de transacciones en memoria.
    /// </summary>
    public IReadOnlyList<Transaccion> ObtenerTodos()
    {
        return transacciones.AsReadOnly();
    }

    /// <summary>
    /// Obtiene una transaccion por su identificador.
    /// </summary>
    public Transaccion? ObtenerPorId(int id)
    {
        return transacciones.FirstOrDefault(t => t.Id == id);
    }

    /// <summary>
    /// Obtiene las transacciones relacionadas con una cuenta.
    /// </summary>
    public IReadOnlyList<Transaccion> ObtenerPorCuenta(int cuentaId)
    {
        return transacciones
            .Where(t => t.CuentaOrigenId == cuentaId || t.CuentaDestinoId == cuentaId)
            .ToList()
            .AsReadOnly();
    }

    /// <summary>
    /// Crea una transaccion aplicando reglas de negocio sobre las cuentas.
    /// </summary>
    public Transaccion Crear(TransaccionDto transaccionDto)
    {
        var tipo = transaccionDto.Tipo.Trim().ToLowerInvariant();

        var cuentaOrigen = cuentaServicio.ObtenerPorId(transaccionDto.CuentaOrigenId)
            ?? throw new ArgumentException("La cuenta origen no existe.");

        if (cuentaOrigen.Estado != "activa" && tipo != "deposito")
        {
            throw new ArgumentException("La cuenta origen no esta activa para esta operacion.");
        }

        switch (tipo)
        {
            case "deposito":
            {
                var cuentaDestino = ObtenerCuentaDestinoParaDeposito(transaccionDto, cuentaOrigen);
                if (cuentaDestino.Estado != "activa")
                {
                    throw new ArgumentException("La cuenta destino no esta activa para depositos.");
                }

                cuentaDestino.Saldo += transaccionDto.Monto;
                break;
            }
            case "retiro":
            {
                if (cuentaOrigen.Saldo < transaccionDto.Monto)
                {
                    throw new ArgumentException("Saldo insuficiente para realizar el retiro.");
                }

                cuentaOrigen.Saldo -= transaccionDto.Monto;
                break;
            }
            case "transferencia":
            {
                if (transaccionDto.CuentaDestinoId is null)
                {
                    throw new ArgumentException("La cuenta destino es obligatoria en una transferencia.");
                }

                if (transaccionDto.CuentaOrigenId == transaccionDto.CuentaDestinoId)
                {
                    throw new ArgumentException("La cuenta origen y destino no pueden ser la misma.");
                }

                var cuentaDestino = cuentaServicio.ObtenerPorId(transaccionDto.CuentaDestinoId.Value)
                    ?? throw new ArgumentException("La cuenta destino no existe.");

                if (cuentaDestino.Estado != "activa")
                {
                    throw new ArgumentException("La cuenta destino no esta activa para transferencias.");
                }

                if (cuentaOrigen.Saldo < transaccionDto.Monto)
                {
                    throw new ArgumentException("Saldo insuficiente para realizar la transferencia.");
                }

                cuentaOrigen.Saldo -= transaccionDto.Monto;
                cuentaDestino.Saldo += transaccionDto.Monto;
                break;
            }
            default:
                throw new ArgumentException("El tipo de transaccion no es valido.");
        }

        var transaccion = new Transaccion
        {
            Id = siguienteId++,
            CuentaOrigenId = transaccionDto.CuentaOrigenId,
            CuentaDestinoId = transaccionDto.CuentaDestinoId,
            Tipo = tipo,
            Monto = transaccionDto.Monto,
            Fecha = DateTime.UtcNow,
            Descripcion = transaccionDto.Descripcion
        };

        transacciones.Add(transaccion);
        return transaccion;
    }

    /// <summary>
    /// Obtiene la cuenta destino para una operacion de deposito.
    /// Si la transaccion no especifica <c>CuentaDestinoId</c>, se utiliza la cuenta origen.
    /// </summary>
    /// <param name="transaccionDto">Datos de la transaccion que contienen el identificador opcional de cuenta destino.</param>
    /// <param name="cuentaOrigen">Cuenta origen de la transaccion, utilizada como destino por defecto en depositos sin cuenta destino explicita.</param>
    /// <returns>
    /// La cuenta destino valida para aplicar el deposito.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Se produce cuando la transaccion especifica una cuenta destino y esta no existe en el sistema.
    /// </exception>
    /// <example>
    /// <code>
    /// var dto = new TransaccionDto(1, null, "deposito", 250m, "Deposito en ventanilla");
    /// var destino = ObtenerCuentaDestinoParaDeposito(dto, cuentaOrigen);
    /// // destino sera cuentaOrigen al no indicar CuentaDestinoId.
    /// </code>
    /// </example>
    private Cuenta ObtenerCuentaDestinoParaDeposito(TransaccionDto transaccionDto, Cuenta cuentaOrigen)
    {
        if (transaccionDto.CuentaDestinoId is null)
        {
            return cuentaOrigen;
        }

        return cuentaServicio.ObtenerPorId(transaccionDto.CuentaDestinoId.Value)
            ?? throw new ArgumentException("La cuenta destino no existe.");
    }
}
