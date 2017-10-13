using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace XamarinFirebaseHA
{
	public partial class XamarinFirebaseHAPage : ContentPage
	{
		ObservableCollection<Student> obs = new ObservableCollection<Student>();
		public XamarinFirebaseHAPage()
		{
			InitializeComponent();
			_lst.BindingContext = obs;
		}

		async void Handle_Clicked(object sender, System.EventArgs e)
		{
			var db = new DbFirebase();

			var dblit =await db.getList();

			foreach (var item in dblit)
			{
				obs.Add(item);
			}







		}
	}
}
