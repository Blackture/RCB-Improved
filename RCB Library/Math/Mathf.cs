using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace RCBLibrary.Math
{
    public static class Mathf
    {
        public static readonly float PiQuarter;
        public static readonly float PiHalf;

        public static readonly float SinePiQuarter;
        public static readonly float CosinePiQuarter;

        public const float e = (float)System.Math.E;
        public const float pi = (float)System.Math.PI;

        static Mathf()
        {
            PiHalf = Mathf.pi / 2.0f;
            PiQuarter = Mathf.pi / 4.0f;

            SinePiQuarter = Mathf.Sin(PiQuarter);
            CosinePiQuarter = Mathf.Cos(PiQuarter);
        }

        public static float Acos(float f)
        {
            return Convert.ToSingle(System.Math.Acos(f));
        }

        public static float Asin(float f)
        {
            return Convert.ToSingle(System.Math.Asin(f));
        }

        public static float Atan(float f)
        {
            return Convert.ToSingle(System.Math.Atan(f));
        }

        public static float Abs(float f)
        {
            return System.Math.Abs(f);
        }

        public static float Atan2(float x, float y)
        {
            return Convert.ToSingle(System.Math.Atan2(x, y));
        }

        public static float Ceiling(float f)
        {
            return Convert.ToSingle(System.Math.Ceiling(f));
        }

        public static float Clamp(float value, float min, float max)
        {
            return Convert.ToSingle(System.Math.Clamp(value, min, max));
        }

        public static float Cos(float f)
        {
            return Convert.ToSingle(System.Math.Cos(f));
        }

        public static float Cosh(float f)
        {
            return Convert.ToSingle(System.Math.Cosh(f));
        }

        public static float Cot(float f)
        {
            return Convert.ToSingle(Cos(f) / Sin(f));
        }

        public static float Sec(float f)
        {
            return Convert.ToSingle(1.0f / Cos(f));
        }
        public static float Csc(float f)
        {
            return Convert.ToSingle(1.0f / Sin(f));
        }

        public static float Exp(float f)
        {
            return Convert.ToSingle(System.Math.Exp(f));
        }

        public static float Floor(float f)
        {
            return Convert.ToSingle(System.Math.Floor(f));
        }

        public static float IEEERemainder(float x, float y)
        {
            return Convert.ToSingle(System.Math.IEEERemainder(x, y));
        }

        public static float Log(float f)
        {
            return Convert.ToSingle(System.Math.Log(f));
        }

        public static float Log(float f, float newBase)
        {
            return Convert.ToSingle(System.Math.Log(f, newBase));
        }

        public static float Log10(float f)
        {
            return Convert.ToSingle(System.Math.Log10(f));
        }

        public static float Max(float val1, float val2)
        {
            return System.Math.Max(val1, val2);
        }

        public static float Min(float val1, float val2)
        {
            return System.Math.Min(val1, val2);
        }

        public static float Pow(float b, float p)
        {      
            return Convert.ToSingle(System.Math.Pow(b, p));
        }

        public static float Round(float f)
        {
            return Convert.ToSingle(System.Math.Round(f));
        }

        public static float Round(float f, int digits)
        {
            return Convert.ToSingle(System.Math.Round(f, digits));
        }

        public static float Round(float f, int digits, MidpointRounding midpointRounding)
        {
            return Convert.ToSingle(System.Math.Round(f, digits, midpointRounding));
        }

        public static int RoundToInt(float f)
        {
            return (int)Round(f, 0, MidpointRounding.AwayFromZero);
        }

        public static int Sign(float f)
        {
            return System.Math.Sign(f);
        }

        public static float Sin(float f)
        {
            return Convert.ToSingle(System.Math.Sin(f));
        }

        public static float Sinh(float f)
        {
            return Convert.ToSingle(System.Math.Sinh(f));
        }

        public static float Sqrt(float f)
        {
            
            return Convert.ToSingle(System.Math.Sqrt(f));
        }

        public static float NthSqrt(float f, int n)
        {
            return Convert.ToSingle(System.Math.Pow((float)f, 1.0f / n));
        }

        public static float Tan(float f)
        {
            return Convert.ToSingle(System.Math.Tan(f));
        }

        public static float Tanh(float f)
        {
            return Convert.ToSingle(System.Math.Tanh(f));
        }

        public static float Truncate(float f)
        {
            return Convert.ToSingle(System.Math.Truncate(f));
        }

        public static float Pi(Func<float, float> innerFunction, int limit1, int limit2)
        {
            float res = 1.0f;
            for (float i = limit1; i < limit2; i++)
            {
                res *= innerFunction(i);
            }
            return res;
        }

        public static float Pi(Func<float,float> innerFunction, float limit1, float limit2, float increment)
        {
            float res = 1.0f;
            for (float i = limit1; i < limit2; i +=  increment)
            {
                res *= innerFunction(i);
            }
            return res;
        }

        /// <summary>
        /// Iterative Implementation
        /// </summary>
        /// <param name="innerFunction"></param>
        /// <param name="limit1"></param>
        /// <param name="limit2"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        public static float Sigma(Func<float, float> innerFunction, float limit1, float limit2, float increment)
        {
            float res = 0.0f;
            for (float i = limit1; i < limit2; i += increment)
            {
                res += innerFunction(i);
            }
            return res;
        }
        /// <summary>
        /// Iterative Implementation
        /// </summary>
        /// <param name="innerFunction"></param>
        /// <param name="limit1"></param>
        /// <param name="limit2"></param>
        /// <returns></returns>
        public static float Sigma(Func<float, float> innerFunction, int limit1, int limit2)
        {
            float res = 0.0f;
            for (float i = limit1; i < limit2; i++)
            {
                res += innerFunction(i);
            }
            return res;
        }

        /// <summary>
        /// Recursive Implementation
        /// </summary>
        /// <param name="innerFunction"></param>
        /// <param name="limit1"></param>
        /// <param name="limit2"></param>
        /// <returns></returns>
        public static float SigmaR(Func<float,float> innerFunction, int limit1, int limit2, float increment = float.Epsilon)
        {
            return SigmaHelper(innerFunction, limit1, limit2, innerFunction(limit1), increment);
        }

        private static float SigmaHelper(Func<float, float> innerFunction, float currentI, int limit2, float current, float increment)
        {

            if (currentI <= limit2)
            {
                return SigmaHelper(innerFunction, currentI + increment, limit2, current + innerFunction(currentI + increment), increment);
            }
            return current;
        }

        /// <summary>
        /// Iterative Sum
        /// </summary>
        /// <param name="floats"></param>
        /// <returns></returns>
        public static float Sigma(params float[] floats)
        {
            float res = 0;
            foreach (float f in floats)
            {
                res += f;
            }
            return res;
        }

        /// <summary>
        /// Iterative Sum
        /// </summary>
        /// <param name="floats"></param>
        /// <returns></returns>
        public static float SigmaPow(float power, params float[] floats)
        {
            float res = 0;
            foreach (float f in floats)
            {
                res += Pow(f,power);
            }
            return res;
        }

        /// <summary>
        /// Iterative Sum
        /// </summary>
        /// <param name="floats"></param>
        /// <returns></returns>
        public static float SigmaPow(float power, List<float> floats)
        {
            float res = 0;
            foreach (float f in floats)
            {
                res += Pow(f, power);
            }
            return res;
        }

        /// <summary>
        /// Recursive Sum
        /// </summary>
        /// <param name="floats"></param>
        /// <returns></returns>
        public static float SigmaR(params float[] floats)
        {
            return SigmaHelper(0, 0, floats);
        }

        /// <summary>
        /// Recursive Sum
        /// </summary>
        /// <param name="floats"></param>
        /// <returns></returns>
        public static float SigmaR(List<float> floats)
        {
            return SigmaHelper(0, 0, floats);
        }

        private static float SigmaHelper(int i, float current, float[] floats)
        {
            if (i < floats.Length)
            {
                return SigmaHelper(i + 1, current + floats[i], floats);
            }
            return current;
        }

        private static float SigmaHelper(int i, float current, List<float> floats)
        {
            if (i < floats.Count)
            {
                return SigmaHelper(i + 1, current + floats[i], floats);
            }
            return current;
        }

        private static float SigmaHelperPow(int i, float current, float[] floats, float power)
        {
            if (i < floats.Length)
            {
                return SigmaHelper(i + 1, current + Pow(floats[i], power), floats);
            }
            return current;
        }

        private static float SigmaHelperPow(int i, float current, List<float> floats, float power)
        {
            if (i < floats.Count)
            {
                return SigmaHelper(i + 1, current + Pow(floats[i], power), floats);
            }
            return current;
        }

        public static bool Approximately(float a, float b, float tolerance)
        {
            return Abs(a - b) < tolerance;
        }

        public static bool Approximately(float a, float b)
        {
            return Abs(a - b) < float.Epsilon;
        }

        public static float Deg2Rad(float deg)
        {
            return deg * pi / 180;
        }

        public static float Rad2Deg(float rad)
        {
            return rad * 180 / pi;
        }

        public static float Sinc(float f, bool Pi = false)
        {
            return Pi ? ((f != 0) ? Sin(pi * f) / (pi * f) : 1) : ((f != 0) ? Sin(f) / f : 1);
        }

        /// <summary>
        /// Iterative Implementation
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int Factorial(int i)
        {
            int result = i;
            for (int a = i; i > 0; i--)
                result *= a;
            return result;
        }

        /// <summary>
        /// Recursive Implementation
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static int FactorialR(int i)
        {
            if (i == 0 && i == 1)
                return 1;
            return FactorialHelper(i, i);
        }

        public static int FactorialHelper(int i, int current)
        {
            if (i > 1)
            {
                return FactorialHelper(i - 1, current * (i - 1));
            }

            return current;
        }


        public static float Gamma(float f)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Definite or improper integral
        /// </summary>
        /// <param name="lowerLimit"></param>
        /// <param name="upperLimit"></param>
        /// <param name="f"></param>
        /// <param name="innerFunction"></param>
        /// <returns></returns>
        public static float Integral(Limits limits, float f, Func<float, float> innerFunction, IntegrationApproximation approximation)
        {
            switch (approximation)
            {
                
            }
            throw new NotImplementedException();
        }
    }
}
