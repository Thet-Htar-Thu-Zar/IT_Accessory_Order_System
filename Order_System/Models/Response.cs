using Order_System.Models.User;

namespace Order_System.Models
{
    public class Response
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<UserModel> listUsers { get; set; }
        public UserModel users { get; set; }

        public List<LoginRequestModel> loginRequests { get; set; }
        public LoginRequestModel loginRequest { get; set; }
        public List<RegisterRequestModel> registerRequests { get; set; }
        public RegisterRequestModel registerRequest { get; set; }

        public List<IT_accessory> listIT_accessory { get; set; }
        public IT_accessory accessory { get; set; }
        public List<Cart> listCart { get; set; }
        public Cart cart { get; set; }
        public List<Order> listOrder { get; set; }
        public Order order { get; set; }
        public List<Order_item> listOrder_item { get; set; }
        public Order_item item { get; set;}
    }
}
