using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

/// <summary>
/// Descripción breve de Email
/// </summary>
public class Email
{
    private String nombrePerfil; //--variable que recupera el nombre del perfil que se mostrara en el nombre del correo
    private String correoPerfil; //--variable que recupera el correo que se utilizara para mandar el correo
    private String hostPerfil; //--variable que recupera el servidor de correo saliente
    private String puertoPerfil; //--variable que recupera el numero de puerto por el cual sale el correo
    private String passPerfil; //--variable que recupera el password del correo
    private bool sslPerfil; //--variable que recupera si aplica conexion ssl
    private bool credencialesPrefil; //--variable que recupera el nombre del perfil que se mostrara en el nombre del correo

    MailMessage _Correo;
    Conexion con;
    List<String> listaRegistros;
    SmtpClient _Smtp;
    /// <summary>
	/// Contructor
	/// </summary>
    public Email(bool html,MailPriority prioridad,String subject){
        //instancia a objeto Correo
        _Correo = new MailMessage();
        _Correo.IsBodyHtml = html;
        _Correo.Priority = prioridad;
        _Correo.Subject = subject;

        recuperaInfoCorreo();
    }


    ///metodo para ingresar lista destinos de correos
    public void _AddTo(String destinos)
    {
        ///se recupera lista de correos en arreglo
        string[] correos = destinos.Split(';');
        //--si es mayor se recorreo el arreglo
        if (correos.Length > 0)
        {
            ///se recorre lista de correos para agregar el correo
            for (int i = 0; i < correos.Length; i++)
            {
                _Correo.To.Add(new MailAddress(correos[i].ToString()));
            }
        }
        
    }

    //metodo para ingresar destinos en copia
    public void _AddCC(String destinos)
    {
        ///se recupera lista de correos en arreglo
        string[] correos = destinos.Split(';');
        //--si es mayor se recorreo el arreglo
        if (correos.Length > 0)
        {
            ///se recorre lista de correos para agregar el correo
            for (int i = 0; i < correos.Length; i++)
            {
                _Correo.CC.Add(new MailAddress(correos[i].ToString()));
            }
        }
    }

    //metodo para ingresar destinos con Copia Oculta
    public void _AddBCC(String destinos)
    {
        ///se recupera lista de correos en arreglo
        string[] correos = destinos.Split(';');
        //--si es mayor se recorreo el arreglo
        if (correos.Length > 0)
        {
            ///se recorre lista de correos para agregar el correo
            for (int i = 0; i < correos.Length; i++)
            {
                _Correo.Bcc.Add(new MailAddress(correos[i].ToString()));
            }
        }
    }

    //Metodo para ingresar el cuerpo del correo
    public void _AddBody(String cuerpoCorreo)
    {
        _Correo.Body = cuerpoCorreo;
    }

    //Metodo agregar adjunto
    public void _AddAttachment(String adjunto)
    {
        Attachment att = new Attachment(adjunto);
        _Correo.Attachments.Add(att);
    }

    //MEtodo para hacer el envio del correo
    public string sendMail() { 
        try{
            
            _Smtp.Send(_Correo);
            _Correo.Dispose();
            return "1";
        }
        catch (Exception ex)
        {
            return "Error enviando correo electrónico: " + ex.Message;
        }   
    }


    /// <summary>
    /// recupera informacion del correo
    /// </summary>
    private void recuperaInfoCorreo()
    {
        con = new Conexion();
        string query = "select top 1sNombrePerfil,sCorreo,sHost,sPuerto,iSSL,iCredenciales,sPass from ct_PerfilesCorreos where iEstado=1";
        
        listaRegistros = con.ejecutarConsultaRegistroMultiples(query);

        Security secCorreo, secHost, secPuerto, secPass;

            if (listaRegistros.Count > 1)
            {

                #region contenido
                for (int i = 1; i < listaRegistros.Count; i++)
                {
                    if (i == 1)
                    {
                        nombrePerfil = listaRegistros[i].ToString();
                    }
                    else if (i == 2)
                    {
                        secCorreo = new Security(listaRegistros[i]);
                        correoPerfil = secCorreo.DesEncriptar();
                    }
                    else if (i == 3)
                    {
                        secHost = new Security(listaRegistros[i]);
                        hostPerfil = secHost.DesEncriptar();
                    }
                    else if (i == 4)
                    {
                        secPuerto = new Security(listaRegistros[i]);
                        puertoPerfil = secPuerto.DesEncriptar();
                    }
                    else if (i == 5)
                    {
                        sslPerfil = bool.Parse(listaRegistros[i]);
                    }
                    else if (i == 6)
                    {
                        credencialesPrefil = bool.Parse(listaRegistros[i]);
                    }
                    else if (i == 7)
                    {
                        secPass = new Security(listaRegistros[i]);
                        passPerfil = secPass.DesEncriptar();
                    }
                }
                #endregion

                _Correo.From = new MailAddress(correoPerfil, nombrePerfil);
                _Smtp = new SmtpClient();

                _Smtp.Host = hostPerfil;
                _Smtp.Port = int.Parse(puertoPerfil);
                _Smtp.EnableSsl = sslPerfil;
                _Smtp.UseDefaultCredentials = credencialesPrefil;
                _Smtp.Credentials = new NetworkCredential(correoPerfil, passPerfil);

            }
        }
        
    
}