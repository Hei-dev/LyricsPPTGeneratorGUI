using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



//https://learn.microsoft.com/en-us/dotnet/desktop/wpf/introduction-to-wpf?view=netframeworkdesktop-4.8&preserve-view=true

namespace wpfLyricsPPT
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{


		public MainWindow()
		{
			InitializeComponent();
		}

		private void btn_addSection_onClick(object sender, RoutedEventArgs e)
		{

		}

		private void btn_selImg_onClick(object sender, RoutedEventArgs e)
		{
			//From https://learn.microsoft.com/en-us/dotnet/api/microsoft.win32.openfiledialog?view=windowsdesktop-6.0
			// Configure open file dialog box
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.Filter = "All Supported Image files|*.jpg;*.jpeg;*.png" +
			 "|JPEG Images|*.jpg;*.jpeg" +
			 "|PNG Images|*.png" +
			 "|All Files|*.*";

			// Show open file dialog box
			Nullable<bool> result = dlg.ShowDialog();

			// Process open file dialog box results
			if (result == true)
			{
				img_preview.Source = new ImageSourceConverter().ConvertFromString(dlg.FileName) as ImageSource;// dlg.FileName;
				lbl_imgName.Content = "Background Image Preview: " + dlg.FileName;
			}
		}
	}
}
