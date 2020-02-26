using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1CG
{
    public static class Lab2
    {
        

        public static void getData()
        {
             Point Point1 = new Point();
            Point Point2 = new Point();
          Point Point3 = new Point();
            

        Console.WriteLine("Enter Origin point.");
            string P1 = Console.ReadLine();

            Console.WriteLine("Enter Reference point.");
            string P2 = Console.ReadLine();

            while (true)
            {

                Console.WriteLine("Enter Point to test.");
                string P3 = Console.ReadLine();


                Point1.x = int.Parse(P1.Split(',')[0]);
                Point1.y = int.Parse(P1.Split(',')[1]);

                Point2.x = int.Parse(P2.Split(',')[0]);
                Point2.y = int.Parse(P2.Split(',')[1]);


                Point3.x = int.Parse(P3.Split(',')[0]);
                Point3.y = int.Parse(P3.Split(',')[1]);

                string turn = turnTest(Point1, Point2, Point3);
                Console.WriteLine("\n\nTest point is {0} turn at point ({1}, {2})\n\n", turn, Point2.x, Point2.y);
            }

        }

        public static void getDataInt()
        {
          
            Line line1 = new Line();
            Line line2 = new Line();

            Console.WriteLine("first point of first line.");
            string l1 = Console.ReadLine();
            line1.Point1.x = int.Parse(l1.Split(',')[0]);
            line1.Point1.y = int.Parse(l1.Split(',')[1]);


            Console.WriteLine("Second point of first line.");
            l1 = Console.ReadLine();
            line1.Point2.x = int.Parse(l1.Split(',')[0]);
            line1.Point2.y = int.Parse(l1.Split(',')[1]);

            while (true)
            {
                Console.WriteLine(" first point of Second line.");
                string l2 = Console.ReadLine();
                line2.Point1.x = int.Parse(l2.Split(',')[0]);
                line2.Point1.y = int.Parse(l2.Split(',')[1]);

                Console.WriteLine("Second point of Second line.");
                l2 = Console.ReadLine();
                line2.Point2.x = int.Parse(l2.Split(',')[0]);
                line2.Point2.y = int.Parse(l2.Split(',')[1]);

                string[] intersections = { "Proper", "Improper" };

                string intersect1 = CheckIntersection(line1, line2);
                string intersect2 = CheckIntersection(line2, line1);
                //if(turnTest())

                if (turnTest(line1.Point1, line1.Point2, line2.Point1) == "collinear" && turnTest(line1.Point1, line1.Point2, line2.Point2) == "collinear"
                           && turnTest(line2.Point1, line2.Point2, line1.Point1) == "collinear" && turnTest(line2.Point1, line2.Point2, line1.Point2) == "collinear")
                {
                    if (!string.IsNullOrEmpty(intersect1) || !string.IsNullOrEmpty(intersect2))
                    {
                        Console.WriteLine("\n\nImproper Intersection for given lines.\n\n");

                    }
                }
                else if (!string.IsNullOrEmpty(intersect1) && !string.IsNullOrEmpty(intersect2))
                {
                    if (intersect1 == "Improper" || intersect2 == "Improper")
                        Console.WriteLine("\n\nImproper Intersection for given lines.\n\n");
                    else
                        Console.WriteLine("\n\nProper Intersection for given lines.\n\n");
                }
                else
                {
                    Console.WriteLine("\n\nLines Does not intersect.\n\n");
                }
            }

        }
        public static string turnTest(Point p, Point q, Point s)
        {
            double area = calculateTriangleArea(p, q, s);
            if (area > 0)
                return "left";

            else if (area < 0)
                return "right";

            else if (area == 0)
                return "collinear";

            else
                return "error";

        }
        //(x1.y2+x2.y3+x3.y1-x2.y1-x3.y2-x1.y3)
        public static double calculateTriangleArea(Point p1, Point p2, Point p3)
        {
            double area = .5 * (p1.x * p2.y
                + p2.x * p3.y + p3.x * p1.y - p2.x * p1.y - p3.x * p2.y - p1.x * p3.y);
            return area;
        }

        public static string CheckIntersection(Line line1, Line line2)
        {
            string result="";
            string[] turn = { "left", "right" };
            string[] pointLineClassification = { "between", "start", "end" };
            string turn1 = turnTest(line1.Point1, line1.Point2, line2.Point1);
            string turn2 = turnTest(line1.Point1, line1.Point2, line2.Point2);

            if (turn1 == "collinear")
            {
                if (pointLineClassification.Contains(Lab1.PointLineClassification(line1, line2.Point1)))
                    result = "Improper";
            }

            if (turn2 == "collinear")
            {
                if (pointLineClassification.Contains(Lab1.PointLineClassification(line1, line2.Point2)))
                    result = "Improper";
            }
            if (turn1 != "collinear" && turn2 != "collinear")
            {
                if (turn1 != turn2 && turn.Contains(turn1) && turn.Contains(turn2))
                    return "Proper";
            }
            return result;

        }

    }
}
