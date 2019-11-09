using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de SubMenu
/// </summary>
public abstract class SubMenu
{
    /// <summary>
    /// Variables generales
    /// </summary>
    #region variables_generales
    private String nombre;
    private String clase;
    private String redirect;
    #endregion

    /// <summary>
    /// CONSTRUCTOR POR DEFECTO
    /// </summary>
    #region constructor_defecto
    public SubMenu(){
        
    }
    #endregion

    /// <summary>
    /// CONSTRUCTOR CON PARÁMETROS
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="clase"></param>
    /// <param name="redirect"></param>
    #region constructor_parámetros
    public SubMenu(String nombre,String clase,String redirect)
    {
        this.nombre = nombre;
        this.clase = clase;
        this.redirect = redirect;
    }
    #endregion

    /// <summary>
    /// VARIABLES SET/GET
    /// </summary>
    /// <returns></returns>
    #region variables_get_set
    public String getRedirect()
    {
        return redirect;
    }

    public void setRedirect(String redirect)
    {
        this.redirect = redirect;
    }

    public String getClase()
    {
        return clase;
    }

    public String getNombre()
    {
        return nombre;
    }

    public void setNombre(String nombre)
    {
        this.nombre = nombre;
    }

    public void setClase(String clase)
    {
        this.clase = clase;
    }
    #endregion

    /// <summary>
    /// VARIABLES ABSTRACTA MOSTRAR
    /// </summary>
    /// <returns></returns>}
    #region variable_mostrar
    public abstract String mostrar();
    #endregion
}