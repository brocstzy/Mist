using Mist.Helper;
using Mist.Model;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;

namespace Mist.UserControls
{
    /// <summary>
    /// Interaction logic for MessageUserControl.xaml
    /// </summary>
    public partial class MessageUserControl : UserControl
    {
        public Message Message;
        public MessageUserControl(Message message)
        {
            InitializeComponent();
            Message = message;
            RefreshThis();
        }

        private void RefreshThis()
        {
            pfp_Image.Source = ImageHelper.GetImage(Message.Sender.Pfp);
            nickname_Label.Content = Message.Sender.Nickname;
            timestamp_Label.Content = $"{Message.Datetime.Day}" +
                        $" {CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Message.Datetime.Month).Substring(0, 3)}" +
                        $" в {Message.Datetime.Hour.ToString("D2")}:{Message.Datetime.Minute.ToString("D2")}";
            message_TextBlock.Text = Message.Message1;
            if (Message.Sender.Status)
            {
                nickname_Label.Foreground = Brushes.DeepSkyBlue;
            }
            else
            {
                nickname_Label.Foreground = Brushes.Gray;
            }
        }
    }
}
