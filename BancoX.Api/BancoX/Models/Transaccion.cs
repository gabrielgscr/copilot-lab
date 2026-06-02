namespace BancoX.Models;

/// <summary>
/// Representa una transaccion bancaria del sistema Banco X.
/// </summary>
public class Transaccion
{
    private decimal monto;

    /// <summary>
    /// Identificador unico de la transaccion.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Identificador de la cuenta origen.
    /// </summary>
    public int CuentaOrigenId { get; set; }

    /// <summary>
    /// Identificador de la cuenta destino cuando aplique.
    /// </summary>
    public int? CuentaDestinoId { get; set; }

    /// <summary>
    /// Tipo de transaccion: deposito, retiro o transferencia.
    /// </summary>
    public string Tipo { get; set; } = string.Empty;

    /// <summary>
    /// Monto de la transaccion. Debe ser positivo.
    /// </summary>
    public decimal Monto
    {
        get => monto;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("El monto debe ser positivo.");
            }

            monto = value;
        }
    }

    /// <summary>
    /// Fecha de registro de la transaccion.
    /// </summary>
    public DateTime Fecha { get; set; }

    /// <summary>
    /// Descripcion o referencia de la transaccion.
    /// </summary>
    public string Descripcion { get; set; } = string.Empty;
}

/// <summary>
/// DTO para crear transacciones.
/// </summary>
public record TransaccionDto(
    int CuentaOrigenId,
    int? CuentaDestinoId,
    string Tipo,
    decimal Monto,
    string Descripcion
);
