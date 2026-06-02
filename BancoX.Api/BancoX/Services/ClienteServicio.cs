using BancoX.Models;

namespace BancoX.Services;

/// <summary>
/// Servicio en memoria para la gestion de clientes.
/// </summary>
public class ClienteServicio
{
    private readonly List<Cliente> clientes;
    private int siguienteId;

    /// <summary>
    /// Inicializa el servicio con datos de ejemplo de clientes.
    /// </summary>
    public ClienteServicio()
    {
        clientes =
        [
            new Cliente
            {
                Id = 1,
                Nombre = "Ana Torres",
                Email = "ana.torres@bancox.com",
                Telefono = "555-1001",
                Direccion = "Av. Reforma 100, Ciudad de Mexico"
            },
            new Cliente
            {
                Id = 2,
                Nombre = "Luis Mendoza",
                Email = "luis.mendoza@bancox.com",
                Telefono = "555-1002",
                Direccion = "Calle Norte 45, Ciudad de Mexico"
            },
            new Cliente
            {
                Id = 3,
                Nombre = "Carla Ruiz",
                Email = "carla.ruiz@bancox.com",
                Telefono = "555-1003",
                Direccion = "Blvd. Sur 220, Guadalajara"
            }
        ];

        siguienteId = clientes.Max(c => c.Id) + 1;
    }

    /// <summary>
    /// Obtiene la lista completa de clientes en memoria.
    /// </summary>
    public IReadOnlyList<Cliente> ObtenerTodos()
    {
        return clientes.AsReadOnly();
    }

    /// <summary>
    /// Obtiene un cliente por su identificador.
    /// </summary>
    public Cliente? ObtenerPorId(int id)
    {
        return clientes.FirstOrDefault(c => c.Id == id);
    }

    /// <summary>
    /// Crea un nuevo cliente y asigna un identificador automatico.
    /// </summary>
    public Cliente Crear(ClienteDto clienteDto)
    {
        var cliente = new Cliente
        {
            Id = siguienteId++,
            Nombre = clienteDto.Nombre,
            Email = clienteDto.Email,
            Telefono = clienteDto.Telefono,
            Direccion = clienteDto.Direccion
        };

        clientes.Add(cliente);
        return cliente;
    }

    /// <summary>
    /// Actualiza los datos de un cliente existente por identificador.
    /// </summary>
    public bool Actualizar(int id, ClienteDto clienteDto)
    {
        var cliente = ObtenerPorId(id);
        if (cliente is null)
        {
            return false;
        }

        cliente.Nombre = clienteDto.Nombre;
        cliente.Email = clienteDto.Email;
        cliente.Telefono = clienteDto.Telefono;
        cliente.Direccion = clienteDto.Direccion;

        return true;
    }

    /// <summary>
    /// Elimina un cliente existente por identificador.
    /// </summary>
    public bool Eliminar(int id)
    {
        var cliente = ObtenerPorId(id);
        if (cliente is null)
        {
            return false;
        }

        clientes.Remove(cliente);
        return true;
    }
}
