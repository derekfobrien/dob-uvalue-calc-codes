using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UValueCalc
{
    public partial class Form1 : Form
    {
        // the class-wide values and objects
        Fabric myFabric = new Fabric();
        ListViewItem.ListViewSubItem SelectedLSI;
        int FabricCommand;
        string theFileName = "Untitled.html";

        // here, is the constructor (I think!)
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            ResizeControls();
            DoListView();
            saveFileDialog1.Filter = "HTML files|*.html";

            /* Fabric Command Codes
             *  0 = nothing
             *  1 = Add Layer
             *  2 = Remove Layer
             *  3 = Add Material
             *  4 = Remove Material
             * */
            FabricCommand = 0;
            
        }

        private void ResizeControls()
        {
            // re-size and re-shape the controls when the form is re-sized
            int BufferEdge = pictureBox1.Left;

            lisProperties.Left = ClientSize.Width - (BufferEdge + lisProperties.Width);

            cmdAddLayer.Left = lisProperties.Left;
            cmdAddMaterial.Left = lisProperties.Left;
            cmdRemoveLayer.Left = cmdAddLayer.Left + BufferEdge + cmdAddLayer.Width;
            cmdRemoveMaterial.Left = cmdRemoveLayer.Left;
            cmdEdit.Left = lisProperties.Left;
            cmdSave.Left = cmdRemoveLayer.Left;

            pictureBox1.Width = lisProperties.Left - (2 * BufferEdge);
            pictureBox1.Top = BufferEdge;
            pictureBox1.Height = ClientSize.Height - (2 * BufferEdge);

            label1.Left = lisProperties.Left;
            lblUValue.Left = lisProperties.Left;

            myFabric.Display(pictureBox1, Font);
        }


        private void DoListView()
        {
            // set up the cells in the list view
            int theWidth = lisProperties.Width;

            lisProperties.View = View.Details;
            lisProperties.GridLines = true;
            lisProperties.FullRowSelect = true;
            lisProperties.LabelEdit = true;

            // add the columns
            lisProperties.Columns.Add("Property", (int)(theWidth * 0.45));
            lisProperties.Columns.Add("Value", (int)(theWidth * 0.45));

            //add the properties
            ListViewItem item1 = new ListViewItem("Layer Number");
            item1.SubItems.Add("1");
            lisProperties.Items.Add(item1);
            lisProperties.Items[0].SubItems[0].BackColor = Color.FromArgb(200, 200, 200);

            ListViewItem item2 = new ListViewItem("Material Number");
            item2.SubItems.Add("1");
            lisProperties.Items.Add(item2);
            lisProperties.Items[1].SubItems[0].BackColor = Color.FromArgb(200, 200, 200);

            ListViewItem item3 = new ListViewItem("Layer Name");
            item3.SubItems.Add("Outer Leaf");
            lisProperties.Items.Add(item3);

            ListViewItem item4 = new ListViewItem("Layer Thickness");
            item4.SubItems.Add("100");
            lisProperties.Items.Add(item4);

            ListViewItem item5 = new ListViewItem("Layer Constant");
            item5.SubItems.Add("False");
            lisProperties.Items.Add(item5);

            ListViewItem item6 = new ListViewItem("Layer Resistance");
            item6.SubItems.Add("Internal Surface");
            lisProperties.Items.Add(item6);

            ListViewItem item7 = new ListViewItem("Material Name");
            item7.SubItems.Add("Blockwork");
            lisProperties.Items.Add(item7);
            lisProperties.Items[6].SubItems[0].BackColor = Color.FromArgb(180, 255, 225);

            ListViewItem item8 = new ListViewItem("Material Conductivity");
            item8.SubItems.Add("1.300");
            lisProperties.Items.Add(item8);
            lisProperties.Items[7].SubItems[0].BackColor = Color.FromArgb(180, 255, 225);

            ListViewItem item9 = new ListViewItem("Material Amount");
            item9.SubItems.Add("400");
            lisProperties.Items.Add(item9);
            lisProperties.Items[8].SubItems[0].BackColor = Color.FromArgb(180, 255, 225);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeControls();
        }

       
        private void lisProperties_MouseUp(object sender, MouseEventArgs e)
        {
            /* this function allows editing values in the list box, we use two controls
             * txtProperty and comConstant, which will be placed on top of the
             * cell in the list box, whose value we want to edit
             * For the "Layer Constant" value, which is either True or False, we
             * show comConstant, for all other values, we show txtProperty */
           
            ListViewHitTestInfo hti = lisProperties.HitTest(e.X, e.Y);
            SelectedLSI = hti.SubItem;
            int ColumnInLisProperties, RowInListProperties;

            if (SelectedLSI != null)
            {
                RowInListProperties = hti.Item.Index;
                ColumnInLisProperties = hti.Item.SubItems.IndexOf(SelectedLSI);
                if (ColumnInLisProperties == 0 || RowInListProperties < 2)
                    return;
            }
            else
                return;

            int border = 0;

            switch (lisProperties.BorderStyle)
            {
                case BorderStyle.FixedSingle:
                    border = 1;
                    break;
                case BorderStyle.Fixed3D:
                    border = 2;
                    break;
            }

            int CellWidth = SelectedLSI.Bounds.Width;
            int CellHeight = SelectedLSI.Bounds.Height;
            int CellLeft = border + lisProperties.Left + hti.SubItem.Bounds.Left;
            int CellTop = lisProperties.Top + hti.SubItem.Bounds.Top;
            // First Column
            if (hti.SubItem == hti.Item.SubItems[0])
                CellWidth = lisProperties.Columns[0].Width;

            // ComboBox for Layer Constant
            if (RowInListProperties == 4)
            {
                comConstant.Location = new Point(CellLeft, CellTop);
                comConstant.Size = new Size(CellWidth, CellHeight);
                comConstant.Visible = true;
                comConstant.BringToFront();
                comConstant.Text = hti.SubItem.Text;
                comConstant.Select();
                comConstant.SelectAll();
            }
            // Otherwise
            else
            {
                txtProperty.Location = new Point(CellLeft, CellTop);
                txtProperty.Size = new Size(CellWidth, CellHeight);
                txtProperty.Visible = true;
                txtProperty.BringToFront();
                txtProperty.Text = hti.SubItem.Text;
                txtProperty.Select();
                txtProperty.SelectAll();
            }
        }

        private void lisProperties_MouseDown(object sender, MouseEventArgs e)
        {
                HideTextEditor();
        }

        private void HideTextEditor()
        {
            // make both the txtProperty and comConstant controls invisible
            if (txtProperty.Visible)
            {
                txtProperty.Visible = false;
                if (SelectedLSI != null && txtProperty.Text != "")
                    SelectedLSI.Text = txtProperty.Text;
                SelectedLSI = null;
                txtProperty.Text = "";
            }
            else if (comConstant.Visible)
            {
                comConstant.Visible = false;
                SelectedLSI.Text = comConstant.Text;
            }
        }

        private void txtProperty_Leave(object sender, EventArgs e)
        {
            HideTextEditor();
        }

        private void txtProperty_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                HideTextEditor();
        }

        // add a layer
        private void cmdAddLayer_Click(object sender, EventArgs e)
        {
            if (FabricCommand == 1)
            {
                FabricCommand = 0;
                cmdAddLayer.BackColor = Color.FromArgb(224, 224, 224);
            }

            else
            {
                FabricCommand = 1;
                cmdAddLayer.BackColor = Color.FromArgb(180, 180, 180);
                cmdRemoveLayer.BackColor = Color.FromArgb(224, 224, 224);
                cmdAddMaterial.BackColor = Color.FromArgb(224, 224, 224);
                cmdRemoveMaterial.BackColor = Color.FromArgb(224, 224, 224);
            }
        }

        // remove a layer
        private void cmdRemoveLayer_Click(object sender, EventArgs e)
        {
            if (FabricCommand == 2)
            {
                FabricCommand = 0;
                cmdRemoveLayer.BackColor = Color.FromArgb(224, 224, 224);
            }
            else
            {
                FabricCommand = 2;
                cmdRemoveLayer.BackColor = Color.FromArgb(180, 180, 180);
                cmdAddLayer.BackColor = Color.FromArgb(224, 224, 224);
                cmdAddMaterial.BackColor = Color.FromArgb(224, 224, 224);
                cmdRemoveMaterial.BackColor = Color.FromArgb(224, 224, 224);
            }
        }

        // add a material
        private void cmdAddMaterial_Click(object sender, EventArgs e)
        {
            if (FabricCommand == 3)
            {
                FabricCommand = 0;
                cmdAddMaterial.BackColor = Color.FromArgb(224, 224, 224);
            }
            else
            {
                FabricCommand = 3;
                cmdAddMaterial.BackColor = Color.FromArgb(180, 180, 180);
                cmdAddLayer.BackColor = Color.FromArgb(224, 224, 224);
                cmdRemoveLayer.BackColor = Color.FromArgb(224, 224, 224);
                cmdRemoveMaterial.BackColor = Color.FromArgb(224, 224, 224);
            }
        }

        // remove a material
        private void cmdRemoveMaterial_Click(object sender, EventArgs e)
        {
            if (FabricCommand == 4)
            {
                FabricCommand = 0;
                cmdRemoveMaterial.BackColor = Color.FromArgb(224, 224, 224);
            }
            else
            {
                FabricCommand = 4;
                cmdRemoveMaterial.BackColor = Color.FromArgb(180, 180, 180);
                cmdAddLayer.BackColor = Color.FromArgb(224, 224, 224);
                cmdRemoveLayer.BackColor = Color.FromArgb(224, 224, 224);
                cmdAddMaterial.BackColor = Color.FromArgb(224, 224, 224);
            }
        }

     
        // this button will adjust the properties of the currently selected layer
        // and material to the values in the list box
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            int LayerNo, MaterialNo;

            LayerNo = int.Parse(lisProperties.Items[0].SubItems[1].Text);
            MaterialNo = int.Parse(lisProperties.Items[1].SubItems[1].Text);

            Layer lyr = (Layer)myFabric.Layers[LayerNo];
            Material mat = (Material)lyr.Materials[MaterialNo];

            mat.UpdateProperties(lisProperties);
            lyr.UpdateProperties(lisProperties);
            

            myFabric.Calculate(lblUValue);

            myFabric.Display(pictureBox1, Font);
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int eX, eY, NrMats, NrLyrs, PH, PW, LyrNo, MatNo;
            eX = e.X;
            eY = e.Y;
            PH = pictureBox1.Height;
            PW = pictureBox1.Width;

            NrLyrs = myFabric.Layers.Count;

            LyrNo = (int)Math.Floor((double)((eY - 10) * NrLyrs) / (PH - 20));
            Layer lyr = (Layer)myFabric.Layers[LyrNo];
            lisProperties.Items[0].SubItems[1].Text = LyrNo.ToString();

            NrMats = lyr.Materials.Count;

            MatNo = (int)Math.Floor((double)((eX - 10) * NrMats) / (PW - 20));
            Material mat = (Material)lyr.Materials[MatNo];
            lisProperties.Items[1].SubItems[1].Text = MatNo.ToString();

            switch (FabricCommand)
            {
                case 1: // add layer
                    myFabric.AddLayer();
                    myFabric.Calculate(lblUValue);
                    myFabric.Display(pictureBox1, Font);
                    break;
                case 2: // remove layer
                    myFabric.RemoveLayer(LyrNo);
                    myFabric.Calculate(lblUValue);
                    myFabric.Display(pictureBox1, Font);
                    break;
                case 3: // add material
                    lyr.AddMaterial();
                    myFabric.Calculate(lblUValue);
                    myFabric.Display(pictureBox1, Font);
                    break;
                case 4: // remove material
                    lyr.RemoveMaterial(MatNo);
                    myFabric.Calculate(lblUValue);
                    myFabric.Display(pictureBox1, Font);
                    break;
                case 0: // show values in table
                    lyr.PutValuesInTable(lisProperties);
                    mat.PutValuesInTable(lisProperties);
                    break;
            }
        }

        // writes an HTML file using the values of the fabric, layers and materials
        // to produce a step-by-step outline of how the final U-value was reached
        private void cmdSave_Click(object sender, EventArgs e)
        { 
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                theFileName = saveFileDialog1.FileName;

                using (StreamWriter str = new StreamWriter(theFileName))
                {
                    myFabric.WriteHTMLTitle(str);
                }       
            }
        }
    }
}
