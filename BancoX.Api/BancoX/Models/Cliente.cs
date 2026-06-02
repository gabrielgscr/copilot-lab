namespace BancoX.Models;

/// <summary>
/// Representa a un cliente del sistema Banco X.
/// </summary>
public class Cliente
{
    /// <summary>
    /// Identificador unico del cliente.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nombre completo del cliente.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Correo electronico del cliente.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Telefono de contacto del cliente.
    /// </summary>
    public string Telefono { get; set; } = string.Empty;

    /// <summary>
    /// Direccion fisica del cliente.
    /// </summary>
    public string Direccion { get; set; } = string.Empty;
}

/// <summary>
/// DTO para crear o actualizar clientes.
/// </summary>
public record ClienteDto(
    string Nombre,
    string Email,
    string Telefono,
    string Direccion
);
