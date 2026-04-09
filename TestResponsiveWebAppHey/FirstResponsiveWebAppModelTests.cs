namespace TestResponsiveWebAppHey;
using Xunit;
using FirstResponsiveWebAppHey.Models;

public class FirstResponsiveWebAppModelTests
{
    [Fact]
    public void AgeThisYearReturnsCorrectAge()
    {
        //Arrange
        var model = new FirstResponsiveWebAppModel
        {
            Name = "John",
            YearOfBirth = 1990
        };
        
        int expected = DateTime.Now.Year - 1990;
        
        //Act
        var age = model.AgeThisYear();
        
        //Assert
        Assert.Equal(expected, age);
    }
    
    [Fact]
    public void AgeThisYearReturnsZero()
    {
        //Arrange
        var model = new FirstResponsiveWebAppModel
        {
            Name = "Jake",
            YearOfBirth = DateTime.Now.Year
        };
        
        int expected = DateTime.Now.Year - 2026;
        
        //Act
        var age = model.AgeThisYear();
        
        //Assert
        Assert.Equal(expected, age);
    }
    
    [Fact]
    public void AgeThisYearMinimumYear()
    {
        //Arrange
        var model = new FirstResponsiveWebAppModel
        {
            Name = "Jill",
            YearOfBirth = 1900
        };
        
        int expected = DateTime.Now.Year - 1900;
        
        //Act
        var age = model.AgeThisYear();
        
        //Assert
        Assert.Equal(expected, age);
    }
    
    [Fact]
    public void AgeThisYearMaximumYear()
    {
        // Arrange
        var model = new FirstResponsiveWebAppModel
        {
            Name = "Newest",
            YearOfBirth = 2026
        };

        int expected = DateTime.Now.Year - 2026;

        // Act
        var age = model.AgeThisYear();

        // Assert
        Assert.Equal(expected, age);
    }\
}