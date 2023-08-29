using System.Data.SqlClient;

namespace ReservasCampoDeportivo.Datos
{
    public class Conexion
    {
        private string cadenaSQL=string.Empty;

        public Conexion()
        {
            var builder=new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            cadenaSQL = builder.GetSection("ConnectionStrings:CadenaSQL").Value;//leemos la cadena de conexion
        }
        public string getCadenaSQL()
        {
            return cadenaSQL;
        }
    }
}
