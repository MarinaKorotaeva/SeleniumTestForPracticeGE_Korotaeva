using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace testSeleniumGitExtension;

public class TestSeleniumForPractice
{
    [Test]
    public void Autorization()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");
        
        // зайти в chrome
        var driver = new ChromeDriver(options);
        
        // перейти по URL
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        Thread.Sleep(5000);
        
        // ввести логин и пароль
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("korotaevamg@mail.ru");

        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys(("@mzmVFsz3110"));
        Thread.Sleep(3000);
        
        // нажать кнопку "войти"
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        Thread.Sleep(3000);
        
        // проверка того, что мы находимся на нужной странице
        var currentUrl = driver.Url;
        Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news");
            
        // закрыть браузер
        driver.Quit();
    }
}
