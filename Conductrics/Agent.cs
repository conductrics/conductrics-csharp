using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;

namespace Conductrics {
	public static class API {
		public static string Owner;
		public static string Key;
		public static string BaseUrl = "http://api.conductrics.com";
	}
	public class SimpleDecision {
		public string session;
		public string decision;
	}
	public class Agent {
		public string name;
		public Agent(string name) {
			this.name = name;
		}
		public string Decide( string[] choices ) {
			return Decide(choices, "");
		}
		public string Decide( string[] choices, string session) {
			// http://api.conductrics.com/owner_sxvgyHUlj/agent-1/decision/decision-1/4?apikey=api-nQyALpnyPsZHQrVbvtOhZpYz
			string url = API.BaseUrl + "/"
				+ API.Owner + "/"
				+ this.name + "/decision/"
				+ choices.Length
				+ "?apikey=" + API.Key;
			var http = (HttpWebRequest)WebRequest.Create(url);
			http.KeepAlive = false;
			http.Timeout = 1000;
			http.Headers.Add("x-mpath-apikey", API.Key);
			if( session != null && session.Length > 0 )
				http.Headers.Add("x-mpath-session", session);
			Debug.Print("Headers added, beginning request... {0}", url);
			try {
				var response = http.GetResponse();
				Debug.Print("Have response.");
				var stream = response.GetResponseStream();
				string content = new StreamReader(stream).ReadToEnd();
				Debug.Print("Finished reading response.");
				JavaScriptSerializer ser = new JavaScriptSerializer();
				SimpleDecision d = ser.Deserialize<SimpleDecision>(content);
				return d.decision;
			} catch( Exception e ) {
				Debug.Print(e.ToString());
				return choices[0];
			}
		}
	}
}
