using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DangNhapShoppeeMSTest
{
    [TestClass]
    public class SeleniumMSTest
    {
        private IWebDriver driver = null!;

        [TestInitialize]
        public void Setup()
        {
            var options = new ChromeOptions();

            // 👉 Mở Chrome với Profile đã đăng nhập sẵn
            options.AddArgument(@"user-data-dir=C:\Users\vuhan\AppData\Local\Google\Chrome\User Data");
            options.AddArgument("--profile-directory=Profile 5");  // Nếu dùng profile khác, đổi thành "Profile 1", "Profile 2"

            // 👉 Giả lập trình duyệt thật để tránh Google phát hiện
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);

            // 👉 Khởi tạo ChromeDriver với cấu hình trên
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void Test_GoogleLoginShopee()
        {
            // 1️⃣ Mở trang Đăng nhập Shopee
            driver.Navigate().GoToUrl("https://shopee.vn/buyer/login?next=https%3A%2F%2Fshopee.vn%2F");

            Thread.Sleep(5000);  // Chờ 5 giây để Google load trang

            var emailInput = driver.FindElement(By.Name("loginKey"));
            emailInput.SendKeys("vuconghan1908@gmail.com");

            Thread.Sleep(5000);  // Chờ 5 giây để hiển thị nút "Tiếp tục"

            var passwordInput = driver.FindElement(By.Name("password"));
            passwordInput.SendKeys("Vuhan1401");

            Thread.Sleep(5000);  // Chờ 5 giây để Google xử lý đăng nhập

            var loginButton = driver.FindElement(By.CssSelector("button.b5aVaf.PVSuiZ.Gqupku.qz7ctP.qxS7lQ.Q4KP5g"));
            loginButton.Click();

            Thread.Sleep(5000);

            try
            {
                // Kiểm tra nếu có thông báo yêu cầu đăng nhập bằng ứng dụng
                var appLoginPrompt = driver.FindElements(By.XPath("//div[contains(text(), 'Vui lòng đăng nhập trên ứng dụng Shopee')]"));
                if (appLoginPrompt.Count > 0)
                {
                    Console.WriteLine("⚠️ Shopee yêu cầu đăng nhập bằng ứng dụng. Tài khoản vẫn đã đăng nhập thành công!");
                }

                // Kiểm tra phần tử xác nhận đăng nhập thành công
                var userProfile = driver.FindElements(By.XPath("//div[contains(@class, 'navbar__username')]"));
                var logoutButton = driver.FindElements(By.XPath("//button[contains(text(), 'Đăng xuất')]"));

                if (userProfile.Count > 0 || logoutButton.Count > 0)
                {
                    Console.WriteLine("🎉 Đăng nhập Shopee thành công bằng Google!");
                    logoutButton[0].Click();
                }
                else
                {
                    Assert.Fail("❌ Đăng nhập thất bại! Không tìm thấy thông tin tài khoản hoặc nút đăng xuất.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("❌ Không tìm thấy phần tử xác nhận đăng nhập! Đăng nhập có thể đã thất bại.");
                Assert.Fail("❌ Đăng nhập thất bại! Không tìm thấy phần tử xác nhận.");
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            driver.Quit();
        }
    }
}
