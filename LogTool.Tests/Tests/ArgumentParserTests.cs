using LogTool.Helpers;

namespace LogTool.Tests.Tests
{
    /// <summary>
    /// Unit test class for <see cref="ArgumentParser"/>. Uses <see cref="XUnit"/>.  
    /// </summary>
    public class ArgumentParserTests
    {
        private readonly ArgumentParser parser;

        public ArgumentParserTests()
        {
            parser = new ArgumentParser();
        }

        [Fact]
        public void Parse_WithValidArgs_ReturnsSuccess()
        {
            string[] args = ["file1.txt", "--top", "5", "--level", "WARNING", "--output", "report.log"];

            var result = parser.Parse(args);

            Assert.True(result.Success);
            Assert.NotNull(result.Arguments);
            Assert.Empty(result.ErrorMessage);

            // check argument values
            Assert.Equal("file1.txt", result.Arguments!.Files.FirstOrDefault());
            Assert.Equal(5, result.Arguments!.NumMessageCount);
            Assert.Equal("WARNING", result.Arguments!.Level);
            Assert.Equal(typeof(FilePrintService), result.Arguments!.OutputType.GetType());
        }
    
        [Fact]
        public void Parse_WithValidFileName_ReturnsSuccess()
        {
            var filename = "file1.txt";
            string[] args = [filename];

            var result = parser.Parse(args);

            Assert.True(result.Success);
            Assert.NotNull(result.Arguments);
            Assert.Empty(result.ErrorMessage);
            Assert.Equal(filename, result.Arguments!.Files.FirstOrDefault());
        }
    
        [Fact]
        public void Parse_MissingFileName_ReturnsFail()
        {
            string[] args = ["--top", "5"];

            var result = parser.Parse(args);

            Assert.False(result.Success);
            Assert.Null(result.Arguments);
            Assert.NotEmpty(result.ErrorMessage);
        }
    
        [Fact]
        public void Parse_NoArgs_ReturnsFail()
        {
            string[] args = [];

            var result = parser.Parse(args);

            Assert.False(result.Success);
            Assert.Null(result.Arguments);
            Assert.NotEmpty(result.ErrorMessage);
        }

        [Fact]
        public void Parse_WhenTopLessThanZero_ReturnsFail()
        {
            string[] args = ["file1.txt", "--top", "-5"];

            var result = parser.Parse(args);

            Assert.False(result.Success);
            Assert.Null(result.Arguments);
            Assert.NotEmpty(result.ErrorMessage);
        }

        [Fact]
        public void Parse_WhenTopEqualsZero_ReturnsFail()
        {
            string[] args = ["file1.txt", "--top", "0"];

            var result = parser.Parse(args);

            Assert.False(result.Success);
            Assert.Null(result.Arguments);
            Assert.NotEmpty(result.ErrorMessage);
        }
        
        [Fact]
        public void Parse_WhenTopGreaterThanZero_ReturnsSuccess()
        {
            string[] args = ["file1.txt", "--top", "1"];

            var result = parser.Parse(args);

            Assert.True(result.Success);
            Assert.NotNull(result.Arguments);
            Assert.Empty(result.ErrorMessage);
            Assert.Equal(1, result.Arguments!.NumMessageCount);
        }
        
        [Theory]
        [InlineData("ERROR", "ERROR")]
        [InlineData("Error", "ERROR")]
        [InlineData("WARNING", "WARNING")]
        [InlineData("Warning", "WARNING")]
        [InlineData("INFO", "INFO")]
        [InlineData("Info", "INFO")]
        public void Parse_WhenLevelIsValid_ReturnsNormalizedLevel(string input, string expected)
        {
            var args = new[] { "file1.txt", "--level", input };

            var result = parser.Parse(args);

            Assert.True(result.Success);
            Assert.NotNull(result.Arguments);
            Assert.Equal(expected, result.Arguments!.Level);
        }
        
        [Fact]
        public void Parse_WhenLevelIsInvalid_ReturnsFail()
        {
            string[] args = ["file1.txt", "--level", "SuperError"];

            var result = parser.Parse(args);

            Assert.False(result.Success);
            Assert.Null(result.Arguments);
            Assert.NotEmpty(result.ErrorMessage);
        }

        [Fact]
        public void Parse_WhenOutputTargetsConsole_ReturnsSuccess()
        {
            string[] args = ["file1.txt", "--output", "console"];

            var result = parser.Parse(args);

            Assert.True(result.Success);
            Assert.NotNull(result.Arguments);
            Assert.Empty(result.ErrorMessage);

            Assert.Equal(typeof(TerminalPrintService), result.Arguments!.OutputType.GetType());
        }
        
        [Fact]
        public void Parse_WhenOutputTargetsFile_ReturnsSuccess()
        {
            string[] args = ["file1.txt", "--output", "report.log"];

            var result = parser.Parse(args);

            Assert.True(result.Success);
            Assert.NotNull(result.Arguments);
            Assert.Empty(result.ErrorMessage);

            Assert.Equal(typeof(FilePrintService), result.Arguments!.OutputType.GetType());
        }

        [Fact]
        public void Parse_WhenHelpDesired_ReturnsSuccess()
        {
            string[] args = ["file1.txt", "--help"];

            var result = parser.Parse(args);

            Assert.True(result.Success);
            Assert.Null(result.Arguments);
            Assert.Empty(result.ErrorMessage);
            Assert.True(result.DisplayHelp);
        }

        [Fact]
        public void Parse_WhenTopValueIsNonNumeric_ReturnsFail()
        {
            string[] args = ["file1.txt", "--top", "abc"];

            var result = parser.Parse(args);

            Assert.False(result.Success);
            Assert.Null(result.Arguments);
            Assert.NotEmpty(result.ErrorMessage);
            Assert.Contains("top", result.ErrorMessage, StringComparison.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData("--top", "top")]
        [InlineData("--level", "level")]
        [InlineData("--output", "output")]
        public void Parse_WhenFlagValueIsMissing_ReturnsFail(string flag, string expectedMessagePart)
        {
            string[] args = ["file1.txt", flag];

            var result = parser.Parse(args);

            Assert.False(result.Success);
            Assert.Null(result.Arguments);
            Assert.NotEmpty(result.ErrorMessage);
            Assert.Contains(expectedMessagePart, result.ErrorMessage, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Parse_WhenFlagIsUnknown_ReturnsFail()
        {
            string[] args = ["file1.txt", "--banana"];

            var result = parser.Parse(args);

            Assert.False(result.Success);
            Assert.Null(result.Arguments);
            Assert.NotEmpty(result.ErrorMessage);
            Assert.Contains("banana", result.ErrorMessage, StringComparison.OrdinalIgnoreCase);
        }
    }
}
