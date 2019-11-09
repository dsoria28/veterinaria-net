using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

/// <summary>
/// Descripción breve de Conexion
/// </summary>
public class Conexion
{

    SqlConnection conexion;
    SqlCommand comando;
    SqlCommand comandoSP;
    SqlDataReader leer;
    SqlDataAdapter adaptador;

    string conn;

    String sConexionSOP = "DB_SOPConnectionString";///ID 1
    String sConexionNADsi = "DB_NADsiConnectionString";/// ID 2


    ////Constructor para conexión por default para SOP
    public Conexion(){
        this.conn = sConexionSOP;
    }

    /// <summary>
    /// Constructor para conexiones adicionales
    /// </summary>
    /// <param name="iTipoConexion"></param>
    public Conexion(int iTipoConexion)
    {
        switch (iTipoConexion) {
            case 2: //Tipo 2 es NAD SI
                this.conn = sConexionNADsi;
                break;
            default: ///si se pone alguna incorrecta se toma Conexion SOP
                this.conn = sConexionSOP;
                break;
        }
    }


    //Metodo para abrir la conexion
    public string abrirConexion()
    {
        conexion = new SqlConnection(ConfigurationManager.ConnectionStrings[conn].ConnectionString);

        try{
            conexion.Open();
            return "1"; //Exito en la conexion
        }catch (Exception ex){
            Console.Write(ex.Message);
            return "Error al abrir la BD: "+ex.Message; //Error en la conexion
        }
        
    }

    //Metodo para cerrar la conexion
    public string cerrarConexion()
    {
        try
        {
            conexion.Close();
            conexion.Dispose();
            return "1";//Exito
        }
        catch (Exception ex)
        {
            return "Error al cerrar Conexion: " + ex.Message;
        }
        
    }

    //Metodo para ejecutar sentencia
    public string ejecutarComando(string query)
    {
        string res = abrirConexion();
        string msj;
        if (res == "1")
        {
            comando = new SqlCommand(query, conexion);

            try
            {
                comando.ExecuteNonQuery();
                msj = "1"; //Exito al ejecutar comando
            }
            catch (Exception ex)
            {
                msj = "Error al ejecutar comando: " + ex.Message; //Error
            }
            finally {
                cerrarConexion();
            }
        }
        else {
            msj= "Error al abrir la BD y ejecutar comando: " + res; //Error al abrir la conexion
        }
        
            return msj;
    }

    //Metodo para ejecutar consulta y regresar un solo registro
    public string[] ejecutarConsultaRegistroSimple(string query)
    { 
        string[] res=new string[2];
        string resCon;
        resCon = abrirConexion();
        string msj;
        if (resCon == "1")
        {
            comando = new SqlCommand(query, conexion);

            try
            {
                leer = comando.ExecuteReader();

                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        res[1] = leer.GetValue(0).ToString();
                    }
                }
                else {
                    res[1] = "";
                }
                //comando.ExecuteNonQuery();
                msj = "1"; //Exito al ejecutar comando
            }
            catch (Exception ex)
            {
                msj = "Error al ejecutar comando: " + ex.Message; //Error
                res[1] = "";
            }
            finally {
                cerrarConexion();
            }
        }
        else
        {
            msj = "Error al abrir la BD y ejecutar la consulta registro: " + res; //Error al abrir la conexion
            res[1] = "";
        }

        res[0] = msj;

        return res;       

    }

    //Metodo para ejecutar consulta y regresa multiples registros
    public List<string> ejecutarConsultaRegistroMultiples(string query)
    {
        List<string> res = new List<string>();
        res.Add("");
        string resCon;
        resCon = abrirConexion();
        string msj;
        if (resCon == "1")
        {
            comando = new SqlCommand(query, conexion);

            try
            {
                leer = comando.ExecuteReader();

                if (leer.HasRows)
                {
                    while (leer.Read())
                    {
                        for (int i = 0; i < leer.FieldCount;i++ )
                        {
                            res.Add(leer.GetValue(i).ToString());
                        }
                        
                    }
                }
                //comando.ExecuteNonQuery();
                msj = "1"; //Exito al ejecutar comando
            }
            catch (Exception ex)
            {
                msj = "Error al ejecutar comando: " + ex.Message; //Error
            }
            finally
            {
                cerrarConexion();
            }
        }
        else
        {
            msj = "Error al abrir la BD y ejecutar la consulta registro: " + res; //Error al abrir la conexion
        }

        res[0] = msj;

        return res;

    }

    //Metodo para ejecutar consulta sin abrir Conexion
    public List<string> ejecutarConsultaRegistroMultiples_sin_conexion(string query)
    {
        List<string> res = new List<string>();
        res.Add("1");

        comando = new SqlCommand(query, conexion);
        leer = comando.ExecuteReader();

        if (leer.HasRows){

            while (leer.Read())
            {
                for (int i = 0; i < leer.FieldCount; i++)
                {
                    res.Add(leer.GetValue(i).ToString());
                }

            }
        }
        leer.Close();
        return res;

    }

    //Metodo para ejecutar consulta y regresa multiples registros en un DataTable
    public DataTable ejecutarConsultaRegistroMultiplesData(string query)
    {
        DataTable res = new DataTable();
        try
        {
            abrirConexion();

            comando = new SqlCommand(query, conexion);

            adaptador = new SqlDataAdapter(comando);

            adaptador.Fill(res);

        }finally
        {
            cerrarConexion();
        }

        return res;

    }

    //Metodo para ejecutar consulta y retornar multiples registros en un DataSet
    public DataSet ejecutarConsultaRegistroMultiplesDataSet(string query,string nombre)
    {
        DataSet res = new DataSet();
        try
        {
            abrirConexion();
            comando = new SqlCommand(query,conexion);
            adaptador = new SqlDataAdapter(comando);

            adaptador.Fill(res,nombre);
        }
        finally {
            cerrarConexion();
        }

        return res;
    }

    //MEtodo para ejecutar precedimiento almacenado y retornar multiples registrs en un DataTable
    public DataTable ejecutarProcRegistroMultiplesData()
    {
        DataTable res = new DataTable();
        try
        {

            adaptador = new SqlDataAdapter(comandoSP);

            adaptador.Fill(res);

        }
        finally
        {
            cerrarConexion();
        }

        return res;
    }

    /**********************************************************************************************
     Metodos para llamar procedimientos almacenados
     **********************************************************************************************/
    //Metodo para inicializar un procedimiento almacenado
    public string generarSP(string nombreSP,int timeout)
    {
        string msj;
        string con = abrirConexion();
        if (con == "1")
        {
            try
            {
                comandoSP = new SqlCommand(nombreSP, conexion);
                if (timeout > 0)
                {
                    comandoSP.CommandTimeout = timeout;
                }
                
                comandoSP.CommandType = CommandType.StoredProcedure;
                msj = "1";
            }
            catch (Exception ex)
            {
                msj = "Erro generar SP: " + ex.Message;
            }
        }
        else {
            msj = "Error con proc_ :"+con;
        }
        

        return msj;

    }

    //Metodo para agregar parametros al SP
    public void agregarParametroSP(string variableProc,SqlDbType tipoSql,string valor)
    {
            comandoSP.Parameters.Add(variableProc, tipoSql).Value = valor;   
    }

    //Metodo para ejecutar el procedimento almacenado
    public string ejecutarProc()
    {
        string msj;
        
        try{
            comandoSP.ExecuteNonQuery();
            msj = "1";
        }catch(Exception ex){
            msj = ex.Message;
        }finally {
            cerrarConexion();
        }

        return msj;
    }

    //Metodo para ejecutar el procedimiento almacenado con valor OUTPUT
    public string[] ejecutarProcOUTPUT_INT(string valor)
    {
        string[] resultado=new string[2];

        try
        {
            comandoSP.Parameters.Add(valor, SqlDbType.VarChar, 100);
            comandoSP.Parameters[valor].Direction = ParameterDirection.Output;

            comandoSP.ExecuteNonQuery();
            resultado[0] = "1";

            resultado[1] = comandoSP.Parameters[valor].Value.ToString();
        }
        catch (Exception ex)
        {
            resultado[0] = ex.Message;
        }
        finally {
            cerrarConexion();
        }

        return resultado;
    }

    //Metodo para ejecutrar el procedimiento almacenado con valor STRING
    public string[] ejecutarProcOUTPUT_STRING(string valor)
    {
        string[] resultado = new string[2];

        try
        {
            comandoSP.Parameters.Add(valor,SqlDbType.VarChar,-1);
            comandoSP.Parameters[valor].Direction = ParameterDirection.Output;

            comandoSP.ExecuteNonQuery();
            resultado[0] = "1";

            resultado[1] = comandoSP.Parameters[valor].Value.ToString();
        }
        catch (Exception ex)
        {
            resultado[0] = ex.Message;
        }
        finally {
            cerrarConexion();
        }

        return resultado;

    }


    //Método para ejecutar procedimiento con parametro de retorno
    public string ejecutarProReturnValue()
    {
        string msj;

        try
        {
            //comandoSP.Parameters.Add("@errSql", SqlDbType.TinyInt).Direction = ParameterDirection.ReturnValue;
            comandoSP.Parameters.Add(new SqlParameter("@errSql", SqlDbType.TinyInt)).Direction = ParameterDirection.ReturnValue;
            comandoSP.ExecuteNonQuery();
            msj = Convert.ToString(comandoSP.Parameters["@errSql"].Value);
        }
        catch
        {
            msj = "20";
        }
        finally
        {
            cerrarConexion();
        }

        return msj;
    }
   
}