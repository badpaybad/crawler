using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Directory.CreateDirectory("C:/du/du/");
        var arr = new[] {"0","1","2","3","4"};

        Response.Write(string.Join("/",arr.Skip(0).Take(arr.Length-1).ToArray()));
    }
}