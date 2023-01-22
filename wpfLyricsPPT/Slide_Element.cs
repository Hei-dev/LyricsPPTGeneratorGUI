using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace wpfLyricsPPT
{
	public class Slide_Element
	{
		public string sec
		{
			get;
			set;
		}
		public string content
		{
			get;
			set;
		}
		public FontFamily font{ get; set; }
		public string font_name { get; set; }
		public System.Drawing.Color glow_color { get; set; }
		public float glow_rad { get; set; }
		public float glow_opa { get; set; }
		public int font_size { get; set; }

		public ImageSource img_src
		{
			get;
			set;
		}
		public string img_path { get; set; }

		public StackPanel panel
		{
			get;
			set;
		}

		public Slide_Element(
			string section,
			string content,
			ImageSource img, string img_path,
			FontFamily font, string font_name,
			System.Drawing.Color glow_color, float glow_rad, float glow_opa,
			int font_size)
		{
			this.sec = section; //Section name, e.g. Verse 1
			this.content = content; //Content of the slide, e.g. the lyrics
			this.img_src = img; //The background image
			this.img_path = img_path; //The background image source path
			this.font = font; //the font typeface
			this.glow_color = glow_color; //The color that makes the text glow
			this.glow_opa = glow_opa/100;
			this.glow_rad = glow_rad; //The glow edge size
			this.font_size = font_size; //The font size

			panel = new_slide_element();
		}

		public StackPanel new_slide_element()
		{
			StackPanel slide_element = new StackPanel();

			TextBox title_element = new TextBox();
			title_element.Text = sec;
			title_element.FontSize = 18;
			slide_element.Children.Add(title_element);

			Image slide_bg_img_element = new Image();
			slide_bg_img_element.Source = img_src;
			slide_element.Children.Add(slide_bg_img_element);

			TextBox lyrics_element = new TextBox{Text = content, FontFamily = font};
			slide_element.Children.Add(lyrics_element);

			return slide_element;
		}
	}
}
