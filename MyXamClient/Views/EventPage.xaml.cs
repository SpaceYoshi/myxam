using MyXamClient.ViewModels;

namespace MyXamClient.Views;

public partial class EventPage : ContentPage
{
	public EventPage(EventViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}