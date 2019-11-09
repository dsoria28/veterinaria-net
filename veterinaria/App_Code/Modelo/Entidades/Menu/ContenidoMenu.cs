using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de ContenidoMenu
/// </summary>
public class ContenidoMenu
{
    /// <summary>
    /// Variable para contenido
    /// </summary>
    #region contenido
    List<string> contenido=new List<string>();
    #endregion

    /// <summary>
    /// CONSTRUCTOR POR DEFECTO
    /// </summary>
    #region método_constructor
    public ContenidoMenu(){

    }
    #endregion

    /// <summary>
    /// Método oara agregar y mostrar nivel
    /// </summary>
    /// <param name="header"></param>
    /// <param name="footer"></param>
    /// <param name="Contenido"></param>
    /// <returns></returns>
    #region AddMostrarNivel
    public String AddMostrarNivel(String header,String footer, String Contenido)
    {
        String nivel="";
        nivel += header +
                    Contenido +
                footer;

        return nivel;
    }
    #endregion
}