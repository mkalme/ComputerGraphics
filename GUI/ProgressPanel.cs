using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace GUI {
    public partial class ProgressPanel : UserControl {
        private Graphics Graphics { get; set; }

        public ProgressPanel(Graphics graphics)
        {
            InitializeComponent();

            Graphics = graphics;
            Visible = false;
        }

        public void UpdatePanel(object instance)
        {
            MethodInfo method = instance.GetType().GetMethod("DisplayProgress",
            BindingFlags.NonPublic | BindingFlags.Instance);
            string[] lines = (string[])method.Invoke(instance, new object[] { });

            LinesRichtextBox.Lines = lines;

            SizeF sizeF = Graphics.MeasureString(LinesRichtextBox.Text, LinesRichtextBox.Font);
            Size = new Size((int)sizeF.Width + 3, (int)sizeF.Height + 14);

            Visible = true;
        }

        private static SizeF Calibrate(SizeF size) {
            int m = 35;

            float resw = (size.Width % m);
            if (resw == 0) resw = 10;

            float w = m - resw + size.Width;

            return new SizeF(w, size.Height);
        }
    }
}
