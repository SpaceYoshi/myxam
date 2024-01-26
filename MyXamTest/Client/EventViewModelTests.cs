using Moq;
using MyXamClient.Services;
using MyXamClient.Services.Navigation;
using MyXamClient.Services.Tcp;
using MyXamClient.ViewModels;

namespace MyXamTest.Client;

public class EventViewModelTests
{
    // [Fact]
    // public void CreateEvent_Should_Create_Event()
    // {
    //     // Arrange
    //     var mockNavigationService = new Mock<INavigationService>();
    //     var mockTcpService = new Mock<ITcpService>();
    //     var sut = new EventViewModel(mockNavigationService.Object, mockTcpService.Object);
    //
    //     // Act
    //     var result = sut.CreateEvent();
    //
    //     // Assert
    //     Assert.NotNull(result);
    // }

    // [Fact]
    // public async void GoBack_Should_Navigate_To_AgendaPage()
    // {
    //     // Arrange
    //     var mockNavigationService = new Mock<INavigationService>();
    //     var mockTcpService = new Mock<ITcpService>();
    //     var sut = new EventViewModel(mockNavigationService.Object, mockTcpService.Object);
    //
    //     // Act
    //     await sut.GoBack();
    //
    //     // Assert
    //     mockNavigationService.Verify(x => x.NavigateAsync("AgendaPage"), Times.Once());
    // }

    // [Fact]
    // public void AddEvent_Should_Add_Event_To_AgendaService()
    // {
    //     // Arrange
    //     var mockNavigationService = new Mock<INavigationService>();
    //     var mockTcpService = new Mock<ITcpService>();
    //     var sut = new EventViewModel(mockNavigationService.Object, mockTcpService.Object);
    //
    //     // Act
    //     sut.AddEvent();
    //
    //     // Assert
    //     Assert.Single(AgendaService.Events);
    // }

}