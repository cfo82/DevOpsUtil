namespace DevOpsUtil.Github.PullRequests.Services;

using System.Collections.Immutable;
using DevOpsUtil.Github.PullRequests.Contracts;

public class PullRequest : IPullRequest
{
    public PullRequest(
        IRepository repository,
        Octokit.PullRequest pullRequest,
        IReadOnlyList<Octokit.PullRequestReview> githubReviews)
    {
        Id = pullRequest.Id;
        Number = pullRequest.Number;
        Title = pullRequest.Title;
        WebUrl = pullRequest.HtmlUrl;
        Repository = repository;

        var reviewers = new List<IReviewer>();
        foreach (var review in githubReviews)
        {
            reviewers.Add(new Reviewer(review.User, review.State.Value == Octokit.PullRequestReviewState.Approved));
        }

        Reviewers = reviewers.ToImmutableArray();
    }

    public long Id { get; }

    public int Number { get; }

    public string Title { get; }

    public string WebUrl { get; }

    public IRepository Repository { get; }

    public IEnumerable<IReviewer> Reviewers { get; }
}