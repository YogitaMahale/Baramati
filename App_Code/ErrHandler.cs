using System;
using System.IO;
using System.Web;
using System.Data;
using System.Globalization;
using System.Configuration;

public class ErrHandler
{
	public ErrHandler()
	{		
	}

    public static void writeError(string msg, string stacktrace)
    {
        try
        {
            string path = string.Format("~/uploads/errorlog.txt");
            if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                File.Create(System.Web.HttpContext.Current.Server.MapPath(path));
            }
            using (StreamWriter w = File.AppendText(System.Web.HttpContext.Current.Server.MapPath(path)))
            {
                w.WriteLine("\r\nLog Entry : ");
                w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                w.WriteLine("Error Message :" + msg);
                w.WriteLine("\n Statck Trace: " + stacktrace);
                w.WriteLine("__________________________");
                w.Flush();
                w.Close();
            }
        }
        catch (Exception ex1)
        {
            HttpContext.Current.Response.Write("Exception:" + ex1.Message);
        }
    }
}
