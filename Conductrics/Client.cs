using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;

namespace Conductrics {
	public class Client {
		string key;
		string owner;
		string baseUrl;
		public Client(string ownerCode) {
			this.baseUrl = "http://api.conductrics.com/";
			this.owner = ownerCode;
		}
		public Client(string url, string ownerCode) {
			this.baseUrl = url;
			this.owner = ownerCode;
		}
		public Client apiKey( string key ) {
			this.key = key;
			return this;
		}
		public string GetDecision( string agentCode, string[] choices ) {
			return GetDecision(agentCode, choices, "");
		}
		public string GetDecision(string agentCode, string[] choices, string session) {
			// http://api.conductrics.com/owner_sxvgyHUlj/agent-1/decision/decision-1/4?apikey=api-nQyALpnyPsZHQrVbvtOhZpYz
			string url = baseUrl + "/" + owner + "/" + agentCode + "/decision/decision-1/" + choices.Length;
			var http = (HttpWebRequest)WebRequest.Create(url);
			http.Headers.Add("mpath-apikey", this.key);
			if( session != null && session.Length > 0 )
				http.Headers.Add("mpath-session", session);
			string content = new StreamReader(http.GetResponse().GetResponseStream()).ReadToEnd();
			JavaScriptSerializer ser = new JavaScriptSerializer();
			object o = ser.DeserializeObject(content);
			Debug.Print(o.ToString());
			return o.ToString();
		}
	}
}
