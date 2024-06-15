using DataAccess.DAO;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalesWpfApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IMemberRepository _memberRepository;

        string Email
        {
            get => txtEmail.Text;
            set => txtEmail.Text = value;
        }

        string Password
        {
            get => txtPassword.Password;
            set => txtPassword.Password = value;
        }


        public LoginWindow()
        {
            InitializeComponent();

            if (_memberRepository is null)
            {
                _memberRepository = new MemberRepository();
            }

            //Events
            btnSubmit.Click += BtnSubmit_Click;
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            var member = _memberRepository.Login(Email, Password);

            if (member is not null)
            {
                new WindowMembers().Show();
            }
            else
            {
                MessageBox.Show("Wrong email or password", "Login error", MessageBoxButton.OK);
            }
        }
    }
}
