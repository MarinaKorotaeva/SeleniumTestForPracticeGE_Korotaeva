using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace testSeleniumGitExtension;

public class TestSeleniumForPractice
{
    public ChromeDriver driver;

    [SetUp]
    public void Setup()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--disable-extensions");
        driver = new ChromeDriver(options);
        
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);
        
        Autorization();
    }
    
    [Test] 
    public void Autorizations()
    {
        var news = driver.FindElement(By.CssSelector("[data-tid='Title']"));
        var currentUrl = driver.Url;
        currentUrl.Should().Be("https://staff-testing.testkontur.ru/news");
    }

    [Test]
    public void NavigationTest()
    {
        var sideMenu = driver.FindElement(By.CssSelector("[data-tid='SidebarMenuButton']"));
        sideMenu.Click();
        
        var communities = driver.FindElements(By.CssSelector("[data-tid='Community']"))
            .First(element => element.Displayed);
        communities.Click();

        var communityTitle = driver.FindElements(By.CssSelector("[data-tid='Title']"));
        Assert.That(driver.Url == "https://staff-testing.testkontur.ru/communities", message: "мы хотели, чтобы URL был https://staff-testing.testkontur.ru/communities");
        
    }

    [Test]
    public void DateOfBirth() //Этот тест и должен упасть: кнопка "сохранить" не кликабельная
    {
        var birthday = driver.FindElement(By.CssSelector("[data-tid='Title']"));
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/profile/settings/edit");
        
        var date = driver.FindElement(By.XPath("//*[@id=\"root\"]/section/section[2]/section[3]/div[1]/div[4]/label[2]/span"));
        date.SendKeys("31.10.2000");
        
        var savechanges = driver.FindElement(By.XPath("//*[@id=\"root\"]/section/section[2]/section[1]/div[2]/button[1]"));
        savechanges.Click();
    }

    [Test]
    public void CreateFolder()
    {
        explicitexpectation();
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/files");
        
        Assert.That(driver.Url == "https://staff-testing.testkontur.ru/files",
            message: "мы хотели получить https://staff-testing.testkontur.ru/files, а получили " + driver.Url);

        var add = driver.FindElement(By.XPath("/html/body/div/section/section[2]/div/div[2]/div[2]/div/div[2]/span/span/button"));
        add.Click();

        var createfolders = driver.FindElement(By.CssSelector("[data-tid='CreateFolder']"));
        createfolders.Click();
        
        var folder = driver.FindElement(By.CssSelector("[data-tid='Input']"));
        folder.SendKeys("NewFolder");
        
        var save = driver.FindElement(By.CssSelector("[data-tid='SaveButton']"));
        //save.Click(); //закомментировала, чтобы не создавать сущности
    }

    [Test]
    public void OnOffEmails()
    {
        explicitexpectation();
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities/a75b6656-92dd-4f99-8e8c-7262e8477594");

        var actions = driver.FindElement(By.XPath("//*[@id=\"root\"]/section/section[2]/div/div/section/div[2]/div[2]/div[2]/div/span/button"));
        actions.Click();

        var checkboxEmail = driver.FindElement(By.CssSelector("[data-tid='Subscribe']"));
        checkboxEmail.Click();
    }

    public void Autorization()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru");
        
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("korotaevamg@mail.ru");

        var password = driver.FindElement(By.Name("Password"));
        password.SendKeys(("@mzmVFsz3110"));
        
        var enter = driver.FindElement(By.Name("button"));
        enter.Click(); 
    }

    public void explicitexpectation() //явное ожидание на главной странице
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(7));
        wait.Until(ExpectedConditions.UrlContains("https://staff-testing.testkontur.ru/news"));
    }
    
    [TearDown]
    public void TearDown()
    {
        driver.Close(); 
        driver.Quit();
    }
}