using Microsoft.Win32;
using Mist.Helper;
using Mist.Model;
using Mist.UserControls;
using Mist.Windows;
using System.Windows;
using System.Windows.Controls;

namespace Mist.Pages.MainWindowPages.CommunityPagePages
{
    /// <summary>
    /// Interaction logic for AddGamePage.xaml
    /// </summary>
    public partial class AddGamePage : Page
    {
        public byte[] libraryIcon;
        public byte[] libraryLogo;
        public byte[] backgroundLibraryImage;
        public byte[] verticalLibraryImage;
        public byte[] frontImage;
        public byte[] frontSearchImage;
        public List<byte[]> screenshots = new List<byte[]>();
        public List<byte[]> videos = new List<byte[]>();
        public AddGamePage()
        {
            InitializeComponent();
            FillDevGroups();
        }
        public (List<TextBox>, List<string>, List<List<byte[]>>) GetEmptyFields()
        {
            List<TextBox> textBoxes =
            [
                gameName_TextBox,
                bio_TextBox,
                description_TextBox,
                price_TextBox
            ];
            List<byte[]> images =
            [
                libraryIcon,
                libraryLogo,
                backgroundLibraryImage,
                verticalLibraryImage,
                frontImage,
                frontSearchImage
            ];
            Dictionary<string, string> imageNamesDictionary = new Dictionary<string, string>
            {
                { "libraryIcon", "Иконка в библиотеке" },
                { "libraryLogo", "Лого в библиотеке" },
                { "backgroundLibraryImage", "Фоновое изображение в библиотеке" },
                { "verticalLibraryImage", "Вертикальное изображение в библиотеке" },
                { "frontImage", "Изображение на главной странице в магазине" },
                { "frontSearchImage", "Изображение в поиске магазина" }
            };
            List<List<byte[]>> multiFiles =
            [
                screenshots,
                videos
            ];

            List<TextBox> emptyTextBoxes = new List<TextBox>();
            List<string> emptyImages = new List<string>();
            List<List<byte[]>> emptyMultiFiles = new List<List<byte[]>>();

            foreach (var textBox in textBoxes)
            {
                if (String.IsNullOrWhiteSpace(textBox.Text))
                    emptyTextBoxes.Add(textBox);
            }
            for (int i = 0; i < images.Count; i++)
            {
                if (images[i] == null)
                {
                    emptyImages.Add(imageNamesDictionary.ElementAt(i).Value);
                }
            }
            foreach (var multiFile in multiFiles)
            {
                if (!(multiFile.Count > 0))
                    emptyMultiFiles.Add(multiFile);
            }

            return (emptyTextBoxes, emptyImages, emptyMultiFiles);
        }

        private void addGame_Button_Click(object sender, RoutedEventArgs e)
        {
            var emptyFields = GetEmptyFields();
            if (emptyFields.Item1.Count > 0 ||
                emptyFields.Item2.Count > 0 ||
                emptyFields.Item3.Count > 0)
            {
                error_Label.Visibility = Visibility.Visible;
                error_Label.Text = "Ошибка: ";
                if (emptyFields.Item1.Count > 0)
                {
                    error_Label.Text += "\nне заполнены следующие текстовые поля: ";
                    foreach (var t in emptyFields.Item1)
                    {
                        if (t != emptyFields.Item1.Last())
                            error_Label.Text += $"{t.Tag}, ";
                        else
                            error_Label.Text += $"{t.Tag}";
                    }
                }
                if (emptyFields.Item2.Count > 0)
                {
                    error_Label.Text += "\n\nне выбраны следующие изображения: ";
                    foreach (var i in emptyFields.Item2)
                    {
                        if (i != emptyFields.Item2.Last())
                            error_Label.Text += $"{i}, ";
                        else
                            error_Label.Text += $"{i}";
                    }
                }
                if (emptyFields.Item3.Count > 0)
                {
                    error_Label.Text += "\n\nне загружены следующие медиа: ";
                    error_Label.Text += "скриншоты или видео";
                }
                return;
            }
            UserControl uc = devGroups_ComboBox.SelectedItem as UserControl;
            var dev = ((DevGroupComboBoxUserControl)uc).Developer;
            using (MistContext mc = new MistContext())
            {
                var game = new Game(gameName_TextBox.Text,
                                      Decimal.Parse(price_TextBox.Text),
                                      libraryLogo,
                                      backgroundLibraryImage,
                                      verticalLibraryImage,
                                      bio_TextBox.Text,
                                      description_TextBox.Text,
                                      DateOnly.FromDateTime(DateTime.Now),
                                      dev.Id,
                                      libraryIcon,
                                      frontImage,
                                      frontSearchImage);
                mc.Games.Add(game);
                mc.SaveChanges();

                foreach (var sc in screenshots)
                {
                    mc.GameImages.Add(new GameImage(game.Id, sc));
                }

                foreach (var v in videos)
                {
                    mc.GameVideos.Add(new GameVideo(game.Id, v));
                }

                mc.SaveChanges();
                PageManager.MainFrame.Navigate(new StorePage());
            }
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }

        public void FillDevGroups()
        {
            devGroups_ComboBox.Items.Add("< Новая группа разработчика >");
            var devGroups = App.Context.Developers.Where(x => x.Owner == App.CurrentUser).ToList();
            if (devGroups.Any())
            {
                foreach (var group in devGroups)
                {
                    devGroups_ComboBox.Items.Add(new DevGroupComboBoxUserControl(group));
                }
                devGroups_ComboBox.SelectedIndex = 1;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            if (!btn.Name.Equals("addGame_Button"))
            {
                if (btn.Tag.Equals("icon"))
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "Выберите изображение";
                    ofd.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                    if (ofd.ShowDialog(WindowManager.GetWindow<MainWindow>()) == true)
                    {
                        libraryIcon = ImageHelper.CreateImage(ofd.FileName);
                        btn.Content = ofd.FileName.Split('\\').Last();
                    }
                }
                else if (btn.Tag.Equals("logo"))
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "Выберите изображение";
                    ofd.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                    if (ofd.ShowDialog(WindowManager.GetWindow<MainWindow>()) == true)
                    {
                        libraryLogo = ImageHelper.CreateImage(ofd.FileName);
                        btn.Content = ofd.FileName.Split('\\').Last();
                    }
                }
                else if (btn.Tag.Equals("back"))
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "Выберите изображение";
                    ofd.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                    if (ofd.ShowDialog(WindowManager.GetWindow<MainWindow>()) == true)
                    {
                        backgroundLibraryImage = ImageHelper.CreateImage(ofd.FileName);
                        btn.Content = ofd.FileName.Split('\\').Last();
                    }
                }
                else if (btn.Tag.Equals("vertical"))
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "Выберите изображение";
                    ofd.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                    if (ofd.ShowDialog(WindowManager.GetWindow<MainWindow>()) == true)
                    {
                        verticalLibraryImage = ImageHelper.CreateImage(ofd.FileName);
                        btn.Content = ofd.FileName.Split('\\').Last();
                    }
                }
                else if (btn.Tag.Equals("front"))
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "Выберите изображение";
                    ofd.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                    if (ofd.ShowDialog(WindowManager.GetWindow<MainWindow>()) == true)
                    {
                        frontImage = ImageHelper.CreateImage(ofd.FileName);
                        btn.Content = ofd.FileName.Split('\\').Last();
                    }
                }
                else if (btn.Tag.Equals("search"))
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "Выберите изображение";
                    ofd.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                    if (ofd.ShowDialog(WindowManager.GetWindow<MainWindow>()) == true)
                    {
                        frontSearchImage = ImageHelper.CreateImage(ofd.FileName);
                        btn.Content = ofd.FileName.Split('\\').Last();
                    }
                }
                else if (btn.Tag.Equals("multi"))
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "Выберите изображения";
                    ofd.Multiselect = true;
                    ofd.Filter = "Images (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
                    if (ofd.ShowDialog(WindowManager.GetWindow<MainWindow>()) == true)
                    {
                        string[] filePaths = ofd.FileNames;
                        if (filePaths.Length > 10)
                        {
                            MessageBox.Show(
                                "Максимальное количество файлов: 10",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            return;
                        }
                        else
                        {
                            screenshots.Clear();
                            foreach (var path in filePaths)
                            {
                                screenshots.Add(ImageHelper.CreateImage(path));
                            }
                            if (filePaths.Length > 1)
                                btn.Content = $"{filePaths.Length} изображений";
                            else
                                btn.Content = $"{filePaths.First().Split('\\').Last()}";
                        }
                    }
                }
                else if (btn.Tag.Equals("video"))
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Title = "Выберите видео";
                    ofd.Multiselect = true;
                    ofd.Filter = "Videos (*.mp4)|*.mp4";
                    if (ofd.ShowDialog(WindowManager.GetWindow<MainWindow>()) == true)
                    {
                        string[] filePaths = ofd.FileNames;
                        if (filePaths.Length > 2)
                        {
                            MessageBox.Show(
                                "Максимальное количество файлов: 2",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                            return;
                        }
                        else
                        {
                            videos.Clear();
                            foreach (var path in filePaths)
                            {
                                videos.Add(ImageHelper.CreateImage(path));
                            }
                            if (filePaths.Length > 1)
                                btn.Content = $"{filePaths.Length} видео";
                            else
                                btn.Content = $"{filePaths.First().Split('\\').Last()}";
                        }
                    }
                }
            }
        }
    }
}
