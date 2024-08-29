using IB_LW_2.HashingImplementation;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using Path = System.IO.Path;

namespace IB_LW_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string resultSHA = "";
        public MainWindow()
        {
            InitializeComponent();
            CreateDataTable();
        }
        private void CreateDataTable()
        {
            ObservableCollection<DataItem> dataItems = new ObservableCollection<DataItem>();

            dataItems.Add(new DataItem { t = "0 ≤ t ≤ 19", F_t = "(m ∧ l) ∨ (¬m ∧ k)", K_t = "0x5A827999" });
            dataItems.Add(new DataItem { t = "20 ≤ t ≤ 39", F_t = "m ⊕ l ⊕ k", K_t = "0x6ED9EBA1" });
            dataItems.Add(new DataItem { t = "40 ≤ t ≤ 59", F_t = "(m ∧ l) ∨ (m ∧ k) ∨ (l ∧ k)", K_t = "0x8F1BBCDC" });
            dataItems.Add(new DataItem { t = "60 ≤ t ≤ 79", F_t = "m ⊕ l ⊕ k", K_t = "0xCA62C1D6" });

            dataGrid.ItemsSource = dataItems;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            saveFileDialog.DefaultExt = ".txt";

            // Отображение диалогового окна и получение выбранного пути файла
            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                string filePath2 = saveFileDialog.FileName;

                // Сохранение текста в выбранный файл
                File.WriteAllText(filePath2, resultSHA);
            }
        }
        private void hashing(byte[] content)
        {
            string result = IB_LW_2.HashingImplementation.Converter.ToHex(IB_LW_2.HashingImplementation.SHA1.SHA(content));
            TestText.Text = result;
            resultSHA = result;
        }
        private void ButtonUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                byte[] fileContent = File.ReadAllBytes(filePath);
                resultSHA = "";
                hashing(fileContent);
            }
        }

        private void Canvas_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void Canvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is string[] droppedFiles)
            {
                foreach (string filePath in droppedFiles)
                {
                    byte[] fileContent = File.ReadAllBytes(filePath);
                    resultSHA = "";
                    hashing(fileContent);     
                }
            }
        }

    }
}
