namespace BancoX.Models;

/// <summary>
/// Representa una cuenta bancaria del sistema Banco X.
/// </summary>
public class Cuenta
{
    /// <summary>
    /// Identificador unico de la cuenta.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identificador del cliente titular de la cuenta.
    /// </summary>
    public int ClienteId { get; set; }

    /// <summary>
    /// Numero de cuenta.
    /// </summary>
    public string NumeroCuenta { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de cuenta: ahorro o corriente.
    /// </summary>
    public string TipoCuenta { get; set; } = string.Empty;

    /// <summary>
    /// Saldo actual de la cuenta.
    /// </summary>
    public decimal Saldo { get; set; }

    /// <summary>
    /// Estado de la cuenta: activa, inactiva o bloqueada.
    /// </summary>
    public string Estado { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de apertura de la cuenta.
    /// </summary>
    public DateTime FechaApertura { get; set; }
}

/// <summary>
/// DTO para crear o actualizar cuentas.
/// </summary>
public record CuentaDto(
    int ClienteId,
    string NumeroCuenta,
    string TipoCuenta,
    decimal Saldo,
    string Estado
);
