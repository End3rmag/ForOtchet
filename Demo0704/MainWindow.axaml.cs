using Avalonia.Controls;
using Demo0704.Context;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using System.Linq;
using System.Threading.Tasks;

namespace Demo0704
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

            var con = new PostgresContext();

            var log = LoginBox.Text?.Trim();
            var pas = PasswordBox.Text?.Trim();

            if(string.IsNullOrEmpty(log) || string.IsNullOrEmpty(pas))
            {
                var mes = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Вы не ввели данные", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                var res = await mes.ShowWindowDialogAsync(this);
            }
            else
            {
                var user = con.Users.Include(x => x.IdRoleNavigation).Where(x => x.Login == log && x.Password == pas);

                if (user.Count() > 0)
                {
                    new ProductWin(user.First()).Show();
                    Close();
                }
                else
                {
                    var mess = MessageBoxManager.GetMessageBoxStandard("Ошибка", "Вы ввели не правельный логин или пароль", MsBox.Avalonia.Enums.ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                    var ress = await mess.ShowWindowDialogAsync(this);
                }
            }

        }
    }
}