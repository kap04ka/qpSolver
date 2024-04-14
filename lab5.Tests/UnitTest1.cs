using lab5;

namespace lab5.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void positive_test_data()
        {
            InputData inputData = new InputData();
            OutputData outputData = new OutputData();

            inputData.iterCount = 2000;

            inputData.Ab = new double[,] {
                {1,  -1,   -1,    0,   0,  0,    0,    0},
                {0,   0,    1,    -1, -1,  0,    0,    0},
                {0,   0,    0,    0,   1,  -1,  -1,    0},
            };

            inputData.x0 = new double[] { 10.005, 3.033, 6.831, 1.985, 5.093, 4.057, 0.991 };
            inputData.errors = new double[] { 0.200, 0.121, 0.683, 0.040, 0.102, 0.081, 0.020 };
            inputData.I = new byte[] { 1, 1, 1, 1, 1, 1, 1 };
            inputData.lb = new double[] { 0, 0, 0, 0, 0, 0, 0 };
            inputData.ub = new double[] { 10000, 10000, 10000, 10000, 10000, 10000, 10000 };

            outputData.x = AlglibDemo.Solver(inputData);

            bool isAppropriate = true;

            double sum = 0;
            for (int i = 0; i < inputData.Ab.GetLength(0); i++)
            {
                sum = 0;
                for (int j = 0; j < inputData.Ab.GetLength(1) - 1; j++)
                {
                    sum += inputData.Ab[i, j] * outputData.x[j];
                }
                if (Math.Round(sum, 3) != 0)
                {
                    isAppropriate = false;
                    break;
                }
            }

            Assert.True(isAppropriate);
        }

        [Fact]
        public void positive_nechet_var_data()
        {
            InputData inputData = new InputData();
            OutputData outputData = new OutputData();

            inputData.iterCount = 2000;

            inputData.Ab = new double[,] {
                {1,  -1,   -1,    0,   0,  0,    0,    -1,  0},
                {0,   0,    1,    -1, -1,  0,    0,    0,   0},
                {0,   0,    0,    0,   1,  -1,  -1,    0,   0},
            };

            inputData.x0 = new double[] { 10.005, 3.033, 6.831, 1.985, 5.093, 4.057, 0.991, 6.667 };
            inputData.errors = new double[] { 0.200, 0.121, 0.683, 0.040, 0.102, 0.081, 0.020, 0.667 };
            inputData.I = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1 };
            inputData.lb = new double[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            inputData.ub = new double[] { 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000 };

            outputData.x = AlglibDemo.Solver(inputData);
            

            bool isAppropriate = true;

            double sum = 0;
            for (int i = 0; i < inputData.Ab.GetLength(0); i++)
            {
                sum = 0;
                for(int j = 0; j < inputData.Ab.GetLength(1) - 1; j++) {
                    sum += inputData.Ab[i, j] * outputData.x[j];
                }
                if (Math.Round(sum, 3) != 0) { 
                    isAppropriate = false;
                    break;
                }
            }

            Assert.True(isAppropriate);
        }

        [Fact]
        public void positive_x1_eq_10x2_data()
        {
            InputData inputData = new InputData();
            OutputData outputData = new OutputData();

            inputData.iterCount = 2000;

            inputData.Ab = new double[,] {
                {1,  -1,   -1,    0,   0,  0,    0,    -1,  0},
                {0,   0,    1,    -1, -1,  0,    0,    0,   0},
                {0,   0,    0,    0,   1,  -1,  -1,    0,   0},
                {1,   -10,  0,    0,   0,  0,    0,    0,   0},
            };

            inputData.x0 = new double[] { 10.005, 3.033, 6.831, 1.985, 5.093, 4.057, 0.991, 6.667 };
            inputData.errors = new double[] { 0.200, 0.121, 0.683, 0.040, 0.102, 0.081, 0.020, 0.667 };
            inputData.I = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1 };
            inputData.lb = new double[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            inputData.ub = new double[] { 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000 };

            outputData.x = AlglibDemo.Solver(inputData);


            bool isAppropriate = true;

            double sum = 0;
            for (int i = 0; i < inputData.Ab.GetLength(0); i++)
            {
                sum = 0;
                for (int j = 0; j < inputData.Ab.GetLength(1) - 1; j++)
                {
                    sum += inputData.Ab[i, j] * outputData.x[j];
                }
                if (Math.Round(sum, 3) != 0)
                {
                    isAppropriate = false;
                    break;
                }
            }

            if(Math.Round(outputData.x[0], 3) != 10 * Math.Round(outputData.x[1], 4))
            {
                isAppropriate = false;
            }

            Assert.True(isAppropriate);
        }
    }
}