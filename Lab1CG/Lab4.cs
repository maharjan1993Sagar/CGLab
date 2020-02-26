using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lab1CG
{
    public static class Lab4
    {
        public static void getData()
        {
            List<Point> Points = new List<Point>();

            StreamReader sr = new StreamReader("F:\\Lab1CGC\\Lab1CG\\TestPoints.txt");
            String s = sr.ReadLine();

            while (s != null)
            {
                Point p = new Point { x = Convert.ToInt32(s.Split(' ')[0]), y = Convert.ToInt32(s.Split(' ')[1]) };
                Points.Add(p);
                s = sr.ReadLine();
            }

            List<Point> SortedPoints = Sort(Points);

            //  ExtremePoint(SortedPoints);
            //  ExtremeEdges(SortedPoints);
            // GiftWrap(SortedPoints);
            GrahamScan(SortedPoints);

        }

        public static void ExtremePoint(List<Point> Points)
        {
            Console.WriteLine("Points/n");
            foreach (var item in Points)
            {
                Console.WriteLine(item.x + "," + item.y + "\n");
            }
            List<ExtremePoints> ExtPoints = Points.Select(x => new ExtremePoints() { Point = x }).ToList();
            List<ExtremePoints> ExtPoints1 = new List<ExtremePoints>();
            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = 0; j < Points.Count; j++)
                {
                    if (i != j)
                    {
                        for (int k = 0; k < Points.Count; k++)
                        {
                            if (j != k)
                            {
                                for (int l = 0; l < Points.Count; l++)
                                {
                                    if (k != l)
                                    {

                                        Polygon pol = new Polygon();
                                        pol.Vertex.AddLast(Points[i]);
                                        pol.Vertex.AddLast(Points[j]);
                                        pol.Vertex.AddLast(Points[k]);

                                        if (Lab3.CheckPointInclusion(pol, Points[l]))
                                        {

                                            ExtPoints[l].IsExtreme = false;
                                            //Console.WriteLine("Triangle {0},{1}|{2},{3}|{4},{5}| Query {6},{7} REsult:{8}|{9}",
                                            //                    Points[i].x, Points[i].y, Points[j].x, Points[j].y, Points[k].x,
                                            //                    Points[k].y, Points[l].x, Points[l].y, Lab3.CheckPointInclusion(pol, Points[l]) ? "Inside" : "Outside",
                                            //                    Lab3.RayCasting(pol, Points[l]) % 2 == 1 ? "Inside" : "Outside");
                                        }

                                    }

                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Sorted Points\n");
            foreach (var item in ExtPoints.Where(m => m.IsExtreme))
            {
                Console.WriteLine(item.Point.x + "," + item.Point.y + "\n");
            }
            Console.ReadKey();


        }

        public static void ExtremeEdges(List<Point> Points)
        {
            string[] turns = { "right", "left", "collinear" };
            string turn = "";
            List<Line> extEdges = new List<Line>();
            Console.WriteLine("Points/n");
            foreach (var item in Points)
            {
                Console.WriteLine(item.x + "," + item.y + "\n");
            }

            for (int i = 0; i < Points.Count; i++)
            {
                for (int j = 0; j < Points.Count; j++)
                {
                    if (i != j)
                    {
                        for (int k = 0; k < Points.Count; k++)
                        {
                            if (j != k)
                            {
                                if (k == 0)
                                {
                                    turn = Lab2.turnTest(Points[i], Points[j], Points[k]);
                                    continue;
                                }
                                else
                                {
                                    string currentTurn = Lab2.turnTest(Points[i], Points[j], Points[k]);
                                    Console.WriteLine("Line {0},{1}|{2},{3} Query|{4},{5}|Result: {6}",
                                                           Points[i].x, Points[i].y, Points[j].x, Points[j].y, Points[k].x,
                                                           Points[k].y, Lab2.turnTest(Points[i], Points[j], Points[k])
                                                           );
                                    if (currentTurn == "collinear" && k < (Points.Count - 1))
                                    {
                                        continue;
                                    }
                                    else if (currentTurn != "collinear" && turn == "collinear" && k < (Points.Count - 1))
                                    {
                                        turn = currentTurn;
                                        continue;
                                    }
                                    else if (turn != "collinear" && currentTurn != "collinear" && turn == currentTurn && k < (Points.Count - 1))
                                    {
                                        turn = currentTurn;
                                        continue;
                                    }
                                    else if (turn != "collinear" && currentTurn != "collinear" && turn != currentTurn && k < (Points.Count - 1))
                                    {
                                        break;
                                    }
                                    else if ((currentTurn == "collinear" || turn == currentTurn) && k == (Points.Count - 1))
                                    {
                                        List<Point> linePoint = Sort(new List<Point> { Points[i], Points[j] });
                                        Line line = new Line { Point1 = linePoint[0], Point2 = linePoint[1] };
                                        if (!extEdges.Any(m => m.Point1 == line.Point1 && m.Point2 == line.Point2))
                                        {
                                            extEdges.Add(line);
                                        }
                                        //turn = currentTurn;
                                        //continue;
                                    }

                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Extreme Edges/n");
            foreach (var item in extEdges)
            {
                Console.WriteLine("Line:|" + item.Point1.x + "," + item.Point1.y + "\t" +
                    item.Point2.x + "," + item.Point2.y + "\n");
            }
            Console.ReadKey();


        }


        public static void GiftWrap(List<Point> Points)
        {
            List<Point> ConvexHull = new List<Point>();
            Point p1 = Points.OrderBy(m => m.x).FirstOrDefault();

            ConvexHull.Add(p1);
            do
            {
                Point QPoint = ConvexHull.Last();
                List<Point> RemainPoints = null;
                if (ConvexHull.Count() >= 2)
                {
                    RemainPoints = Points.Except(ConvexHull).ToList();
                    RemainPoints.Add(ConvexHull.First());
                }
                else
                {
                    RemainPoints = Points.Except(ConvexHull).ToList();
                }
                List<GWPoints> GWpoints = (from p in RemainPoints
                                           where p != QPoint
                                           select new GWPoints
                                           {
                                               Point = p,
                                               Angle = Math.Atan2((p.y - QPoint.y), (p.x - QPoint.x))
                                           }).ToList();

                Point Result = GWpoints.OrderByDescending(m => m.Angle).ThenByDescending(m => m.Point.x).ThenByDescending(m=>m.Point.y).FirstOrDefault().Point;
                //Console.WriteLine("Query:|" + QPoint.x + "," + QPoint.y + "\t Result |" + Result.x + "," + Result.y + "\n");
                ConvexHull.Add(GWpoints.OrderByDescending(m => m.Angle).ThenByDescending(m => m.Point.x).FirstOrDefault().Point);
                if (ConvexHull.First() == ConvexHull.Last())
                {
                    break;
                }
            } while (ConvexHull.First() != ConvexHull.Last());



            Console.WriteLine("Gift Wrap Convex hull points/n");
            foreach (var item in ConvexHull)
            {
                Console.WriteLine("Point:|" + item.x + "," + item.y + "\n");
            }
            Console.ReadKey();

        }

        public static void GrahamScan(List<Point> points)
        {
            Point MinPoint = points.OrderBy(m => m.y).First();
            List<SortPoint> sorted = (from p in points
                                      select new SortPoint
                                      {
                                          Point = p,
                                          Angle = Math.Atan2(p.y - MinPoint.y, p.x - MinPoint.x)
                                      }).OrderBy(m=>m.Angle).ToList();

            Stack<Point> Graham = new Stack<Point>();
            Graham.Push(MinPoint);
            foreach (var item in sorted)
            {
                if (item != sorted.First())
                {
                    if (Graham.Count() >= 2)
                    {
                        string turn = Lab2.turnTest(Graham.Skip(1).First(), Graham.Peek(), item.Point);
                        if (turn.ToUpper() == "RIGHT")
                        {
                            Graham.Pop();
                            foreach (var stitem in Graham.ToList())
                            {
                                if (Lab2.turnTest(Graham.Skip(1).First(), Graham.Peek(), item.Point).ToUpper() == "RIGHT")
                                {
                                    Graham.Pop();
                                }
                                else
                                {
                                    Graham.Push(item.Point);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Graham.Push(item.Point);
                        }
                    }
                    else
                    {
                        Graham.Push(item.Point);
                    }
                }
            }

            Console.WriteLine("Graham Scan Points /n");
            foreach (var item in Graham)
            {
               
                Console.WriteLine("Point:|" + item.x + "," + item.y + "\n");
            }
            Console.ReadKey();


        }

        public static List<Point> Sort(List<Point> Points)
        {
            Point CenterPoint = new Point { x = Points.Sum(m => m.x) / Points.Count, y = Points.Sum(m => m.y) / Points.Count };
            List<SortPoint> SortedPoint = new List<SortPoint>();
            SortedPoint = (from p in Points
                           select new SortPoint
                           {
                               Angle = Math.Atan2((p.y - CenterPoint.y), (p.x - CenterPoint.x)),
                               Point = p
                           }).OrderBy(m => m.Angle).ToList();

            for (int i = 0; i < SortedPoint.Count; i++)
            {
                if (i != SortedPoint.Count - 1)
                {
                    if (SortedPoint[i].Point.y > SortedPoint[i + 1].Point.y)
                    {
                        SortPoint temp = new SortPoint();
                        temp = SortedPoint[i];
                        SortedPoint[i] = SortedPoint[i + 1];
                        SortedPoint[i + 1] = temp;
                    }
                }
                else
                {
                    break;
                }
            }
            return SortedPoint.Select(m => m.Point).ToList();

        }
    }
}
