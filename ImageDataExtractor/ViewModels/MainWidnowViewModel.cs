using Components;
using ImageDataExtractor.Enums;
using ImageDataExtractor.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageDataExtractor.ViewModels
{
    class MainWidnowViewModel : INotifyPropertyChanged
    {
        private int selectedImageIndex;
        private ObservableCollection<BitmapImage> imageList;

        private StackPanel _indexingFieldsStackPanel;
        public StackPanel IndexingFieldsStackPanel
        {
            get { return _indexingFieldsStackPanel; }
            set { _indexingFieldsStackPanel = value; OnPropertyChanged(nameof(IndexingFieldsStackPanel)); }
        }
        private ObservableCollection<FieldViewModel> _indexingFields;
        public ObservableCollection<FieldViewModel> IndexingFields
        {
            get { return _indexingFields; }
            set
            {
                _indexingFields = value;
                OnPropertyChanged(nameof(IndexingFields));
            }
        }
        public List<Form> Forms { get; set; }


        private int selectedFormIndex;
        public int SelectedFormIndex
        {
            get { return selectedFormIndex; }
            set
            {
                selectedFormIndex = value;
                OnPropertyChanged(nameof(SelectedFormIndex));
            }
        }

        private Form selectedForm;
        public Form SelectedForm
        {
            get { return selectedForm; }
            set
            {
                selectedForm = value;
                updateCurrentIndexingFields();
                OnPropertyChanged(nameof(SelectedForm));
            }
        }

        public MainWidnowViewModel()
        {
            IndexingFieldsStackPanel = new StackPanel();
            #region dummyData
            Forms = new List<Form>
            {
                new Form
                {
                    Id = 1,
                    Name = "امر تنفيذ",
                    Fields = new List<Field>
                    {
                        new Field
                        {
                            Id = 14,
                            Value = "تاريخ الحكم",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Date,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        },
                        new Field
                        {
                            Id = 16,
                            Value = "التهمة/نوع الجريمة",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Text,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        },
                        new Field
                        {
                            Id = 17,
                            Value = "الحكم",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Text,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        },
                        new Field
                        {
                            Id = 18,
                            Value = "اسم المحكمة",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Text,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        },
                        new Field
                        {
                            Id = 19,
                            Value = "نوع القضية",
                            Required = false,
                            InputValue = "قضية 1",
                            FieldType = FieldType.DropDownList,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = new List<FieldOptionLists>
                            {
                                new FieldOptionLists
                                {
                                    Value = "قضية 1"
                                },
                                new FieldOptionLists
                                {
                                    Value = "قضية 2"
                                },
                                new FieldOptionLists
                                {
                                    Value = "قضية 3"
                                }
                            }
                        },
                        new Field
                        {
                            Id = 20,
                            Value = "سنة القضية",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Date,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        },
                        new Field
                        {
                            Id = 21,
                            Value = "رقم القضية",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Number,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        }
                    }
                },
                new Form
                {
                    Id = 2,
                    Name = "صورة بطاقة أو برنت أحوال أو شهادة ميلاد",
                    Fields = new List<Field>
                    {
                        new Field
                        {
                            Id = 22,
                            Value = "الاسم",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Text,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        },
                        new Field
                        {
                            Id = 23,
                            Value = "رقم البطاقة",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Number,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        }
                    }
                },
                new Form
                {
                    Id = 3,
                    Name = "نموذج بصمة",
                    Fields = new List<Field>
                    {
                        new Field
                        {
                            Id = 24,
                            Value = "تاريخ الحكم",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Date,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        },
                        new Field
                        {
                            Id = 25,
                            Value = "التهمة/نوع الجريمة",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Text,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        },
                        new Field
                        {
                            Id = 26,
                            Value = "الحكم",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Text,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        },
                        new Field
                        {
                            Id = 27,
                            Value = "اسم المحكمة",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Text,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        },
                        new Field
                        {
                            Id = 28,
                            Value = "نوع القضية",
                            Required = false,
                            InputValue = "جناية",
                            FieldType = FieldType.DropDownList,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = new List<FieldOptionLists>
                            {
                                new FieldOptionLists
                                {
                                    Value = "جناية"
                                },
                                new FieldOptionLists
                                {
                                    Value = "جنحة"
                                }
                            }
                        },
                        new Field
                        {
                            Id = 29,
                            Value = "سنة القضية",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Number,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        },
                        new Field
                        {
                            Id = 30,
                            Value = "رقم القضية",
                            Required = false,
                            InputValue = "",
                            FieldType = FieldType.Number,
                            CascadingFieldId = null,
                            Unique = false,
                            FieldOptionLists = null
                        }
                    }
                }
            };
            #endregion
            NameScope.SetNameScope(IndexingFieldsStackPanel, new NameScope());
            ImageList = new ObservableCollection<BitmapImage>();
            SelectedImageIndex = -1;
            IndexingFields = new ObservableCollection<FieldViewModel>();
            selectedForm = Forms.FirstOrDefault();
            updateCurrentIndexingFields();

        }

        public ObservableCollection<BitmapImage> ImageList
        {
            get { return imageList; }
            set
            {
                imageList = value;
                OnPropertyChanged(nameof(ImageList));
            }
        }

        public int SelectedImageIndex
        {
            get { return selectedImageIndex; }
            set
            {
                selectedImageIndex = value;
                OnPropertyChanged(nameof(SelectedImageIndex));
                if (selectedImageIndex >= 0 && selectedImageIndex < ImageList.Count)
                {
                    SelectedImage = ImageList[selectedImageIndex];
                }
                else
                {
                    SelectedImage = null;
                }
            }
        }


        private BitmapImage selectedImage;
        public BitmapImage SelectedImage
        {
            get { return selectedImage; }
            set
            {
                selectedImage = value;
                OnPropertyChanged(nameof(SelectedImage));
            }
        }

        public ICommand FinishButton => new RelayCommand(Finish);

        private async void Finish(object obj)
        {
            try
            {
                ExportImagesAndCSV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }
        public ICommand SaveFieldsCommand => new RelayCommand(SaveFields);

        private void SaveFields(object obj)
        {
            if (SelectedForm != null)
            {
                foreach (FieldViewModel fieldViewModel in IndexingFields)
                {
                    Field field = selectedForm.Fields.FirstOrDefault(f => f.Value == fieldViewModel.Label);

                    if (field != null)
                    {
                        if (field.FieldType == FieldType.Text)
                        {
                            if (fieldViewModel.FieldTypeInputControl is TextBox textBox)
                            {
                                field.InputValue = textBox.Text;
                            }
                        }
                        else if (field.FieldType == FieldType.Number)
                        {
                            if (fieldViewModel.FieldTypeInputControl is TextBox textBox)
                            {
                                if (int.TryParse(textBox.Text, out int numberValue))
                                {
                                    field.InputValue = numberValue.ToString();
                                }
                                else
                                {
                                }
                            }
                        }
                        else if (field.FieldType == FieldType.Date)
                        {
                            if (fieldViewModel.FieldTypeInputControl is DatePicker datePicker)
                            {
                                field.InputValue = datePicker.SelectedDate?.ToString("yyyy-MM-dd");
                            }
                        }
                        else if (field.FieldType == FieldType.DropDownList)
                        {
                            if (fieldViewModel.FieldTypeInputControl is ComboBox comboBox)
                            {
                                field.InputValue = comboBox.SelectedItem?.ToString();
                            }
                        }
                    }
                }
                MessageBox.Show("تم الحفظ بنجاح", "نجاح", MessageBoxButton.OK);
            }
        }

        public ICommand SelectFormCommand => new RelayCommand(SelectForm);

        private async void SelectForm(object obj)
        {
            try
            {
                if (SelectedFormIndex >= 0 && SelectedFormIndex < Forms.Count)
                {
                    SelectedForm = Forms[SelectedFormIndex];

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK);
            }
        }

        public ICommand LoadImagesCommand => new RelayCommand(LoadImages);

        private void LoadImages(object obj)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.tif;*.bmp;*.gif|All Files|*.*",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (var filePath in openFileDialog.FileNames)
                {
                    try
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.UriSource = new Uri(filePath);
                        image.EndInit();
                        ImageList.Add(image);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"لم يتم تحميل الصور: {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void ExportImagesAndCSV()
        {
            try
            {
                int nextFolderNumber = 1;
                string exportFolderBase = @"C:\ExportedData";
                string exportFolder;

                do
                {
                    exportFolder = Path.Combine(exportFolderBase, nextFolderNumber.ToString("D5"));
                    nextFolderNumber++;
                } while (Directory.Exists(exportFolder));

                Directory.CreateDirectory(exportFolder);

                for (int i = 0; i < ImageList.Count; i++)
                {
                    string imagePath = Path.Combine(exportFolder, $"Image_{i + 1}.jpg");
                    SaveBitmapImageAsJpeg(ImageList[i], imagePath);
                }
                string pdfFilePath = Path.Combine(exportFolder, nextFolderNumber.ToString("D5") + ".pdf");
                ExportToPDF(pdfFilePath, exportFolder);
                string csvFilePath = Path.Combine(exportFolder, "exported_data.csv");
                ExportToCSV(csvFilePath);

                MessageBox.Show("تم استخراج الصور وحقول الفهرسة بنجاح", "Success", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا فى استخراج الصور والحقول: {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ExportToPDF(string pdfFileName, string folderPath)
        {
            try
            {

                Document doc = new Document();
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(pdfFileName, FileMode.Create));

                doc.Open();

                // Add images to the PDF
                for (int i = 0; i < ImageList.Count; i++)
                {
                    string imagePath = Path.Combine(folderPath, $"Image_{i + 1}.jpg");
                    iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                    image.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    doc.Add(image);
                }

                doc.Close();
                writer.Close();

                MessageBox.Show($"تم استخراخ  '{pdfFileName}' بنجاح.", "نجاح", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"خطا فى استخراج ملف: {ex.Message}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void SaveBitmapImageAsJpeg(BitmapImage image, string filePath)
        {
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                encoder.Save(fs);
            }
        }

        private void ExportToCSV(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                StringBuilder csvContent = new StringBuilder();

                StringBuilder headerRow = new StringBuilder();
                foreach (FieldViewModel fieldViewModel in IndexingFields)
                {
                    headerRow.Append($"{fieldViewModel.Label};");
                }
                csvContent.AppendLine(headerRow.ToString());

                StringBuilder dataRow = new StringBuilder();
                foreach (var field in selectedForm.Fields)
                {
                    dataRow.Append($"{field.InputValue};");
                }
                csvContent.AppendLine(dataRow.ToString());

                sw.Write(csvContent.ToString());
            }


        }

        private void updateCurrentIndexingFields()
        {
            try
            {
                IndexingFields.Clear();
                NameScope.SetNameScope(IndexingFieldsStackPanel, new NameScope());

                foreach (var item in selectedForm.Fields)
                {
                    UIElement fieldTypeInputControl = null;

                    switch (item.FieldType)
                    {
                        case FieldType.Text:
                            TextBox txtText = new TextBox();
                            txtText.Name = "Indexing_" + item.Id.ToString();
                            txtText.Text = (item.InputValue != null && item.InputValue.Length > 0) ? item.InputValue : "";
                            txtText.Text = item.InputValue ?? "";
                            fieldTypeInputControl = txtText;
                            txtText.MaxLength = 50;
                            ApplyModernTextBoxStyle(txtText);
                            IndexingFieldsStackPanel.RegisterName(txtText.Name, txtText);
                            break;

                        case FieldType.Number:
                            TextBox txtNumber = new TextBox();
                            txtNumber.Name = "Indexing_" + item.Id.ToString();
                            txtNumber.Text = (item.InputValue != null && item.InputValue.Length > 0) ? item.InputValue : "";
                            fieldTypeInputControl = txtNumber;
                            txtNumber.PreviewTextInput += IntegerTextBox_PreviewTextInput;
                            ApplyModernTextBoxStyle(txtNumber);
                            IndexingFieldsStackPanel.RegisterName(txtNumber.Name, txtNumber);
                            break;

                        case FieldType.Decimal:
                            TextBox txtFloat = new TextBox();
                            txtFloat.Name = "Indexing_" + item.Id.ToString();
                            txtFloat.Text = (item.InputValue != null && item.InputValue.Length > 0) ? item.InputValue : "";
                            fieldTypeInputControl = txtFloat;
                            txtFloat.MaxLength = 9;
                            txtFloat.PreviewTextInput += DecimalTextBox_PreviewTextInput;
                            ApplyModernTextBoxStyle(txtFloat);
                            IndexingFieldsStackPanel.RegisterName(txtFloat.Name, txtFloat);
                            break;

                        case FieldType.Date:
                            DatePicker datePicker = new DatePicker();
                            datePicker.Name = "Indexing_" + item.Id.ToString();
                            fieldTypeInputControl = datePicker;
                            datePicker.SelectedDateFormat = DatePickerFormat.Short;
                            datePicker.Language = XmlLanguage.GetLanguage("en-GB");
                            if (item.InputValue != null && DateTime.TryParse(item.InputValue, out DateTime date))
                            {
                                datePicker.SelectedDate = date;
                                datePicker.Text = date.ToString("dd/MM/yyyy");
                            }
                            ApplyModernDatePickerStyle(datePicker);
                            IndexingFieldsStackPanel.RegisterName(datePicker.Name, datePicker);
                            break;

                        case FieldType.Memo:
                            TextBox txtMemo = new TextBox();
                            txtMemo.Name = "Indexing_" + item.Id.ToString();
                            txtMemo.Text = (item.InputValue != null && item.InputValue.Length > 0) ? item.InputValue : "";
                            fieldTypeInputControl = txtMemo;
                            ApplyModernMemoTextBoxStyle(txtMemo);
                            IndexingFieldsStackPanel.RegisterName(txtMemo.Name, txtMemo);
                            break;

                        case FieldType.DropDownList:
                            if (item.FieldOptionLists == null)
                            {
                                TextBox txtTextDrop = new TextBox();
                                txtTextDrop.Name = "Indexing_" + item.Id.ToString();
                                txtTextDrop.Text = item.InputValue ?? "";
                                fieldTypeInputControl = txtTextDrop;
                                ApplyModernTextBoxStyle(txtTextDrop);
                                IndexingFieldsStackPanel.RegisterName(txtTextDrop.Name, txtTextDrop);
                            }
                            else
                            {
                                ComboBox comboBox = new ComboBox();
                                comboBox.Name = "Indexing_" + item.Id.ToString();
                                fieldTypeInputControl = comboBox;
                                comboBox.ItemsSource = item.FieldOptionLists.Select(opt => opt.Value);
                                ApplyModernComboBoxStyle(comboBox);
                                IndexingFieldsStackPanel.RegisterName(comboBox.Name, comboBox);

                                if (item.InputValue == null && item.FieldOptionLists.Any())
                                {
                                    comboBox.SelectedIndex = 0;
                                }
                                else
                                {
                                    comboBox.SelectedItem = item.InputValue;
                                }
                                if (item.CascadingFieldId > 0)
                                {
                                    ComboBox cascadingComboBox = (ComboBox)IndexingFieldsStackPanel.FindName("Indexing_" + item.CascadingFieldId.ToString());
                                    if (cascadingComboBox != null)
                                    {
                                        comboBox.ItemsSource = item.FieldOptionLists
                                             .Where(opt => opt.CascadingFieldOptions?.Value == cascadingComboBox.SelectedItem?.ToString())
                                             .Select(opt => opt.Value);
                                        cascadingComboBox.SelectionChanged += (sender, e) =>
                                        {
                                            string selectedValue = cascadingComboBox.SelectedItem?.ToString();
                                            comboBox.ItemsSource = item.FieldOptionLists
                                                .Where(opt => opt.CascadingFieldOptions?.Value == selectedValue)
                                                .Select(opt => opt.Value);
                                            comboBox.SelectedIndex = 0;
                                        };
                                    }
                                }
                                string selectedItem = comboBox.SelectedItem?.ToString();
                                var selectedFieldOptionList = item.FieldOptionLists.FirstOrDefault(opt => opt.Value == selectedItem);
                                if (selectedFieldOptionList != null && selectedFieldOptionList.FieldOptionValidations != null && selectedFieldOptionList.FieldOptionValidations.Count > 0)
                                {
                                    ApplyFieldOptionValidations(selectedFieldOptionList, comboBox.Name);
                                    comboBox.SelectionChanged += (sender, e) =>
                                    {
                                        ApplyFieldOptionValidations(selectedFieldOptionList, comboBox.Name);
                                    };
                                }
                            }
                            break;

                        case FieldType.CheckBox:
                            CheckBox checkBox = new CheckBox();
                            checkBox.Name = "Indexing_" + item.Id.ToString();
                            fieldTypeInputControl = checkBox;
                            ApplyModernCheckBoxStyle(checkBox);
                            checkBox.HorizontalAlignment = HorizontalAlignment.Center;
                            IndexingFieldsStackPanel.RegisterName(checkBox.Name, checkBox);
                            if (item.InputValue != null && bool.TryParse(item.InputValue, out bool isChecked))
                            {
                                checkBox.IsChecked = isChecked;
                            }
                            break;
                    }

                    if (fieldTypeInputControl != null)
                    {
                        FieldViewModel fieldViewModel = new FieldViewModel();
                        fieldViewModel.Label = item.Value;
                        fieldViewModel.FieldTypeInputControl = fieldTypeInputControl;
                        IndexingFields.Add(fieldViewModel);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK);
            }
        }

        #region validation
        private void ApplyFieldOptionValidations(FieldOptionLists selectedFieldOptionList, string comboBoxName)
        {
            try
            {
                var comboBox = (ComboBox)IndexingFieldsStackPanel.FindName(comboBoxName);
                string selectedItem = comboBox.SelectedItem?.ToString();
                foreach (var validation in selectedFieldOptionList.FieldOptionValidations)
                {
                    var fieldToValidate = (Control)IndexingFieldsStackPanel.FindName("Indexing_" + validation.FieldId.ToString());
                    if (fieldToValidate != null && selectedItem == validation.CascadingFieldOptionsValue)
                    {
                        if (validation.Required)
                        {
                            fieldToValidate.SetValue(Validation.HasErrorProperty, true);
                        }
                        if (validation.MaxLength != null)
                        {
                            if (fieldToValidate is TextBox textBox)
                            {
                                textBox.MaxLength = validation.MaxLength.Value;
                                textBox.TextChanged += (sender, e) =>
                                {
                                    if (textBox.Text.Length < validation.MaxLength.Value)
                                    {
                                        textBox.Tag = "Minimum length is " + validation.MaxLength.Value + " characters.";
                                        textBox.BorderBrush = Brushes.Red;
                                    }
                                    else
                                    {
                                        textBox.ToolTip = null;
                                        textBox.ClearValue(TextBox.BorderBrushProperty);
                                    }
                                };
                            }
                        }
                        if (validation.IsNumbric)
                        {
                            // Apply numeric validation
                        }
                    }
                    else if (fieldToValidate != null)
                    {
                        if (fieldToValidate is TextBox textBox)
                        {
                            textBox.ClearValue(TextBox.MaxLengthProperty);
                            textBox.Tag = null;
                            textBox.ClearValue(TextBox.BorderBrushProperty);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK);
            }
        }

        private void IntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumericInput(e.Text))
            {
                e.Handled = true;
            }
        }

        private void DecimalTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsDecimalInput(e.Text, (TextBox)sender))
            {
                e.Handled = true;
            }
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".")
            {
                e.Handled = true;
            }
            else
            {
                TextBox textBox = (TextBox)sender;
                string newText = textBox.Text.Insert(textBox.CaretIndex, e.Text);
                if (newText.Count(c => c == '.') > 1)
                {
                    e.Handled = true;
                }
            }
        }

        private bool IsNumericInput(string text)
        {
            return int.TryParse(text, out _);
        }

        private bool IsDecimalInput(string text, TextBox textBox)
        {
            return decimal.TryParse(text, out _) && !textBox.Text.Contains('.');
        }

        #endregion
        #region style
        private void ApplyModernTextBoxStyle(TextBox textBox)
        {
            textBox.Style = (Style)Application.Current.Resources["ModernTextBoxStyle"];
        }

        private void ApplyModernDatePickerStyle(DatePicker datePicker)
        {
            datePicker.Style = (Style)Application.Current.Resources["ModernDatePickerStyle"];
        }

        private void ApplyModernMemoTextBoxStyle(TextBox textBox)
        {
            textBox.Style = (Style)Application.Current.Resources["ModernMemoTextBoxStyle"];
        }

        private void ApplyModernComboBoxStyle(ComboBox comboBox)
        {
            comboBox.Style = (Style)Application.Current.Resources["ModernComboBoxStyle"];
        }

        private void ApplyModernCheckBoxStyle(CheckBox checkBox)
        {
            checkBox.Style = (Style)Application.Current.Resources["ModernCheckBoxStyle"];
        }

        #endregion
        public void HandleSelectionChange()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
