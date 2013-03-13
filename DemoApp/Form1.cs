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
			string sessionId = Guid.NewGuid().ToString();
			label3.Text = "Session: " + sessionId;
			Agent agent = new Agent("agent-dotnet")
				.setKey("api-nQyALpnyPsZHQrVbvtOhZpYz")
				.setOwner("owner_sxvgyHUlj");
			label2.Text = agent.Decide<string>(sessionId, "a", "b");
		}

	}
}
