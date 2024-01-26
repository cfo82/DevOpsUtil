using System.Net.Http.Headers;
using Gitlab.GraphQL;
using Microsoft.Extensions.DependencyInjection;
using StrawberryShake;

var serviceCollection = new ServiceCollection();
serviceCollection
    .AddGitlabClient()
    .ConfigureHttpClient(client =>
    {
        client.BaseAddress = new Uri("https://gitlab.com/api/graphql");
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", "xxx");
    });

IServiceProvider services = serviceCollection.BuildServiceProvider();

var client = services.GetRequiredService<IGitlabClient>();

var result = await client.QueryMergeRequests.ExecuteAsync("xxx");

result.EnsureNoErrors();

foreach (var project in result.Data?.Group?.Projects.Nodes ??
                        new List<IQueryMergeRequests_Group_Projects_Nodes>())
{
    foreach (var mergeRequest in project?.MergeRequests?.Nodes ??
                                 new List<IQueryMergeRequests_Group_Projects_Nodes_MergeRequests_Nodes>())
    {
        Console.WriteLine($"Merge-Request '{mergeRequest?.Title ?? string.Empty}'");
        Console.WriteLine($"  Project: {project?.Name ?? string.Empty}");
        Console.WriteLine($"  Author: {mergeRequest?.Author?.Name ?? string.Empty}");
        foreach (var assignee in mergeRequest?.Assignees?.Nodes ??
                                 new List<
                                     IQueryMergeRequests_Group_Projects_Nodes_MergeRequests_Nodes_Assignees_Nodes>())
        {
            Console.WriteLine($"  Assignee: {assignee?.Name ?? string.Empty}");
        }

        foreach (var reviewer in mergeRequest?.Reviewers?.Nodes ??
                                 new List<IQueryMergeRequests_Group_Projects_Nodes_MergeRequests_Nodes_Reviewers_Nodes>())
        {
            Console.WriteLine($"  Reviewer: {reviewer?.Username ?? string.Empty}");
        }
    }
}