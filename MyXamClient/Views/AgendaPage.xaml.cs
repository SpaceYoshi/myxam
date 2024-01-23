using MyXamClient.ViewModels;

namespace MyXamClient.Views;

public partial class AgendaPage : ContentPage
{
	public AgendaPage(AgendaViewModel mv)
	{
		InitializeComponent();
        BindingContext = mv;

    }
}