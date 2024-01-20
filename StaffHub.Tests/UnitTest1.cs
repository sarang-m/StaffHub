namespace StaffHub.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            //arrange
            Mymath mymath = new Mymath();
            int input1 = 10;
            int input2 = 20;
            int expected = 30;
            //act
            double actual = mymath.Add(input1, input2);
            //assert
            Assert.Equal(expected, actual);
        }
    }
}