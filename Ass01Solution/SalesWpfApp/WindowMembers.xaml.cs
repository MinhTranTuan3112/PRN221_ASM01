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
    /// Interaction logic for WindowMembers.xaml
    /// </summary>
    public partial class WindowMembers : Window
    {
        private readonly IMemberRepository _memberRepository;
        public WindowMembers()
        {
            InitializeComponent();

            if (_memberRepository is null)
            {
                _memberRepository = new MemberRepository();
            }

            this.Loaded += WindowMembers_Loaded;
            txtKeyword.TextChanged += TxtKeyword_TextChanged;
        }

        private void TxtKeyword_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadMembers();
        }

        private void WindowMembers_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMembers();
        }

        private void LoadMembers()
        {
            var members = _memberRepository.GetMembers(txtKeyword.Text);
            dgMembers.ItemsSource = members;
        }
    }
}
