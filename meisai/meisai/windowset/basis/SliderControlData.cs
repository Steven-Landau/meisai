using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meisai.windowset.basis
{
    public abstract class SliderControlData
    {
        public String showName = "";
        public double InitLocation = 0;
        public virtual String setValue(double ratio)
        {
            return "";
        }
    }
}
