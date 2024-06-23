using Microsoft.Win32;
using Mist.Helper;
using Mist.Model;
using Mist.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mist.Pages.MainWindowPages
{
    /// <summary>
    /// Interaction logic for CreateGroupPage.xaml
    /// </summary>
    public partial class CreateGroupPage : Page
    {
        public byte[]? GroupImage = null;
        public bool IsPrivate = false;

        public CreateGroupPage()
        {
            InitializeComponent();
            ButtonPainter.SetButtonBackground(createGroup_Button);
        }

        public List<Control> GetEmptyFields()
        {
            List<Control> allFields =
            [
                groupName_TextBox,
                tag_TextBox,
                bio_TextBox
            ];
            List<Control> emptyFields = new List<Control>();
            foreach (var field in allFields )
            {
                if (String.IsNullOrWhiteSpace(((TextBox)field).Text))
                    emptyFields.Add(field);
            }
            return emptyFields;
        }

        private void createGroup_Button_Click(object sender, RoutedEventArgs e)
        {
            var emptyFields = GetEmptyFields();
            if (emptyFields.Any())
            {
                foreach (var field in emptyFields)
                {
                    field.BorderBrush = Brushes.Red;
                }
                return;
            }
            using (MistContext mc = new MistContext())
            {
                var group = new Group(App.CurrentUser.Id,
                                        groupName_TextBox.Text,
                                        GroupImage,
                                        tag_TextBox.Text,
                                        bio_TextBox.Text,
                                        IsPrivate, DateTime.Now);
                mc.Groups.Add(group);
                mc.SaveChanges();
                mc.GroupMembers.Add(new GroupMember(group.Id, App.CurrentUser.Id, DateTime.Now));
                mc.SaveChanges();

            }
            App.Context = new MistContext();
            PageManager.MainFrame.Navigate(new CommunityPage());
        }

        private void choosePfp_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберите изображение";
            ofd.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (ofd.ShowDialog(WindowManager.GetWindow<MainWindow>()) == true)
            {
                GroupImage = ImageHelper.CreateImage(ofd.FileName);
                ((Button)sender).Content = ofd.FileName.Split('\\').Last();
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }

        private void createGroup_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonPainter.SetButtonBackgroundHover(sender);
        }

        private void createGroup_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonPainter.SetButtonBackground(sender);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).BorderBrush = Brushes.Black;
        }

        private void privateGroup_CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            IsPrivate = true;
        }

        private void privateGroup_CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            IsPrivate = false;
        }
    }
}
