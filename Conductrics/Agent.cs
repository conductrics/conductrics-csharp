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
		public string Name;
		public Agent(string name ) {
			this.Name = name;
		}

		private string _apiKey = null;
		public string Key {
			get { return _apiKey == null ? Conductrics.API.Key : _apiKey; }
			set { _apiKey = value; }
		}
		public Agent setKey( string apiKey ) {
			_apiKey = apiKey;
			return this;
		}

		private string _baseUrl = null;
		public string Url {
			get { return _baseUrl == null ? Conductrics.API.BaseUrl: _baseUrl; }
			set { _baseUrl = value; }
		}
		public Agent setUrl( string url ) {
			_baseUrl = url;
			return this;
		}


		private string _ownerCode = null;
		public string Owner {
			get { return _ownerCode == null ? Conductrics.API.Owner: _ownerCode; }
			set { _ownerCode = value; }
		}
		public Agent setOwner( string ownerCode ) {
			_ownerCode = ownerCode;
			return this;
		}

		private HttpWebRequest mpathRequest( string url, string session ) {
			var http = (HttpWebRequest)WebRequest.Create(url);
			http.CachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
			http.Pipelined = true;
			http.KeepAlive = false;
			http.Timeout = 1000;
			http.Headers.Add("x-mpath-apikey", this.Key);
			if( session != null && session.Length > 0 )
				http.Headers.Add("x-mpath-session", session);
			return http;
		}
		public T Decide<T>( string session, params T[] choices) {
			// http://api.conductrics.com/owner_sxvgyHUlj/agent-1/decision/decision-1/4?apikey=api-nQyALpnyPsZHQrVbvtOhZpYz
			if( choices.Length == 0 )
				return default(T);
			string url = this.Url + "/"
				+ this.Owner + "/"
				+ this.Name + "/decision/"
				+ choices.Length;
			var http = mpathRequest(url, session);
			try {
				var response = http.GetResponse();
				string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
				response.Close();
				SimpleDecision d = new JavaScriptSerializer().Deserialize<SimpleDecision>(content);
				int index = int.Parse(d.decision);
				if( index > 0 && index < choices.Length )
					return choices[index];
			} catch( Exception e ) {
				Debug.Print(e.ToString());
			}
			return choices[0];
		}
		public void Expire( string session ) {
			string url = this.Url + "/"
				+ this.Owner + "/"
				+ this.Name + "/expire";
			try {
				mpathRequest(url, session).GetResponse().Close();
			} catch( Exception e ) {
				Debug.Print(e.ToString());
			}
		}
		public void Reward( string session, string goalCode = "goal-1", double value = 1.0 ) {
			string url = this.Url + "/"
				+ this.Owner + "/"
				+ this.Name + "/goal";
			if( goalCode != "goal-1" )
				url += "/" + goalCode;
			if( value != 1.0 )
				url += "?reward=" + String.Format("{0:0.00}", value);
			try {
				mpathRequest(url, session).GetResponse().Close();
			} catch( Exception e ) {
				Debug.Print(e.ToString());
			}
		}
	}
}
