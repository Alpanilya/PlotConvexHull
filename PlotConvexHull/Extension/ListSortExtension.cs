using OxyPlot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PlotConvexHull.Extension
{
    public static class ListSortExtension
    {

        #region Sort by Counter Clockwise

        public static List<DataPoint> GetSortByCCW(this List<DataPoint> CartesianPoints)
        {
            Dictionary<Double, DataPoint> points = ConvertToPolar(CartesianPoints);

            return points
                .OrderBy(p => p.Key)
                .Select(p => p.Value)
                .ToList();
        }

        #endregion

        #region Sort by Clockwise

        public static List<DataPoint> GetSortByCW(this List<DataPoint> CartesianPoints)
        {
            Dictionary<Double, DataPoint> points = ConvertToPolar(CartesianPoints);

            return points
                .OrderBy(p => p.Key)
                .Select(p => p.Value)
                .Reverse()
                .ToList();
        }

        #endregion

        #region ConvertToPolar

        private static Dictionary<Double, DataPoint> ConvertToPolar(List<DataPoint> CartesianPoints)
        {
            Dictionary<Double, DataPoint> polar = new Dictionary<double, DataPoint>();

            foreach (var point in CartesianPoints)
            {
                polar.Add(Math.Atan2(point.Y, point.X), point);
            }

            return polar;
        }

        #endregion
    }
}
