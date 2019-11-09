using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ItemMenu
/// </summary>
public class ItemMenu:SubMenu
{
    /// <summary>
    /// CONSTRUCTOR POR DEFECTO
    /// </summary>
    #region constructor_defecto
    public ItemMenu(){ 

    }
    #endregion

    /// <summary>
    /// MÉTODO CONSTRUCTOR CON PARÁMETROS
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="clase"></param>
    /// <param name="redirect"></param>
    #region constructor_parámetros
    public ItemMenu(String nombre,String clase,String redirect): base(nombre,clase,redirect)
    {

    }
    #endregion

    /// <summary>
    /// MÉTODO PARA MOSTRAR SUBMENÚ
    /// </summary>
    /// <returns></returns>
    #region mostrar
    public override String mostrar()
    {
        return "<li>" +
                    "<a href='"+getRedirect()+"'>"+
                        getNombre() + 
                    "</a>"+
                "</li>";
    }
    #endregion

}