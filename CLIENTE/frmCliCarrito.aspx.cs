using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wssProyecto.CLIENTE
{
    public partial class Formulario_web12 : System.Web.UI.Page
    {
        clsCarrito objCarrito = new clsCarrito();
        // Variable que recibe los registros para mostrarlos en el GridView
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cveUsuario"] != null && Session["nombreUsuario"] != null)
            {
                lblSesion.Text = Session["cveUsuario"].ToString() + "-" + Session["nombreUsuario"].ToString();
            }

            if (Session["venActual"] != null && Session["venActual"].ToString() != "0")
            {
                lblVenta.Text = "Compra actual: " + Session["venActual"].ToString();
                //se visualizara los prodcutos que se pueden comprar y el boton de agregar
                lblVenta.Visible = true;
                gvServicios.Visible = true;
                btnAñadir.Visible = true;
            }

            //VALIDACION PARA SABER SI HAY UNA VENTA ACTUAL
            if (Session["venActual"].ToString() == "0")
            {
                lblProductos.Visible = false;
                gvServicios.Visible = false;
                lblCarrito.Visible = false;
                gvServicosCarrito.Visible = false;
                lblTotal.Visible = false;
                lblSubtotal.Visible = false;
                lblIva.Visible = false;
                btnCerrarCarrito.Visible = false;
                btnAñadir.Visible = false;
                btnConfirmarCompra.Visible = false;
                txtTotal.Visible = false;
            }
            else
            {
                lblVenta.Text = "Compra actual: " + Session["venActual"].ToString();
                //SE VISUALIZAN LOS PRODUCTOS QUE SE PUEDEN COMPRAR Y EL BOTON DE AGREGAR
                lblProductos.Visible = true;
                gvServicios.Visible = true;
                btnAñadir.Visible = true;
                btnCerrarCarrito.Visible = true;
                btnConfirmarCompra.Visible = true;
            }




            // Sección de consultas para llenar GridViews
            if (!IsPostBack)
            {
                ventasRealizadas();
                servicios();
            }

        }
        void ventasRealizadas()
        {
            string id = "";
            ds = new DataSet();
            ds = objCarrito.listarVentasCliente(
                Application["cnnVentas"].ToString(),
                int.Parse(Session["cveUsuario"].ToString())
            );

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                id = dr["ClaveReserva"].ToString();
                break;
            }

            if (id != "0")
            {
                // Pasa los datos de la BD al GridView
                gvReservasAnteriores.DataSource = ds;
                gvReservasAnteriores.DataMember = "ReservasRealizadas";
                gvReservasAnteriores.DataBind();
            }
            else
            {
                lblVenta.Text = "Por el momento no ha realizado ninguna compra";
            }
        }

        protected void gvReservasAnteriores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //Establecer el nuevo índice de página
            gvReservasAnteriores.PageIndex = e.NewPageIndex;
            //Volver a cargar y enlazar los datos
            ventasRealizadas();

        }

        protected void btnGenerarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                // Asegurarse de que Session["venActual"] no sea null
                if (Session["venActual"] == null || Session["venActual"].ToString() == "0")
                {
                    // Genera una nueva venta y asigna el número de venta a la variable de sesión
                    Session["venActual"] = objCarrito.insertarVenta(
                        Application["cnnVentas"].ToString(),
                        int.Parse(Session["cveUsuario"].ToString()),
                        0
                    ).ToString();

                    // Mostrar alerta y redireccionar
                    string script = "alert('Venta registrada: " + Session["venActual"].ToString() + "');" +
                                    "window.location='frmCliCarrito.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "VentaRegistrada", script, true);
                }
                else
                {
                    string script = "alert('Ya hay una venta registrada, seleccionar productos...');" +
                                    "window.location='frmCliCarrito.aspx';";
                    ClientScript.RegisterStartupScript(this.GetType(), "VentaExistente", script, true);
                }

                lblVenta.Text = "Venta actual: " + Session["venActual"].ToString();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "ErrorVenta",
                    "alert('Ha sucedido un error en el registro de venta.');", true);
                // Opcional: log del error
                // System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        protected void gvServicios_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Session["selProducto"] = gvServicios.Rows[e.NewSelectedIndex].Cells[3].Text;

            Response.Write("<script> window.open('frmCliDetProducto.aspx', 'Detalle " +
                "producto', 'height=600,width=350,resizable=no,left=-1000,top=-1000','_blank');" + "</script>");
        }
        void servicios()
        {
            ds = new DataSet();
            ds = objCarrito.listarServiciosCliente(Application["cnnVentas"].ToString());
            gvServicios.DataSource = ds;
            gvServicios.DataMember = "Servicios";
            gvServicios.DataBind();
        }
        void detalleReservas()
        {
            ds = new DataSet();
            ds = objCarrito.listarDetalleReserva(Application["cnnVentas"].ToString(),
                int.Parse(Session["venActual"].ToString()));
            gvServicosCarrito.DataSource = ds;
            gvServicosCarrito.DataMember = "ReservaActual";
            gvServicosCarrito.DataBind();
        }
        void calcularTotalReserva()
        {
            double stotalVtaNva = 0, totalVta = 0, iva = 0;
            for (int i = 0; i < gvServicosCarrito.Rows.Count; i++)
            {
                stotalVtaNva = stotalVtaNva + double.Parse(gvServicosCarrito.Rows[i].Cells[6].Text);
            }
            iva = stotalVtaNva * 0.16;
            totalVta = stotalVtaNva + iva;
            lblSubtotal.Text = "Subtotal: $" + stotalVtaNva.ToString();
            lblIva.Text = "I.V.A. (16%) : $" + iva.ToString();
            lblTotal.Text = "Total Venta: $";
            txtTotal.Text = totalVta.ToString();
        }

        protected void btnAñadir_Click(object sender, EventArgs e)
        {
            double subtotalUnidad = 0;
            lblCarrito.Visible = true;
            lblTotal.Visible = true;
            lblSubtotal.Visible = true;
            lblIva.Visible = true;
            txtTotal.Visible = true;

            //RECORRE EL GRID PARA AGREGAR LOS PRODUCTOS SELECCIONADOS
            for (int i = 0; i < gvServicios.Rows.Count; i++)
            {
                //chk toma el valor del checkbox que tiene cada registro de productos 
                CheckBox chk = (CheckBox)gvServicios.Rows[i].FindControl("chkSeleccionado");
                //verifica que se haya seleccionado para poder insertarlo en el carrito de compras 
                if (chk.Checked && chk != null)
                {
                    //accedo al drop
                    DropDownList dd = (DropDownList)gvServicios.Rows[i].FindControl("dwlCantidad");
                    if (int.Parse(dd.SelectedItem.ToString()) <= int.Parse(gvServicios.Rows[i].Cells[6].Text))
                    {
                        subtotalUnidad = double.Parse(dd.SelectedItem.ToString()) * double.Parse(gvServicios.Rows[i].Cells[7].Text);
                        //enviar el regristro al grid de carrito de compras nuevas
                        objCarrito.insertarDetalleReserva(Application["cnnVentas"].ToString(),
                            int.Parse(Session["venActual"].ToString()),
                            int.Parse(gvServicios.Rows[i].Cells[3].Text),
                            int.Parse(dd.SelectedItem.ToString()), subtotalUnidad);
                        detalleReservas();
                        calcularTotalReserva();

                    }
                }

            }
            lblCarrito.Visible = true;
            gvServicosCarrito.Visible = true;
            btnCerrarCarrito.Visible = true;
            btnEliminarProd.Visible = true;
            servicios();
        }

        protected void gvServicosCarrito_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
   
        }

        protected void btnEliminarProd_Click(object sender, EventArgs e)
        {
            //RECORRE EL GRID PARA QUITAR LOS PRODUCTOS DE LA NUEVA COMPRA
            for (int i = 0; i < gvServicosCarrito.Rows.Count; i++)
            {
                //chk, toma el valor del checkbox que tiene cada registro de productos
                CheckBox chk = (CheckBox)gvServicosCarrito.Rows[i].FindControl("chkSelect");
                //vreifica que se haya seleccionado para poder insertarlo en el carrito de compras
                if (chk.Checked && chk != null)
                {
                    //ELIMINAR EL PRODUCTO SELECCIONADO DEL CARRITO DE COMPRAS
                    objCarrito.eliminarServicioReserva(Application["cnnVentas"].ToString(),
                        int.Parse(gvServicosCarrito.Rows[i].Cells[1].Text),
                        int.Parse(gvServicosCarrito.Rows[i].Cells[2].Text),
                        int.Parse(gvServicosCarrito.Rows[i].Cells[4].Text));
                    //resta del total la cantidad de productos que ya no se van a comprar
                    txtTotal.Text = (double.Parse(txtTotal.Text) - double.Parse(gvServicosCarrito.Rows[i].Cells[6].Text)).ToString();
                }
            }
            servicios();
            detalleReservas();
            calcularTotalReserva();

        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            if (Session["venActual"].ToString() == "0")
            {
                Response.Write("<script language='javascript'>" +
                    "alert('No puedes confimar la compra porque no" +
                    "has generado ninguna compra actual...');</script>");
            }
            else if (txtTotal.Text == "" || txtTotal.Text == "0")
            {
                Response.Write("<script language='javascript'>" +
                    "alert('No puedes confirmar la compra " +
                    "porque no has comprado productos...');</script>");
            }
            else
            {
                //ejecuta la actualización de la venta
                objCarrito.confirmarReserva(Application["cnnVentas"].ToString(),
                    int.Parse(Session["venActual"].ToString()),
                    float.Parse(txtTotal.Text));
                Session["venActual"] = "0";

                ventasRealizadas();
                servicios();
                detalleReservas();
                lblVenta.Text = "[Pendiente de generarse una venta]";
                gvServicosCarrito.Visible = false;
                gvServicios.Visible = false;
                btnAñadir.Visible = false;
                lblReservas.Visible = false;
                lblTotal.Visible = false;
                lblIva.Visible = false;
                lblSubtotal.Visible = false;
                txtTotal.Visible = false;
                btnCancelar.Visible = false;
                lblCarrito.Visible = false;
                btnConfirmarCompra.Visible = false;
                Response.Write("<script language='javascript'>" +
                    "alert('La compra se ha confirmado y cerrado correctamente...');" +
                    "</script>");

            }
        }

        protected void btnCerrarCarrito_Click(object sender, EventArgs e)
        {
            // 1) Validaciones iguales a tu lógica vieja
            if (Session["venActual"].ToString() == "0")
            {
                Response.Write("<script language='javascript'>" +
                    "alert('No puedes cerrar el carrito porque no has generado ninguna compra actual...');" +
                    "</script>");
            }
            else if (txtTotal.Text == "" || txtTotal.Text == "0")
            {
                Response.Write("<script language='javascript'>" +
                    "alert('No puedes cerrar el carrito porque no has agregado productos...');" +
                    "</script>");
            }
            else
            {
                // ============================================
                // 2) Quitar TODOS los productos del detalle reserva
                //    (similar a tu viejo for de gvListaCarrito)
                // ============================================
                for (int i = 0; i < gvServicosCarrito.Rows.Count; i++)
                {
                    // NO usamos checkbox, aquí se eliminan todos los renglones
                    int claveReserva = int.Parse(gvServicosCarrito.Rows[i].Cells[1].Text); // ClaveReserva
                    int claveServicio = int.Parse(gvServicosCarrito.Rows[i].Cells[2].Text); // ClaveServicio
                    int cantidad = int.Parse(gvServicosCarrito.Rows[i].Cells[4].Text);      // CantidadReservada

                    objCarrito.eliminarServicioReserva(
                        Application["cnnVentas"].ToString(),
                        claveReserva,
                        claveServicio,
                        cantidad
                    );
                }

                // ============================================
                // 3) Actualizar grids / datos
                // ============================================
                servicios();        // vuelve a listar servicios disponibles (actualiza cupos)
                detalleReservas();  // ya no debería mostrar nada para la venta actual
                gvServicosCarrito.Visible = false;

                // Ocultar resumen de compra
                lblSubtotal.Visible = false;
                lblIva.Visible = false;
                lblTotal.Visible = false;
                txtTotal.Visible = false;
                txtTotal.Text = "0";

                // Ocultar productos / botones de carrito
                lblCarrito.Visible = false;
                btnCerrarCarrito.Visible = false;
                btnEliminarProd.Visible = false;
                btnConfirmarCompra.Visible = false;

                lblProductos.Visible = false;
                gvServicios.Visible = false;
                btnAñadir.Visible = false;

                // ============================================
                // 4) Cancelar la reserva en BD (tspCancelarReserva)
                // ============================================
                objCarrito.cancelarReserva(
                    Application["cnnVentas"].ToString(),
                    int.Parse(Session["venActual"].ToString())
                );

                // 5) Liberar la venta actual
                Session["venActual"] = "0";

                // Actualizar historial de reservas
                ventasRealizadas();

                lblVenta.Text = "[Pendiente de generarse una venta]";

                // Mensaje final
                Response.Write("<script language='javascript'>" +
                    "alert('La compra se ha cancelado y cerrado correctamente...');" +
                    "</script>");
            }
        }


        protected void gvServicios_SelectedIndexChanging1(object sender, GridViewSelectEventArgs e)
        {
            Session["selProducto"] = gvServicios.Rows[e.NewSelectedIndex].Cells[3].Text;

            Response.Write("<script> window.open('frmCliDetServicio.aspx', 'Detalle " +
                "producto', 'height=600,width=350,resizable=no,left=-1000,top=-1000','_blank');" + "</script>");
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {

        }

        protected void gvServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Cambiar a la página seleccionada
            gvServicios.PageIndex = e.NewPageIndex;

            // Volver a cargar los datos del GridView
            servicios();
        }
    }
}