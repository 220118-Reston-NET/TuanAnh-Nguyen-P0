using Xunit;
using Model;

namespace Test;

public class CustomerModelTest
{
  /// <summary>
  /// Check the validation is CustomerModel Name for a valid data
  /// </summary>
  [Fact]
  public void NameShouldSetValidData()
  {
    //Arrange
    Customer _cus = new Customer();
    string _name = "Kira";

    //Act
    _cus.Name = _name;

    //Assert
    Assert.NotNull(_cus.Name);
    Assert.Equal(_cus.Name, _name);
  }
}