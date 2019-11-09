using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Nivel
/// </summary>
public class Nivel
{
    /// <summary>
    /// Variables generales
    /// </summary>
    #region declaración_variables
    public String headerNivel;
    public String footerNivel;
    public String contenidoNivel;
    public List<Nivel> hijos = new List<Nivel>();
    #endregion

    /// <summary>
    /// CONSTRUCTOR CON PARÁMETROS
    /// </summary>
    /// <param name="header"></param>
    /// <param name="footer"></param>
    /// <param name="nivel"></param>
    #region método_constructor
    public Nivel(String header,String footer,String nivel){
        this.headerNivel = header;
        this.footerNivel = footer;
        this.contenidoNivel = nivel;
    }
    #endregion

    /// <summary>
    /// MÉTODO PARA AGREGAR ELEMENTOS
    /// </summary>
    /// <param name="element"></param>
    #region agregar
    public void agregar(Nivel element)
    {
        hijos.Add(element);
    }
    #endregion

    /// <summary>
    /// MÉTODO PARA MOSTRAR NIVEL
    /// </summary>
    /// <returns></returns>
    #region mostrarNivel
    public string mostrarNivel()
    {
        string nivelR="";
        nivelR = headerNivel;

        if (contenidoNivel == "")
        {
            foreach(Nivel element in hijos)
            {
                nivelR += element.mostrarNivel();
            }
        }
        else {
            nivelR += contenidoNivel;
        }

        nivelR += footerNivel;

        return nivelR;
    }
    #endregion
}