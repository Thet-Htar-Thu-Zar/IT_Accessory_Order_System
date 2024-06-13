namespace Order_System.Queries
{
    public static class CartQuery
    {
        #region Get Cart List Query
        public static string GetCartListQuery()
        {
            return @"SELECT Cart.CartId, IT_accessories.AccessoryName,IT_accessories.AccessoryUnitPrice, Cart.Quantity,
 Cart.IsActive
FROM Cart
INNER JOIN Users ON Cart.UserId = Users.UserId
INNER JOIN IT_accessories ON Cart.AccessoryId = IT_accessories.AccessoryId
WHERE Cart.IsActive = @IsActive
ORDER BY CartId DESC"; 
        }

        #endregion

        #region Get Cart List By UserId Query

        public static string GetIncomeListByUserIdQuery()
        {
            return @"SELECT Cart.CartId, IT_accessories.AccessoryName,IT_accessories.AccessoryUnitPrice, Cart.Quantity,
 Cart.IsActive
FROM Cart
INNER JOIN Users ON Cart.UserId = Users.UserId
INNER JOIN IT_accessories ON Cart.AccessoryId = IT_accessories.AccessoryId
WHERE Cart.IsActive = @IsActive AND Cart.UserId = @UserId
ORDER BY CartId DESC";
        }

        #endregion


        #region Create Cart Query

        public static string CreateCartQuery()
        {
            return @"INSERT INTO Cart (AccessoryId, UserId,AccessoryName, Quantity, IsActive)
VALUES (@AccessoryId, @UserId,@AccessoryName, @Quantity, @IsActive)";
        }

        #endregion
    }
}
