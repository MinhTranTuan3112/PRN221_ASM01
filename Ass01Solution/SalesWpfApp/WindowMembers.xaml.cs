using DataAccess.DTOs.Member;
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
            btnCreate.Click += BtnCreate_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Delete this member?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result is not MessageBoxResult.Yes)
                {
                    return;
                }

                var currentMember = dgMembers.SelectedItem as GetMemberDto;

                if (currentMember is null)
                {
                    throw new Exception("Please select a member");
                }

                _memberRepository.DeleteMember(currentMember.MemberId);

                MessageBox.Show("Delete member success");
                LoadMembers();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentMember = dgMembers.SelectedItem as GetMemberDto;

                if (currentMember is null)
                {
                    throw new Exception("Please select a member");
                }

                var detailsWindow = new MemberDetailsWindow()
                {
                    IsUpdate = true,
                    MemberId = currentMember.MemberId
                };

                detailsWindow.Show();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            var detailsWindow = new MemberDetailsWindow()
            {
                IsUpdate = false
            };

            detailsWindow.Show();
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
            try
            {
                var members = _memberRepository.GetMembers(txtKeyword.Text);
                dgMembers.ItemsSource = members;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
