using CommunityToolkit.Maui.Views;
using MyXamClient.ViewModels;
using System.Runtime.CompilerServices;

namespace MyXamClient.Views;

public partial class AgendaPage : ContentPage
{
	public AgendaPage(AgendaViewModel mv)
	{
		InitializeComponent();
		BindingContext = mv;
	}
}