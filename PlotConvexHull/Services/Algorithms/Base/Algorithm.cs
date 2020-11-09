using OxyPlot;
using System;
using System.Collections.Generic;

namespace PlotConvexHull.Services.Algorithms.Base
{
    internal abstract class Algorithm
    {
        protected private Double Rotate(DataPoint a, DataPoint b, DataPoint c)
            => (c.X - b.X) * (b.Y - a.Y) - (c.Y - b.Y) * (b.X - a.X);

        public abstract List<DataPoint> ConvexHull(List<DataPoint> points);
    }
}
