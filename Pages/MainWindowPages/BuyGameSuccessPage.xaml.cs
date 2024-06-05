using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using Microsoft.Win32;
using Mist.Helper;
using Mist.Model;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Mist.Pages.MainWindowPages
{
    /// <summary>
    /// Interaction logic for BuyGameSuccessPage.xaml
    /// </summary>
    public partial class BuyGameSuccessPage : Page
    {
        public Game Game;

        public string? CreatedPDFPath = null;
        public BuyGameSuccessPage(Game game)
        {
            InitializeComponent();
            Game = game;
            RefreshData();
        }

        public void RefreshData()
        {
            success_TextBlock.Text =
                $"Вы успешно приобрели {Game.Name} за {Game.UsdPrice} руб. Вы можете настроить и запустить игру через вкладку \"Библиотека\".";

        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var m = this.ActualWidth / 2 - 480;
            var margin = new Thickness(m, 15, m, 0);
            main_Grid.Margin = margin;
        }

        private void goToLibrary_Button_Click(object sender, RoutedEventArgs e)
        {
            PageManager.MainFrame.Navigate(new LibraryPage());
        }

        private void saveCheck_Button_Click(object sender, RoutedEventArgs e)
        {
            CreatePDF(true);
        }

        private void openCheck_Button_Click(object sender, RoutedEventArgs e)
        {
            CreatePDF(false);
        }

        public void CreatePDF(bool saveToFIle)
        {
            string path = "";

            if (saveToFIle == true)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfd.Filter = "PDF документы (*.pdf)|*.pdf";
                sfd.DefaultExt = ".pdf";
                if (sfd.ShowDialog() == true)
                {
                    path = sfd.FileName;
                    CreatedPDFPath = sfd.FileName;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (CreatedPDFPath != null)
                {
                    OpenPDF(CreatedPDFPath);
                    return;
                }
                else
                {
                    path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.pdf");
                }
            }
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Define the font path (make sure the font file is accessible)
            string fontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "times.ttf");

            // Create a base font with identity-H encoding and embed it
            BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(path, FileMode.Create));
            document.Open();

            Font regularFont = new Font(baseFont, 14, Font.NORMAL);
            Font smallFont = new Font(baseFont, 11, Font.NORMAL);
            Font boldFont = new Font(baseFont, 14, Font.BOLD);
            Font smallBoldFont = new Font(baseFont, 12, Font.BOLD);
            Font bigBoldFont = new Font(baseFont, 17, Font.BOLD);
            Font coloredBoldFont = new Font(baseFont, 14, Font.BOLD, new BaseColor(75, 161, 165));
            Font coloredFont = new Font(baseFont, 14, Font.NORMAL, new BaseColor(75, 161, 165));

            document.Add(new Paragraph("ОБЩЕСТВО С ОГРАНИЧЕННОЙ ОТВЕТСТВЕННОСТЬЮ \"MIST LTD.\"", boldFont) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph("450005, Уфа, ул. Кирова, д. 65", regularFont) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph("ИНН 5029069967", regularFont) { Alignment = Element.ALIGN_CENTER });

            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_CENTER, 1))));
            document.Add(new Paragraph(" "));

            document.Add(new Paragraph("КАССОВЫЙ ЧЕК", coloredBoldFont) { Alignment = Element.ALIGN_CENTER });
            document.Add(new Paragraph(" "));

            PdfPTable table1 = new PdfPTable(2);
            table1.WidthPercentage = 100;
            table1.AddCell(new PdfPCell(new Phrase("Приход", smallFont)) { Border = Rectangle.NO_BORDER });
            table1.AddCell(new PdfPCell(new Phrase($"{GetFormattedDateTime(App.Context.UserGames.Where(x => x.UserId == App.CurrentUser.Id && x.GameId == Game.Id).First().AcquiredAt)}", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table1.AddCell(new PdfPCell(new Phrase("Смена", smallFont)) { Border = Rectangle.NO_BORDER });
            table1.AddCell(new PdfPCell(new Phrase("1", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table1.AddCell(new PdfPCell(new Phrase("Кассир", smallFont)) { Border = Rectangle.NO_BORDER });
            table1.AddCell(new PdfPCell(new Phrase("Администратор", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table1.AddCell(new PdfPCell(new Phrase("Телефон или электронный адрес получателя", smallFont)) { Border = Rectangle.NO_BORDER });
            table1.AddCell(new PdfPCell(new Phrase($"{App.CurrentUser.Email}", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table1.AddCell(new PdfPCell(new Phrase("Электронный адрес отправителя", smallFont)) { Border = Rectangle.NO_BORDER });
            table1.AddCell(new PdfPCell(new Phrase("noreply@mistsoftware.com", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table1.AddCell(new PdfPCell(new Phrase("Применяемая система налогообложения", smallFont)) { Border = Rectangle.NO_BORDER });
            table1.AddCell(new PdfPCell(new Phrase("ОСН", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table1.AddCell(new PdfPCell(new Phrase("Признак расчетов в сети Интернет", smallFont)) { Border = Rectangle.NO_BORDER });
            table1.AddCell(new PdfPCell(new Phrase("ДА", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            document.Add(table1);

            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_CENTER, 1))));
            document.Add(new Paragraph(" "));

            document.Add(new Paragraph($"1: {Game.Name}", smallBoldFont));

            PdfPTable table2 = new PdfPTable(2);
            table2.WidthPercentage = 100;
            table2.AddCell(new PdfPCell(new Phrase("", smallFont)) { Border = Rectangle.NO_BORDER });
            table2.AddCell(new PdfPCell(new Phrase($"1 шт. х {Game.UsdPrice}", smallBoldFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table2.AddCell(new PdfPCell(new Phrase("Общая стоимость позиции с учетом скидок и наценок", smallFont)) { Border = Rectangle.NO_BORDER });
            table2.AddCell(new PdfPCell(new Phrase($"{Game.UsdPrice}", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table2.AddCell(new PdfPCell(new Phrase("Ставка НДС", smallFont)) { Border = Rectangle.NO_BORDER });
            table2.AddCell(new PdfPCell(new Phrase("ставка 20%", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table2.AddCell(new PdfPCell(new Phrase("Сумма НДС за предмет расчета", smallFont)) { Border = Rectangle.NO_BORDER });
            table2.AddCell(new PdfPCell(new Phrase($"{(Game.UsdPrice * ((decimal)0.2)).ToString("F2")}", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table2.AddCell(new PdfPCell(new Phrase("Способ расчета", smallFont)) { Border = Rectangle.NO_BORDER });
            table2.AddCell(new PdfPCell(new Phrase("ПОЛНЫЙ РАСЧЕТ", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            document.Add(table2);

            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_CENTER, 1))));
            document.Add(new Paragraph(" "));

            PdfPTable table3 = new PdfPTable(2);
            table3.WidthPercentage = 100;
            table3.AddCell(new PdfPCell(new Phrase("ИТОГ", bigBoldFont)) { Border = Rectangle.NO_BORDER });
            table3.AddCell(new PdfPCell(new Phrase($"= {Game.UsdPrice}", bigBoldFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table3.AddCell(new PdfPCell(new Phrase(" ", bigBoldFont)) { Border = Rectangle.NO_BORDER });
            table3.AddCell(new PdfPCell(new Phrase($" ", bigBoldFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table3.AddCell(new PdfPCell(new Phrase("НАЛИЧНЫМИ", smallFont)) { Border = Rectangle.NO_BORDER });
            table3.AddCell(new PdfPCell(new Phrase("0,00", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table3.AddCell(new PdfPCell(new Phrase("БЕЗНАЛИЧНЫМИ", smallFont)) { Border = Rectangle.NO_BORDER });
            table3.AddCell(new PdfPCell(new Phrase("0,00", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table3.AddCell(new PdfPCell(new Phrase("ЗАЧЕТ ПРЕДОПЛАТЫ (АВАНСА)", smallFont)) { Border = Rectangle.NO_BORDER });
            table3.AddCell(new PdfPCell(new Phrase($"{Game.UsdPrice}", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table3.AddCell(new PdfPCell(new Phrase("СУММА НДС ЧЕКА ПО СТАВКЕ 20%", smallFont)) { Border = Rectangle.NO_BORDER });
            table3.AddCell(new PdfPCell(new Phrase($"{(Game.UsdPrice * ((decimal)0.2)).ToString("F2")}", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            document.Add(table3);

            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_CENTER, 1))));
            document.Add(new Paragraph(" "));

            PdfPTable table4 = new PdfPTable(2);
            table4.WidthPercentage = 100;
            table4.AddCell(new PdfPCell(new Phrase("N ФН", smallFont)) { Border = Rectangle.NO_BORDER });
            table4.AddCell(new PdfPCell(new Phrase("9960440302076748", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table4.AddCell(new PdfPCell(new Phrase("Регистрационный номер ККТ", smallFont)) { Border = Rectangle.NO_BORDER });
            table4.AddCell(new PdfPCell(new Phrase("0006841430011088", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table4.AddCell(new PdfPCell(new Phrase("N ФД", smallFont)) { Border = Rectangle.NO_BORDER });
            table4.AddCell(new PdfPCell(new Phrase("7207", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table4.AddCell(new PdfPCell(new Phrase("ФПД", smallFont)) { Border = Rectangle.NO_BORDER });
            table4.AddCell(new PdfPCell(new Phrase("1014681163", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            table4.AddCell(new PdfPCell(new Phrase("Версия ФФД", smallFont)) { Border = Rectangle.NO_BORDER });
            table4.AddCell(new PdfPCell(new Phrase("1.0", smallFont)) { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT });

            document.Add(table4);

            document.Add(new Paragraph(" "));
            document.Add(new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_CENTER, 1))));
            document.Add(new Paragraph(" "));

            document.Add(new Paragraph("СПАСИБО ЗА ПОКУПКУ!", coloredFont) { Alignment = Element.ALIGN_CENTER });

            document.Close();

            if (!saveToFIle)
            {
                OpenPDF(path);
            }
        }

        public void OpenPDF(string path)
        {
            ProcessStartInfo psi = new ProcessStartInfo(path);
            psi.UseShellExecute = true;
            Process.Start(psi);
        }

        public string GetFormattedDateTime(DateTime datetime)
        {
            return datetime.ToString("dd.MM.yyyy HH:mm");
        }
    }
}
