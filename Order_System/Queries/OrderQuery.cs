//namespace Order_System.Queries
//{
//    public class OrderQuery
//    {
//        #region Get Order List Query

//        public static string GetOrderListQuery()
//        {
//            return @"SELECT Order.orderId, Users.UserId, Order.OrderNo, Order_item.AccessoryName,
//        Order.OrderStatus
//        FROM Order
//        INNER JOIN Users ON Order.UserId = Users.UserId
//        INNER JOIN Order_item ON Order.OrderId = Order_item.OrderId
//        ORDER BY OrderId DESC";
//        }

//        #endregion

//        #region Get Order List By UserId Query

//        public static string GetIncomeListByUserIdQuery()
//        {
//            return @"SELECT Order.orderId, Users.UserId, Order.OrderNo, Order_item.AccessoryName,
//Order.OrderStatus
//FROM Order
//INNER JOIN Users ON Order.UserId = Users.UserId
//INNER JOIN Order_item ON Order.OrderId = Order_item.OrderId
//WHERE Order.UserId = @UserId
//ORDER BY OrderId DESC";
//        }
//    }
//}
    

//        #endregion



