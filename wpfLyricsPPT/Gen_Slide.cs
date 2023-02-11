using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using pptApplication = Microsoft.Office.Interop.PowerPoint.Application;

namespace wpfLyricsPPT
{
	public class Gen_Slide
	{

		private static pptApplication pptapp;
		private static Presentation pptfile;
		public static void generate_ppt(List<Slide_Element> se_list)
		{
			//Open PowerPoint
			pptapp = new pptApplication();
			pptfile = pptapp.Presentations.Add(MsoTriState.msoTrue);

			//pptfile.

			int current_slide_idx = 1;
			Color font_color = Color.White;
			//foreach loop
			foreach (Slide_Element se in se_list)
			{
				//1. Split existing texts by \n
				string[] slide_content_text = se.content.Split("\n".ToCharArray());
				foreach(string slide_text in slide_content_text)
				{
					//2. foreach text segments, add to slide with color and bg
					//2a. Replace punctuation
					string final_slide_text = replaceUnnessaryChar(slide_text);
					//2a. split line in slide if needed
					if (slide_text.Length >= 13)
					{
						final_slide_text = slide_text.Replace(" ", "\n");
					}

					font_color = (se.glow_color.GetBrightness() > 0.5) ? Color.Black : Color.White;
					addToSlide(
						pptfile.Slides,
						current_slide_idx,
						pptfile.SlideMaster.CustomLayouts[PpSlideLayout.ppLayoutTitle],
						final_slide_text, se.font.Source, se.font_size, font_color,
						se.glow_color, se.glow_opa, se.glow_rad,
						se.img_path,
						0,0 //TODO calculate the center point of the slide
						);
					current_slide_idx++;
				}
			}

			
		}

		private static void addToSlide(
			Slides slides,
			int slide_idx,
			CustomLayout customLayout,
			string text, string typeface, float size, Color fontColor,
			Color glowColor, float glowOpa, float glowRad,
			string bgimg_path,
			int pos_x, int pos_y
			)
		{
			_Slide newSlide = slides.AddSlide(slide_idx, customLayout);
			//Set background
			newSlide.FollowMasterBackground = MsoTriState.msoFalse;
			newSlide.Background.Fill.UserPicture(bgimg_path);
			//Add text
			//Using TextRange2 to support Glow effect
			TextRange2 titleBox = newSlide.Shapes[1].TextFrame2.TextRange;

			//newSlide.Shapes[1].TextFrame2.Lay

			titleBox.Font.Size = size;
			titleBox.Font.Glow.Color.RGB = glowColor.ToArgb();
			titleBox.Font.Glow.Radius = glowRad;
			titleBox.Font.Glow.Transparency = glowOpa;
			Console.WriteLine(glowRad);
			//newSlide.Shapes[1].TextFrame2.TextRange.Font.Color.RGB = fontColor.ToArgb();

			//Use TextRange to support normal font color
			newSlide.Shapes[1].TextFrame.TextRange.Font.Color.RGB = fontColor.ToArgb();
			newSlide.Shapes[1].TextFrame.TextRange.ParagraphFormat.Alignment = PpParagraphAlignment.ppAlignCenter;
			newSlide.Shapes[1].TextFrame.TextRange.ParagraphFormat.BaseLineAlignment = PpBaselineAlignment.ppBaselineAlignCenter;
			//newSlide.Shapes[1].TextFrame.TextRange.Font.Name = typeface;

			//Sets the shape's position
			newSlide.Shapes[1].TextFrame.HorizontalAnchor = MsoHorizontalAnchor.msoAnchorCenter;
			newSlide.Shapes[1].TextFrame.VerticalAnchor = MsoVerticalAnchor.msoAnchorMiddle;// MsoAnchorCenter;
			newSlide.Shapes[1].TextFrame.HorizontalAnchor = MsoHorizontalAnchor.msoAnchorCenter;
			newSlide.Shapes[1].TextFrame.VerticalAnchor = MsoVerticalAnchor.msoAnchorMiddle;// MsoAnchorCenter;
			newSlide.Shapes[1].Width = pptfile.PageSetup.SlideWidth;
			//newSlide.Shapes[1].Left = pos_x;
			newSlide.Shapes[1].Top = pptfile.PageSetup.SlideHeight / 2 - newSlide.Shapes[1].Height / 2;/**/
			/*
			newSlide.Shapes[1].Top = pptfile.PageSetup.SlideHeight - newSlide.Shapes[1].Height / 2;
			*/
			newSlide.Shapes[1].Left = 0;

			titleBox.Font.Name = typeface;
			//titleBox.Font.NameOther = typeface;
			titleBox.Font.NameFarEast = typeface;
			//titleBox.Font.NameComplexScript = typeface;
			titleBox.Characters.Font.Name = typeface;
			//titleBox.Characters.Font.NameOther = typeface;
			titleBox.Characters.Font.NameFarEast = typeface;
			//titleBox.Characters.Font.NameOther = typeface;
			//titleBox.Characters.Font.NameComplexScript = typeface;
			titleBox.Text = text;
			
			
			/**/

		}

		private static string replaceUnnessaryChar(string text)
		{
			return text
				.Replace("，", " ")
				.Replace("。", " ")
				.Replace("：", " ")
				.Replace(".", " ")
				.Replace(",", " ")
				.Replace(":", " ")
				.Replace(";", " ")
				.Replace("\n", "");
		}
	}
}
