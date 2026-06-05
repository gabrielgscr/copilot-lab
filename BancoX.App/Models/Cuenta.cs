namespace BancoX.App.Models;

/// <summary>
/// Modelo de cuenta bancaria para consumo de la API.
/// </summary>
public class Cuenta
{
    public int Id { get; set; }
    public int ClienteId { get; set; }
    public string NumeroCuenta { get; set; } = string.Empty;
    public string TipoCuenta { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaApertura { get; set; }
}

/// <summary>
/// DTO para crear o actualizar cuentas.
/// </summary>
public class CuentaDto
{
    public int ClienteId { get; set; }
    public string NumeroCuenta { get; set; } = string.Empty;
    public string TipoCuenta { get; set; } = string.Empty;
    public decimal Saldo { get; set; }
    public string Estado { get; set; } = string.Empty;
}
