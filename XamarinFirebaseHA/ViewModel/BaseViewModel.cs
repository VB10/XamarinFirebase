using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFirebaseHA.Database;

namespace XamarinFirebaseHA.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        Page page;
        public INavigation Navigation
        {
            get
            {
                return page.Navigation;
            }
        }

        public SqliteManager sqliteManager;

        public BaseViewModel(Page page)
        {
            this.page = page;
            sqliteManager = new SqliteManager();
        }

        public void errorAlert(string text)
        {
            page.DisplayAlert("Erro", text, "Okey");
        }
        public async Task<string> actionSheet(string text, string text2)
        {
            var result = await page.DisplayActionSheet("Seçim yapınız", "Kapat", "Fotoğraf çek", "Galeriden seç");
            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
