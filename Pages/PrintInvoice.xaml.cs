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
using курсовая_работа.Models;
using Xceed.Words.NET;
using Xceed.Document.NET;
using Word = Microsoft.Office.Interop.Word;

namespace курсовая_работа.Pages
{
    /// <summary>
    /// Логика взаимодействия для PrintInvoice.xaml
    /// </summary>
    public partial class PrintInvoice : Page
    {
        private List<INVOICE> invoices;
        private List<CONSUMABLE> consumbles;
        CONSUMABLE selectedConsumable;
        INVOICE selectedInvoice;
        EMPLOYEE curentuser;
        public PrintInvoice(EMPLOYEE user)
        {
            InitializeComponent();
            curentuser = user;
            invoices = new List<INVOICE>();
            consumbles = new List<CONSUMABLE>();
            DataGrid.ItemsSource = App.SR1Entities.INVOICEs.ToList();
            DataGrid2.ItemsSource = App.SR1Entities.CONSUMABLES.ToList();
        }


        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem == null && DataGrid2.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите накладную для формирования документа ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                if (selectedInvoice != null)
                {
                  
                    try
                    {
                        var wordApp = new Word.Application();
                        var doc = wordApp.Documents.Add();

                        Word.Paragraph paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = $"От « {DateTime.Now.ToString("dd.MM.yyyy")} г.»";
                        paragraph.Range.Font.Size = 12;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = $"Накладная приходная № {selectedInvoice.INVOICE_ID}";
                        paragraph.Range.Font.Size = 14;
                        paragraph.Range.Font.Bold = 1;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = "Кому __________________________ _________________________________";
                        paragraph.Range.Font.Size = 12;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = $"                                                 должность                                        Ф.,И.,О";
                        paragraph.Range.Font.Size = 8;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = $"От кого                     {curentuser.ROL.NAMES}          {curentuser.FIRSTNAME} {curentuser.MIDLENAME} {curentuser.LASTNAME}";
                        paragraph.Range.Font.Size = 12;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = "                ________________________ _________________________________";
                        paragraph.Range.Font.Size = 12;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = $"                                                  должность                                       Ф.,И.,О";
                        paragraph.Range.Font.Size = 8;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        Word.Table table = doc.Tables.Add(doc.Paragraphs[doc.Paragraphs.Count].Range, 1, 5);
                        table.Range.Font.Bold = 1;
                        table.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        table.AllowAutoFit = true;
                        table.Borders.Enable = 1;

                        table.Cell(1, 1).Range.Text = "№ п-п";
                        table.Cell(1, 2).Range.Text = "Наименование";
                        table.Cell(1, 3).Range.Text = "Кол-во";
                        table.Cell(1, 4).Range.Text = "Цена";
                        table.Cell(1, 5).Range.Text = "Сумма";

                        List<GOODS_ARIVAL> goods = App.SR1Entities.GOODS_ARIVAL.Where(g => g.INVOICE_ID == selectedInvoice.INVOICE_ID).ToList();

                        int count = 1;
                        foreach (var item in goods)
                        {
                            DELIVERY deliveryItem = App.SR1Entities.DELIVERies.FirstOrDefault(d => d.DELIVERY_ID == item.DELIVERY_ID);
                            if (deliveryItem != null)
                            {
                                table.Rows.Add();
                                table.Cell(count + 1, 1).Range.Text = count.ToString();
                                table.Cell(count + 1, 2).Range.Text = deliveryItem.NAMES;
                                table.Cell(count + 1, 3).Range.Text = item.COUNTS.ToString();
                                table.Cell(count + 1, 4).Range.Text = item.PRICE.ToString();
                                table.Cell(count + 1, 5).Range.Text = (item.COUNTS * item.PRICE).ToString();
                                count++;
                            }
                        }

                        Word.Paragraph totalParagraph = doc.Paragraphs.Add();
                        totalParagraph.Range.Text = $"Сумма: {goods.Sum(g => g.COUNTS * g.PRICE)}";
                        totalParagraph.Range.Font.Size = 12;
                        totalParagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        totalParagraph.Range.InsertParagraphAfter();

                        totalParagraph = doc.Paragraphs.Add();
                        totalParagraph.Range.Text = $"Сумма НДС: {goods.Sum(g => g.COUNTS * g.PRICE) * 0.2m}";
                        totalParagraph.Range.Font.Size = 12;
                        totalParagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        totalParagraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = "Сдал: __________ ________________ Принял: __________ ________________";
                        paragraph.Range.Font.Size = 12;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = "                        подпись                    Ф., И., О.                                                               подпись                     Ф., И., О.";
                        paragraph.Range.Font.Size = 8;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();


                        doc.SaveAs2(filePath, Word.WdSaveFormat.wdFormatDocumentDefault);
                        doc.ExportAsFixedFormat(filePath, Word.WdExportFormat.wdExportFormatPDF);

                        doc.Close();
                        wordApp.Quit();

                        MessageBox.Show("Накладная успешно сохранена в файле", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при создании накладной: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }


                else if (selectedConsumable != null)
                {
                    try
                    {
                        var wordApp = new Word.Application();
                        var doc = wordApp.Documents.Add();

                        Word.Paragraph paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = $"От « {DateTime.Now.ToString("dd.MM.yyyy")} г.»";
                        paragraph.Range.Font.Size = 12;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = $"Накладная расходная № {selectedConsumable.CONSUMABLES_ID}";
                        paragraph.Range.Font.Size = 14;
                        paragraph.Range.Font.Bold = 1;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = "Кому __________________________ _________________________________";
                        paragraph.Range.Font.Size = 12;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = $"                                                 должность                                        Ф.,И.,О";
                        paragraph.Range.Font.Size = 8;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = $"От кого                     {curentuser.ROL.NAMES}          {curentuser.FIRSTNAME} {curentuser.MIDLENAME} {curentuser.LASTNAME}";
                        paragraph.Range.Font.Size = 12;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = "                ________________________ _________________________________";
                        paragraph.Range.Font.Size = 12;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = $"                                                  должность                                       Ф.,И.,О";
                        paragraph.Range.Font.Size = 8;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        Word.Table table = doc.Tables.Add(doc.Paragraphs[doc.Paragraphs.Count].Range, 1, 5);
                        table.Range.Font.Bold = 1;
                        table.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        table.AllowAutoFit = true;
                        table.Borders.Enable = 1;
                        table.Cell(1, 1).Range.Text = "№ п-п";
                        table.Cell(1, 2).Range.Text = "Наименование";
                        table.Cell(1, 3).Range.Text = "Кол-во";
                        table.Cell(1, 4).Range.Text = "Цена";
                        table.Cell(1, 5).Range.Text = "Сумма";

                        List<GOODS_CARE> goods = App.SR1Entities.GOODS_CARE.Where(g => g.CONSUMABLES_ID == selectedConsumable.CONSUMABLES_ID).ToList();

                        int count = 1;
                        foreach (var item in goods)
                        {
                            DELIVERY deliveryItem = App.SR1Entities.DELIVERies.FirstOrDefault(d => d.DELIVERY_ID == item.DELIVERY_ID);
                            if (deliveryItem != null)
                            {
                                table.Rows.Add();
                                table.Cell(count + 1, 1).Range.Text = count.ToString();
                                table.Cell(count + 1, 2).Range.Text = deliveryItem.NAMES;
                                table.Cell(count + 1, 3).Range.Text = item.COUNTS.ToString();
                                table.Cell(count + 1, 4).Range.Text = item.PRICE.ToString();
                                table.Cell(count + 1, 5).Range.Text = (item.COUNTS * item.PRICE).ToString();
                                count++;
                            }
                        }

                        Word.Paragraph totalParagraph = doc.Paragraphs.Add();
                        totalParagraph.Range.Text = $"Сумма: {goods.Sum(g => g.COUNTS * g.PRICE)}";
                        totalParagraph.Range.Font.Size = 12;
                        totalParagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        totalParagraph.Range.InsertParagraphAfter();

                        totalParagraph = doc.Paragraphs.Add();
                        totalParagraph.Range.Text = $"Сумма НДС: {goods.Sum(g => g.COUNTS * g.PRICE) * 0.2m}";
                        totalParagraph.Range.Font.Size = 12;
                        totalParagraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        totalParagraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = "Сдал: __________ ________________ Принял: __________ ________________";
                        paragraph.Range.Font.Size = 12;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();

                        paragraph = doc.Paragraphs.Add();
                        paragraph.Range.Text = "                        подпись                    Ф., И., О.                                                               подпись                     Ф., И., О.";
                        paragraph.Range.Font.Size = 8;
                        paragraph.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        paragraph.Range.InsertParagraphAfter();


                        doc.SaveAs2(filePath, Word.WdSaveFormat.wdFormatDocumentDefault);
                        doc.ExportAsFixedFormat(filePath, Word.WdExportFormat.wdExportFormatPDF);

                        doc.Close();
                        wordApp.Quit();

                        MessageBox.Show("Накладная успешно сохранена в файле", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при создании накладной: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите накладную для создания документа", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void DataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedConsumable = DataGrid2.SelectedItem as CONSUMABLE;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedInvoice = DataGrid.SelectedItem as INVOICE;
        }
    }

}