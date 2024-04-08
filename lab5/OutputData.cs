using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class OutputData
    {
        // Сбалансированные значения потоков
        public double[] x { get; set; }

        public OutputData()
        {

        }

        public OutputData(double[] _x)
        {
            x = _x;
        }
    }
}
