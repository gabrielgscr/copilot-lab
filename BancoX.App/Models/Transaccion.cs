namespace BancoX.App.Models;

/// <summary>
/// Modelo de transaccion financiera para consumo de la API.
/// </summary>
public class Transaccion
{
    public int Id { get; set; }
    public string TipoTransaccion { get; set; } = string.Empty;
    public int? CuentaOrigenId { get; set; }
    public int? CuentaDestinoId { get; set; }
    public decimal Monto { get; set; }
    public string Estado { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public string? Descripcion { get; set; }
}
