using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Conductrics;

namespace DemoApp {
	public partial class Form1 : Form {
		public Form1() {
			InitializeComponent();
		}

		private void Form1_Load( object sender, EventArgs e ) {
			Conductrics.API.Key = "api-nQyALpnyPsZHQrVbvtOhZpYz";
			Conductrics.API.Owner = "owner_sxvgyHUlj";
			Agent agent = new Agent("agent-dotnet");
			textBox1.Text = agent.Decide(new string[] { "a", "b" });
		}

	}
}
