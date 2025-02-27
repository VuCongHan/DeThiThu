using DeThiThu;
using static DeThiThu.OrderService;

namespace OrderServiceTests
{
    public class Tests
    {
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _orderService = new OrderService();
        }

        [Test]
        public void CalculateTotalAmount_ShouldReturnCorrectTotal_WhenOrderHasMultipleItems()
        {
            // Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductName = "Item1", Quantity = 2, Price = 100 },
                    new OrderItem { ProductName = "Item2", Quantity = 1, Price = 200 }
                }
            };

            // Act
            var total = _orderService.CalculateTotalAmount(order);

            // Assert
            Assert.That(total, Is.EqualTo(400));
        }

        [Test]
        public void CalculateTotalAmount_ShouldReturnZero_WhenOrderHasNoItems()
        {
            // Arrange
            var order = new Order(); // Danh sách Items mặc định là rỗng

            // Act
            var total = _orderService.CalculateTotalAmount(order);

            // Assert
            Assert.That(total, Is.EqualTo(0));
        }

        [Test]
        public void CalculateTotalAmount_ShouldHandleItemsWithZeroPrice()
        {
            // Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductName = "Item1", Quantity = 2, Price = 0 }
                }
            };

            // Act
            var total = _orderService.CalculateTotalAmount(order);

            // Assert
            Assert.That(total, Is.EqualTo(0));
        }

        [Test]
        public void CalculateTotalAmount_ShouldThrowException_WhenOrderIsNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _orderService.CalculateTotalAmount(null));
        }

        [Test]
        public void CalculateTotalAmount_ShouldThrowException_WhenOrderHasNullItems()
        {
            // Arrange
            var order = new Order { Items = null };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _orderService.CalculateTotalAmount(order));
        }

        [Test]
        public void CalculateTotalAmount_ShouldHandleNegativeQuantity()
        {
            // Arrange
            var order = new Order
            {
                Items = new List<OrderItem>
                {
                    new OrderItem { ProductName = "Item1", Quantity = -2, Price = 100 }
                }
            };

            // Act
            var total = _orderService.CalculateTotalAmount(order);

            // Assert
            Assert.That(total, Is.EqualTo(-200)); // Vì không có kiểm tra số âm trong logic gốc
        }
    }
}