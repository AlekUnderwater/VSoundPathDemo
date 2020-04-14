using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UCNLDrivers;
using System.Globalization;

namespace SoundPathDemo
{
    public partial class VerticalPropagationPlot : UserControl
    {
        #region Properties

        RectangleF graphBorder;
        
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

        Color frontColor;
        Pen frontPen;
        Brush frontBrush;

        float fronPenWidth = 3.0f;

        public float FrontPenWidth
        {
            get { return fronPenWidth; }
            set
            {
                frontPen = new Pen(frontColor, fronPenWidth);
            }
        }

        public Color FrontGraphColor
        {
            get { return frontColor; }
            set
            {
                frontColor = value;
                frontPen = new Pen(frontColor, fronPenWidth);
                frontBrush = new SolidBrush(frontColor);
            }
        }

        int frontSegmentSize = 4;
        public int FrontSegmentSize
        {
            get { return frontSegmentSize; }
            set
            {
                if ((value > 0) && (value <= 10))
                    frontSegmentSize = value;
                else
                    throw new ArgumentOutOfRangeException("Value should be in range from 1 to 10");
            }
        }

        int dAlpha = 20;

        int frontSegments = 10;
        public int FrontSegments
        {
            get { return frontSegments; }
            set
            {
                if ((value > 0) && (value < 128))
                {
                    frontSegments = value;
                    dAlpha = 255 / frontSegments;
                }
                else
                    throw new ArgumentOutOfRangeException("Value should be in range from 1 to 128");
            }
        }

        #endregion

        public bool IsSimulationStepEvent { get; set; }

        double zmax = 1000;
        public double Zmax
        {
            get { return zmax; }
            set
            {
                if (value > 0)
                    zmax = value;
                else
                    throw new ArgumentOutOfRangeException("Value should be greater than zero");
            }
        }

        double zstep = 100;
        public double Zstep
        {
            get { return zstep; }
            set
            {
                if (value > 0)
                    zstep = value;
                else
                    throw new ArgumentOutOfRangeException("Value should be greater than zero");
            }
        }

        double zmin = 0;
        public double Zmin
        {
            get { return zmin; }
            set
            {
                if ((value >= 0) && (value < zmax))
                    zmin = value;
                else
                    throw new ArgumentOutOfRangeException("Value should be greater or equal to zero and less than Zmax");
            }
        }


        double pTime = 1.0;
        public double FullPropagationTime
        {
            get
            {
                return pTime;
            }
            set
            {
                if (value > 0)
                {
                    ClearDistance();
                    pTime = value;
                }
                else
                    throw new ArgumentOutOfRangeException("Value should be greater than zero");
            }
        }

        PrecisionTimer timer;

        double t = 0;
        double dt = 0.01;

        double direction = 1;
        Dictionary<string, double> speeds = new Dictionary<string, double>();
        Dictionary<string, double> zmaxPoints = new Dictionary<string, double>();
        Dictionary<string, double> zcoordinates = new Dictionary<string, double>();
        Dictionary<string, Func<double, double>> speedFunctions = new Dictionary<string, Func<double, double>>();

        #endregion

        #region Constructor

        public VerticalPropagationPlot()
        {
            InitializeComponent();

            timer = new PrecisionTimer();
            timer.Period = 10;
            timer.Mode = Mode.Periodic;
            timer.Tick += new EventHandler(timer_Tick);

            timer.Stopped += (o, e) =>
                {
                    if (this.InvokeRequired)
                        this.Invoke((MethodInvoker)delegate { SimulationFinishedEvent.Rise(o, e); });
                    else
                        SimulationFinishedEvent.Rise(o, e);
                };

            timer.Started += (o, e) =>
                {
                    if (this.InvokeRequired)
                        this.Invoke((MethodInvoker)delegate { SimulationStartedEvent.Rise(o, e); });
                    else
                        SimulationStartedEvent.Rise(o, e);
                };
        }

        #endregion

        #region Methods

        public void ClearDistance()
        {
            foreach (var item in zcoordinates)
                zmaxPoints[item.Key] = -1;

            this.Invalidate();
        }

        public void AddItem(string key, Func<double, double> speedFunction)
        {
            if (speedFunctions.ContainsKey(key))
                throw new ArgumentOutOfRangeException("Specified key is already present");

            speeds.Add(key, speedFunction(0));
            zmaxPoints.Add(key, -1);
            zcoordinates.Add(key, 0);
            speedFunctions.Add(key, speedFunction);
        }

        public void RemoveItem(string key)
        {
            if (!speedFunctions.ContainsKey(key))
                throw new ArgumentOutOfRangeException("Specified key is not present");

            speeds.Remove(key);
            zmaxPoints.Remove(key);
            zcoordinates.Remove(key);
            speedFunctions.Remove(key);
        }

        public void ClearItems()
        {
            speeds.Clear();
            zmaxPoints.Clear();
            zcoordinates.Clear();
            speedFunctions.Clear();
        }

        public void Start()
        {
            if (!timer.IsRunning)
            {
                t = 0;
                foreach (var item in speedFunctions)
                {
                    direction = 1;
                    zcoordinates[item.Key] = 0;                    
                    zmaxPoints[item.Key] = -1;
                    speeds[item.Key] = speedFunctions[item.Key](0);
                }
                
                timer.Start();
            }
        }

        private bool IsApproxEq(double v1, double v2, double eps)
        {
            return Math.Abs(v1 - v2) <= eps;
        }

        #endregion

        #region Handlers

        private void timer_Tick(object sender, EventArgs e)
        {
            if (IsApproxEq(t, pTime, dt * 0.1))
                timer.Stop();
            else
            {                
                t += dt;
                foreach (var item in speedFunctions)
                {
                    speeds[item.Key] = item.Value(zcoordinates[item.Key]);
                    zcoordinates[item.Key] += dt * direction * speeds[item.Key];
                }

                if (IsApproxEq(t, pTime / 2.0, dt * 0.1))
                {
                    if (direction > 0)
                    {
                        direction = -1;
                        foreach (var item in zcoordinates)
                            zmaxPoints[item.Key] = item.Value;
                    }
                }

            }

            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { this.Invalidate(); });
            else
                this.Invalidate();
         
            if (IsSimulationStepEvent)
                SimulationStepEvent.Rise(this, new EventArgs());
        }

        private void VerticalSoundPropagationPlot_Paint(object sender, PaintEventArgs e)
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

                e.Graphics.DrawRectangle(axisPen, graphBorder.Left, graphBorder.Top, graphBorder.Width, graphBorder.Height);

                e.Graphics.DrawString(Caption, captionFont, captionBrush,
                    graphBorder.Left + graphBorder.Width / 2 - captionSize.Width / 2,
                    graphBorder.Top - captionSize.Height - axisLblMaxSize.Height);

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

                float z;
                float zStep = Convert.ToSingle(zstep);
                float x = graphBorder.Left;                
                z = zStep;

                float za;
                while (z <= Zmax - zStep / 2)
                {
                    za = graphBorder.Top + z * zscale;
                    e.Graphics.DrawLine(axisPen, x, za, x - axisTickSize, za);
                    lbl = string.Format(CultureInfo.InvariantCulture, "{0:F00}", z);
                    lblSize = e.Graphics.MeasureString(lbl, axisLblFont);

                    e.Graphics.DrawString(lbl, axisLblFont, axisLblBrush,
                        x - axisTickSize - lblSize.Width,
                        za - lblSize.Height / 2);

                    z += zStep;
                }

                lbl = string.Format(CultureInfo.InvariantCulture, "{0:F00}", Zmin);
                lblSize = e.Graphics.MeasureString(lbl, axisLblFont);
                e.Graphics.DrawString(lbl, axisLblFont, axisLblBrush,
                    x - axisTickSize - lblSize.Width,
                    graphBorder.Top - lblSize.Height / 2);

                lbl = string.Format(CultureInfo.InvariantCulture, "{0:F00}", Zmax);
                lblSize = e.Graphics.MeasureString(lbl, axisLblFont);
                e.Graphics.DrawString(lbl, axisLblFont, axisLblBrush,
                    x - axisTickSize - lblSize.Width,
                    graphBorder.Bottom - lblSize.Height / 2);



                int nView = zcoordinates.Count;
                if (nView > 0)
                {
                    float viewWidth = graphBorder.Width / nView;
                    float viewHalfWidth = viewWidth / 2.0f;
                    float viewCenter;
                    float frontZcoordinate;
                    float dir = Convert.ToSingle(direction);

                    double[] zcoords = new double[zcoordinates.Count];
                    double[] zmaxs = new double[zmaxPoints.Count];
                    double[] cspeeds = new double[speeds.Count];
                    string[] vnames = new string[zcoordinates.Count];
                    
                    zcoordinates.Values.CopyTo(zcoords, 0);
                    zmaxPoints.Values.CopyTo(zmaxs, 0);
                    speeds.Values.CopyTo(cspeeds, 0);
                    zcoordinates.Keys.CopyTo(vnames, 0);
               
                    float zLowLimit = graphBorder.Bottom;

                    for (int view = 0; view < zcoords.Length; view++)
                    {
                        lbl = string.Format(CultureInfo.InvariantCulture, "{0}, v={1:F02} m/s",
                            vnames[view], cspeeds[view]);

                        lblSize = e.Graphics.MeasureString(lbl, axisLblFont);

                        viewCenter = graphBorder.Left + viewWidth * (view + 1) - viewHalfWidth;

                        e.Graphics.DrawString(lbl, axisLblFont, axisLblBrush,
                            viewCenter - lblSize.Width / 2, 
                            graphBorder.Top - lblSize.Height);

                        e.Graphics.DrawLine(axisPen, graphBorder.Left + viewWidth * (view + 1), graphBorder.Top,
                            graphBorder.Left + viewWidth * (view + 1), graphBorder.Bottom);

                        
                        if (zmaxs[view] > 0)
                        {                           
                            lbl = string.Format(CultureInfo.InvariantCulture, "{0:F02} m", zmaxs[view]);
                            lblSize = e.Graphics.MeasureString(lbl, axisLblFont);

                            zLowLimit = graphBorder.Top + Convert.ToSingle(zmaxs[view]) * zscale;

                            if (zLowLimit > graphBorder.Bottom)
                                e.Graphics.DrawString(lbl, axisLblFont, captionBrush,
                                    graphBorder.Left + viewWidth * view + lblSize.Height, graphBorder.Top + lblSize.Height);
                            else
                            {
                                e.Graphics.DrawLine(frontPen,
                                    graphBorder.Left + viewWidth * view, zLowLimit,
                                    graphBorder.Left + viewWidth * (view + 1), zLowLimit);

                                e.Graphics.DrawString(lbl, axisLblFont, captionBrush,
                                    graphBorder.Left + viewWidth * view + lblSize.Height, zLowLimit + lblSize.Height / 2);
                            }
                        }
                        

                        if (timer.IsRunning)
                        {
                            frontZcoordinate = graphBorder.Top + Convert.ToSingle(zcoords[view] * zscale);
                            frontZcoordinate = frontZcoordinate > graphBorder.Bottom ? graphBorder.Bottom : frontZcoordinate;
                            frontZcoordinate = frontZcoordinate < graphBorder.Top ? graphBorder.Top : frontZcoordinate;

                            int frontAlpha = 255;
                            float fst;
                            float fnd;
                            for (int i = 0; i < frontSegments - 1; i++)
                            {
                                fst = frontZcoordinate - dir * i * frontSegmentSize;
                                fnd = frontZcoordinate - dir * (i + 1) * frontSegmentSize;

                                fst = fst > zLowLimit ? zLowLimit : fst;
                                fst = fst < graphBorder.Top ? graphBorder.Top : fst;
                                fnd = fnd > zLowLimit ? zLowLimit : fnd;
                                fnd = fnd < graphBorder.Top ? graphBorder.Top : fnd;

                                e.Graphics.DrawLine(new Pen(Color.FromArgb(frontAlpha, frontColor), fronPenWidth),
                                    viewCenter, fst,
                                    viewCenter, fnd);

                                frontAlpha -= dAlpha;                                
                            }
                        }
                    }
                   
                    lbl = string.Format(CultureInfo.InvariantCulture, "t = {0:F02} s", t);
                    lblSize = e.Graphics.MeasureString(lbl, axisLblFont);

                    e.Graphics.DrawString(lbl, axisLblFont, axisLblBrush,
                        graphBorder.Left + graphBorder.Width / 2.0f - lblSize.Width / 2,
                        graphBorder.Bottom);

                }
            }
        }

        private void VerticalSoundPropagationPlot_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        #endregion

        #region Events

        public EventHandler SimulationStartedEvent;
        public EventHandler SimulationFinishedEvent;

        public EventHandler SimulationStepEvent;

        #endregion
    }
}
