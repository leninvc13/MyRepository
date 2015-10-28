using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;

namespace ServicioWebGCM
{
    /// <summary>
    /// Summary description for ServicioClientes
    /// </summary>
    [WebService(Namespace = "http://mytest.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioClientes : System.Web.Services.WebService
    {
        DBCLIENTESEntities dataBase = new DBCLIENTESEntities();

        [WebMethod]
        public int Add(int a, int b)
        {
            return a + b;
        } 


        [WebMethod]
        public int RegistroUsuario(string NombreUsuario, string CodigoC2DM)
        {
            User user = new User();

            User usuar = CodigoUsuario(NombreUsuario);
            int id = 0;
            if (usuar == null)
            {
                user.NombreUsuario = NombreUsuario;
                user.CodigoC2DM = CodigoC2DM;
                dataBase.Users.Add(user);
                dataBase.SaveChanges();
                id = user.IdUsuario;
            }
            else
            {
                User u = dataBase.Users.First(i => i.IdUsuario == usuar.IdUsuario);
                u.CodigoC2DM = CodigoC2DM;
                dataBase.SaveChanges();
                id = u.IdUsuario;
            }



            return id;
            
        }

        [WebMethod]
        public User CodigoUsuario(string NombreUsuario)
        {

            var us1 = (from u in dataBase.Users
                       where u.NombreUsuario.Equals(NombreUsuario)
                       select u).SingleOrDefault();

            return us1;
        }

        //[WebMethod]
        //public Cliente[] ListadoClientes()
        //{
        //    List<Cliente> lista = new List<Cliente>();
        //    var clientList = dataBase.Clientes.ToList();
        //    lista = clientList;

        //    return lista.ToArray();

        //}

        //[WebMethod]
        //public int NuevoClienteObjeto(Cliente cliente)
        //{
        //    Cliente mycliente = new Cliente();
        //    mycliente.Nombre = cliente.Nombre;
        //    mycliente.Telefono = cliente.Telefono;
        //    dataBase.Clientes.Add(mycliente);
        //    dataBase.SaveChanges();

        //    int id = mycliente.IdCliente;

        //    return id;
        //}

        //[WebMethod]
        //public int NuevoClientesSimple(string nombre, int telefono)
        //{
        //    Cliente cliente = new Cliente();
        //    cliente.Nombre = nombre;
        //    cliente.Telefono = telefono;
        //    dataBase.Clientes.Add(cliente);
        //    dataBase.SaveChanges();

        //    int id = cliente.IdCliente;

        //    return id;
        //}

        //[WebMethod]
        //public int RegistroUsuario(string usuario, string regGCM)
        //{
        //    User user = new User();

        //    User usuar = CodigoUsuario(usuario);
        //    int id = 0;
        //    if (usuar == null)
        //    {
        //        user.NombreUsuario = usuario;
        //        user.CodigoC2DM = regGCM;
        //        dataBase.Users.Add(user);
        //        dataBase.SaveChanges();
        //        id = user.IdUsuario;
        //    }
        //    else
        //    {
        //        User u = dataBase.Users.First(i => i.IdUsuario == usuar.IdUsuario);
        //        u.CodigoC2DM = regGCM;
        //        dataBase.SaveChanges();
        //        id = u.IdUsuario;
        //    }



        //    return id;

        //}


        //[WebMethod]
        //public User CodigoUsuario(string usuario)
        //{
        //    var us1 = (from u in dataBase.Users
        //               where u.NombreUsuario.Equals(usuario)
        //               select u).SingleOrDefault();

        //    return us1;
        //}


    }
}
