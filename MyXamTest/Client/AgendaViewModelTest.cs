using Moq;
using MyXamClient.Services.Navigation;
using MyXamClient.ViewModels;

namespace MyXamTest.Client;

public class AgendaViewModelTest
{
    [Fact]
    public async void NavigateToEventPage_Should_Navigate_To_EventPage()
    {
        // Arrange
        var mockNavigationService = new Mock<INavigationService>();
        var sut = new AgendaViewModel(mockNavigationService.Object);

        // Act
        await sut.NavigateToEventPage();

        // Assert
        mockNavigationService.Verify(x => x.NavigateAsync("EventPage"), Times.Once());
    }

    [Fact]
    public async void UpdateAgenda_Should_Update_Agenda()
    {
        // Arrange
        var mockNavigationService = new Mock<INavigationService>();
        var sut = new AgendaViewModel(mockNavigationService.Object);

        // Act
        await AgendaViewModel.UpdateAgenda();

        // Assert
        Assert.NotEmpty(AgendaViewModel._agenda);
    }
}