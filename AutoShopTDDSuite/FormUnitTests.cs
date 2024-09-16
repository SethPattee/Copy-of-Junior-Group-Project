using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoShopAppLibrary.Components;
using AutoShopAppLibrary.Services;

using Microsoft.AspNetCore.Components;

namespace AutoShopTDDSuite;

public class FormUnitTests
{
    AutoShopAppLibrary.Components.TesterForm _testForm { get; set; }

    [Fact]
    public void FormValidatesCorrectName()
    {
        //Arrange
        string goodName = "John Doe";

        //Act
        var result = _testForm.IsValidName(goodName);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void FormFailsImproperName()
    {
        //Arrange
        string badName = "Bo";

        //Act
        var result = _testForm.IsValidName(badName);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void FormValidatesCorrectEmail()
    {
        //Arrange
        string goodEmail = "testemail@gmail.com";

        //Act
        var result = _testForm.IsValidEmail(goodEmail);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void FormFailsImproperEmail()
    {
        //Arrange
        string badEmail = "testemail";

        //Act
        var result = _testForm.IsValidEmail(badEmail);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void FormValidatesCorrectPhone()
    {
        //Arrange
        string goodPhone = "4351234567";

        //Act
        bool result = (_testForm.IsValidPhone(goodPhone) == "good");

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void FormFailsImproperPhone()
    {
        //Arrange
        string badPhoneWithLetter = "123r567890";
        //string badPhoneWithWhiteSpace = "123 597660";
        string badPhoneWithShortSequence = "1234567";
        string badPhoneWithLongSequence = "12345678901";
        string badPhoneWithEmptyString = "";

        //Act
        var resultLetter = _testForm.IsValidPhone(badPhoneWithLetter);
        //var resultSpace = _testForm.IsValidPhone(badPhoneWithWhiteSpace);
        var resultShort = _testForm.IsValidPhone(badPhoneWithShortSequence);
        var resultLong = _testForm.IsValidPhone(badPhoneWithLongSequence);
        var resultEmpty = _testForm.IsValidPhone(badPhoneWithEmptyString);


        //Assert
        Assert.True(resultLetter == "bad");
        //Assert.True(resultSpace == "bad");
        Assert.True(resultShort == "short");
        Assert.True(resultLong == "long");
        Assert.True(resultEmpty == "empty");
    }

    [Fact]
    public void FormValidatesCorrectOdometer()
    {
        //Arrange
        string goodOdometer = "123990";

        //Act
        var result = _testForm.IsValidName(goodOdometer);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void FormFailsImproperOdometer()
    {
        //Arrange
        string badOdometer = "1303E4";

        //Act
        var result = _testForm.IsValidOdometer(badOdometer);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void FormValidatesCorrectLicensePlate()
    {
        //Arrange
        string goodLicensePlate = "A23 B87X";

        //Act
        var result = _testForm.IsValidLicensePlate(goodLicensePlate);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void FormFailsImproperLicensePlate()
    {
        //Arrange
        string badLicensePlate = "333344445555";

        //Act
        var result = _testForm.IsValidLicensePlate(badLicensePlate);

        //Assert
        Assert.False(result);

    }

    [Fact]
    public void FormValidatesCorrectYear()
    {
        //Arrange
        string goodYear = "2001";

        //Act
        var result = _testForm.IsValidYear(goodYear);

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void FormFailsImproperYear()
    {
        //Arrange
        string badYear = "2031f";

        //Act
        var result = _testForm.IsValidYear(badYear);

        //Assert
        Assert.False(result);
    }

    [Fact]
    public void Test_CanSubmitForm_ReturnsTrueOnValidInput()
    {
        //arrange
        Dictionary<string, string> elements = new();
        elements.Add("Name", "John Doe");
        elements.Add("Phone", "4351234567");
        elements.Add("Email", "testemail@gmail.com");
        elements.Add("Make", "Nissan");
        elements.Add("Model", "Clown Car");
        elements.Add("Year", "2001");
        elements.Add("Odometer", "123990");
        elements.Add("License Plate", "A23 B87X");
        elements.Add("Comments", "");
        _testForm.SetFormElements(elements);


        //act
        var result = _testForm.CheckFormKeys();

        //Assert
        Assert.True(result);
    }

    [Fact]
    public void Test_CanSubmitForm_ReturnsFalseOnInvalidInput()
    {
        //arrange
        Dictionary<string, string> elements = new();
        elements.Add("Name", "Bo");
        elements.Add("Phone", "4351rf");
        elements.Add("Email", "testemail");
        elements.Add("Make", "Nissan");
        elements.Add("Model", "Clown Car");
        elements.Add("Year", "2001");
        elements.Add("Odometer", "-123990");
        elements.Add("License Plate", "A23 B87X 345");
        elements.Add("Comments", "");
        _testForm.SetFormElements(elements);


        //act
        var result = _testForm.CheckFormKeys();

        //Assert
        Assert.False(result);
    }

    public FormUnitTests()
    {
        _testForm = new AutoShopAppLibrary.Components.TesterForm();
    }
}