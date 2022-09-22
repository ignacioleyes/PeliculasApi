namespace PeliculasApi.Servicios
{
    public interface IAlmacenadorArchivos
    {
        Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor,
            string contentType);
        Task<string> EditarArchivo(byte[] contenido, string extension, string ruta, string contenedor,
            string contentType);
        Task BorrarArchivo(string ruta, string contenedor);
    }
}
