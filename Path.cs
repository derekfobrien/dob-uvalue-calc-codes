using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UValueCalc
{
    /* this class is used for the calculation of the "upper resistance" of a fabric
     * with bridged layers, where there is more than one material in one or more 
     * layers, each path is a combination of materials from the outer layer to
     * the inner layer */
    
    public class Path
    {
        public double Fraction;
        public double Resistance;
        public ArrayList Materials = new ArrayList();

        // getter and setters
        public double GetFraction()
        {
            return Fraction;
        }

        /* the fraction of the path is the product of the fractions of each
         * of the materials traversed across the fabric */
        public void SetFraction()
        {
            double f;

            f = 1;
            for (int i = 0; i < Materials.Count; i++)
            {
                Material m = (Material)Materials[i];
                f = f * m.GetFraction();
            }

            Fraction = f;
        }

        public double GetResistance()
        {
            return Resistance;
        }

        // a new material to the path
        public void AddMaterial(Material mat)
        {
            Materials.Add(mat);
            SetFraction();
            SetResistance();
        }

        // the sum of the resistances of each of the materials
        private void SetResistance()
        {
            Resistance = 0;
            for (int i = 0; i < Materials.Count; i++)
            {
                Material mat = (Material)Materials[i];
                Resistance = Resistance + mat.GetResistance();
            }
        }


    }
}
