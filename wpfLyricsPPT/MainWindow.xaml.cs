using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
		private List<Slide_Element> slide_elements = new List<Slide_Element>();
		private string img_path = "";

		public MainWindow()
		{
			InitializeComponent();

			//List fonts
			//cb_font.DataContext = Fonts.SystemFontFamilies;
		}

		// Placeholder / Hint text for textbox
		private void tb_section_num_text_changed(object sender, TextChangedEventArgs e)
		{
			//set_placeholder(tb_section_num,"hint")
		}
		/**
		 * Set the placeholder / hint of a textbox
		 */
		private void set_placeholder(TextBox tb, string hint)
		{
			if (tb.Text == "")
			{
				//https://learn.microsoft.com/en-us/dotnet/desktop/wpf/controls/how-to-add-a-watermark-to-a-textbox?view=netframeworkdesktop-4.8
				ImageBrush textImageBrush = new ImageBrush();
				textImageBrush.ImageSource =
					new BitmapImage(
						new Uri(@"TextBoxBackground.gif", UriKind.Relative)
					);
				textImageBrush.AlignmentX = AlignmentX.Left;
				textImageBrush.Stretch = Stretch.None;
				tb.Background = textImageBrush;
			}
			else
			{
				tb.Background = null;
			}
		}

		private void tb_section_num_GotFocus(object sender, RoutedEventArgs e)
		{
			lbl_help.Content = "Section Number:\n" +
				"Set the represented " + cb_section.Text + " number of the lyrics.\n" +
				"NOTE: For title, always set the number to 1 for less confusion.*";
		}

		private void tb_lyrics_GotFocus(object sender, RoutedEventArgs e)
		{
			lbl_help.Content = "Section Content:\n" +
				"Set the content, such as title or lyrics,\nthat will be displayed on the slide.\n" +
				"NOTE: Paste the ENTIRE paragraph (i.e. Verse, Chorus, Bridge) in here.\n" +
				"Indicate new slide for a lyric by NEW LINE (Enter)\n" +
				"The program will determine slide contents based on\n" +
				"the position of the line breaks.";
		}

		private void btn_addSection_onClick(object sender, RoutedEventArgs e)
		{
			// Input checking
			if(cb_section.Text==""||tb_section_num.Text==""||
				//img_preview.Source==null||
				img_path==""||
				cb_font.Text == "")
			{
				lbl_help.Content = "Some field is empty. Please enter the value and try again.";
				return;
			}
			slide_elements.Add(
				new Slide_Element(cb_section.Text + tb_section_num.Text,
				tb_lyrics.Text,
				img_preview.Source,
				img_path,
				new FontFamily(cb_font.Text),
				cb_font.Text,
				System.Drawing.Color.FromArgb(int.Parse(tb_color_a.Text), int.Parse(tb_color_r.Text), int.Parse(tb_color_g.Text), int.Parse(tb_color_b.Text)),
				int.Parse(tb_font_size.Text)
				));
			pnl_added.Children.Add(slide_elements[slide_elements.Count-1].panel);

			// Resets the contents
			img_preview.Source = null; // dlg.FileName;
			lbl_imgName.Content = "";
			tb_section_num.Text = "";
			tb_lyrics.Text = "";
			img_path = "";
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
				img_path = dlg.FileName;
			}
		}

		private void btn_gen_Click(object sender, RoutedEventArgs e)
		{
			Gen_Slide.generate_ppt(slide_elements);
		}

		//Limit text box to numbers only
		private void numbersOnlyTextbox(object sender, TextCompositionEventArgs e)
		{
			e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
		}

		private void tb_color_a_TextChanged(object sender, TextChangedEventArgs e)
		{
			try
			{
				if (int.Parse(tb_color_a.Text) > 255)
				{
					tb_color_a.Text = "255";
				}
				else if (int.Parse(tb_color_a.Text) < 0)
				{
					tb_color_a.Text = "0";
				}
			}
			catch (FormatException)
			{

			}
		}

		private void tb_color_r_TextChanged(object sender, TextChangedEventArgs e)
		{
			try {
				if (int.Parse(tb_color_r.Text) > 255)
				{
					tb_color_r.Text = "255";
				}
				else if (int.Parse(tb_color_r.Text) < 0)
				{
					tb_color_r.Text = "0";
				}
			}
			catch (FormatException)
			{

			}
		}

		private void tb_color_g_TextChanged(object sender, TextChangedEventArgs e)
		{
			try { 
				if (int.Parse(tb_color_g.Text) > 255)
				{
					tb_color_g.Text = "255";
				}
				else if (int.Parse(tb_color_g.Text) < 0)
				{
					tb_color_g.Text = "0";
				}
			}
			catch (FormatException)
			{

			}
		}

		private void tb_color_b_TextChanged(object sender, TextChangedEventArgs e)
		{
			try { 
				if (int.Parse(tb_color_b.Text) > 255)
				{
					tb_color_b.Text = "255";
				}
				else if (int.Parse(tb_color_b.Text) < 0)
				{
					tb_color_b.Text = "0";
				}
			}
			catch (FormatException)
			{

			}
		}
	}
}
