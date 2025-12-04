<%@ Page Title="" Language="C#" MasterPageFile="~/ADMINISTRADOR/mpAdministrador.Master" AutoEventWireup="true" CodeBehind="frmAdministrador.aspx.cs" Inherits="wssProyecto.ADMINISTRADOR.Formulario_web1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <style type="text/css">
        :root {
            --primary: #FE810B;
            --bg-page: #f5f5f7;
            --card-bg: #ffffff;
            --text-main: #111111;
            --text-soft: #6e6e73;
            --border-soft: #d2d2d7;
        }

        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }

        body, html {
            font-family: -apple-system, BlinkMacSystemFont, "SF Pro Text", system-ui, sans-serif;
        }

        /* CONTENEDOR GENERAL */
        .admin-page {
            background: var(--bg-page);
            min-height: calc(100vh - 150px);
            padding: 40px 16px 60px 16px;
        }

        .admin-shell {
            max-width: 1100px;
            margin: 0 auto;
        }

        /* HEADER TIPO APPLE */
        .admin-header {
            margin-bottom: 32px;
        }

        .admin-kicker {
            font-size: 13px;
            letter-spacing: .12em;
            text-transform: uppercase;
            color: var(--text-soft);
            margin-bottom: 6px;
        }

        .admin-title-row {
            display: flex;
            flex-wrap: wrap;
            align-items: baseline;
            gap: 10px;
        }

        .admin-title {
            font-size: 32px;
            font-weight: 600;
            color: var(--text-main);
        }

        .admin-subtitle {
            font-size: 14px;
            color: var(--text-soft);
        }

        .admin-user-chip {
            margin-top: 14px;
            display: inline-flex;
            align-items: center;
            gap: 8px;
            padding: 6px 12px;
            border-radius: 999px;
            border: 1px solid var(--border-soft);
            background: rgba(255,255,255,0.9);
            font-size: 13px;
            color: var(--text-soft);
        }

        .admin-user-dot {
            width: 7px;
            height: 7px;
            border-radius: 50%;
            background: #34c759;
        }

        .admin-user-name {
            font-weight: 500;
            color: var(--text-main);
        }

        /* BLOQUE PRINCIPAL: TARJETA GRANDE */
        .admin-main-card {
            background: var(--card-bg);
            border-radius: 26px;
            padding: 24px 24px 22px 24px;
            border: 1px solid #e5e5ea;
            box-shadow: 0 8px 25px rgba(0,0,0,0.03);
            margin-bottom: 24px;
        }

        .admin-main-top {
            display: flex;
            justify-content: space-between;
            gap: 16px;
            margin-bottom: 22px;
        }

        .admin-main-heading {
            font-size: 18px;
            font-weight: 500;
            color: var(--text-main);
        }

        .admin-main-desc {
            font-size: 13px;
            color: var(--text-soft);
            margin-top: 4px;
        }

        .admin-main-pill {
            align-self: flex-start;
            padding: 4px 10px;
            border-radius: 999px;
            border: 1px solid var(--border-soft);
            font-size: 12px;
            color: var(--text-soft);
        }

        /* GRID DE MÉTRICAS (POCO PERO CLARO) */
        .admin-metrics {
            display: grid;
            grid-template-columns: repeat(4, minmax(0, 1fr));
            gap: 14px;
        }

        .metric-card {
            padding: 12px 10px 10px 10px;
            border-radius: 18px;
            border: 1px solid #e5e5ea;
            background: #f9f9fb;
        }

        .metric-label {
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: .08em;
            color: var(--text-soft);
            margin-bottom: 6px;
        }

        .metric-value {
            font-size: 20px;
            font-weight: 600;
            color: var(--text-main);
            margin-bottom: 2px;
        }

        .metric-note {
            font-size: 12px;
            color: var(--text-soft);
        }

        .metric-pill {
            display: inline-block;
            margin-top: 6px;
            font-size: 11px;
            padding: 3px 8px;
            border-radius: 999px;
            background: rgba(254,129,11,0.06);
            color: var(--primary);
        }

        /* SEGUNDA FILA: DOS TARJETAS SENCILLAS */
        .admin-secondary-grid {
            display: grid;
            grid-template-columns: 1.4fr 1fr;
            gap: 18px;
            margin-top: 22px;
        }

        .card-simple {
            background: var(--card-bg);
            border-radius: 24px;
            padding: 18px 18px 16px 18px;
            border: 1px solid #e5e5ea;
        }

        .card-simple-title {
            font-size: 14px;
            font-weight: 500;
            color: var(--text-main);
            margin-bottom: 10px;
        }

        .card-simple-sub {
            font-size: 12px;
            color: var(--text-soft);
            margin-bottom: 12px;
        }

        /* LISTAS MUY SIMPLES */
        .list-clean {
            list-style: none;
        }

        .list-item {
            padding: 8px 0;
            border-bottom: 1px solid #f0f0f5;
            display: flex;
            justify-content: space-between;
            font-size: 13px;
            color: var(--text-main);
        }

        .list-item:last-child {
            border-bottom: none;
        }

        .list-label {
            color: var(--text-soft);
        }

        .list-strong {
            font-weight: 500;
        }

        /* TAGS */
        .tag-soft {
            display: inline-flex;
            align-items: center;
            gap: 6px;
            padding: 4px 9px;
            border-radius: 999px;
            border: 1px solid var(--border-soft);
            font-size: 11px;
            color: var(--text-soft);
        }

        .tag-dot {
            width: 6px;
            height: 6px;
            border-radius: 50%;
            background: var(--primary);
        }

        /* ACCESOS RÁPIDOS (NUEVA SECCIÓN) */
        .admin-quick {
            margin-top: 8px;
            margin-bottom: 18px;
        }

        .admin-quick-title {
            font-size: 13px;
            text-transform: uppercase;
            letter-spacing: .12em;
            color: var(--text-soft);
            margin-bottom: 10px;
        }

        .admin-quick-grid {
            display: grid;
            grid-template-columns: repeat(3, minmax(0, 1fr));
            gap: 12px;
        }

        .quick-card {
            background: var(--card-bg);
            border-radius: 20px;
            padding: 14px 14px 12px 14px;
            border: 1px solid #e5e5ea;
            cursor: pointer;
            transition: all 0.25s ease;
            text-decoration: none;
            color: inherit;
            display: block;
        }

        .quick-card:hover {
            box-shadow: 0 8px 22px rgba(0,0,0,0.05);
            transform: translateY(-3px);
        }

        .quick-title {
            font-size: 14px;
            font-weight: 500;
            color: var(--text-main);
            margin-bottom: 4px;
        }

        .quick-sub {
            font-size: 12px;
            color: var(--text-soft);
        }

        /* FOOTER SUAVE */
        .admin-footer-soft {
            margin-top: 16px;
            font-size: 11px;
            color: var(--text-soft);
            text-align: center;
        }

        /* RESPONSIVE */
        @media (max-width: 900px) {
            .admin-metrics {
                grid-template-columns: repeat(2, minmax(0, 1fr));
            }

            .admin-secondary-grid {
                grid-template-columns: 1fr;
            }

            .admin-quick-grid {
                grid-template-columns: repeat(2, minmax(0, 1fr));
            }
        }

        @media (max-width: 600px) {
            .admin-page {
                padding: 24px 12px 40px 12px;
            }

            .admin-main-card {
                padding: 18px 16px 16px 16px;
            }

            .admin-title {
                font-size: 26px;
            }

            .admin-metrics {
                grid-template-columns: 1fr;
            }

            .admin-quick-grid {
                grid-template-columns: 1fr;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="admin-page">
        <div class="admin-shell">

            <!-- HEADER -->
            <div class="admin-header">
                <div class="admin-kicker">TripGo · Panel</div>

                <div class="admin-title-row">
                    <h1 class="admin-title">Administrador</h1>
                    <p class="admin-subtitle">Resumen general del sistema</p>
                </div>

                <div class="admin-user-chip">
                    <span class="admin-user-dot"></span>
                    <span class="admin-user-name">
                        <asp:Label ID="lblSesion" runat="server" Text="Administrador"></asp:Label>
                    </span>
                    <span>· Sesión activa</span>
                </div>
            </div>

            <!-- TARJETA PRINCIPAL -->
            <div class="admin-main-card">
                <div class="admin-main-top">
                    <div>
                        <div class="admin-main-heading">Estado actual</div>
                        <div class="admin-main-desc">
                            Una vista rápida de viajes, clientes, reservas e ingresos.
                        </div>
                    </div>
                    <div class="admin-main-pill">
                        Actualizado hoy · 08:45
                    </div>
                </div>

                <!-- MÉTRICAS -->
                <div class="admin-metrics">
                    <div class="metric-card">
                        <div class="metric-label">Viajes</div>
                        <div class="metric-value">2,847</div>
                        <div class="metric-note">156 activos · 2,691 completados</div>
                    </div>

                    <div class="metric-card">
                        <div class="metric-label">Clientes</div>
                        <div class="metric-value">12,458</div>
                        <div class="metric-note">342 nuevos este mes</div>
                    </div>

                    <div class="metric-card">
                        <div class="metric-label">Reservas hoy</div>
                        <div class="metric-value">89</div>
                        <div class="metric-note">Tendencia al alza</div>
                        <span class="metric-pill">+23% vs ayer</span>
                    </div>

                    <div class="metric-card">
                        <div class="metric-label">Ingresos mensuales</div>
                        <div class="metric-value">$427K</div>
                        <div class="metric-note">85% de la meta</div>
                    </div>
                </div>

                <!-- SEGUNDA FILA (simple) -->
                <div class="admin-secondary-grid">
                    <!-- Actividad reciente -->
                    <div class="card-simple">
                        <div class="card-simple-title">Actividad reciente</div>
                        <div class="card-simple-sub">Últimos movimientos registrados en TripGo.</div>

                        <ul class="list-clean">
                            <li class="list-item">
                                <span class="list-label">Reserva confirmada</span>
                                <span class="list-strong">Cancún · $4,500</span>
                            </li>
                            <li class="list-item">
                                <span class="list-label">Nuevo cliente</span>
                                <span class="list-strong">Ana Martínez</span>
                            </li>
                            <li class="list-item">
                                <span class="list-label">Servicio actualizado</span>
                                <span class="list-strong">Tour Teotihuacán</span>
                            </li>
                            <li class="list-item">
                                <span class="list-label">Factura emitida</span>
                                <span class="list-strong">Folio TRP-2025-001234</span>
                            </li>
                        </ul>
                    </div>

                    <!-- Resumen rápido -->
                    <div class="card-simple">
                        <div class="card-simple-title">Resumen rápido</div>
                        <div class="card-simple-sub">Puntos a considerar hoy.</div>

                        <ul class="list-clean">
                            <li class="list-item">
                                <span class="list-label">Pagos por confirmar</span>
                                <span class="list-strong">5 reservas</span>
                            </li>
                            <li class="list-item">
                                <span class="list-label">Reseñas 5★ esta semana</span>
                                <span class="list-strong">47</span>
                            </li>
                            <li class="list-item">
                                <span class="list-label">Destinos en alta demanda</span>
                                <span class="list-strong">Cancún, Tokio</span>
                            </li>
                        </ul>

                        <div style="margin-top:14px; display:flex; gap:8px; flex-wrap:wrap;">
                            <span class="tag-soft">
                                <span class="tag-dot"></span>
                                Usuarios
                            </span>
                            <span class="tag-soft">
                                <span class="tag-dot"></span>
                                Servicios
                            </span>
                            <span class="tag-soft">
                                <span class="tag-dot"></span>
                                Ventas
                            </span>
                        </div>
                    </div>
                </div>

            </div>

            <!-- ACCESOS RÁPIDOS (NUEVA SECCIÓN) -->
            <div class="admin-quick">
                <div class="admin-quick-title">Accesos rápidos</div>

                <div class="admin-quick-grid">
                    <asp:HyperLink ID="lnkUsuarios" runat="server"
                        NavigateUrl="~/ADMINISTRADOR/frmUadmin.aspx"
                        CssClass="quick-card">
                        <div class="quick-title">Usuarios</div>
                        <div class="quick-sub">Gestiona cuentas, roles y estatus.</div>
                    </asp:HyperLink>

                    <asp:HyperLink ID="lnkServicios" runat="server"
                        NavigateUrl="~/ADMINISTRADOR/frmAdminProd.aspx"
                        CssClass="quick-card">
                        <div class="quick-title">Servicios</div>
                        <div class="quick-sub">Consulta y administra los servicios de TripGo.</div>
                    </asp:HyperLink>

                    <asp:HyperLink ID="lnkVentas" runat="server"
                        NavigateUrl="~/ADMINISTRADOR/frmAdVentas.aspx"
                        CssClass="quick-card">
                        <div class="quick-title">Ventas</div>
                        <div class="quick-sub">Revisa reservas y reportes de ventas.</div>
                    </asp:HyperLink>
                </div>
            </div>

            <!-- FOOTER SUAVE -->
            <div class="admin-footer-soft">
                TripGo · Panel de administración · v1.0
            </div>

        </div>
    </div>
</asp:Content>
