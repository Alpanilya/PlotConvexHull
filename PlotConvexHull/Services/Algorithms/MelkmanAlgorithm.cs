using Nito.Collections;
using OxyPlot;
using PlotConvexHull.Extension;
using PlotConvexHull.Services.Algorithms.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlotConvexHull.Services.Algorithms
{
    internal class MelkmanAlgorithm : Algorithm
    {
        public override List<DataPoint> ConvexHull(List<DataPoint> points)
        {
            var len = points.Count;

            if (len < 3) throw new ArgumentException("Number of points less than required");

            Deque<DataPoint> deque = new Deque<DataPoint>();

            points = points.GetSortByCCW();

            if (Rotate(points[0], points[1], points[2]) > 0)
            {
                deque.AddToBack(points[0]);
                deque.AddToBack(points[1]);
            }
            else
            {
                deque.AddToBack(points[1]);
                deque.AddToBack(points[0]);
            }

            deque.AddToBack(points[2]);
            deque.AddToFront(points[2]);

            if (len == 3) return deque.ToList();

            for (int i = 3; i < len; i++)
            {
                int t = deque.Count;

                while(Rotate(points[i], deque.ElementAt(0), deque.ElementAt(1)) > 0 && Rotate(deque.ElementAt(t - 2), deque.ElementAt(t - 1), points[i]) > 0)
                {
                    i++;
                    if (i > len - 1) return deque.ToList(); 
                }

                while(Rotate(deque.ElementAt(t - 2), deque.ElementAt(t - 1), points[i]) < 0)
                {
                    deque.RemoveFromBack();
                    t = deque.Count;
                }

                deque.AddToBack(points[i]);

                while(Rotate(points[i], deque.ElementAt(0), deque.ElementAt(1)) < 0)
                {
                    deque.RemoveFromFront();
                }

                deque.AddToFront(points[i]);
            }

            return deque.ToList();
        }
    }
}
