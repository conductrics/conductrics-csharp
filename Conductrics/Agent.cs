using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Web.Script.Serialization;
using System.Net.Cache;

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
		public Agent(string name ) {
			this.name = name;
		}
		private HttpWebRequest mpathRequest( string url, string session ) {
			var http = (HttpWebRequest)WebRequest.Create(url);
			http.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
			http.Pipelined = true;
			http.KeepAlive = false;
			http.Timeout = 1000;
			http.Headers.Add("x-mpath-apikey", API.Key);
			if( session != null && session.Length > 0 )
				http.Headers.Add("x-mpath-session", session);
			return http;
		}
		public void Reward( string session, string goalCode = "goal-1", double value = 1.0 ) {
			string url = API.BaseUrl + "/"
				+ API.Owner + "/"
				+ this.name + "/goal/"
				+ goalCode
				+ "?apikey=" + API.Key
				+ "&reward=" + String.Format("{0:0.00}", value);
			var http = mpathRequest(url, session);
			try {
				var response = http.GetResponse();
				response.Close();
			} catch( Exception e ) {
				Debug.Print(e.ToString());
			}

		}
		public T Decide<T>( string session, params T[] choices) {
			// http://api.conductrics.com/owner_sxvgyHUlj/agent-1/decision/decision-1/4?apikey=api-nQyALpnyPsZHQrVbvtOhZpYz
			string url = API.BaseUrl + "/"
				+ API.Owner + "/"
				+ this.name + "/decision/"
				+ choices.Length
				+ "?apikey=" + API.Key;
			Debug.Print("Headers added, beginning request... {0}", url);
			var http = mpathRequest(url, session);
			try {
				var response = http.GetResponse();
				var stream = response.GetResponseStream();
				string content = new StreamReader(stream).ReadToEnd();
				JavaScriptSerializer ser = new JavaScriptSerializer();
				SimpleDecision d = ser.Deserialize<SimpleDecision>(content);
				int index = int.Parse(d.decision);
				if( index > 0 && index < choices.Length )
					return choices[index];
			} catch( Exception e ) {
				Debug.Print(e.ToString());
			}
			return choices[0];
		}
	}
}
