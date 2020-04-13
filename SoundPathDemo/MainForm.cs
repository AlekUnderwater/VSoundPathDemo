using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using UCNLDrivers;
using UCNLPhysics;

namespace SoundPathDemo
{
    public partial class MainForm : Form
    {
        #region Properties

        string snapshotsPath;

        TSProfilePoint[] tsp;

        double v_surface = 1450.0;
        double v_mean = 1450.0;
        double g = PHX.PHX_GRAVITY_ACC_MPS2;

        double Latitude
        {
            get { return Convert.ToDouble(latEdit.Value); }
            set { TrySetNumericEditValue(latEdit, value); }
        }

        int snapshotNumber = 0;

        #endregion

        #region Constrcutor

        public MainForm()
        {
            InitializeComponent();

            vProfileView.ProfileChangedEvent += (o, e) =>
                {
                    vProfileView.Invalidate();
                    verticalPropagationPlot.Zmin = vProfileView.Zmin;
                    verticalPropagationPlot.Zmax = vProfileView.Zmax;
                    verticalPropagationPlot.ZTicks = vProfileView.ZTicks;
                    verticalPropagationPlot.Invalidate();

                    tsp = vProfileView.GetProfile();
                    UpdateProfileDependant();
                };

            tspDemoNPacificToolStripMenuItem_Click(null, null);
            verticalPropagationPlot.SimulationFinishedEvent += (o, e) =>
                {
                    animationBtn.Checked = false;
                    animationBtn.Enabled = true;
                    parametersGroup.Enabled = true;
                    profilesBtn.Enabled = true;
                    isAutoscreenshotBtn.Enabled = true;
                };

            verticalPropagationPlot.AddItem("Std. fresh water", (x) => PHX.PHX_FWTR_SOUND_SPEED_MPS);
            verticalPropagationPlot.AddItem("Surface", (x) => v_surface);
            verticalPropagationPlot.AddItem("Mean", (x) => v_mean);
            verticalPropagationPlot.AddItem("Σ", (x) => getVByProfile(x));

            verticalPropagationPlot.SimulationStepEvent += new EventHandler(verticalPropagationPlot_SimulationStep);

        }

        #endregion

        #region Methods

        private void SaveFullScreenshot()
        {
            Bitmap target = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(target, this.DisplayRectangle);

            try
            {
                if (!Directory.Exists(snapshotsPath))
                    Directory.CreateDirectory(snapshotsPath);

                target.Save(
                    Path.Combine(snapshotsPath, 
                    string.Format("{0}.{1}", snapshotNumber, ImageFormat.Png)),
                    ImageFormat.Png);

                snapshotNumber++;
            }
            catch
            {
                //
            }
        }
        
        private void TrySetNumericEditValue(NumericUpDown nedit, double value)
        {
            Decimal val = Convert.ToDecimal(value);
            val = val > nedit.Maximum ? nedit.Maximum : val;
            val = val < nedit.Minimum ? nedit.Minimum : val;
            latEdit.Value = val;
        }

        private void ApplyProfile(TSProfile profile)
        {
            Latitude = profile.LatitudeDeg;
            vProfileView.Caption = profile.ToString();
            vProfileView.SetProfile(profile.Profile);
            verticalPropagationPlot.ClearDistance();
        }

        private double getVByProfile(double z)
        {
            double t, p, s, v, rho0;

            if (z < tsp[tsp.Length - 1].Z)
            {
                int idx1 = 0, idx2 = tsp.Length - 1;

                while ((tsp[idx1].Z < z) && (idx1 < tsp.Length - 1))
                {
                    idx1++;
                }

                while ((z <= tsp[idx2].Z) && (idx2 > idx1 + 1))
                {
                    idx2--;
                }


                double z1 = tsp[idx1].Z;
                double t1 = tsp[idx1].T;
                double s1 = tsp[idx1].S;
                rho0 = PHX.Water_density_calc(t1, PHX.PHX_ATM_PRESSURE_MBAR, s1);
                double p1 = PHX.Pressure_by_depth_calc(z1, PHX.PHX_ATM_PRESSURE_MBAR, rho0, g);

                double z2 = tsp[idx2].Z;
                double t2 = tsp[idx2].T;
                double s2 = tsp[idx2].S;
                double p2 = PHX.Pressure_by_depth_calc(z2, PHX.PHX_ATM_PRESSURE_MBAR, rho0, g);


                t = PHX.Linterp(z1, t1, z2, t2, z);
                p = PHX.Linterp(z1, p1, z2, p2, z);
                s = PHX.Linterp(z1, s1, z2, s2, z);               
            }
            else
            {
                t = tsp[tsp.Length - 1].T;
                s = tsp[tsp.Length - 1].S;
                rho0 = PHX.Water_density_calc(t, PHX.PHX_ATM_PRESSURE_MBAR, s);
                p = PHX.Pressure_by_depth_calc(z, PHX.PHX_ATM_PRESSURE_MBAR, rho0, g);
            }

            v = PHX.Speed_of_sound_UNESCO_calc(t, p, s);

            return v;
        }

        private void UpdateProfileDependant()
        {
            if (tsp.Length > 2)
            {
                v_surface = PHX.Speed_of_sound_UNESCO_calc(tsp[0].T, PHX.PHX_ATM_PRESSURE_MBAR, tsp[0].S);
                double t_mean = 0, s_mean = 0;

                for (int i = 0; i < tsp.Length; i++)
                {
                    t_mean += tsp[i].T;
                    s_mean += tsp[i].S;
                }

                t_mean /= tsp.Length;
                s_mean /= tsp.Length;

                v_mean = PHX.Speed_of_sound_UNESCO_calc(t_mean, PHX.PHX_ATM_PRESSURE_MBAR, s_mean);
            }            
        }

        #endregion

        #region Handlers

        private void loadProfileBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog oDialog = new OpenFileDialog())
            {
                oDialog.Title = "Select a CSV-file containing TS-profile...";
                oDialog.Filter = "CSV (*.csv)|*.csv";

                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    try
                    {
                        var tsProfile = TSProfile.LoadFromFile(oDialog.FileName);
                        ApplyProfile(tsProfile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void isAutoscreenshotBtn_Click(object sender, EventArgs e)
        {
            verticalPropagationPlot.IsSimulationStepEvent = isAutoscreenshotBtn.Checked;
        }

        private void animationBtn_Click(object sender, EventArgs e)
        {
            animationBtn.Checked = true;
            animationBtn.Enabled = false;
            parametersGroup.Enabled = false;
            profilesBtn.Enabled = false;
            isAutoscreenshotBtn.Enabled = false;
            snapshotNumber = 0;

            if (verticalPropagationPlot.IsSimulationStepEvent)
            {
                snapshotsPath = Path.Combine(StrUtils.GetTimeDirTree(Application.ExecutablePath, "SNAPSHOTS", false), StrUtils.GetHMSString());
            }

            verticalPropagationPlot.Start();
        }

        private void pTimeEdit_ValueChanged(object sender, EventArgs e)
        {
            verticalPropagationPlot.FullPropagationTime = Convert.ToDouble(pTimeEdit.Value);
        }

        private void tspDemoNPacificToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyProfile(DemoProfiles.PacificN39);
        }

        private void tspDemoSAtlanticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyProfile(DemoProfiles.AltanticS20);
        }

        private void tspDemoArcticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplyProfile(DemoProfiles.ArcticN89);
        }

        private void latEdit_ValueChanged(object sender, EventArgs e)
        {
            g = PHX.Gravity_constant_wgs84_calc(Latitude);
        }

        private void verticalPropagationPlot_SimulationStep(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { SaveFullScreenshot(); });
            else
                SaveFullScreenshot();
        }

        

        #endregion                       

        private void infoBtn_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/AlekUnderwater/VSoundPathDemo");
        }
    }
}
