using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UValueCalc
{
    /* thic class is set up to faciliate writing data into an HTML file
     * for viewing in a web browser, e.g. Internet Explorer, Firefox, Chrome etc.*/
    public class Writing
    {
        public void WriteOneLine(string myStr, StreamWriter sw)
        {
            sw.Write("<p>");
            sw.Write(myStr);
            sw.Write("</p>\n");
        }

        public void WriteH1(string myStr, StreamWriter sw)
        {
            sw.Write("<h1>");
            sw.Write(myStr);
            sw.Write("</h1>\n");
        }

        public void WriteH2(string myStr, StreamWriter sw)
        {
            sw.Write("<h2>");
            sw.Write(myStr);
            sw.Write("</h2>\n");
        }

        public void WriteH3(string myStr, StreamWriter sw)
        {
            sw.Write("<h3>");
            sw.Write(myStr);
            sw.Write("</h3>\n");
        }

        public void OpenTable(StreamWriter sw)
        {
            sw.Write("<table>\n");
        }

        public void CloseTable(StreamWriter sw)
        {
            sw.Write("</table>\n");
        }

        public void OpenRow(StreamWriter sw)
        {
            sw.Write("<tr>\n");
        }

        public void CloseRow(StreamWriter sw)
        {
            sw.Write("</tr>\n");
        }

        public void WriteCell(string myStr, StreamWriter sw)
        {
            sw.Write("<td>");
            sw.Write(myStr);
            sw.Write("</td>\n");
        }
    }
}
