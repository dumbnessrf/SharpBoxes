using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SharpBoxes.Helpers;

public static class XMLHelper
{
    public static string FindValueByKeyInXML(string key, string xmlPath)
    {
        string value = "";
        XDocument xDoc = XDocument.Load(xmlPath);
        var a = xDoc.Nodes();
        foreach (var item in xDoc.Descendants())
        {
            if (item.Name == key)
            {
                value = item.Value;
                break;
            }
        }

        return value;
    }

    public static void UpdateValueByKeyInXML(string value, string key, string xmlPath)
    {
        XDocument xDoc = XDocument.Load(xmlPath);
        var a = xDoc.Nodes();
        foreach (var item in xDoc.Descendants())
        {
            if (item.Name == key)
            {
                item.Value = value;
            }
        }
        xDoc.Save(xmlPath);
    }
}
