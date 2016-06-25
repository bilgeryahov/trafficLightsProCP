using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrafficLights.Properties;

namespace TrafficLights
{
    public partial class Manual : Form
    {
        public Manual()
        {
            InitializeComponent();
            FillRichText();
        }

        public void FillRichText()
        {
            var content = Resources.manual;
            rtbxManual.Text = content;

        }
    }
}
