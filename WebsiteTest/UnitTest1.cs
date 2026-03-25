using Xunit;
namespace Website.Tests
{
    public class UtilityTests
    {
        [Fact]
        public void ToMonthYear_ValidInput_ReturnsExpectedResult()
        {
            var result = ""; 
            //result = "June 2024";
            result = Website.Utilities.Utility.ToMonthYear("2024-06");
            Assert.Equal("June 2024", result);
        }
    }
}
