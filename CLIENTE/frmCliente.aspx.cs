using System;
using System.Web;
using System.Web.UI;

namespace wssProyecto.CLIENTE
{
    public partial class Formulario_web1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Valor por defecto
                string soloNombre = "Cliente";

                // Usamos la variable de sesión que inicializas en Global.asax
                // Session["nombreUsuario"]
                if (Session["nombreUsuario"] != null)
                {
                    string nombreCompleto = Session["nombreUsuario"].ToString();
                    soloNombre = ObtenerSoloNombre(nombreCompleto);
                }

                lblNombreCliente.Text = soloNombre;
            }
        }

        /// <summary>
        /// Toma un nombre completo y regresa solo la primera palabra (el nombre).
        /// Ejemplo: "Carlos Ramírez López" → "Carlos"
        /// </summary>
        private string ObtenerSoloNombre(string nombreCompleto)
        {
            if (string.IsNullOrWhiteSpace(nombreCompleto))
                return "Cliente";

            string[] partes = nombreCompleto
                                .Trim()
                                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (partes.Length == 0)
                return "Cliente";

            return partes[0];
        }
    }
}
