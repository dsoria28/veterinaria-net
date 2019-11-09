using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Menu
/// </summary>
public class Menu: SubMenu
{
    /// <summary>
    /// Varaibles generales
    /// </summary>
    #region declaración_variables
    private List<SubMenu> hijos = new List<SubMenu>();
    public String nombre;
    public String clase;
    private bool estiloDrop;
    #endregion

    /// <summary>
    /// Método para agregar elementos
    /// </summary>
    /// <param name="element"></param>
    #region agrega
    public void agrega(SubMenu element)
    {
        hijos.Add(element);
    }
    #endregion

    /// <summary>
    /// Método para eliminar elementos
    /// </summary>
    /// <param name="element"></param>
    #region elimina
    public void elimina(SubMenu element)
    {
        hijos.Remove(element);
    }
    #endregion

    /// <summary>
    /// Método para obtener elementos
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    #region obtener
    public SubMenu obtener(int index)
    {
        if (hijos.Count > index)
        {
            return hijos[index];
        }else{
            return null;
        }
    }
    #endregion

    /// <summary>
    /// EstiloDrop SET y GET
    /// </summary>
    /// <param name="estiloDrop"></param>
    #region estiloDrop
    ///setEstiloDrop
    public void setEstiloDrop(bool estiloDrop)
    {
        this.estiloDrop = estiloDrop;
    }

    ///getEstiloDrop
    public String getEstiloDrop()
    {
        if (this.estiloDrop)
        {
            return " class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false' ";
        }else
            return "";
    }
    #endregion

    /// <summary>
    /// Método para agregar icono a Menú
    /// </summary>
    /// <returns></returns>
    #region getIconMenu
    public String getIconMenu()
    {
        if (this.estiloDrop)
        {
            return " <span class='caret'></span> ";
        }
        else
            return "";
    }
    #endregion

    /// <summary>
    /// Método para mostrar elementos
    /// </summary>
    /// <returns></returns>
    #region mostrar
    public override String mostrar()
    {
        String contenido = "";
        contenido = "<li class='"+getClase()+"'>" +
                        "<a href='"+getRedirect()+"' "+getEstiloDrop()+">" + 
                            getNombre() + 
                            getIconMenu()+
                        "</a>"+
                       "<ul class='dropdown-menu shadow2'>";

        foreach (SubMenu element in hijos)
        {
            contenido+=element.mostrar();
        }
        contenido += "</ul>" +
            "</li>";


        return contenido;
    }
    #endregion

    /// <summary>
    /// CONSTRUCTOR CON PARÁMETROS
    /// </summary>
    /// <param name="nombre"></param>
    /// <param name="clase"></param>
    /// <param name="redirect"></param>
    /// <param name="estiloDrop"></param>
    #region constructor_parémetros
    public Menu(String nombre,String clase,String redirect,bool estiloDrop):base(nombre,clase,redirect){
        this.nombre=nombre;
        this.clase = clase;
        this.estiloDrop = estiloDrop;
    }
    #endregion

    /// <summary>
    /// CONSTRCTOR POR DEFECTO
    /// </summary>
    #region constructor_defecto
    public Menu(){

    }
    #endregion

}