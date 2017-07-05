using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UValueCalc
{
    public class Layer
    {
        private string Name;
        public ArrayList Materials = new ArrayList();
        private double Thickness;
        private double Resistance;
        private bool isConstant;

        //constructor - name it "Unnamed Layer", set thickness to 100mm, create one material
        public Layer()
        {
            SetName("Unnamed Layer");
            SetThickness(0.1);
            Material M = new Material();
            Materials.Add(M);
            
        }

        //getters and setters
        public string GetName()
        {
            return Name;
        }

        public void SetName(string N)
        {
            Name = N;
        }

        public double GetThickness()
        {
            return Thickness;
        }

        public void SetThickness(double t)
        {
            Thickness = t;
            for (int i = 0; i < Materials.Count; i++)
            {
                Material mat = (Material)Materials[i];
                mat.SetThickness(Thickness);
            }
        }

        public double GetResistance()
        {
            return Resistance;
        }

        public void SetResistance(bool myConstant, double R)
        {
            double AmountsTotal, Res, Rm;

            if (myConstant)
            {
                Resistance = R;
            }
            else
            {
                //add up the amounts
                AmountsTotal = 0;
                Res = 0;

                // add up the amounts and then calculate the fractions of the materials
                for (int i = 0; i < Materials.Count; i++)
                {
                    Material Mat = (Material)Materials[i];
                    AmountsTotal = AmountsTotal + Mat.GetAmount();
                }

                // add up the resistances to get the resistance of the layer, to calculate the lower resistance

                for (int i = 0; i < Materials.Count; i++)
                {
                    Material Mat = (Material)Materials[i];
                    Mat.SetFractionFromTotal(AmountsTotal);

                    Rm = Mat.GetResistance();

                    if (Rm != 0)
                        Res = Res + (Mat.GetFraction() / Rm);
                }

                Resistance = 1 / Res;

            }
        }

        public bool GetConstant()
        {
            return isConstant;
        }

        public void SetConstant(bool k)
        {
            isConstant = k;
        }

        public void AddMaterial()
        {
            // put new material in array
            Material M = new Material();
            M.SetThickness(Thickness);

            Materials.Add(M);
            
        }

        public void RemoveMaterial(int n)
        {
            string myStr;
            DialogResult myResult;
            if(Materials.Count == 1)
            {
                MessageBox.Show("There is only one material in this layer. The material will not be deleted.");
            }
            else
            {
                myStr = string.Concat("This will delete Material No. ", n.ToString(), " Are you sure?");

                MessageBoxButtons myButtons = MessageBoxButtons.YesNo;

                myResult = MessageBox.Show(myStr, "Confirm Delete Layer", myButtons);

                if (myResult == System.Windows.Forms.DialogResult.Yes)
                {
                    Materials.RemoveAt(n);
                    SetResistance(false, 0);
                }
            }
        }

        // other methods

        // copy the properties of the selected layer into the list box
        public void PutValuesInTable(ListView lv)
        {
            lv.Items[2].SubItems[1].Text = Name;
            lv.Items[3].SubItems[1].Text = Thickness.ToString();
            if (isConstant)
                lv.Items[4].SubItems[1].Text = "True";
            else
                lv.Items[4].SubItems[1].Text = "False";
            lv.Items[5].SubItems[1].Text = Resistance.ToString("F3");
        }

        // edit the properties of the layer to the values currently in the list box
        public void UpdateProperties(ListView lv)
        {
            SetName(lv.Items[2].SubItems[1].Text);
            SetThickness(double.Parse(lv.Items[3].SubItems[1].Text));
            SetConstant(bool.Parse(lv.Items[4].SubItems[1].Text));
            SetResistance(isConstant, double.Parse(lv.Items[5].SubItems[1].Text));
        }
    }
}
