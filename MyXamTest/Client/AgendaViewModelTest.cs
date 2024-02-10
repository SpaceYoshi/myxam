using Moq;
using MyXamClient.Services;
using MyXamClient.Services.Navigation;
using MyXamClient.ViewModels;
using MyXamLibrary.Models;

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
        AgendaService.Events.Add(new AgendaEvent(Guid.NewGuid(), Guid.NewGuid(), "Test", DateTimeOffset.Now, DateTimeOffset.Now));

        // Act
        await AgendaViewModel.UpdateAgenda();

        // Assert
        Assert.NotEmpty(AgendaViewModel._agenda);
    }

    [Fact]
    public void ClearAgenda_Should_Clear_Agenda()
    {
        // Arrange
        var mockNavigationService = new Mock<INavigationService>();
        var sut = new AgendaViewModel(mockNavigationService.Object);
        AgendaService.Events.Add(new AgendaEvent(Guid.NewGuid(), Guid.NewGuid(), "Test", DateTimeOffset.Now, DateTimeOffset.Now));

        // Act
        AgendaViewModel.ClearAgenda();

        // Assert
        Assert.Empty(AgendaViewModel._agenda);
    }

    [Fact]
    public void Agenda_Should_Initially_Be_Empty()
    {
        // Arrange
        var mockNavigationService = new Mock<INavigationService>();
        var sut = new AgendaViewModel(mockNavigationService.Object);

        // Act
        var result = AgendaViewModel._agenda;

        // Assert
        Assert.Empty(result);
    }

}