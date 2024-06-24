using Mist.Helper;
using Mist.Model;
using Mist.UserControls;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Mist.Windows
{
    /// <summary>
    /// Interaction logic for MessengerWindow.xaml
    /// </summary>
    public partial class MessengerWindow : Window
    {
        public User Recipient;
        public MessengerWindow(User recipient)
        {
            InitializeComponent();
            Recipient = recipient;
            RefreshInfo();
            RefreshMessages();
            this.Owner = WindowManager.GetWindow<MainWindow>();
        }

        private void RefreshMessages()
        {
            messages_StackPanel.Children.Clear();
            using (MistContext mc = new MistContext())
            {
                var messages = mc.Messages.Where(f => (f.SenderId == App.CurrentUser.Id && f.Recipient.Id == Recipient.Id) ||
                (f.RecipientId == App.CurrentUser.Id && f.SenderId == Recipient.Id)).OrderBy(f => f.Datetime).ToList();
                foreach (var message in messages)
                {
                    messages_StackPanel.Children.Add(new MessageUserControl(message));
                }
            }
        }

        private void RefreshInfo()
        {
            recipientPfp_Image.Source = ImageHelper.GetImage(Recipient.Pfp);
            recipientNickname_Label.Content = Recipient.Nickname;
            if (Recipient.Status)
            {
                recipientNickname_Label.Foreground = Brushes.DeepSkyBlue;
            }
            else
            {
                recipientNickname_Label.Foreground = Brushes.Gray;
            }
        }

        private void sendMessage_Button_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Regex.Replace(message_TextBox.Text.Trim(), @"\s+", " ")))
            {
                return;
            }
            else
            {
                using (MistContext mc = new MistContext())
                {
                    mc.Messages.Add(new Message(App.CurrentUser.Id, Recipient.Id, Regex.Replace(message_TextBox.Text.Trim(), @"\s+", " "), DateTime.Now, true));
                    mc.SaveChanges();
                }
                App.Context = new MistContext();
                RefreshMessages();
                message_TextBox.Text = String.Empty;
            }
        }

        private void sendMessage_Button_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void sendMessage_Button_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = null;
        }

        private void message_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var theSender = ((TextBox)sender);
            if (String.IsNullOrEmpty(Regex.Replace(theSender.Text.Trim(), @"\s+", " ")))
            {
                sendMessage_Button.Background = new SolidColorBrush((Color)(ColorConverter.ConvertFromString("#24272b")));
                sendMsg_Image.Source = ImageHelper.GetImageFromPath("/Assets/send.png");
            }
            else
            {
                var background = new LinearGradientBrush();
                background.StartPoint = new Point(0, 0);
                background.EndPoint = new Point(1, 1);
                background.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#1a9fff")), 0.0));
                background.GradientStops.Add(new GradientStop((Color)(ColorConverter.ConvertFromString("#0057d6")), 1.0));
                sendMessage_Button.Background = background;
                sendMsg_Image.Source = ImageHelper.GetImageFromPath("/Assets/send-white.png");
            }
        }

        private void resize_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                this.DragMove();
            }
        }

        private void message_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Keyboard.IsKeyDown(Key.RightShift))
                {
                    // Add a new line to the TextBox
                    int caretIndex = message_TextBox.CaretIndex;
                    message_TextBox.Text = message_TextBox.Text.Insert(caretIndex, Environment.NewLine);
                    message_TextBox.CaretIndex = caretIndex + Environment.NewLine.Length;
                    return;
                }
                sendMessage_Button_Click(sendMessage_Button, new RoutedEventArgs());
            }
        }

        private void message_TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int caretIndex = message_TextBox.CaretIndex;
                message_TextBox.Text = message_TextBox.Text.Insert(caretIndex, Environment.NewLine);
                message_TextBox.CaretIndex = caretIndex - Environment.NewLine.Length;
                return;
            }
        }
    }
}
