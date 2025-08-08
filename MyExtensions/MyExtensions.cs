namespace MauiSoft.SRP.MyExtensions
{
    public static class MyExtensions
    {

        // Ordenamiento de bits
        // 7  6  5  4  3  2  1  0

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBitSet(this byte b, int pos) => (b & 1 << pos) != 0;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullEmptyOrSpace(this string str) =>
            string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);


        public static float MapRange(this float input, float inputLow, float inputHigh, float outputLow, float outputHigh)
            => (input - inputLow) * (outputHigh - outputLow) / (inputHigh - inputLow) + outputLow;

        public static double MapRange(this double input, double inputLow, double inputHigh, double outputLow, double outputHigh)
            => (input - inputLow) * (outputHigh - outputLow) / (inputHigh - inputLow) + outputLow;


        public static int MapRange(this int value, int fromSource, int toSource, int fromTarget, int toTarget) => (int)Math.Round((value - fromSource) / (double)(toSource - fromSource) * (toTarget - fromTarget) + fromTarget);


        public static double Normalize360(this double angle)
        {
            angle %= 360d;

            if (angle < 0d) angle += 360d;

            return angle;

        }
    }
}
