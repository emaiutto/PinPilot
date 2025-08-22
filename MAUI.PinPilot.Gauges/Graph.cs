namespace MAUI.PinPilot.Gauges
{
    public static class Graph
    {
        public static TransformGroup GetTransformGroup(double x, double y, double angle, double scalefactor)
        {

            TransformGroup group = new();

            group.Children.Add(new RotateTransform(angle));

            group.Children.Add(new TranslateTransform(x, y));

            group.Children.Add(new ScaleTransform(scalefactor, scalefactor));

            return group;
        }

    }
}
