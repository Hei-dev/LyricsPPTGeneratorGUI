using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using pptApplication = Microsoft.Office.Interop.PowerPoint.Application;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

public class lyPptx
{
	public lyPptx()
	{

	}

	public static void addToSlide(Slides slides, int idx, CustomLayout customLayout, String text, String typeface, float size, Color fontColor, Color glowColor)
	{
		_Slide newSlide = slides.AddSlide(idx, customLayout);
		//Add text
		//Using TextRange2 to support Glow effect
		TextRange2 titleBox = newSlide.Shapes[1].TextFrame2.TextRange;
		titleBox.Text = text;
		titleBox.Font.Name = typeface;
		titleBox.Font.Size = size;
		titleBox.Font.Glow.Color.RGB = glowColor.ToArgb();
		//newSlide.Shapes[1].TextFrame2.TextRange.Font.Color.RGB = fontColor.ToArgb();

		//Use TextRange to support noral font color
		newSlide.Shapes[1].TextFrame.TextRange.Font.Color.RGB = fontColor.ToArgb();

	}

	//static void Main(string[] args)
	public static void newPresentation()
	{
		pptApplication pptapp = new pptApplication();

		Presentation pptfile = pptapp.Presentations.Add(MsoTriState.msoTrue);

		OpenFileDialog dlg = new OpenFileDialog();
		dlg.ShowDialog();

		if (dlg.ShowDialog() == DialogResult.OK)
		{
			string fileName;
			fileName = dlg.FileName;
			MessageBox.Show(fileName);
		}

		Slides slideObj = pptfile.Slides;

		addToSlide(slideObj, 0, pptfile.SlideMaster.CustomLayouts[PpSlideLayout.ppLayoutTitleOnly],"Hello World","Arial",20,Color.FromArgb(128,128,128),Color.Black);

		pptfile.SaveAs("test.pptx");
	}

	
}

