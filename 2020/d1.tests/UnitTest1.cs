using System;
using Xunit;

namespace d1.tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var input = new[] {"1721", "979", "366", "299", "675", "1456"};

            var result = Program.C1(input);
            
            Assert.Equal(514579, result);
        }
        
        [Fact]
        public void Test2()
        {
            var input = new[] {"1721", "979", "366", "299", "675", "1456"};

            var result = Program.C2(input);
            
            Assert.Equal(241861950, result);
        }
    }
}