<%@ WebHandler Language="C#" Class="h_listar_actualizaciones" %>

using System;
using System.Web;

public class h_listar_actualizaciones : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        int iEcho = Int32.Parse(context.Request["draw"]);
        int sDisplayLength = Int32.Parse(context.Request["length"]);
        int sDisplayStart = Int32.Parse(context.Request["start"]);
        ///ARREGLO PARA RECORRER COLUMNAS A MOSTRAR
        string[] sColumnas = new string[] { "iSec", "sVersion", "dFechaNotificacionAccion", "sDescripcion" };

        string sSearch = context.Request["search[value]"];

        ///////////
        //SEARCH (filter)
        //- build the where clause
        ////////
        System.Text.StringBuilder where = new System.Text.StringBuilder();
        string sWhereClause = string.Empty;

        for (int i = 0; i < 4; i++)
        {
            if (context.Request["columns[" + i + "][search][value]"] != null)
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
            }
        }

        if (!String.IsNullOrEmpty(sSearch))
        {
            if (where.ToString().Equals(""))
            {
                where.Append(" WHERE ");
            }
            for (int i = 0; i < 4; i++)
            {
                where.Append("  " + sColumnas[i] + " like '%" + sSearch + "%' AND");
            }
            sWhereClause = where.ToString().Substring(0, (where.ToString().Length - 3));
        }
        else
        {
            sWhereClause = where.ToString().Substring(0, (where.ToString().Length - 3));
        }


        ///////////////
        //ORDERING
        //- build the order by clause
        //////////////
        System.Text.StringBuilder sOrderBy = new System.Text.StringBuilder();
        string sOrderByClause = string.Empty;
        int iContadorOrden = 0;
        //Check which column is to be sorted by in which direction
        for (int i = 0; i < 4; i++)
        {
            if (context.Request.Params["order[" + i + "][column]"] != null)
            {
                sOrderBy.Append(context.Request.Params["order[" + i + "][column]"]);
                sOrderBy.Append(" ");
                sOrderBy.Append(context.Request.Params["order[" + i + "][dir]"]);

            }
        }

        ///Retorna registro
        int.TryParse(sOrderBy.ToString().Replace("desc", "").Replace("asc", ""), out iContadorOrden);


        sOrderByClause = sOrderBy.ToString();
        //Replace the number corresponding the column position by the corresponding name of the column in the database
        if (!String.IsNullOrEmpty(sOrderByClause))
        {
            for (int i = 0; i < 4; i++)
            {
                if (iContadorOrden == i)
                    sOrderByClause = sOrderByClause.Replace(iContadorOrden.ToString(), ", " + sColumnas[i] + "");
            }

            //Eliminate the first comma of the variable "order"
            sOrderByClause = sOrderByClause.Remove(0, 1);
        }
        else
        {
            sOrderByClause = "" + sColumnas[0] + " ASC";
        }
        sOrderByClause = "ORDER BY " + sOrderByClause;

        /////////////
        //T-SQL query   
        //- ROW_NUMBER() is used for db side pagination
        /////////////
        string sQuery = "SELECT * FROM ( SELECT ROW_NUMBER() OVER ({0}) AS RowNumber," +
            "* FROM ( select ( select count(iIdNotificacion) from v_ListaActualizaciones {1} ) AS TotalDisplayRows, " +
            "(select count(iIdNotificacion) from v_ListaActualizaciones ) AS TotalRows," +
            "* from  v_ListaActualizaciones {1} ) RawResults ) " +
            "Results WHERE RowNumber BETWEEN {2} AND {3}";
        sQuery = String.Format(sQuery, sOrderByClause, sWhereClause, sDisplayStart + 1, sDisplayStart + sDisplayLength);

        //Get result rows from DB
        System.Data.SqlClient.SqlConnection obj_conexion = new
        System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DB_SOPConnectionString"].ConnectionString);
        obj_conexion.Open();
        System.Data.SqlClient.SqlCommand cmd = obj_conexion.CreateCommand();
        cmd.CommandText = sQuery;
        cmd.CommandType = System.Data.CommandType.Text;
        cmd.CommandTimeout = 120;
        System.Data.IDataReader rdrBrowsers = cmd.ExecuteReader();


        /////////////
        /// JSON output
        /// - build JSON output from DB results
        /// ///////////
        System.Text.StringBuilder sJson = new System.Text.StringBuilder();
        string sOutputJson = string.Empty;
        int iTotalDisplayRecords = 0;
        int iTotalRecords = 0;


        ///CICLO PARA RECORRER LOS REGISTROS DE LA TABLA
        while (rdrBrowsers.Read())
        {
            if (iTotalRecords == 0)
                iTotalRecords = Int32.Parse(rdrBrowsers["TotalRows"].ToString());
            if (iTotalDisplayRecords == 0)
                iTotalDisplayRecords = Int32.Parse(rdrBrowsers["TotalDisplayRows"].ToString());
            ///SE ABRE REGISTROS
            sJson.Append("{");
            string sIdNotificacion = rdrBrowsers["iIdNotificacion"].ToString();
            ///Se instancia la clase Security para encriptar el id
            Security obj_idNotificacion = new Security(sIdNotificacion);
            //Se le agrega el id del registro a cada fila
            sJson.Append("\"DT_RowId\":\"" + obj_idNotificacion.Encriptar() + "\",");
            ///CICLO PARA RECORRER Y MOSTRAR REGISTROS
            for (int i = 0; i < 4; i++)
            {
                ///Verifca si es ultimo registros
                if (i == 3)
                {
                    ///Agrega la accion inicial
                    sJson.Append("\"" + sColumnas[i] + "\":\"" + rdrBrowsers[sColumnas[i]] + "\"");
                }//FIN VERIRICA ULTIMO REGISTRO
                ///INICIO ELSE MUESTRA REGISTROS
                else
                {
                    ///MOSTRAR REGISTROS
                    sJson.Append("\"" + sColumnas[i] + "\":\"" + rdrBrowsers[sColumnas[i]] + "\",");
                }///FIN ELSE MUESTR REGISTROS
            }
            ///SE CIERRA REGISTRO
            sJson.Append("},");
        }
        sOutputJson = sJson.ToString();
        if (!sOutputJson.Equals(""))
        {
            sOutputJson = sOutputJson.Remove(sOutputJson.Length - 1);
        }
        System.Text.StringBuilder response = new System.Text.StringBuilder();

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

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}