<%@ WebHandler Language="C#" Class="h_listar_pantallas_frecuentes" %>

using System;
using System.Web;

public class h_listar_pantallas_frecuentes : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        //Se recupera id del usuario
        string sIdUsuario = context.Request["sIdUsuario"];
        //Se desencripta id del usuario
        Security sec_idUsuario = new Security(sIdUsuario);
        int iIdUsuario = int.Parse(sec_idUsuario.DesEncriptar());
        context.Response.ContentType = "text/plain";
        int iEcho = Int32.Parse(context.Request["draw"]);
        int iDisplayLength = Int32.Parse(context.Request["length"]);
        int iDisplayStart = Int32.Parse(context.Request["start"]);
        string[] sColumnas = new string[] { "sNombrePantallaFrecuente", "sMenu", "iIdPantallaEliminar" };
        string sSearch = context.Request["search[value]"];
        ///////////
        //SEARCH (filter)
        //- build the where clause
        ////////
        System.Text.StringBuilder where = new System.Text.StringBuilder();
        string sWhereClause = string.Empty;
        for (int i = 0; i < sColumnas.Length; i++)
        {
            if (where.ToString().Equals(""))
            {
                where.Append(" WHERE ");
            }
            string sText = context.Request["columns[" + i + "][search][value]"];
            if (!sText.Equals(""))
            {
                where.Append("  " + sColumnas[i] + " like '%" + sText + "%' AND");
            }
            sWhereClause = where.ToString().Substring(0, (where.ToString().Length - 3));
        }
        if (!String.IsNullOrEmpty(sSearch))
        {
            if (where.ToString().Equals(""))
            {
                where.Append(" WHERE ");
            }
            for (int i = 0; i < sColumnas.Length; i++)
            {
                where.Append("  " + sColumnas[i] + " like '%" + sSearch + "%' AND");
            }
            sWhereClause = where.ToString().Substring(0, (where.ToString().Length - 3));
        }
        else
        {
            if (!where.ToString().Equals(""))
            {
                sWhereClause = where.ToString() + " iIdUsuario = " + iIdUsuario.ToString();
            }
        }
        ///////////////
        //ORDERING
        //- build the order by clause
        //////////////
        System.Text.StringBuilder orderBy = new System.Text.StringBuilder();
        string sOrderByClause = string.Empty;
        int iNumOrden = -0;
        //Check which column is to be sorted by in which direction
        for (int i = 0; i < sColumnas.Length; i++)
        {
            if (context.Request.Params["order[" + i + "][column]"] != null)
            {
                iNumOrden = int.Parse(context.Request.Params["order[" + i + "][column]"].ToString());
                orderBy.Append(context.Request.Params["order[" + i + "][column]"]);
                orderBy.Append(" ");
                orderBy.Append(context.Request.Params["order[" + i + "][dir]"]);
            }
        }
        sOrderByClause = orderBy.ToString();
        //Replace the number corresponding the column position by the corresponding name of the column in the database
        if (!String.IsNullOrEmpty(sOrderByClause))
        {
            for (int i = 0; i < sColumnas.Length; i++)
            {
                if (i == iNumOrden)
                {
                    sOrderByClause = sOrderByClause.Replace(i.ToString(), ", " + sColumnas[(i)] + "");
                }

            }

            //Eliminate the first comma of the variable "order"
            sOrderByClause = sOrderByClause.Remove(0, 1);
        }
        sOrderByClause = "ORDER BY " + sOrderByClause;
        /////////////
        //T-SQL query
        //- ROW_NUMBER() is used for db side pagination
        /////////////
        string sQuery = "SELECT * FROM ( SELECT ROW_NUMBER() OVER ({0}) AS RowNumber, " +
            " * FROM ( SELECT ( SELECT COUNT(*) FROM v_PantallasFreuentes {1} ) AS TotalDisplayRows, " +
            "(SELECT COUNT(*) FROM v_PantallasFreuentes ) AS TotalRows, " +
            "iIdUsuario, sNombrePantallaFrecuente, iEstado, sMenu, iIdPantallaEliminar " +
            "FROM v_PantallasFreuentes {1}) RawResults ) " +
            "Results WHERE RowNumber BETWEEN {2} AND {3}";
        string sDisplayLengthMod = iDisplayLength == -1 ? "TotalDisplayRows" : (iDisplayLength + iDisplayStart).ToString();
        sQuery = String.Format(sQuery, sOrderByClause, sWhereClause, iDisplayStart + 1, sDisplayLengthMod);
        //Get result rows from DB
        System.Data.SqlClient.SqlConnection conn = new
        System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DB_SOPConnectionString"].ConnectionString);
        conn.Open();
        System.Data.SqlClient.SqlCommand cmd = conn.CreateCommand();
        cmd.CommandText = sQuery;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandTimeout = 120;
        System.Data.IDataReader rdrBrowsers = cmd.ExecuteReader();
        /////////////
        /// JSON output
        /// - build JSON output from DB results
        /// ///////////
        System.Text.StringBuilder json = new System.Text.StringBuilder();
        string sOutputJson = string.Empty;
        int iTotalDisplayRecords = 0;
        int iTotalRecords = 0;
        while (rdrBrowsers.Read())
        {
            if (iTotalRecords == 0)
                iTotalRecords = Int32.Parse(rdrBrowsers["TotalRows"].ToString());
            if (iTotalDisplayRecords == 0)
                iTotalDisplayRecords = Int32.Parse(rdrBrowsers["TotalDisplayRows"].ToString());
            json.Append("{");
            ///Ciclo para recorrer los registros de las tablas
            ///
            for (int i = 0; i < sColumnas.Length; i++)
            {
                if (i == sColumnas.Length - 1)
                {
                    json.Append("\"" + sColumnas[i] + "\":\"" + "<a href='#_' data-toggle='modal' data-target='#dialogEliminarPantallaFrecuente' onclick='javascript:fn_obtener_id_pantalla_frecuente_eliminar(" + rdrBrowsers[sColumnas[i]] + ");'><span class='glyphicon glyphicon-trash icon_red' aria-hidden='true'></span></a>" + " \"");
                }
                else
                {
                    json.Append("\"" + sColumnas[i] + "\":\"" + "<span id='" + i + "' name='" + rdrBrowsers[sColumnas[i]].ToString() + "'>" + rdrBrowsers[sColumnas[i]] + "</span>" + " \",");
                }
            }
            json.Append("},");
        }
        sOutputJson = json.ToString();
        if (!sOutputJson.Equals(""))
        {
            sOutputJson = sOutputJson.Remove(sOutputJson.Length - 1);
        }
        System.Text.StringBuilder response = new System.Text.StringBuilder();
        sOutputJson = sOutputJson.Replace(System.Environment.NewLine, "");
        sOutputJson = sOutputJson.Replace("\u0009", "");
        response.Append("{");
        response.Append("\"draw\": ");
        response.Append(iEcho);
        response.Append(",");
        response.Append("\"recordsTotal\": ");
        response.Append(iTotalRecords);
        response.Append(",");
        response.Append("\"recordsFiltered\": ");
        response.Append(iTotalDisplayRecords);
        response.Append(",");
        response.Append("\"data\": [");
        response.Append(sOutputJson);
        response.Append("]}");
        sOutputJson = response.ToString();
        /////////////
        /// Write to Response
        /// - clear other HTML elements
        /// - flush out JSON output
        /// ///////////
        context.Response.Clear();
        context.Response.ClearHeaders();
        context.Response.ClearContent();
        context.Response.Write(sOutputJson);
        context.Response.Flush();
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}