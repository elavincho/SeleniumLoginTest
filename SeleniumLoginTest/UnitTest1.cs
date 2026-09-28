using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumLoginTest;

public class Tests
{

    private IWebDriver driver;

    private const string BasePath = "file:///C:/Users/elavi/OneDrive/Escritorio/SeleniumLogin/SeleniumLoginTest/";

    [SetUp]
    public void Setup()
    {
        driver = new ChromeDriver();
    }

    [Test, Order(1)]
    public void TestLogin()
    {
        driver.Navigate().GoToUrl($"{BasePath}login.html");

        IWebElement usernameField = driver.FindElement(By.Id("username"));
        IWebElement passwordField = driver.FindElement(By.Id("password"));
        IWebElement loginButton = driver.FindElement(By.Id("loginButton"));


        usernameField.SendKeys("testuser");
        passwordField.SendKeys("password123");
        loginButton.Click();

        Assert.That(driver.Url, Is.EqualTo($"{BasePath}success.html"));
        Assert.That(driver.PageSource.Contains("Formulario completado exitosamente"), Is.True);

        Console.WriteLine("Fin del Test 1.");
    }


    [Test, Order(2)]
    public void TestElementoInexistente()
    {
        driver.Navigate().GoToUrl($"{BasePath}login.html");

        try
        {
            IWebElement botonInexistente = driver.FindElement(By.Id("noExsite"));
            botonInexistente.Click();
            Console.WriteLine("Click correcto...");
        }
        catch (NoSuchElementException ex)
        {
            Console.WriteLine("Elemento no encontrado.. Se controla la prueba.");
            Console.WriteLine(ex.Message);
            Assert.Pass("El try-catch funcionó al manejar bien el error...");
        }
        finally
        {
            Console.WriteLine("Mensaje dentro del finally");

            Console.WriteLine("Fin del Test 2.");
        }
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
        driver.Dispose();
    }
}
