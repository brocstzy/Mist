using Microsoft.Win32;
using Mist.Helper;
using Mist.Model;
using Mist.Pages.MainWindowPages.CommunityPagePages;
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
    /// Interaction logic for CreateDevGroupPage.xaml
    /// </summary>
    public partial class CreateDevGroupPage : Page
    {
        public byte[]? PfpImage = null;
        public byte[]? BackgroundImage = null;

        public bool FromAddGamePage = false;

        public CreateDevGroupPage(bool fromAddGamePage)
        {
            InitializeComponent();
            ButtonPainter.SetButtonBackground(createDevGroup_Button);
            FromAddGamePage = fromAddGamePage;
        }

        private List<TextBox> GetEmptyFields()
        {
            List<TextBox> allFields =
                [
                groupName_TextBox,
                bio_TextBox
                ];
            List<TextBox> emptyFields = new List<TextBox>();
            foreach ( var field in allFields )
            {
                if (String.IsNullOrWhiteSpace(field.Text))
                    emptyFields.Add(field);
            }
            return emptyFields;
        }

        private void choosePfp_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберите изображение";
            ofd.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (ofd.ShowDialog(WindowManager.GetWindow<MainWindow>()) == true)
            {
                PfpImage = ImageHelper.CreateImage(ofd.FileName);
                ((Button)sender).Content = ofd.FileName.Split('\\').Last();
            }
        }

        private void chooseBackgroundPfp_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Выберите изображение";
            ofd.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (ofd.ShowDialog(WindowManager.GetWindow<MainWindow>()) == true)
            {
                BackgroundImage = ImageHelper.CreateImage(ofd.FileName);
                ((Button)sender).Content = ofd.FileName.Split('\\').Last();
            }
        }

        private void createDevGroup_Button_Click(object sender, RoutedEventArgs e)
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
            App.Context.Developers.Add(new Developer(App.CurrentUser.Id,
                                                     PfpImage,
                                                     BackgroundImage,
                                                     groupName_TextBox.Text,
                                                     bio_TextBox.Text));
            App.Context.SaveChanges();
            if (FromAddGamePage)
                PageManager.MainFrame.Navigate(new AddGamePage());
            else
            {
                var page = new CommunityPage();
                page.groupType_ComboBox.SelectedIndex = 1;
                PageManager.MainFrame.Navigate(page);
            }

        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).BorderBrush = Brushes.Black;
        }

        private void createDevGroup_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonPainter.SetButtonBackgroundHover(createDevGroup_Button);
        }

        private void createDevGroup_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonPainter.SetButtonBackground(createDevGroup_Button);
        }
    }
}
