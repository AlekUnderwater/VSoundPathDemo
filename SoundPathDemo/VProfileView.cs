using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UCNLPhysics;
using UCNLDrivers;
using System.Globalization;

namespace SoundPathDemo
{
    public partial class VProfileView : UserControl
    {
        #region Properties

        RectangleF graphBorder;
        public int tsTicks { get; private set; }
        int vTickPeriod = 1;

        public int ZTicks { get; private set; }
                      
        #region Axis

        int axisTickSize = 4;

        Color axisColor;
        Pen axisPen;

        public Color AxisColor
        {
            get { return axisColor; }
            set
            {
                axisColor = value;
                axisPen = new Pen(axisColor, 1.0f);
            }
        }

        #endregion

        #region Axis labels

        int axisLblFntSize = 3;
        Font axisLblFont;
        Color axisLblColor;
        Brush axisLblBrush;

        public int AxisLblFntSize
        {
            get { return axisLblFntSize; }
            set
            {
                axisLblFntSize = value;
                axisLblFont = new Font("Consolas", axisLblFntSize, GraphicsUnit.Millimeter);
            }
        }

        public Color AxisLblColor
        {
            get { return axisLblColor; }
            set
            {
                axisLblColor = value;
                axisLblBrush = new SolidBrush(axisLblColor);
            }
        }

        #endregion

        #region Caption

        int captionFntSize = 3;
        Font captionFont;
        Color captionColor;
        Brush captionBrush;

        public string Caption { get; set; }

        public int CaptionLblFntSize
        {
            get { return captionFntSize; }
            set
            {
                captionFntSize = value;
                captionFont = new Font("Consolas", captionFntSize, GraphicsUnit.Millimeter);
            }
        }

        public Color CaptionColor
        {
            get { return captionColor; }
            set
            {
                captionColor = value;
                captionBrush = new SolidBrush(captionColor);
            }
        }

        #endregion

        #region Graphs items

        Color tempColor;
        Color styColor;

        Brush tempBrush;
        Brush styBrush;

        Pen tPen;
        Pen sPen;

        float graphPenWidth = 2.0f;

        public float GraphPenWidth
        {
            get { return graphPenWidth; }
            set
            {
                tPen = new Pen(tempColor, graphPenWidth);
                sPen = new Pen(styColor, graphPenWidth);
            }
        }

        public Color TempGraphColor
        {
            get { return tempColor; }
            set
            {
                tempColor = value;
                tPen = new Pen(tempColor, graphPenWidth);
                tempBrush = new SolidBrush(tempColor);
            }
        }

        public Color StyGraphColor
        {
            get { return styColor; }
            set
            {
                styColor = value;
                sPen = new Pen(styColor, graphPenWidth);
                styBrush = new SolidBrush(styColor);
            }
        }

        #endregion
        
        TSProfilePoint[] tsp;

        public double Tmax { get; private set; }
        public double Tmin { get; private set; }
        public double Smax { get; private set; }
        public double Smin { get; private set; }
        public double Zmax { get; private set; }
        public double Zmin { get; private set; }

        float tRange;
        float sRange;
        float tStep;
        float sStep;

        #endregion

        #region Constructor

        public VProfileView()
        {
            InitializeComponent();

            AxisLblFntSize = 3;
            AxisColor = Color.Gainsboro;

            CaptionColor = Color.LightGray;
            CaptionLblFntSize = 4;

            tsTicks = 7;

            Caption = DemoProfiles.AltanticS20.Description;
            SetProfile(DemoProfiles.AltanticS20.Profile);
            
        }

        #endregion

        #region Methods

        public void SetProfile(TSProfilePoint[] profile)
        {
            tsp = new TSProfilePoint[profile.Length];
            Array.Copy(profile, tsp, profile.Length);

            Zmin = tsp[0].Z;
            Zmax = tsp[tsp.Length - 1].Z;

            double tmax = tsp[0].T;
            double tmin = tmax;
            double smax = tsp[0].S;
            double smin = smax;

            for (int i = 1; i < tsp.Length; i++)
            {
                if (tsp[i].T > tmax)
                    tmax = tsp[i].T;

                if (tsp[i].T < tmin)
                    tmin = tsp[i].T;

                if (tsp[i].S > smax)
                    smax = tsp[i].S;

                if (tsp[i].S < smin)
                    smin = tsp[i].S;
            }

            Tmax = tmax;
            Tmin = tmin;
            Smax = smax;
            Smin = smin;

            tRange = Convert.ToSingle(Math.Abs(Tmax - Tmin));
            sRange = Convert.ToSingle(Math.Abs(Smax - Smin));
            tStep = tRange / tsTicks;
            sStep = sRange / tsTicks;

            if (tsp.Length <= 30)
                vTickPeriod = 1;
            else
            {
                vTickPeriod = Convert.ToInt32(Math.Ceiling((double)tsp.Length / 10));
            }

            ZTicks = tsp.Length / vTickPeriod - 1;

            ProfileChangedEvent.Rise(this, new EventArgs());
        }

        public TSProfilePoint[] GetProfile()
        {
            TSProfilePoint[] result = new TSProfilePoint[tsp.Length];
            Array.Copy(tsp, result, tsp.Length);
            return result;
        }

        #endregion

        #region Handlers

        private void VProfileView_Paint(object sender, PaintEventArgs e)
        {
            if (!e.ClipRectangle.IsEmpty)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                var captionSize = e.Graphics.MeasureString(Caption, captionFont);
                var axisLblMaxSize = e.Graphics.MeasureString(Zmax.ToString(), axisLblFont);

                float graphBorderLeft = axisLblMaxSize.Width + axisTickSize + axisLblMaxSize.Height;
                float graphBorderTop = captionSize.Height + axisLblMaxSize.Height + axisTickSize;

                graphBorder = new RectangleF(graphBorderLeft, graphBorderTop,
                    this.Width - graphBorderLeft - axisTickSize - axisLblMaxSize.Width / 2 - axisLblMaxSize.Height,
                    this.Height - graphBorderTop - axisLblMaxSize.Height - axisTickSize);


                float zscale = graphBorder.Height / Convert.ToSingle(Math.Abs(Zmax - Zmin));
                float tscale = graphBorder.Width / Convert.ToSingle(Math.Abs(Tmax - Tmin));
                float sscale = graphBorder.Width / Convert.ToSingle(Math.Abs(Smax - Smin));

                e.Graphics.DrawRectangle(axisPen, graphBorder.Left, graphBorder.Top, graphBorder.Width, graphBorder.Height);

                e.Graphics.DrawString(Caption, captionFont, captionBrush,
                    graphBorder.Left + graphBorder.Width / 2 - captionSize.Width / 2,
                    graphBorder.Top - captionSize.Height - axisLblMaxSize.Height);

                float z, z1;
                float x = graphBorder.Left;
                
                string lbl = "Z, m";
                var lblSize = e.Graphics.MeasureString(lbl, axisLblFont);
                e.Graphics.TranslateTransform(
                    graphBorder.Left - axisTickSize - axisLblMaxSize.Width - lblSize.Height,
                    graphBorder.Top + graphBorder.Height / 2 - lblSize.Width / 2);
                e.Graphics.RotateTransform(-90);
                e.Graphics.DrawString(lbl, axisLblFont, axisLblBrush, 0, 0);
                e.Graphics.RotateTransform(90);
                e.Graphics.TranslateTransform(
                    graphBorder.Left - axisTickSize - axisLblMaxSize.Width - lblSize.Height,
                    -(graphBorder.Top + graphBorder.Height / 2 - lblSize.Width / 2));
                
                for (int i = 0; i < tsp.Length; i++)
                {                    
                    z = graphBorder.Top + Convert.ToSingle(tsp[i].Z * zscale);

                    if (i % vTickPeriod == 0)
                    {

                        e.Graphics.DrawLine(axisPen, x, z, x - axisTickSize, z);
                        lbl = string.Format(CultureInfo.InvariantCulture, "{0:F00}", tsp[i].Z);
                        lblSize = e.Graphics.MeasureString(lbl, axisLblFont);
                        e.Graphics.DrawString(lbl, axisLblFont, axisLblBrush,
                            x - axisTickSize - lblSize.Width,
                            z - lblSize.Height / 2);
                    }

                    if (i < tsp.Length - 1)
                    {
                        z1 = graphBorder.Top + Convert.ToSingle(tsp[i + 1].Z * zscale);
                        e.Graphics.DrawLine(tPen,
                            graphBorder.Left + Convert.ToSingle(tsp[i].T - Tmin) * tscale, z,
                            graphBorder.Left + Convert.ToSingle(tsp[i + 1].T - Tmin) * tscale, z1);

                        e.Graphics.DrawLine(sPen,
                            graphBorder.Left + Convert.ToSingle(tsp[i].S - Smin) * sscale, z,
                            graphBorder.Left + Convert.ToSingle(tsp[i + 1].S - Smin) * sscale, z1);
                    }
                }                

                for (int i = 0; i < tsTicks; i++)
                {
                    x = graphBorder.Left + i * graphBorder.Width / tsTicks;

                    lbl = string.Format(CultureInfo.InvariantCulture, "{0:F01}", Tmin + i * tStep);
                    lblSize = e.Graphics.MeasureString(lbl, axisLblFont);
                    e.Graphics.DrawString(lbl, axisLblFont, axisLblBrush,
                        x - lblSize.Width / 2, graphBorder.Top - axisTickSize - lblSize.Height);

                    lbl = string.Format(CultureInfo.InvariantCulture, "{0:F01}", Smin + i * sStep);
                    lblSize = e.Graphics.MeasureString(lbl, axisLblFont);
                    e.Graphics.DrawString(lbl, axisLblFont, axisLblBrush,
                        x - lblSize.Width / 2, graphBorder.Bottom + axisTickSize);

                    e.Graphics.DrawLine(axisPen,
                        x, graphBorder.Top,
                        x, graphBorder.Top - axisTickSize);

                    e.Graphics.DrawLine(axisPen,
                        x, graphBorder.Bottom,
                        x, graphBorder.Bottom + axisTickSize);
                }

                lbl = "t, °C";
                lblSize = e.Graphics.MeasureString(lbl, axisLblFont);
                e.Graphics.DrawString(lbl, axisLblFont, tempBrush,
                    graphBorder.Right - lblSize.Width / 2,
                    graphBorder.Top - axisTickSize - lblSize.Height);

                lbl = "s, PSU";
                lblSize = e.Graphics.MeasureString(lbl, axisLblFont);
                e.Graphics.DrawString(lbl, axisLblFont, styBrush,
                    graphBorder.Right - lblSize.Width / 2,
                    graphBorder.Bottom + axisTickSize);

            }
        }

        private void VProfileView_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        #endregion

        #region Events

        public EventHandler ProfileChangedEvent;

        #endregion
    }
}
