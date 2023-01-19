﻿using System;
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
		public static void generate_ppt(List<Slide_Element> se_list)
		{
			//Open PowerPoint
			pptApplication pptapp = new pptApplication();

			Presentation pptfile = pptapp.Presentations.Add(MsoTriState.msoTrue);

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
					//2a. split line in slide if needed
					if (slide_text.Length >= 13)
					{
						slide_text.Replace(" ", "\n");
					}

					font_color = (se.glow_color.GetBrightness() > 0.5) ? Color.Black : Color.White;
					
					addToSlide(
						pptfile.Slides,
						current_slide_idx,
						pptfile.SlideMaster.CustomLayouts[PpSlideLayout.ppLayoutTitle],
						slide_text, se.font.Source, se.font_size, font_color, se.glow_color,
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
			string text, string typeface, float size, Color fontColor, Color glowColor,
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

			titleBox.Font.Name = typeface;
			titleBox.Font.NameFarEast = typeface;
			titleBox.Characters.Font.NameFarEast = typeface;
			titleBox.Text = text;
			
			titleBox.Font.Size = size;
			titleBox.Font.Glow.Color.RGB = glowColor.ToArgb();
			titleBox.Font.Glow.Radius = 50;
			//newSlide.Shapes[1].TextFrame2.TextRange.Font.Color.RGB = fontColor.ToArgb();

			//Use TextRange to support normal font color
			newSlide.Shapes[1].TextFrame.TextRange.Font.Color.RGB = fontColor.ToArgb();
			//newSlide.Shapes[1].TextFrame.TextRange.Font.Name = typeface;

			//Sets the shape's position
			/*
			newSlide.Shapes[1].TextFrame.HorizontalAnchor = 0;
			newSlide.Shapes[1].TextFrame.VerticalAnchor = 0;
			newSlide.Shapes[1].Left = pos_x;
			newSlide.Shapes[1].Top = pos_y;*/


		}
	}
}
