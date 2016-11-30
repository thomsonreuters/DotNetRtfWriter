using Elistia.DotNetRtfWriter;
using NUnit.Framework;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace RtfWriter.UnitTests
{
	[TestFixture]
	public class RtfDocumentTests
	{
		private const string RtlContent = "rtl";
		private const string LtrContent = "ltr";

		[Test]
		public void CheckReadingDirectionForRtfDocument()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("en-US"));
			Assert.AreEqual(ReadingDirection.LeftToRight, rtfDocument.ReadingDirection);
		}
		[Test]
		public void OverrideReadingDirectionForRtfDocument()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("en-US"));
			rtfDocument.ReadingDirection = ReadingDirection.RightToLeft;
			Assert.AreEqual(ReadingDirection.RightToLeft, rtfDocument.ReadingDirection);
		}
		[Test]
		public void CreateFontWhichIsNotExistsInFontTableRtfDocumentTest()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("en-US"));
			Assert.DoesNotThrow(() => rtfDocument.createFont("Arial"));
		}

		[Test]
		public void CreateFontWhichIsExistsInFontTableRtfDocumentTest()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("en-US"));
			Assert.DoesNotThrow(() => rtfDocument.createFont("Times New Roman"));
		}

		[Test]
		public void CreateColorWhichIsExistsInColorTableRtfDocumentTest()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("en-US"));
			var color = new RtfColor("000000");
			Assert.DoesNotThrow(() => rtfDocument.createColor(color));
		}


		[Test]
		public void AddTableInRtfDocumentTest()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("en-US"));
			var rtfTable = rtfDocument.addTable(1, 1, 1, 1);
			Assert.AreEqual(1, rtfTable.RowCount);
			Assert.AreEqual(1, rtfTable.ColCount);
		}
		[Test]
		public void AddSectionInRtfDocumentTest_LTR()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("en-US"));
			var rtfSection = rtfDocument.addSection(SectionStartEnd.Start, rtfDocument);
			var sb = new StringBuilder();
			sb.AppendLine(
				string.Format(@"{{\sectd\{0}sect\footery{1}\{2}\sftnbj\qd ", LtrContent, "720", "sectdefaultcl"))
				.AppendLine(@"\pgwsxn0\pghsxn0 \marglsxn0\margrsxn0\margtsxn0\margbsxn0 {\footerr \ltrpar \pard\plain")
				.AppendLine(@"\par ")
				.AppendLine()
				.AppendLine(@"\par")
				.AppendLine("}")
				.AppendLine();


			var actualText = rtfSection.render();

			Assert.AreEqual(SectionStartEnd.Start, rtfSection.StartEnd);
			Assert.AreEqual(actualText.Trim(), sb.ToString().Trim());
		}

		[Test]
		public void AddSectionInRtfDocumentTest_RTL()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("ar-AE"));
			var rtfSection = rtfDocument.addSection(SectionStartEnd.Start, rtfDocument);
			var sb = new StringBuilder();
			sb.AppendLine(
				string.Format(@"{{\sectd\{0}sect\footery{1}\{2}\sftnbj\qd ", RtlContent, "720", "sectdefaultcl"))
				.AppendLine(@"\pgwsxn0\pghsxn0 \marglsxn0\margrsxn0\margtsxn0\margbsxn0 {\footerr \rtlpar \pard\plain")
				.AppendLine(@"\par ")
				.AppendLine()
				.AppendLine(@"\par")
				.AppendLine("}")
				.AppendLine();


			var actualText = rtfSection.render();

			Assert.AreEqual(SectionStartEnd.Start, rtfSection.StartEnd);
			Assert.AreEqual(actualText.Trim(), sb.ToString().Trim());
		}

		[Test]
		public void AddImageInRtfDocumentTest()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape,
				new CultureInfo("en-US"));
			using (
				var stream =
					Assembly.GetExecutingAssembly().GetManifestResourceStream("RtfWriter.UnitTests.Images.demo5.jpg"))
			{
				using (var memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					var rtfImage = rtfDocument.addImage(memoryStream);
					Assert.IsNotNull(rtfImage.Width);
				}
			}

		}

		[Test]
		public void RendererInRtfImageTest()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("en-US"));

			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RtfWriter.UnitTests.Images.demo5.jpg"))
			{
				using (var memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					var rtfImage = rtfDocument.addImage(memoryStream);
					var actualText = rtfImage.render();
					Assert.IsTrue(actualText.Length > 0);
				}
			}
		}

		[Test]
		public void RendererInRtfDocument()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("en-US"));
			var actualString = rtfDocument.render();

			Assert.IsTrue(actualString.Length > 0);
		}

		[Test]
		public void AddTableInRtfDocument()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("en-US"));
			var rtfTable = rtfDocument.addTable(1, 1, 1, 10);

			Assert.AreEqual(1, rtfTable.RowCount);
			Assert.AreEqual(1, rtfTable.ColCount);
		}

		[Test]
		public void PassLcidInnRtfDocument()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
			Assert.AreEqual(ReadingDirection.LeftToRight, rtfDocument.ReadingDirection);
		}

		[Test]
		public void SetDefaultFontInnRtfDocument()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
			Assert.DoesNotThrow(() => rtfDocument.setDefaultFont("Times New Roman"));
		}

		[Test]
		public void CreateInnRtfDocument()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
			Assert.DoesNotThrow(() => rtfDocument.createColor(new RtfColor("000000")));
		}

		[Test]
		public void SaveInnRtfDocument()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
			Assert.DoesNotThrow(() => rtfDocument.save("Test.rtf"));
		}

		[Test]
		public void ReadingDirectionShouldSetBasedOnCulture()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("ar-AE"));
			int rows = 1;
			int columns = 2;

			//Add Paragraph
			var rtfParagraph = rtfDocument.addParagraph();
			rtfParagraph.setText("Testing\n");

			//Add Table
			var rtfTable = rtfDocument.addTable(rows, columns, 10);

			//Add a cell in Table
			for (var i = 0; i < rtfTable.RowCount; i++)
			{
				for (var j = 0; j < rtfTable.ColCount; j++)
				{

					rtfTable.cell(i, j).addParagraph().setText("CELL " + i + "," + j);
				}
			}

			//Add Section
			var rtfSection = rtfDocument.addSection(SectionStartEnd.Start, rtfDocument);

			//Add Image
			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RtfWriter.UnitTests.Images.demo5.jpg"))
			{
				using (var memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					var rtfImage = rtfDocument.addImage(memoryStream);
					Assert.AreEqual(ReadingDirection.RightToLeft, rtfImage.ReadingDirection);
				}
			}
			//Get Particular cell in Table to test Direction
			var rtfTableCell = rtfTable.cell(0, 1);

			Assert.AreEqual(ReadingDirection.RightToLeft, rtfParagraph.ReadingDirection);
			Assert.AreEqual(ReadingDirection.RightToLeft, rtfTable.ReadingDirection);
			Assert.AreEqual(ReadingDirection.RightToLeft, rtfTableCell.ReadingDirection);
			Assert.AreEqual(ReadingDirection.RightToLeft, rtfSection.ReadingDirection);
		}

		[Test]
		public void ReadingDirectionShouldBeSetBasedOnCulture()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
			int rows = 1;
			int columns = 2;

			//Add Paragraph
			var rtfParagraph = rtfDocument.addParagraph();
			rtfParagraph.setText("Testing\n");

			//Add Table
			var rtfTable = rtfDocument.addTable(rows, columns, 10);

			//Add a cell in Table
			for (var i = 0; i < rtfTable.RowCount; i++)
			{
				for (var j = 0; j < rtfTable.ColCount; j++)
				{

					rtfTable.cell(i, j).addParagraph().setText("CELL " + i + "," + j);
				}
			}

			//Add Section
			var rtfSection = rtfDocument.addSection(SectionStartEnd.Start, rtfDocument);

			//Add Image

			using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("RtfWriter.UnitTests.Images.demo5.jpg"))
			{
				using (var memoryStream = new MemoryStream())
				{
					stream.CopyTo(memoryStream);
					var rtfImage = rtfDocument.addImage(memoryStream);
					Assert.AreEqual(ReadingDirection.LeftToRight, rtfImage.ReadingDirection);
				}
			}

			//Get Particular cell in Table to test Direction
			var rtfTableCell = rtfTable.cell(0, 1);

			Assert.AreEqual(ReadingDirection.LeftToRight, rtfParagraph.ReadingDirection);
			Assert.AreEqual(ReadingDirection.LeftToRight, rtfTable.ReadingDirection);
			Assert.AreEqual(ReadingDirection.LeftToRight, rtfTableCell.ReadingDirection);

			Assert.AreEqual(ReadingDirection.LeftToRight, rtfSection.ReadingDirection);
		}

		[Test]
		public void RtfHeaderTest()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
			Assert.DoesNotThrow(() => rtfDocument.Header.addParagraph());
		}

		[Test]
		public void RtfHeaderRendererTest()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
			Assert.IsTrue(rtfDocument.Header.render().Length > 0);
		}

		[Test]
		public void RtfFooterTest()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
			Assert.DoesNotThrow(() => rtfDocument.Footer.addParagraph());
		}

		[Test]
		public void RtfSectionFooterTestWithRightToLeftCulture()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("ar-AE"));
			var rtfSection = rtfDocument.addSection(SectionStartEnd.Start, rtfDocument);
			Assert.AreEqual(ReadingDirection.RightToLeft, rtfSection.SectionFooter.ReadingDirection);
		}

		[Test]
		public void RtfSectionFooterTestWithLeftToRightCulture()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
			var rtfSection = rtfDocument.addSection(SectionStartEnd.Start, rtfDocument);
			Assert.AreEqual(ReadingDirection.LeftToRight, rtfSection.SectionFooter.ReadingDirection);
		}

		[Test]
		public void RtfFooterRendererTest()
		{
			var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
			Assert.DoesNotThrow(() => rtfDocument.Footer.render());
		}
	}
}
