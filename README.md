C# Wrapper for the Conductrics API.

Install
-------

    git clone git@github.com:conductrics/conductrics-csharp.git

Add a Project Reference to the Conductrics.csproj within.


Code
----


Set your credentials.

    Conductrics.API.Key = "...";
    Conductrics.API.Owner = "...";


Create one or more agents.

    Conductrics.Agent sortAgent = new Conductrics.Agent("sample-agent");


Make a choice between any number/type of things.

    SortOrder order = sortAgent.Decide<SortOrder>(sessionId, SortOrder.Ascending, SortOrder.Descending);


Send a reward when a session reaches one of your application's goals.

    sortAgent.Reward(sessionId, value: 11.99);

The call to Decide() will begin to learn which decisions maximize the Reward().
