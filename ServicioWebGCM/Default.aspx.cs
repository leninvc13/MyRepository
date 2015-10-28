using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

namespace ServicioWebGCM
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGCM_Click(object sender, EventArgs e)
        {



            ServicioClientes svc = new ServicioClientes();
            User codUsuario = svc.CodigoUsuario((txtUsuario.Text));
            bool res = enviarMensajePrueba(codUsuario.CodigoC2DM);

            if (res == true)
                lblEnvio.Text = "Envio OK";
            else
                lblEnvio.Text = "Envio NO oK";
        }


        private static bool enviarMensajePrueba(String registration_id)
        {
            bool flag = false;
            StringBuilder sb = new StringBuilder();

            String GCM_URL = @"https://android.googleapis.com/gcm/send";

            string collapseKey = DateTime.Now.ToString();

            sb.AppendFormat("registration_id={0}&collapse_key={1}",
                            registration_id, collapseKey);

            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("data.msg",
                HttpUtility.UrlEncode("Prueba. Timestamp: " + DateTime.Now.ToString()));

            foreach (string item in data.Keys)
            {
                if (item.Contains("data."))
                    sb.AppendFormat("&{0}={1}", item, data[item]);
            }

            string msg = sb.ToString();

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(GCM_URL);
            req.Method = "POST";
            req.ContentLength = msg.Length;
            req.ContentType = "application/x-www-form-urlencoded";

            string apiKey = "AIzaSyCRLEpdWf9PNAdiNRv4pRrh_Qp_WSe5vSI";
            req.Headers.Add("Authorization:key=" + apiKey);

            using (StreamWriter oWriter = new StreamWriter(req.GetRequestStream()))
            {
                oWriter.Write(msg);
            }

            using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
            {
                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    string respData = sr.ReadToEnd();

                    if (resp.StatusCode == HttpStatusCode.OK)   // OK = 200
                    {
                        //if (respData.StartsWith("id="))
                            flag = true;
                    }
                    else if (resp.StatusCode == HttpStatusCode.InternalServerError)    // 500
                        Console.WriteLine("Error interno del servidor, prueba más tarde.");
                    else if (resp.StatusCode == HttpStatusCode.ServiceUnavailable)    // 503
                        Console.WriteLine("Servidor no disponible temporalmente, prueba más tarde.");
                    else if (resp.StatusCode == HttpStatusCode.Unauthorized)          // 401
                        Console.WriteLine("La API Key utilizada no es válida.");
                    else
                        Console.WriteLine("Error: " + resp.StatusCode);
                }
            }

            return flag;
        }

    }
}