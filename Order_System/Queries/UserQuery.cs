﻿namespace Order_System.Queries
{
    public class UserQuery
    {
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
            return @"UPDATE Users SET FirstName = @FirstName, LastName = @LastName   WHERE
UserId = @UserId";
        }

        #endregion

    }
}