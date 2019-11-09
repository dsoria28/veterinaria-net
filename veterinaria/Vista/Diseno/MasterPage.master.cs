using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Vista_Diseno_MasterPage3 : System.Web.UI.MasterPage
{
    #region Page Load
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #endregion

    /*
    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        var html = new StringWriter();
        var render = new HtmlTextWriter(html);
        base.Render(render);

        writer.Write(html.ToString().Replace("id=\"ctl00_contend_","id=\""));
        writer.Write(html.ToString().Replace("id=\"contend_","id=\""));
    }
    */
}
