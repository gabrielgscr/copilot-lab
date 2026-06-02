using BancoX.Models;

namespace BancoX.Services;

/// <summary>
/// Servicio en memoria para la gestion de cuentas bancarias.
/// </summary>
public class CuentaServicio
{
    private readonly List<Cuenta> cuentas;
    private int siguienteId;

    /// <summary>
    /// Inicializa el servicio con datos de ejemplo de cuentas.
    /// </summary>
    public CuentaServicio()
    {
        cuentas =
        [
            new Cuenta
            {
                Id = 1,
                ClienteId = 1,
                NumeroCuenta = "1002003001",
                TipoCuenta = "ahorro",
                Saldo = 12500.75m,
                Estado = "activa",
                FechaApertura = new DateTime(2026, 1, 15, 9, 0, 0, DateTimeKind.Utc)
            },
            new Cuenta
            {
                Id = 2,
                ClienteId = 2,
                NumeroCuenta = "1002003002",
                TipoCuenta = "corriente",
                Saldo = 5400.00m,
                Estado = "activa",
                FechaApertura = new DateTime(2026, 1, 16, 9, 0, 0, DateTimeKind.Utc)
            },
            new Cuenta
            {
                Id = 3,
                ClienteId = 3,
                NumeroCuenta = "1002003003",
                TipoCuenta = "ahorro",
                Saldo = 0.00m,
                Estado = "bloqueada",
                FechaApertura = new DateTime(2026, 1, 17, 9, 0, 0, DateTimeKind.Utc)
            }
        ];

        siguienteId = cuentas.Max(c => c.Id) + 1;
    }

    /// <summary>
    /// Obtiene la lista completa de cuentas en memoria.
    /// </summary>
    public IReadOnlyList<Cuenta> ObtenerTodos()
    {
        return cuentas.AsReadOnly();
    }

    /// <summary>
    /// Obtiene una cuenta por su identificador.
    /// </summary>
    public Cuenta? ObtenerPorId(int id)
    {
        return cuentas.FirstOrDefault(c => c.Id == id);
    }

    /// <summary>
    /// Crea una nueva cuenta y asigna un identificador y fecha de apertura automatica.
    /// </summary>
    public Cuenta Crear(CuentaDto cuentaDto)
    {
        ValidarSaldoNoNegativo(cuentaDto.Saldo);

        var cuenta = new Cuenta
        {
            Id = siguienteId++,
            ClienteId = cuentaDto.ClienteId,
            NumeroCuenta = cuentaDto.NumeroCuenta,
            TipoCuenta = cuentaDto.TipoCuenta,
            Saldo = cuentaDto.Saldo,
            Estado = cuentaDto.Estado,
            FechaApertura = DateTime.UtcNow
        };

        cuentas.Add(cuenta);
        return cuenta;
    }

    /// <summary>
    /// Actualiza los datos de una cuenta existente por identificador.
    /// </summary>
    public bool Actualizar(int id, CuentaDto cuentaDto)
    {
        ValidarSaldoNoNegativo(cuentaDto.Saldo);

        var cuenta = ObtenerPorId(id);
        if (cuenta is null)
        {
            return false;
        }

        cuenta.ClienteId = cuentaDto.ClienteId;
        cuenta.NumeroCuenta = cuentaDto.NumeroCuenta;
        cuenta.TipoCuenta = cuentaDto.TipoCuenta;
        cuenta.Saldo = cuentaDto.Saldo;
        cuenta.Estado = cuentaDto.Estado;

        return true;
    }

    /// <summary>
    /// Elimina una cuenta existente por identificador.
    /// </summary>
    public bool Eliminar(int id)
    {
        var cuenta = ObtenerPorId(id);
        if (cuenta is null)
        {
            return false;
        }

        cuentas.Remove(cuenta);
        return true;
    }

    /// <summary>
    /// Valida que el saldo sea mayor o igual a cero.
    /// </summary>
    private static void ValidarSaldoNoNegativo(decimal saldo)
    {
        if (saldo < 0)
        {
            throw new ArgumentException("El saldo no puede ser negativo.");
        }
    }
}
