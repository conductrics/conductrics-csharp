
C# Wrapper for the Conductrics API.

Install
-------

	Clone this project.

    git clone git@github.com:conductrics/conductrics-csharp.git

	In your own project, add a reference to the Conductrics project within.

Code
----

    Conductrics.API.Key = "...";
    Conductrics.API.Owner = "...";
    Conductrics.Agent sortAgent = new Conductrics.Agent("sample-agent");
		string sessionId = Guid.NewGuid().ToString()
    SortOrder order = fileSortAgent.Decide<SortOrder>(sessionId, SortOrder.Ascending, SortOrder.Descending);
