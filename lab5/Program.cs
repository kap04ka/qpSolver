namespace lab5
{
    public class AlglibDemo
    {
        public static int Main(string[] args)
        {
            InputData inputData;
            OutputData outputData;

            Console.WriteLine("Начало");
            inputData = DataSetting();
            outputData = Solver(inputData);

            Console.WriteLine("Ожидается:");
            Console.WriteLine("10.056	3.014	7.041	1.982	5.059	4.067	0.992");

            for(int i = 0; i < outputData.x.GetLength(0); i++)
            {
                Console.Write(Math.Round(outputData.x[i], 3));
                Console.Write('\t');
            }

            Console.ReadKey();
            return 0;
        }

        private static InputData DataSetting()
        {
            int iterCount = 200;

            double[,] Ab =
            {
                {1,  -1,   -1,    0,   0,  0,    0,    0}, 
                {0,   0,    1,    -1, -1,  0,    0,    0}, 
                {0,   0,    0,    0,   1,  -1,  -1,    0},
            };

            double[] x0 =
            {
                10.005,
                3.033,
                6.831,
                1.985,
                5.093,
                4.057,
                0.991
            };

            double[] errors =
            {
                0.200,
                0.121,
                0.683,
                0.040,
                0.102,
                0.081,
                0.020
            };

            byte[] I =
            {
                1,
                1,
                1,
                1,
                1,
                1,
                1
            };

            double[] lb =
            {
                0,
                0,
                0,
                0,
                0,
                0,
                0
            };

            double[] ub =
            {
                10000,
                10000,
                10000,
                10000,
                10000,
                10000,
                10000
            };
            Console.WriteLine("Данные заданы");
            return new InputData(Ab, x0, errors, I, lb, ub, iterCount);
        }

        private static OutputData Solver(InputData inputData)
        {
            // решение
            double[] x = { };

            // Получение размерности
            int n = inputData.errors.GetLength(0);
            int m = inputData.Ab.GetLength(0);

            // Формирование матрицы H = W * I, W = 1/error[i]^2
            double[,] H = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                    {
                        H[i, j] = inputData.I[i] / Math.Pow(inputData.errors[i], 2);
                    }

                    else
                    {
                        H[i, j] = 0;
                    }
                }
            }
            for (int iter = 0; iter < inputData.iterCount; iter++)
            {
                // Формирование вектора d = -H * x0
                double[] d = new double[n];

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        d[i] += -H[i, j] * inputData.x0[j];
                    }
                }

                try
                {
                    // Переменные для алглиба
                    double[] s = new double[n]; // == 1
                    int[] ct = new int[m];// == 0
                    bool isupper = true;

                    for (int i = 0; i < n; i++) s[i] = 1;
                    for (int i = 0; i < m; i++) ct[i] = 0;


                    alglib.minqpstate state;
                    alglib.minqpreport rep;

                    //create solver
                    alglib.minqpcreate(n, out state);
                    alglib.minqpsetquadraticterm(state, H, isupper);
                    alglib.minqpsetlinearterm(state, d);
                    alglib.minqpsetlc(state, inputData.Ab, ct);
                    alglib.minqpsetbc(state, inputData.lb, inputData.ub);

                    // Set scale of the parameters.
                    alglib.minqpsetscale(state, s);

                    // Solve problem with the sparse interior-point method (sparse IPM) solver.
                    alglib.minqpsetalgosparseipm(state, 0.0);
                    alglib.minqpoptimize(state);
                    alglib.minqpresults(state, out x, out rep);

                }

                catch (alglib.alglibexception alglib_exception)
                {
                    System.Console.WriteLine("ALGLIB exception with message '{0}'", alglib_exception.msg);
                }

                for(int i = 0; i < n; i++)
                {
                    inputData.x0[i] = x[i];
                }
            }
            return new OutputData(x);
        }
    }
}