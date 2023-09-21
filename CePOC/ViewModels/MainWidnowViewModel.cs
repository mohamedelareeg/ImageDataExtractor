using Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using Microsoft.VisualBasic.FileIO;
using static System.Net.WebRequestMethods;
using iTextSharp.text.pdf;
using System.Windows.Documents;
using iTextSharp.text;

namespace CePOC.ViewModels
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
                    Name = "امر تنفيذ", // Add your form name here
                    Fields = new List<Field>
                    {
                        new Field
                        {
                            Id = 14,
                            Value = "تاريخ الحكم",
                            Required = false,
                            InputValue = "", // Replace with the actual date value
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
                            InputValue = "", // Replace with the actual text value
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
                            InputValue = "", // Replace with the actual text value
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
                            InputValue = "", // Replace with the actual text value
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
                            InputValue = "قضية 1", // Default value or leave it empty
                            FieldType = FieldType.DropDownList, // Use ComboBox for drop-down list
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
                                // Add more case types as needed
                            }
                        },
                        new Field
                        {
                            Id = 20,
                            Value = "سنة القضية",
                            Required = false,
                            InputValue = "", // Replace with the actual date value
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
                            InputValue = "", // Replace with the actual integer value
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
                                InputValue = "", // Replace with the actual text value
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
                                InputValue = "", // Replace with the actual number value
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
            InputValue = "", // Replace with the actual date value
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
            InputValue = "", // Replace with the actual text value
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
            InputValue = "", // Replace with the actual text value
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
            InputValue = "", // Replace with the actual text value
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
            InputValue = "جناية", // Default value or leave it empty
            FieldType = FieldType.DropDownList, // Use ComboBox for drop-down list
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
            InputValue = "", // Replace with the actual number value
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
            InputValue = "", // Replace with the actual number value
            FieldType = FieldType.Number,
            CascadingFieldId = null,
            Unique = false,
            FieldOptionLists = null
        }
    }
}



            };
    
            
    
    
            
            #endregion
            // Create a custom NameScope for the IndexingFieldsStackPanel
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
            // Ensure that a form is selected
            if (SelectedForm != null)
            {
                // Iterate through the fields of the selected form
                foreach (FieldViewModel fieldViewModel in IndexingFields)
                {
                    // Find the corresponding Field based on the Label
                    Field field = selectedForm.Fields.FirstOrDefault(f => f.Value == fieldViewModel.Label);

                    if (field != null)
                    {
                        // Update the InputValue of the Field with the control's value
                        if (field.FieldType == FieldType.Text)
                        {
                            // Assuming fieldViewModel.FieldTypeInputControl is TextBox
                            if (fieldViewModel.FieldTypeInputControl is TextBox textBox)
                            {
                                field.InputValue = textBox.Text;
                            }
                        }
                        else if (field.FieldType == FieldType.Number)
                        {
                            // Assuming fieldViewModel.FieldTypeInputControl is TextBox for numbers
                            if (fieldViewModel.FieldTypeInputControl is TextBox textBox)
                            {
                                if (int.TryParse(textBox.Text, out int numberValue))
                                {
                                    field.InputValue = numberValue.ToString();
                                }
                                else
                                {
                                    // Handle invalid input (e.g., display an error message)
                                }
                            }
                        }
                        else if (field.FieldType == FieldType.Date)
                        {
                            // Assuming fieldViewModel.FieldTypeInputControl is DatePicker
                            if (fieldViewModel.FieldTypeInputControl is DatePicker datePicker)
                            {
                                field.InputValue = datePicker.SelectedDate?.ToString("yyyy-MM-dd");
                            }
                        }
                        else if (field.FieldType == FieldType.DropDownList)
                        {
                            // Assuming fieldViewModel.FieldTypeInputControl is ComboBox
                            if (fieldViewModel.FieldTypeInputControl is ComboBox comboBox)
                            {
                                field.InputValue = comboBox.SelectedItem?.ToString();
                            }
                        }
                        // Handle other field types as needed
                    }
                }
                MessageBox.Show("Saved Successfully.", "Success", MessageBoxButton.OK);
                // Save the changes to your data model or perform other actions as needed
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
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        public ICommand LoadImagesCommand => new RelayCommand(LoadImages);

        private void LoadImages(object obj)
        {
            // You can load images from a directory or any other source
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
                        MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void ExportImagesAndCSV()
        {
            try
            {
                // Find the last exported folder and increment its name
                int nextFolderNumber = 1;
                string exportFolderBase = @"C:\ExportedData"; // Change the base folder path as needed
                string exportFolder;

                do
                {
                    exportFolder = Path.Combine(exportFolderBase, nextFolderNumber.ToString("D5"));
                    nextFolderNumber++;
                } while (Directory.Exists(exportFolder));

                Directory.CreateDirectory(exportFolder);

                // Export images
                for (int i = 0; i < ImageList.Count; i++)
                {
                    string imagePath = Path.Combine(exportFolder, $"Image_{i + 1}.jpg");
                    SaveBitmapImageAsJpeg(ImageList[i], imagePath);
                }
                //string pdfFilePath = Path.Combine(exportFolder, nextFolderNumber.ToString("D5")+".pdf");
                //ExportToPDF(pdfFilePath , exportFolder);
                // Export CSV file
                string csvFilePath = Path.Combine(exportFolder, "exported_data.csv");
                ExportToCSV(csvFilePath);

                MessageBox.Show("Images and CSV file exported successfully.", "Success", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

                MessageBox.Show($"PDF file '{pdfFileName}' exported successfully.", "Success", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting data to PDF: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            // Create a StreamWriter with UTF-8 encoding
            using (StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                // Create a StringBuilder to build the CSV content
                StringBuilder csvContent = new StringBuilder();

                // Create the header row based on FieldViewModel labels
                StringBuilder headerRow = new StringBuilder();
                foreach (FieldViewModel fieldViewModel in IndexingFields)
                {
                    headerRow.Append($"{fieldViewModel.Label};");
                }
                csvContent.AppendLine(headerRow.ToString());

                // Create a data row with values from the fields list
                StringBuilder dataRow = new StringBuilder();
                foreach (var field in selectedForm.Fields)
                {
                    dataRow.Append($"{field.InputValue};");
                }
                csvContent.AppendLine(dataRow.ToString());

                // Write the CSV content to the StreamWriter
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
                    // Create the appropriate field type input control based on the field type
                    UIElement fieldTypeInputControl = null;

                    switch (item.FieldType)
                    {
                        case FieldType.Text:
                            TextBox txtText = new TextBox();
                            txtText.Name = "Indexing_" + item.Id.ToString();
                            txtText.Text = (item.InputValue != null && item.InputValue.Length > 0) ? item.InputValue : "";
                            txtText.Text = item.InputValue ?? ""; // Display item.InputValue if not null
                            fieldTypeInputControl = txtText;
                            txtText.MaxLength = 50; // Set the maximum length
                            ApplyModernTextBoxStyle(txtText); // Apply custom style to the TextBox



                            IndexingFieldsStackPanel.RegisterName(txtText.Name, txtText);
                            break;

                        case FieldType.Number:
                            TextBox txtNumber = new TextBox();
                            txtNumber.Name = "Indexing_" + item.Id.ToString();
                            txtNumber.Text = (item.InputValue != null && item.InputValue.Length > 0) ? item.InputValue : "";

                            fieldTypeInputControl = txtNumber;
                            //txtNumber.MaxLength = item.opt; // Set the maximum digits
                            txtNumber.PreviewTextInput += IntegerTextBox_PreviewTextInput; // Add event handler for integer input validation
                            ApplyModernTextBoxStyle(txtNumber); // Apply custom style to the TextBox

                            IndexingFieldsStackPanel.RegisterName(txtNumber.Name, txtNumber);
                            break;


                        case FieldType.Decimal:
                            TextBox txtFloat = new TextBox();
                            txtFloat.Name = "Indexing_" + item.Id.ToString();
                            txtFloat.Text = (item.InputValue != null && item.InputValue.Length > 0) ? item.InputValue :"";
                            fieldTypeInputControl = txtFloat;
                            txtFloat.MaxLength = 9; // Set the maximum digits
                            txtFloat.PreviewTextInput += DecimalTextBox_PreviewTextInput; // Add event handler for decimal input validation
                            ApplyModernTextBoxStyle(txtFloat); // Apply custom style to the TextBox

                            IndexingFieldsStackPanel.RegisterName(txtFloat.Name, txtFloat);
                            break;


                        case FieldType.Date:
                            DatePicker datePicker = new DatePicker();
                            datePicker.Name = "Indexing_" + item.Id.ToString();
                            fieldTypeInputControl = datePicker;

                            datePicker.SelectedDateFormat = DatePickerFormat.Short;
                            datePicker.Language = XmlLanguage.GetLanguage("en-GB");

                            // Parse and format the date for display
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
                            ApplyModernMemoTextBoxStyle(txtMemo); // Apply custom style to the TextBox


                            IndexingFieldsStackPanel.RegisterName(txtMemo.Name, txtMemo);
                            break;

                        case FieldType.DropDownList:

                            if (item.FieldOptionLists == null)
                            {
                                TextBox txtTextDrop = new TextBox();
                                txtTextDrop.Name = "Indexing_" + item.Id.ToString();
                                txtTextDrop.Text = item.InputValue ?? ""; // Display item.InputValue if not null
                                fieldTypeInputControl = txtTextDrop;
                                ApplyModernTextBoxStyle(txtTextDrop); // Apply custom style to the TextBox
                                IndexingFieldsStackPanel.RegisterName(txtTextDrop.Name, txtTextDrop);
                            }
                            else
                            {
                                ComboBox comboBox = new ComboBox();
                                comboBox.Name = "Indexing_" + item.Id.ToString();
                                fieldTypeInputControl = comboBox;
                                comboBox.ItemsSource = item.FieldOptionLists.Select(opt => opt.Value); // Set the drop-down options
                                ApplyModernComboBoxStyle(comboBox); // Apply custom style to the ComboBox
                                IndexingFieldsStackPanel.RegisterName(comboBox.Name, comboBox);

                                // Preselect the first item in the FieldOptionLists if item.InputValue is null
                                if (item.InputValue == null && item.FieldOptionLists.Any())
                                {
                                    comboBox.SelectedIndex = 0;
                                }
                                else
                                {
                                    comboBox.SelectedItem = item.InputValue; // Set the selected item based on item.InputValue
                                }
                                if (item.CascadingFieldId > 0)
                                {
                                    ComboBox cascadingComboBox = (ComboBox)IndexingFieldsStackPanel.FindName("Indexing_" + item.CascadingFieldId.ToString());
                                    if (cascadingComboBox != null)
                                    {
                                        comboBox.ItemsSource = item.FieldOptionLists
                                             .Where(opt => opt.CascadingFieldOptions?.Value == cascadingComboBox.SelectedItem?.ToString())
                                             .Select(opt => opt.Value);
                                        // Subscribe to the SelectionChanged event of the cascading ComboBox
                                        cascadingComboBox.SelectionChanged += (sender, e) =>
                                        {
                                            // Perform cascading logic here
                                            // Get the selected value from the cascading ComboBox
                                            string selectedValue = cascadingComboBox.SelectedItem?.ToString();

                                            // Update the items in the current ComboBox based on the selected value
                                            comboBox.ItemsSource = item.FieldOptionLists
                                                .Where(opt => opt.CascadingFieldOptions?.Value == selectedValue)
                                                .Select(opt => opt.Value);

                                            // Reset the selected item to first index
                                            comboBox.SelectedIndex = 0;

                                        };
                                    }
                                }
                                // Find the FieldOptionList based on the selected item
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
                            ApplyModernCheckBoxStyle(checkBox); // Apply custom style to the CheckBox

                            checkBox.HorizontalAlignment = HorizontalAlignment.Center; // Align center
                            IndexingFieldsStackPanel.RegisterName(checkBox.Name, checkBox);

                            // Set the IsChecked property based on item.InputValue
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
                        // fieldViewModel.IsRequired = CurrentFolder.Form.Required; // Set the required flag based on Form.Required

                        IndexingFields.Add(fieldViewModel);
                    }
                    //TODO ApplyFieldOptionValidations here
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }
        #region validation
        private void ApplyFieldOptionValidations(FieldOptionLists selectedFieldOptionList, string comboBoxName)
        {
            try
            {


                var comboBox = (ComboBox)IndexingFieldsStackPanel.FindName(comboBoxName);
                string selectedItem = comboBox.SelectedItem?.ToString();
                // Perform validations based on FieldOptionValidations
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
                                        // Show error hint if length is less than 14
                                        textBox.Tag = "Minimum length is " + validation.MaxLength.Value + " characters.";
                                        //textBox.ToolTip = "Minimum length is 14 characters";
                                        textBox.BorderBrush = Brushes.Red;
                                    }
                                    else
                                    {
                                        // Clear error hint if length is 14 or greater
                                        textBox.ToolTip = null;
                                        textBox.ClearValue(TextBox.BorderBrushProperty);
                                    }
                                };
                            }
                        }

                        if (validation.IsNumbric)
                        {
                            // Apply numeric validation
                            // You can use regular expressions or other methods to validate numeric input
                        }
                    }
                    else if (fieldToValidate != null)
                    {
                        // Remove any existing validation if fieldToValidate is not found or selectedItem doesn't match validation.CascadingFieldOptionsValue
                        //fieldToValidate.ClearValue(Validation.HasErrorProperty);
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

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }
        private void IntegerTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only numeric input
            if (!IsNumericInput(e.Text))
            {
                e.Handled = true; // Ignore the input
            }
        }

        private void DecimalTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only numeric input and a single decimal point
            if (!IsDecimalInput(e.Text, (TextBox)sender))
            {
                e.Handled = true; // Ignore the input
            }
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".")
            {
                e.Handled = true; // Prevent the input
            }
            else
            {
                // Ensure there's only one decimal point
                TextBox textBox = (TextBox)sender;
                string newText = textBox.Text.Insert(textBox.CaretIndex, e.Text);
                if (newText.Count(c => c == '.') > 1)
                {
                    e.Handled = true; // Prevent the input
                }
            }
        }

        private bool IsNumericInput(string text)
        {
            return int.TryParse(text, out _); // Check if the input can be parsed as an integer
        }

        private bool IsDecimalInput(string text, TextBox textBox)
        {
            // Check if the input can be parsed as a decimal and there is only one decimal point
            return decimal.TryParse(text, out _) && !textBox.Text.Contains('.');
        }
        #endregion
        #region style
        private void ApplyModernTextBoxStyle(TextBox textBox)
        {
            // Apply your custom modern TextBox style here
            // Modify the appearance, colors, animations, etc. as per your design preferences
            textBox.Style = (Style)Application.Current.Resources["ModernTextBoxStyle"];
        }

        private void ApplyModernDatePickerStyle(DatePicker datePicker)
        {
            // Apply your custom modern DatePicker style here
            // Modify the appearance, colors, animations, etc. as per your design preferences
            datePicker.Style = (Style)Application.Current.Resources["ModernDatePickerStyle"];
        }

        private void ApplyModernMemoTextBoxStyle(TextBox textBox)
        {
            // Apply your custom modern Memo TextBox style here
            // Modify the appearance, colors, animations, etc. as per your design preferences
            textBox.Style = (Style)Application.Current.Resources["ModernMemoTextBoxStyle"];
        }

        private void ApplyModernComboBoxStyle(ComboBox comboBox)
        {
            // Apply your custom modern ComboBox style here
            // Modify the appearance, colors, animations, etc. as per your design preferences
            comboBox.Style = (Style)Application.Current.Resources["ModernComboBoxStyle"];
        }

        private void ApplyModernCheckBoxStyle(CheckBox checkBox)
        {
            // Apply your custom modern CheckBox style here
            // Modify the appearance, colors, animations, etc. as per your design preferences
            checkBox.Style = (Style)Application.Current.Resources["ModernCheckBoxStyle"];
        }

        #endregion

        #region dummyData
     



        #endregion

        // Handle the selection change event or perform other actions here
        public void HandleSelectionChange()
        {
            // You can add your logic for handling selection change here
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class FieldViewModel : INotifyPropertyChanged
    {
        private string _label;
        private UIElement _fieldTypeInputControl;
        private bool? _isRequired;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Label
        {
            get { return _label; }
            set
            {
                if (_label != value)
                {
                    _label = value;
                    OnPropertyChanged();
                }
            }
        }

        public UIElement FieldTypeInputControl
        {
            get { return _fieldTypeInputControl; }
            set
            {
                if (_fieldTypeInputControl != value)
                {
                    _fieldTypeInputControl = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool? IsRequired
        {
            get { return _isRequired; }
            set
            {
                if (_isRequired != value)
                {
                    _isRequired = value;
                    OnPropertyChanged();
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public class Form
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Field> Fields { get; set; }
    }
    public class Field
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public bool Required { get; set; }
        public string InputValue { get; set; }
        public FieldType FieldType { get; set; }
        public int? CascadingFieldId { get; set; }
        public bool Unique { get; set; } = false;
        public List<FieldOptionLists>? FieldOptionLists { get; set; }
    }



    public enum FieldType
    {
        Text,
        Number,
        Decimal,
        Date,
        Memo,
        DropDownList,
        CheckBox
    }

    public class FieldOptionLists
    {
        public string Value { get; set; }
        public FieldOptionLists? CascadingFieldOptions { get; set; }
        public List<FieldOptionValidations>? FieldOptionValidations { get; set; }
    }
    public class FieldOptionValidations
    {
        public bool Required { get; set; } = false;
        public int? MaxLength { get; set; }
        public bool IsNumbric { get; set; } = false;
        public string? CascadingFieldOptionsValue { get; set; }
        public int? FieldId { get; set; }
    }
}
