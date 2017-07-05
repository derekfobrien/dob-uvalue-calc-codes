using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UValueCalc
{
    public class Material
    {
        private string Name;
        private double Conductivity;
        private double Amount;
        private double Fraction;
        private double Thickness;
        private double Resistance;
        private bool isConstant;

        // constructor

        public Material ()
        {
            SetName("Unnamed Material");
            SetConductivity(0.9);
            SetAmount(1);
            SetFraction(1);
            SetConstant(false);
        }

        // getters and setters
        public string GetName()
        {
            return Name;
        }

        public void SetName(string N)
        {
            Name = N;
        }

        public double GetConductivity()
        {
            return Conductivity;
        }

        public void SetConductivity(double k)
        {
            Conductivity = k;
            SetResistance();
        }

        public double GetAmount()
        {
            return Amount;
        }

        public void SetAmount(double A)
        {
            Amount = A;
        }
        
        public double GetThickness()
        {
            return Thickness;
        }

        public void SetThickness(double t)
        {
            Thickness = t;
            SetResistance();
        }

        public bool GetConstant()
        {
            return isConstant;
        }

        public void SetConstant(bool x)
        {
            isConstant = x;
        }

        public double GetFraction()
        {
            return Fraction;
        }

        public void SetFraction(double F)
        {
            Fraction = F;
        }

        // there is a method in the Layer class which gets the sum of the Amounts of each of the
        // Material objects, and passes it to this method, which then calculates the fraction value
        // (between 0 and 1)
        public void SetFractionFromTotal(double F)
        {
            Fraction = Amount / F;
        }

        // copy the properties of the selected material into the list box
        public void PutValuesInTable(ListView lv)
        {
            lv.Items[6].SubItems[1].Text = Name;
            lv.Items[7].SubItems[1].Text = Conductivity.ToString();
            lv.Items[8].SubItems[1].Text = Amount.ToString();
        }

        // edit the properties of the material to the values currently in the list box
        public void UpdateProperties(ListView lv)
        {
            SetName(lv.Items[6].SubItems[1].Text);
            SetConductivity(double.Parse(lv.Items[7].SubItems[1].Text));
            SetAmount(double.Parse(lv.Items[8].SubItems[1].Text));
        }

        public double GetResistance()
        {
            return Resistance;
        }

        // set the Resistance property where the resistance is to be calculated from the thickness and conductivity
        private void SetResistance()
        {
            isConstant = false;
            if (Conductivity == 0)
                Resistance = 0;
            else
                Resistance = Thickness / Conductivity;
        }

        // set the Resistance property where the resistance is constant
        public void SetResistanceConstant(Layer lyr)
        {
            Resistance = lyr.GetResistance();
            isConstant = true;
        }
    }
}
