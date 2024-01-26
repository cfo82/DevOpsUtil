namespace GitLabApiClient.Internal.Paths;

using System;
using GitLabApiClient.Models.Users.Responses;

public class UserId
{
    private readonly string _id;

    private UserId(string userId) => _id = userId;

    public static implicit operator UserId(int userId)
    {
        return new UserId(userId.ToString());
    }

    public static implicit operator UserId(User user)
    {
        int id = user.Id;
        if (id > 0)
        {
            return new UserId(id.ToString());
        }

        throw new ArgumentException("Cannot determine user id from provided User instance.");
    }

    public override string ToString()
    {
        return _id;
    }
}
