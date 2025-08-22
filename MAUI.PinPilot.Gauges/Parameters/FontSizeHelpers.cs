
internal static class FontSizeHelpers
{

    public static double Small = 0.08;

    public static double Medium = 0.14;

    public static double Big = 0.2;

    public static double GPS = 0.12;

    public static double SafeWidth(double width, double fallback = 100) => (!double.IsNaN(width) && width > 0) ? width : fallback;

}