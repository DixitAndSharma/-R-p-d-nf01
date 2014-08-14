using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for Cmn
/// </summary>
public class Cmn
{
    const Boolean UseCache = true;

    public Cmn()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string GetUnCompressed(byte[] Data, int Size)
    {
        if (Data == null)
            return string.Empty;
        MemoryStream ms = new MemoryStream(Data);
        GZipStream gz = null;
        try
        {

            gz = new GZipStream(ms, CompressionMode.Decompress);
            byte[] decompressedBuffer = new byte[Size];
            int DataLength = gz.Read(decompressedBuffer, 0, Size);
            using (MemoryStream msDec = new MemoryStream())
            {
                msDec.Write(decompressedBuffer, 0, DataLength);
                msDec.Position = 0;
                string s = new StreamReader(msDec).ReadToEnd();
                return s;
            }
        }
        catch
        {
            //return ex.Message;
        }
        finally
        {
            ms.Close();
            gz.Close();
        }

        return string.Empty;
    }

    public static string ReplaceForURL(string Name)
    {
        return (Name.Replace(" ", "").Replace("/", "").Replace("+", "").Replace("&", "").Replace("*", "").Replace(".", "").Replace("-", "")).ToLower();
    }

    public static DateTime MinDate = new DateTime(1900, 1, 1);
    public static DateTime MaxDate = new DateTime(2099, 1, 1);

    public static string GetEncode(Page pg)
    {
        string encodings = pg.Request.Headers.Get("Accept-Encoding");
        string encode = "no";

        if (encodings != null)
        {
            encodings = encodings.ToLower();
            if (encodings.Contains("gzip"))
            {
                pg.Response.AppendHeader("Content-Encoding", "gzip");
                encode = "gzip";
            }
            else if (encodings.Contains("deflate"))
            {
                pg.Response.AppendHeader("Content-Encoding", "deflate");
                encode = "deflate";
            }
        }

        return encode;
    }


    public static void WriteFile(string str, string FileName, string CompressionType)
    {
        byte[] buffer = System.Text.Encoding.ASCII.GetBytes(str);

        switch (CompressionType)
        {
            case "gzip":
                {
                    FileStream sw = new FileStream(FileName, FileMode.Create);
                    GZipStream gz = new GZipStream(sw, CompressionMode.Compress);
                    gz.Write(buffer, 0, buffer.Length);
                    gz.Close();
                    sw.Close();
                }
                break;
            case "deflate":
                {
                    FileStream sw = new FileStream(FileName, FileMode.Create);
                    DeflateStream dz = new DeflateStream(sw, CompressionMode.Compress);
                    dz.Write(buffer, 0, buffer.Length);
                    dz.Close();
                    sw.Close();
                }
                break;
            default:
                {
                    StreamWriter sw = new StreamWriter(FileName, false);
                    sw.Write(str);
                    sw.Close();
                }
                break;
        }

        File.SetCreationTime(FileName, DateTime.Now);
    }

    public static void WriteResponse(Page p, string Message, string Compression)
    {
        if (Compression == "no" || string.IsNullOrEmpty(Compression))
            p.Response.Write(Message);
        else
            p.Response.BinaryWrite(GetCompressed(Message, Compression));
    }

    public static byte[] GetCompressed(string str, string CompressionType = "gzip")
    {
        byte[] buffer = System.Text.Encoding.ASCII.GetBytes(str);
        MemoryStream ms = new MemoryStream();

        switch (CompressionType)
        {
            case "gzip":
                {
                    GZipStream gz = new GZipStream(ms, CompressionMode.Compress, true);
                    gz.Write(buffer, 0, buffer.Length);
                    gz.Close();
                }
                break;
            case "deflate":
                {
                    DeflateStream dz = new DeflateStream(ms, CompressionMode.Compress);
                    dz.Write(buffer, 0, buffer.Length);
                    dz.Close();
                }
                break;
        }

        byte[] compressedData = (byte[])ms.ToArray();
        return compressedData;

    }

    public static string CheckString(string text)
    {
        return text.Replace("'", "").Replace("\"", "").Replace("@", "").Replace("?", "").Replace("*", "");
    }

    public static void LogException(string ModuleName, string FunctionName, Exception ex)
    {
        if (HttpContext.Current.Application["ERROR_IDS"] == null)
        {
            HttpContext.Current.Application["ERROR_IDS"] = new Dictionary<string, int>();
            HttpContext.Current.Application["ERROR_LIST"] = new Dictionary<int, string>();
        }

        Dictionary<string, int> ErrorLog = (Dictionary<string, int>)HttpContext.Current.Application["ERROR_IDS"];
        if (!ErrorLog.ContainsKey(ModuleName + "_" + FunctionName))
        {
            ErrorLog.Add(ModuleName + "_" + FunctionName, ErrorLog.Count + 1);
        }

        //get the id of the error
        int i = ErrorLog[ModuleName + "_" + FunctionName];
        Dictionary<int, string> ErrorList = (Dictionary<int, string>)HttpContext.Current.Application["ERROR_LIST"];

        if (!ErrorList.ContainsKey(i))
            ErrorList.Add(i, "");

        ErrorList[i] = ModuleName + ":" + FunctionName + ":" + ex.Message + ":" + DateTime.Now.ToString();

    }

    public static string ValidateInput(string Data, int Length, Boolean CheckforValidDate, Boolean ConvertToUpper, Boolean CleanInput)
    {
        if (Length > 0)
            if (Data.Length > Length)
                Data = Data.Substring(0, Length);

        if (ConvertToUpper)
            Data = Data.ToUpper();

        if (CleanInput)
            Data = Data.Replace("'", "").Replace("%", "").Replace("*", "").Replace(" ", "");

        return Data;
    }


    public static DateTime GetIndiaTime()
    {
        return DateTime.Now.ToUniversalTime().AddHours(5).AddMinutes(30);
    }

    public static DateTime ToDate(object txt)
    {
        DateTime X;
        if (DateTime.TryParse(txt.ToString(), out X) == false)
            return Cmn.MinDate;

        return X;
    }

    public static string ToDateFormatMonth(object txt)
    {
        if (txt == null)
            return "";

        DateTime X;
        if (DateTime.TryParse(txt.ToString(), out X) == false)
            X = Cmn.MinDate;

        if (X == Cmn.MinDate)
            return "";

        return X.ToString("MMM-yy");
    }

    public static string ToDateFormatY2(object txt)
    {
        if (txt == null)
            return "";

        DateTime X;
        if (DateTime.TryParse(txt.ToString(), out X) == false)
            X = Cmn.MinDate;

        if (X == Cmn.MinDate)
            return "";

        return X.ToString("dd-MMM-yy");
    }

    public static string ToDateFormat(object txt)
    {
        if (txt == null)
            return "";

        DateTime X;
        if (DateTime.TryParse(txt.ToString(), out X) == false)
            X = Cmn.MinDate;

        if (X == Cmn.MinDate)
            return "";

        return X.ToString("dd-MMM-yyyy");
    }

    public static DateTime ToDateFormat24(object txt)
    {
        if (txt == null)
            return MinDate;

        DateTime X;
        if (DateTime.TryParse(txt.ToString(), out X) == false)
            X = Cmn.MinDate;

        if (X == Cmn.MinDate)
            return MinDate;

        return Cmn.ToDate(X.ToString("dd-MMM-yyyy HH:mm"));
    }

    public static decimal ToDec(TextBox txt)
    {
        decimal X;
        if (decimal.TryParse(txt.Text, out X) == false)
            return 0;

        return X;
    }

    public static decimal ToDec(string txt)
    {
        decimal X;
        if (decimal.TryParse(txt, out X) == false)
            return 0;

        return X;
    }

    public static double ToDbl(object txt)
    {
        double X;
        if (double.TryParse(txt.ToString(), out X) == false)
            return 0;

        return X;
    }

    public static int ToInt(TextBox txt)
    {
        int X;
        if (int.TryParse(txt.Text, out X) == false)
            return 0;

        return X;
    }

    public static int ToInt(string txt, int DefaultValue)
    {
        int X;
        if (int.TryParse(txt, out X) == false)
            return DefaultValue;

        return X;
    }

    public static int ToInt(object txt)
    {
        int X;
        if (int.TryParse(txt.ToString(), out X) == false)
            return 0;

        return X;
    }

    public static string ProperCase(string str)
    {
        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        TextInfo textInfo = cultureInfo.TextInfo;
        return textInfo.ToTitleCase(str.Trim().ToLower());
    }

    public static void ClearTextBoxes(Control parent)
    {
        foreach (Control ctl in parent.Controls)
        {
            if (ctl.GetType().ToString().Equals("System.Web.UI.WebControls.TextBox"))
                ((TextBox)ctl).Text = "";

            if (ctl.GetType().ToString().Equals("System.Web.UI.WebControls.CheckBox"))
                ((CheckBox)ctl).Checked = false;

            if (ctl.GetType().ToString().Equals("System.Web.UI.WebControls.DropDownList"))
                ((DropDownList)ctl).SelectedIndex = 0;

            if (ctl.Controls.Count > 0)
                ClearTextBoxes(ctl);
        }
    }

    public static void LogError(Exception ex = null, string Message = "")
    {
        string Filename = "";
        Filename = HttpContext.Current.Server.MapPath(@"~\Error\Company" + Global.CompanyID + ".txt");
        string Error = Cmn.GetIndiaTime().ToString("dd-MMM-yyyy HH.mm") + Environment.NewLine;
        if (!string.IsNullOrWhiteSpace(Message))
            Error += Message + Environment.NewLine;

        if (ex != null)
        {
            Error += ex.Message + Environment.NewLine;
            Error += ex.StackTrace + Environment.NewLine;

            //Error += ex.InnerException.Message !=null ? ex.InnerException.Message : "";
        }
        try
        {
            File.AppendAllText(Filename, Error);
        }
        catch { }
    }


    public static string ConvertNumberToWord(Int32 numberVal)
    {
        string[] powers = new string[] { "Thousand ", "Million ", "Billion " };
        string[] ones = new string[] { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        string[] tens = new string[] { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        string wordValue = "";
        if (numberVal == 0) return "Zero";
        if (numberVal < 0)
        {
            wordValue = "Negative ";
            numberVal = -numberVal;
        }
        long[] partStack = new long[] { 0, 0, 0, 0 };
        int partNdx = 0;
        while (numberVal > 0)
        {
            partStack[partNdx++] = numberVal % 1000;
            numberVal /= 1000;
        }
        for (int i = 3; i >= 0; i--)
        {
            long part = partStack[i];
            if (part >= 100)
            {
                wordValue += ones[part / 100 - 1] + " Hundred ";
                part %= 100;
            }
            if (part >= 20)
            {
                if ((part % 10) != 0) wordValue += tens[part / 10 - 2] + " " + ones[part % 10 - 1] + " ";
                else wordValue += tens[part / 10 - 2] + " ";
            }
            else if (part > 0) wordValue += ones[part - 1] + " ";
            if (part != 0 && i > 0) wordValue += powers[i - 1];
        }
        return wordValue;
    }

    public static double CalcDistance(double lat1, double lon1, double lat2, double lon2, char unit)
    {
        var theta = lon1 - lon2;
        var dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
        dist = Math.Acos(dist);
        dist = rad2deg(dist);
        dist = dist * 60 * 1.1515;
        if (unit == 'K')
        {
            dist = dist * 1.609344;
        }
        else if (unit == 'N')
        {
            dist = dist * 0.8684;
        }
        return (dist);
    }

    private static double deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }

    private static double rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }
}

