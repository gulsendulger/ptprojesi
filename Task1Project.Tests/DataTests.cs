using Xunit;
using Task1Project.Data;

namespace Task1Project.Tests
{
    public class DataTests
    {
        [Fact]
        public void GenerateSampleUsers_ShouldCreateUsers()
        {
            var repo = new InMemoryDataRepository();
            repo.GenerateSampleUsers();

            Assert.True(repo.GetUsers().Count >= 2, "Sample users generation failed.");
        }

        [Fact]
        public void GenerateSampleCatalog_ShouldCreateBooks()
        {
            var repo = new InMemoryDataRepository();
            repo.GenerateSampleCatalog();

            Assert.True(repo.GetBooks().Count >= 2, "Sample catalog generation failed.");
        }
    }
}
