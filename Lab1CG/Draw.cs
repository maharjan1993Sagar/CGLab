using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1CG
{
    public partial class Draw : Form
    {
        public Draw()
        {
            InitializeComponent();
        }

        private void Draw_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Red, 1);
            Pen penBlue = new Pen(Color.Blue, 5);
            Polygon p = Lab3.p1;
            PointF[] points = new PointF[p.Vertex.Count];
            int i = 0;
            foreach (Point point in p.Vertex)
            {
                PointF p1 = new PointF((float)(point.x*10), (float)(point.y*10));
                points[i] = p1;
                i++;
            }
            e.Graphics.DrawPolygon(pen, points);

           // e.Graphics.DrawLine(penBlue,(float)(Lab3.QureyPoint.x),(float)(Lab3.QureyPoint.y), (float)(p.Vertex.Max(m => m.x) + 100))

        }
    }
}
