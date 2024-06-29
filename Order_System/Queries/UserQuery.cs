namespace Order_System.Queries;

public class UserQuery
{
    #region Get User List Query
    public static string GetUserListQuery()
    {
        return @"SELECT UserId, FirstName, LastName, Email, PhoneNo, Password, IsActive 
FROM Users
WHERE IsActive = @IsActive";
    }

    #endregion

    #region Get User List By UserId Query

    public static string GetUserListByUserIdQuery()
    {
        return @"SELECT UserId, FirstName, LastName, Email, PhoneNo, Password, IsActive 
FROM Users
WHERE IsActive = @IsActive AND UserId = @UserId";
    }

    #endregion

    #region CheckUpdateUserDuplicateQuery

    public static string CheckUpdateUserDuplicateQuery()
    {
        return @"SELECT [UserId]
      ,[FirstName]
      ,[LastName]
      ,[IsActive]
  FROM [dbo].[Users] WHERE FirstName = @FirstName AND LastName = @LastName AND
IsActive = @IsActive AND
UserId != @UserId";
    }

    #endregion

    #region UpdateUserQuery

    public static string UpdateUserQuery()
    {
        return @"UPDATE Users SET FirstName = @FirstName, LastName = @LastName WHERE
UserId = @UserId";
    }

    #endregion


    #region Delete User Query

    public static string DeleteUserQuery()
    {
        return @"UPDATE Users SET IsActive = @IsActive WHERE UserId = @UserId";
    }

    #endregion
}
