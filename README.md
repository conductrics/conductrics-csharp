
C# Wrapper for the Conductrics API.

Usage:
	Clone this project.
	In your own project, add a reference to it.

Code:
    Conductrics.API.Key = "...";
    Conductrics.API.Owner = "...";
    Conductrics.Agent sortAgent = new Conductrics.Agent("sample-agent");
		string sessionId = Guid.NewGuid().ToString()
    SortOrder order = fileSortAgent.Decide<SortOrder>(sessionId, SortOrder.Ascending, SortOrder.Descending);
