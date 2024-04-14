using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5
{
    public class InputData
    {
        // Матрица связей + вектор решений (для x1 + x2 = 0 : {1.0, 1.0, 0.0})
        public double[,] Ab { get; set; }

        // Вектор значений по потокам
        public double[] x0 { get; set; }

        // Вектор погрешностей потоков
        public double[] errors { get; set; }

        // Вектор имзеряемости потков.
        public byte[] I { get; set; }

        // Нижняя граница
        public double[] lb { get; set; }

        // Верхняя граница
        public double[] ub { get; set; }

        // Количество итераций
        public int iterCount { get; set; }

        public InputData()
        {

        }

        public InputData(double[,] _Ab,  double[] _x0, double[] _errors, byte[] _I, double[] _lb, double[] _ub, int _iterCount)
        {
            Ab = _Ab;
            x0 = _x0;
            errors = _errors;
            I = _I;
            lb = _lb;
            ub = _ub;
            iterCount = _iterCount;
        }
    }
}
