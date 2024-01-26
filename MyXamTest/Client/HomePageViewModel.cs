using MyXamClient.ViewModels;

namespace MyXamTest.Client;

public class HomePageViewModelTest
{
    [Fact]
    public void Init_ShouldSetFilePath()
    {
        // Arrange
        var viewModel = new HomePageViewModel();

        // Act
        viewModel.Init();

        // Assert
        Assert.Equal(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "C:\\agenda.json"), viewModel.filePath);
    }

}