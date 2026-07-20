

namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            new List<Customer>
            {
                Customer.Create(CustomerId.Of(new Guid("b8d4f6c2-3e1a-4f7b-9c8d-2e5f7a1b9c3d")), "Jitesh", "jitesh@gmail.com"),
                Customer.Create(CustomerId.Of(new Guid("b8d4f6c2-3e1a-4f7b-9c8d-2e5f7a1b9c9a")), "Sam", "sam@gmail.com"),
            };


        public static IEnumerable<Product> Products =>
            new List<Product>
            {
                Product.Create(ProductId.Of(new Guid("b8d4f6c2-3e1a-4f7b-9c8d-2e5f7a1b9c5a")),"IPhone X", 500),
                Product.Create(ProductId.Of(new Guid("b8d4f6c2-3e1a-4f7b-9c8d-2e5f7a1b9c2b")),"Samsung 10", 400),
                Product.Create(ProductId.Of(new Guid("b8d4f6c2-3e1a-4f7b-9c8d-2e5f7a1b9c0c")),"Huawei Plus", 650),
                Product.Create(ProductId.Of(new Guid("b8d4f6c2-3e1a-4f7b-9c8d-2e5f7a1b9c7d")),"Xiaomi Mi", 450),
            };

        public static IEnumerable<Order> OrdersWithItems
        {
            get {

                var address1 = Address.Of("Jitesh", "Mahajan", "jitesh@gmail.com", "Rambaug, Kalyan", "Mumbai", "India", "45432");
                var address2 = Address.Of("Sam", "doe", "sam@gmail.com", "sector 4 road 4", "Panvel, New Mumbai", "India", "43785");

                var payment1 = Payment.Of("Jitesh", "56784389243434", "12/28", "355", 1);
                var payment2 = Payment.Of("Sam", "653562624562634", "06/30", "222", 2);


                var order1 = Order.Create(
                                        OrderId.Of(Guid.NewGuid()),
                                        CustomerId.Of(new Guid("b8d4f6c2-3e1a-4f7b-9c8d-2e5f7a1b9c3d")),
                                        OrderName.Of("ORD_1"),
                                        shippingAddress: address1,
                                        billingAddress: address1,
                                        payment1
                                        );


                order1.Add(ProductId.Of(new Guid("b8d4f6c2-3e1a-4f7b-9c8d-2e5f7a1b9c5a")), 2, 500);
                order1.Add(ProductId.Of(new Guid("b8d4f6c2-3e1a-4f7b-9c8d-2e5f7a1b9c2b")), 1, 400);
                    

                var order2 = Order.Create(
                                        OrderId.Of(Guid.NewGuid()),
                                        CustomerId.Of(new Guid("b8d4f6c2-3e1a-4f7b-9c8d-2e5f7a1b9c9a")),
                                        OrderName.Of("ORD_2"),
                                        shippingAddress: address2,
                                        billingAddress: address2,
                                        payment2
                                        );


                order2.Add(ProductId.Of(new Guid("b8d4f6c2-3e1a-4f7b-9c8d-2e5f7a1b9c0c")), 1, 650);
                order2.Add(ProductId.Of(new Guid("b8d4f6c2-3e1a-4f7b-9c8d-2e5f7a1b9c7d")), 2, 850);

                return new List<Order> { order1, order2 };
            }
        }
    }
}
