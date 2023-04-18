using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using static System.Net.WebRequestMethods;

namespace Data_Driven_Automation_Tests
{
    public class DataDrivenWebTests
    {
        private WebDriver driver;
        private const string baseUrl = 
            "http://softuni-qa-loadbalancer-2137572849.eu-north-1.elb.amazonaws.com/number-calculator/";
         private ChromeOptions options;

        IWebElement searchField1;
        IWebElement searchField2;  
        IWebElement operationOption;
        IWebElement result;
        IWebElement resultField;
        

        [SetUp]
        public void OpenBrowser()
        {
            options = new ChromeOptions();
            options.AddArgument("--headless");
            driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            driver.Url = baseUrl;

            searchField1 = driver.FindElement(By.Id("number1"));
            searchField2 = driver.FindElement(By.Id("number2"));
            operationOption = driver.FindElement(By.XPath("//select[contains(@id,'operation')]"));
            result = driver.FindElement(By.Id("calcButton"));
            resultField = driver.FindElement(By.Id("result"));
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }

        [Test]
        public void Test_Sum_Two_Positive_Numbers()
        {
            
            searchField1.SendKeys("1");
            searchField2.SendKeys("2");
            operationOption.SendKeys("+ sum");
            result.Click();
            

            //Assert
            Assert.That(resultField.Text, Is.EqualTo("Result: 3"));
        }
        [Test]
        public void Test_Subtract_Negative_Numbers()
        {

            searchField1.SendKeys("-2");
            searchField2.SendKeys("-3");
            operationOption.SendKeys("- subtract");
            result.Click();


            //Assert
            Assert.That(resultField.Text, Is.EqualTo("Result: 1"));
        }
        [Test]
        public void Test_Multiply_Positive_Numbers()
        {

            searchField1.SendKeys("3");
            searchField2.SendKeys("5");
            operationOption.SendKeys("* multiply");
            result.Click();

            //Assert
            Assert.That(resultField.Text, Is.EqualTo("Result: 15"));
        }
        [Test]
        public void Test_Divide_Positive_Numbers()
        {

            searchField1.SendKeys("15");
            searchField2.SendKeys("5");
            operationOption.SendKeys("/ divide");
            result.Click();

            //Assert
            Assert.That(resultField.Text, Is.EqualTo("Result: 3"));
        }

        [Test]
        public void Test_Sum_Two_Invalid_Data()
        {
           
            searchField1.SendKeys("");
            searchField2.SendKeys("2");
            operationOption.SendKeys("+ sum");
            result.Click();
            
            //Assert
            Assert.That(resultField.Text, Is.EqualTo("Result: invalid input"));
        }
        [Test]
        public void Test_Multiply_Invalid_Data()
        {

            searchField1.SendKeys("");
            searchField2.SendKeys("3");
            operationOption.SendKeys("* multiply");
            result.Click();

            //Assert
            Assert.That(resultField.Text, Is.EqualTo("Result: invalid input"));
        }
        [Test]
        public void Test_Sum_Invalid_Data()
        {

            searchField1.SendKeys("");
            searchField2.SendKeys("");
            operationOption.SendKeys("+ sum");
            result.Click();

            //Assert
            Assert.That(resultField.Text, Is.EqualTo("Result: invalid input"));
        }
        [Test]
        public void Test_Sum_Infinity()
        {

            searchField1.SendKeys("Infinity");
            searchField2.SendKeys("Infinity");
            operationOption.SendKeys("+ sum");
            result.Click();

            //Assert
            Assert.That(resultField.Text, Is.EqualTo("Result: Infinity"));
        }
        [Test]
        public void Test_Sum_Decimal_Data()
        {

            searchField1.SendKeys("3.5");
            searchField2.SendKeys("6.4");
            operationOption.SendKeys("+ sum");
            result.Click();

            //Assert
            Assert.That(resultField.Text, Is.EqualTo("Result: 9.9"));
        }
        
        //valid data
        [TestCase("1", "+", "2", "Result: 3")]
        [TestCase("9", "-", "3", "Result: 6")]
        [TestCase("10", "*", "3", "Result: 30")]
        [TestCase("15", "/", "5", "Result: 3")]
        [TestCase("3.5", "+", "6.3", "Result: 9.8")]
        [TestCase("2.5", "/", "5", "Result: 0.5")]
        [TestCase("6", "*", "5", "Result: 30")]
        [TestCase("-9", "*", "10", "Result: -90")]

        //Invalid data
        [TestCase("Infinity", "+", "Infinity", "Result: Infinity")]
        [TestCase("Infinity", "/", "Infinity", "Result: invalid calculation")]
        [TestCase("-9", "/", "0", "Result: -Infinity")]
        [TestCase("Infinity", "*", "", "Result: invalid input")]
        [TestCase("$$$", "+", "§§", "Result: invalid input")]
        [TestCase("", "", "", "Result: invalid input")]
        [TestCase("kjiut", "*", "kjiuy", "Result: invalid input")]
        [TestCase("5", "", "345", "Result: invalid operation")]
        [TestCase("4", "/", "Infinity", "Result: 0")]

        public void Test_Calculate_Valid_Data(string num1, string operation, string num2, string expectedResult)
        {
           
            searchField1.SendKeys(num1);
            operationOption.SendKeys(operation);
            searchField2.SendKeys(num2);

            result.Click();

            Assert.That(resultField.Text, Is.EqualTo(expectedResult));
        }



    }

}

