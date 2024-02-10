using MyXamClient.ViewModels;

namespace MyXamClient.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}