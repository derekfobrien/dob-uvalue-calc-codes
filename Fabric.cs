using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UValueCalc
{
    class Fabric
    {
        public ArrayList Layers = new ArrayList();
        public ArrayList Paths = new ArrayList();

        private string Name;
        private double FabricResistance;
        private double UpperResistance;
        private double LowerResistance;
        private double UValue;
        private int NrPaths;

        // constructor
        public Fabric()
        { 
            Layer lay = new Layer();

            Layers.Add(lay);
            Name = "Unnamed Fabric";


        }

        // getter and setter methods
        public string GetName()
        {
            return Name;
        }


        public void SetName(string myStr)
        {
            Name = myStr;
        }

        // other methods
        public void AddLayer()
        {
            Layer lay = new Layer();
            Layers.Add(lay);
        }

        public void RemoveLayer(int n)
        {
            string myStr;
            DialogResult myResult;
            if (Layers.Count == 1)
            {
                MessageBox.Show("There is only one layer. It will not be deleted.");
            }
            else
            {
                myStr = string.Concat("This will delete Layer No. ", n.ToString(), " Are you sure?");

                MessageBoxButtons myButtons = MessageBoxButtons.YesNo;

                myResult = MessageBox.Show(myStr, "Confirm Delete Layer", myButtons);

                if (myResult == System.Windows.Forms.DialogResult.Yes)
                {
                    Layers.RemoveAt(n);
                }
            }
        }

        // Display the fabric on the picturebox
        public void Display(PictureBox pb, Font f)
        {
            int PW, PH;
            PW = pb.Width;
            PH = pb.Height;
            int posX1, posY1, posX2, posY2, posX;
            string myStr;

            // The Graphics object
            Graphics g = pb.CreateGraphics();
            SolidBrush b1 = new SolidBrush(Color.FromArgb(210, 240, 255));
            Brush brushBlack = Brushes.Black;
            Pen penBlack = new Pen(Color.Black);
            // the font will be the same as the form font

            // clear the picturebox
            g.Clear(Color.White);

            // Draw the fill and outline
            g.FillRectangle(b1, 10, 10, PW - 20, PH - 20);
            g.DrawRectangle(penBlack, 10, 10, PW - 20, PH - 20);

            // Draw the horizontal lines between the Layers
            for (int i = 0; i < Layers.Count; i++)
            {
                Layer theLayer = (Layer)Layers[i];

                posX1 = 10;
                posX2 = PW - 10;
                posY1 = 10 + ((PH - 20) * i / Layers.Count);
                posY2 = 10 + ((PH - 20) * (i + 1) / Layers.Count);

                if (i > 0)
                    g.DrawLine(penBlack, posX1, posY1, posX2, posY1);

                //draw the vertical lines representing materials within the layer
                for (int j = 0; j < theLayer.Materials.Count; j++)
                {
                    posX = 10 + ((PW - 20) * j / theLayer.Materials.Count);
                    if (j > 0)
                        g.DrawLine(penBlack, posX, posY1, posX, posY2);

                    // write in the details - Layer Name, Thickness, Material Name, Conductivity
                    Material mat = (Material)theLayer.Materials[j];

                    // layer name
                    myStr = theLayer.GetName();
                    g.DrawString(myStr, f, brushBlack, posX + 5, posY1 + 4);

                    // thickness
                    myStr = string.Concat("t= ", theLayer.GetThickness().ToString(), " m");
                    g.DrawString(myStr, f, brushBlack, posX + 5, posY1 + 18);

                    // if resistance is constant
                    if (theLayer.GetConstant())
                    {
                        myStr = "Constant Resistance";
                        g.DrawString(myStr, f, brushBlack, posX + 5, posY1 + 32);

                        // resistance
                        myStr = string.Concat("R= ", theLayer.GetResistance().ToString("F3"), " m2.K/W");
                        g.DrawString(myStr, f, brushBlack, posX + 5, posY1 + 46);
                    }
                    else // resistance is not constant
                    {
                        // name
                        myStr = mat.GetName();
                        g.DrawString(myStr, f, brushBlack, posX + 5, posY1 + 32);
                        
                        // conductivity of material
                        myStr = string.Concat("k= ", mat.GetConductivity().ToString(), " W/m.K");
                        g.DrawString(myStr, f, brushBlack, posX + 5, posY1 + 46);

                        // resistance of material
                        myStr = string.Concat("Rm= ", mat.GetResistance().ToString("F3"), " m2K/W");
                        g.DrawString(myStr, f, brushBlack, posX + 5, posY1 + 60);

                        // resistance of layer
                        myStr = string.Concat("R= ", theLayer.GetResistance().ToString("F3"), " m2K/W");
                        g.DrawString(myStr, f, brushBlack, posX + 5, posY1 + 74);
                    }
                }
            }
            g.Dispose();
        }

        // Calculate the U-value
        public void Calculate(Label lbl)
        {
            double FiRi, Fpath, Rpath;
            int n, r;
            string myStr;

            // to keep track of the material numbers in each layer
            ArrayList MatlCounts = new ArrayList();

            // number of possible paths - the product of all the numbers of materials in the layers
            Paths.Clear();

            //calculate the number of paths
            NrPaths = 1;
            for (int i = 0; i < Layers.Count; i++)
            {
                Layer lyr = (Layer)Layers[i];
                NrPaths = NrPaths * lyr.Materials.Count;
            }

            // initialise the various values
            FabricResistance = 0;
            UValue = 0;
            n = 0;
            UpperResistance = 0;
            LowerResistance = 0;
            FiRi = 0;

            //populate the MatlCounts array
            for (int i = 0; i < Layers.Count; i++)
            {
                // each element of the MatlCounts array will hold a number between 0 and the number of materials in each layer less 1
                MatlCounts.Add(n);
            }

            // if the fabric does not have any thermal bridging
            if (NrPaths == 1)
            {
                for (int i = 0; i < Layers.Count; i++)
                {
                    Layer lyr = (Layer)Layers[i];
                    FabricResistance = FabricResistance + lyr.GetResistance();
                }
            }
            else // there is thermal bridging
            {
                // upper resistance
                for (int i = 0; i < NrPaths; i++)
                {
                    myStr = "";
                    n = i;
                    Path pat = new Path();

                    // here, we calculate which material is to be evaluated in each layer
                    for (int j = 0; j < Layers.Count; j++)
                    {
                        Layer lyr = (Layer)Layers[j];
                        r = n % lyr.Materials.Count;

                        MatlCounts[j] = r;
                        n = (n - r) / lyr.Materials.Count;
                    }

                    Fpath = 1;
                    Rpath = 0;

                    // here, we add up the resistances in the layers
                    for (int j = 0; j < Layers.Count; j++)
                    {
                        n = (int)MatlCounts[j];
                        myStr = string.Concat(n.ToString(), myStr);

                        Layer lyr = (Layer)Layers[j];
                        Material mat = (Material)lyr.Materials[n];

                        if (lyr.GetConstant())
                            mat.SetResistanceConstant(lyr);

                        pat.AddMaterial(mat);
                    }

                    // here, we add up the fration-to-resistance values for each path
                    Fpath = pat.GetFraction();
                    Rpath = pat.GetResistance();
                    Paths.Add(pat);
                    FiRi = FiRi + (Fpath / Rpath);


                }

                UpperResistance = 1 / FiRi;

                //lower resistance

                for (int i = 0; i < Layers.Count; i++)
                {
                    Layer lyr = (Layer)Layers[i];
                    LowerResistance = LowerResistance + lyr.GetResistance();
                }
                // Here we get the average of the lower and upper resistances, to get the overall resistance#
                // the reciprocall of which will be the U-value
                FabricResistance = (LowerResistance + UpperResistance) / 2;
            }

            // calculate the U-value - the reciprocal of the resistance
            if (FabricResistance != 0)
                UValue = 1 / FabricResistance;

            // display it!
            myStr = UValue.ToString("F3");
            lbl.Text = myStr;

        }

        // HTML
        public void WriteHTMLTitle(StreamWriter sw)
        {
            string myStr;
            Writing wr = new Writing();

            //start of file
            sw.Write("<html>\n");
            sw.Write("<head>\n");
            sw.Write("<title>");

            myStr = string.Concat("Calculation of U-value for ", Name);

            sw.Write(myStr);
            sw.Write("</title>\n");
            // style

            sw.Write("<style type = \"text/css\">");
            sw.Write("td {border: rgb(63, 148, 219) 1px solid; padding: 5px;}");
            sw.Write("table {border - collapse:collapse;}");
            sw.Write("</style>");

            sw.Write("</head>\n");

            sw.Write("<body>\n");
            wr.WriteH1(myStr, sw);

            if (NrPaths == 1) // the fabric has no bridged layers
            {
                //heading of table
                wr.OpenTable(sw);
                wr.OpenRow(sw);
                wr.WriteCell("Layer Name", sw);
                wr.WriteCell("Material Name", sw);
                wr.WriteCell("Constant Resistance", sw);
                wr.WriteCell("Thickness", sw);
                wr.WriteCell("Conductivity", sw);
                wr.WriteCell("Resistance", sw);
                wr.CloseRow(sw);

                for (int i = 0; i < Layers.Count; i++)
                {
                    Layer lyr = (Layer)Layers[i];
                    wr.OpenRow(sw);
                    wr.WriteCell(lyr.GetName(), sw); //layer name
                    if (lyr.GetConstant())
                    {
                        Material mat = (Material)lyr.Materials[0];
                        wr.WriteCell("Constant Layer", sw); // material name
                        wr.WriteCell("Yes", sw); // constant?
                        wr.WriteCell(lyr.GetThickness().ToString("F3"), sw); // thickness
                        wr.WriteCell("N/A", sw); // conductivity
                        wr.WriteCell(lyr.GetResistance().ToString("F3"), sw); // resistance
                    }
                    else
                    {
                        Material mat = (Material)lyr.Materials[0];
                        wr.WriteCell(mat.GetName(), sw); // material name
                        wr.WriteCell("No", sw); // constant?
                        wr.WriteCell(lyr.GetThickness().ToString("F3"), sw); // thickness
                        wr.WriteCell(mat.GetConductivity().ToString("F3"), sw); // conductivity
                        wr.WriteCell(lyr.GetResistance().ToString("F3"), sw); // resistance
                    }
                    wr.CloseRow(sw);
                }
                // Row at the bottom, to show the total resistance
                wr.OpenRow(sw);
                wr.WriteCell("&nbsp;", sw);
                wr.WriteCell("&nbsp;", sw);
                wr.WriteCell("&nbsp;", sw);
                wr.WriteCell("&nbsp;", sw);
                wr.WriteCell("Total Resistance", sw);
                wr.WriteCell(FabricResistance.ToString("F3"), sw);
                wr.CloseRow(sw);
                wr.CloseTable(sw);

                // show U-value
                myStr = string.Concat("<p>U-value: ", UValue.ToString("F3"), "</p>");
                wr.WriteOneLine(myStr, sw);
            }
            else // here, there are bridged layers
            {
                // calculating the upper resistance
                wr.WriteH2("Upper Resistance", sw);
                for (int i = 0; i < Paths.Count; i++)
                {
                    // Heading
                    Path pat = (Path)Paths[i];
                    myStr = string.Concat("Path No. ", (i + 1).ToString(), ", Fraction: ", pat.GetFraction().ToString("F3"));
                    wr.WriteH3(myStr, sw);
                    // table - heading
                    wr.OpenTable(sw);
                    wr.OpenRow(sw);
                    wr.WriteCell("Material Name", sw);
                    wr.WriteCell("Constant Resistance", sw);
                    wr.WriteCell("Thickness", sw);
                    wr.WriteCell("Conductivity", sw);
                    wr.WriteCell("Resistance", sw);
                    wr.CloseRow(sw);
                    // table - individual rows
                    for (int j = 0; j < pat.Materials.Count; j++)
                    {
                        Material mat = (Material)pat.Materials[j];
                        wr.OpenRow(sw);
                        wr.WriteCell(mat.GetName(), sw);
                        if (mat.GetConstant())
                        {
                            wr.WriteCell("Yes", sw);
                            wr.WriteCell(mat.GetThickness().ToString("F3"), sw);
                            wr.WriteCell("N/A", sw);
                        }
                        else
                        {
                            wr.WriteCell("No", sw);
                            wr.WriteCell(mat.GetThickness().ToString("F3"), sw);
                            wr.WriteCell(mat.GetConductivity().ToString("F3"), sw);
                        }
                        wr.WriteCell(mat.GetResistance().ToString("F3"), sw);
                        wr.CloseRow(sw);
                    }
                    // final row
                    wr.OpenRow(sw);
                    wr.WriteCell("&nbsp;", sw);
                    wr.WriteCell("&nbsp;", sw);
                    wr.WriteCell("&nbsp;", sw);
                    wr.WriteCell("Total Resistance", sw);
                    wr.WriteCell(pat.GetResistance().ToString("F3"), sw);
                    wr.CloseRow(sw);
                    wr.CloseTable(sw);
                }
                // calculating the lower resistance
                wr.WriteH2("Lower Resistance", sw);

                // the table
                wr.OpenTable(sw);
                wr.OpenRow(sw);
                wr.WriteCell("Layer Name", sw);
                wr.WriteCell("Material Name", sw);
                wr.WriteCell("Constant Resistance", sw);
                wr.WriteCell("Thickness", sw);
                wr.WriteCell("Conductivity", sw);
                wr.WriteCell("Material Resistance", sw);
                wr.WriteCell("Layer Resistance", sw);
                wr.CloseRow(sw);

                for (int i = 0; i < Layers.Count; i++)
                {
                    Layer lyr = (Layer)Layers[i];
                    if (lyr.Materials.Count > 1)
                    {
                        for (int j = 0; j < lyr.Materials.Count; j++)
                        {
                            Material mat = (Material)lyr.Materials[j];
                            wr.OpenRow(sw);
                            wr.WriteCell(lyr.GetName(), sw); // layer name
                            wr.WriteCell(mat.GetName(), sw); // material name
                            wr.WriteCell("No", sw); // constant?
                            wr.WriteCell(lyr.GetThickness().ToString("F3"), sw); // thickness
                            wr.WriteCell(mat.GetConductivity().ToString("F3"), sw); // conductivity
                            wr.WriteCell(mat.GetResistance().ToString("F3"), sw); // material resistance
                            wr.WriteCell("&nbsp;", sw); // layer resistance
                            wr.CloseRow(sw);
                        }
                        wr.OpenRow(sw);
                        wr.WriteCell("&nbsp;", sw); // layer name
                        wr.WriteCell("&nbsp;", sw); // material name
                        wr.WriteCell("&nbsp;", sw); // constant?
                        wr.WriteCell("&nbsp;", sw); // thickness
                        wr.WriteCell("&nbsp;", sw); // conductivity
                        wr.WriteCell("&nbsp;", sw); // material resistance
                        wr.WriteCell(lyr.GetResistance().ToString("F3"), sw); // layer resistance
                        wr.CloseRow(sw);
                    }
                    else // only one material
                    {
                        Material mat = (Material)lyr.Materials[0];
                        wr.OpenRow(sw);
                        if (lyr.GetConstant())
                        {
                            wr.WriteCell(lyr.GetName(), sw); // layer name
                            wr.WriteCell("Constant Layer", sw); // material name
                            wr.WriteCell("Yes", sw); // constant?
                            wr.WriteCell(lyr.GetThickness().ToString("F3"), sw); // thickness
                            wr.WriteCell("N/A", sw); // conductivity
                            wr.WriteCell("N/A", sw); // material resistance
                            wr.WriteCell(lyr.GetResistance().ToString("F3"), sw); // layer resistance
                        }
                        else
                        {
                            wr.WriteCell(lyr.GetName(), sw);
                            wr.WriteCell(mat.GetName(), sw);
                            wr.WriteCell("No", sw);
                            wr.WriteCell(lyr.GetThickness().ToString("F3"), sw);
                            wr.WriteCell(mat.GetConductivity().ToString("F3"), sw);
                            wr.WriteCell(mat.GetResistance().ToString("F3"), sw);
                            wr.WriteCell(lyr.GetResistance().ToString("F3"), sw);
                        }
                        wr.CloseRow(sw);
                    }
                }

                // bottom row
                wr.CloseTable(sw);

                // calculating the final U-value
                wr.WriteOneLine(string.Concat("Upper Resistance: ", UpperResistance.ToString("F3")), sw);
                wr.WriteOneLine(string.Concat("Lower Resistance: ", LowerResistance.ToString("F3")), sw);
            }


            // closing tags on the HTML
            wr.WriteH3(string.Concat("U-Value: ", UValue.ToString("F3")), sw);
            sw.Write("</body>\n");
            sw.Write("</html>");
        }
    }
}
