
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using Elistia.DotNetRtfWriter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RtfWriter.UnitTests.Helpers;

namespace RtfWriter.UnitTests
{
    [TestClass]
    public class RtfDocumentTests
    {
        private const string RtlContent = "rtl";
        private const string LtrContent = "ltr";
        [TestMethod]
        public void CheckReadingDirectionForRtfDocument()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape,
                new CultureInfo("en-US"));
            Assert.AreEqual(rtfDocument.ReadingDirection, ReadingDirection.LeftToRight);
        }
        [TestMethod]
        public void OverrideReadingDirectionForRtfDocument()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape,
                new CultureInfo("en-US"));
            rtfDocument.ReadingDirection = ReadingDirection.RightToLeft;
            Assert.AreEqual(rtfDocument.ReadingDirection, ReadingDirection.RightToLeft);
        }
        [TestMethod]
        public void CreateFontWhichIsNotExistsInFontTableRtfDocumentTest()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape,
                new CultureInfo("en-US"));
            AssertHelper.DoesNotThrowException(() => rtfDocument.createFont("Arial"));

        }

        [TestMethod]
        public void CreateFontWhichIsExistsInFontTableRtfDocumentTest()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape,
                new CultureInfo("en-US"));
            AssertHelper.DoesNotThrowException(() => rtfDocument.createFont("Times New Roman"));
        }

        [TestMethod]
        public void CreateColorWhichIsExistsInColorTableRtfDocumentTest()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape,
                new CultureInfo("en-US"));
            var color = new RtfColor("000000");
            AssertHelper.DoesNotThrowException(() => rtfDocument.createColor(color));
        }


        [TestMethod]
        public void AddTableInRtfDocumentTest()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape,
                new CultureInfo("en-US"));
            var rtfTable = rtfDocument.addTable(1, 1, 1, 1);
            Assert.AreEqual(rtfTable.RowCount, 1);
            Assert.AreEqual(rtfTable.ColCount, 1);
        }
        [TestMethod]
        public void AddSectionInRtfDocumentTest_LTR()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape,
                new CultureInfo("en-US"));
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

            Assert.AreEqual(rtfSection.StartEnd, SectionStartEnd.Start);
            Assert.AreEqual(sb.ToString().Trim(), actualText.Trim());
        }

        [TestMethod]
        public void AddSectionInRtfDocumentTest_RTL()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape,
                new CultureInfo("ar-AE"));
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

            Assert.AreEqual(rtfSection.StartEnd, SectionStartEnd.Start);
            Assert.AreEqual(sb.ToString().Trim(), actualText.Trim());
        }

        [TestMethod]
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

        [TestMethod]
        public void RendererInRtfImageTest()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape,
                new CultureInfo("en-US"));

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




        [TestMethod]
        public void RendererInRtfDocument()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape,
                new CultureInfo("en-US"));
            var actualString = rtfDocument.render();

            Assert.IsTrue(actualString.Length > 0);
        }

        [TestMethod]
        public void AddTableInRtfDocument()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape,
                new CultureInfo("en-US"));
            var rtfTable = rtfDocument.addTable(1, 1, 1, 10);
            Assert.AreEqual(rtfTable.RowCount, 1);
            Assert.AreEqual(rtfTable.ColCount, 1);

        }

        [TestMethod]
        public void PassLcidInnRtfDocument()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
            Assert.AreEqual(rtfDocument.ReadingDirection, ReadingDirection.LeftToRight);

        }

        [TestMethod]
        public void SetDefaultFontInnRtfDocument()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
            AssertHelper.DoesNotThrowException(() => rtfDocument.setDefaultFont("Times New Roman"));
        }

        [TestMethod]
        public void CreateInnRtfDocument()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
            AssertHelper.DoesNotThrowException(() => rtfDocument.createColor(new RtfColor("000000")));

        }

        [TestMethod]
        public void SaveInnRtfDocument()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
            AssertHelper.DoesNotThrowException(() => rtfDocument.save("Test.rtf"));

        }

        [TestMethod]
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

                    rtfTable.cell(i, j).addParagraph().setText("CELL " + i.ToString() + "," + j.ToString());
                }
            }

            //Add Section
            var rtfSection = rtfDocument.addSection(SectionStartEnd.Start, rtfDocument);

            //Add Image
            using (
                var stream =
                    Assembly.GetExecutingAssembly().GetManifestResourceStream("RtfWriter.UnitTests.Images.demo5.jpg"))
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    var rtfImage = rtfDocument.addImage(memoryStream);
                    Assert.AreEqual(rtfImage.ReadingDirection, ReadingDirection.RightToLeft);
                }
            }
            //Get Particular cell in Table to test Direction
            var rtfTableCell = rtfTable.cell(0, 1);

            Assert.AreEqual(rtfParagraph.ReadingDirection, ReadingDirection.RightToLeft);
            Assert.AreEqual(rtfTable.ReadingDirection, ReadingDirection.RightToLeft);
            Assert.AreEqual(rtfTableCell.ReadingDirection, ReadingDirection.RightToLeft);
            Assert.AreEqual(rtfSection.ReadingDirection, ReadingDirection.RightToLeft);


        }

        [TestMethod]
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

                    rtfTable.cell(i, j).addParagraph().setText("CELL " + i.ToString() + "," + j.ToString());
                }
            }

            //Add Section
            var rtfSection = rtfDocument.addSection(SectionStartEnd.Start, rtfDocument);

            //Add Image

            using (
      var stream =
          Assembly.GetExecutingAssembly().GetManifestResourceStream("RtfWriter.UnitTests.Images.demo5.jpg"))
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    var rtfImage = rtfDocument.addImage(memoryStream);
                    Assert.AreEqual(rtfImage.ReadingDirection, ReadingDirection.LeftToRight);
                }
            }

            //Get Particular cell in Table to test Direction
            var rtfTableCell = rtfTable.cell(0, 1);

            Assert.AreEqual(rtfParagraph.ReadingDirection, ReadingDirection.LeftToRight);
            Assert.AreEqual(rtfTable.ReadingDirection, ReadingDirection.LeftToRight);
            Assert.AreEqual(rtfTableCell.ReadingDirection, ReadingDirection.LeftToRight);

            Assert.AreEqual(rtfSection.ReadingDirection, ReadingDirection.LeftToRight);
        }
        [TestMethod]
        public void RtfHeaderTest()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
            AssertHelper.DoesNotThrowException(() => rtfDocument.Header.addParagraph());

        }


        [TestMethod]
        public void RtfHeaderRendererTest()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
            Assert.IsTrue(rtfDocument.Header.render().Length > 0);

        }

        [TestMethod]
        public void RtfFooterTest()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
            AssertHelper.DoesNotThrowException(() => rtfDocument.Footer.addParagraph());

        }


        [TestMethod]
        public void RtfSectionFooterTestWithRightToLeftCulture()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, new CultureInfo("ar-AE"));
            var rtfSection = rtfDocument.addSection(SectionStartEnd.Start, rtfDocument);
            Assert.AreEqual(ReadingDirection.RightToLeft, rtfSection.SectionFooter.ReadingDirection);
        }
        [TestMethod]
        public void RtfSectionFooterTestWithLeftToRightCulture()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
            var rtfSection = rtfDocument.addSection(SectionStartEnd.Start, rtfDocument);
            Assert.AreEqual(ReadingDirection.LeftToRight, rtfSection.SectionFooter.ReadingDirection);
        }

        [TestMethod]
        public void RtfFooterRendererTest()
        {
            var rtfDocument = new RtfDocument(PaperSize.A4, PaperOrientation.Landscape, Lcid.English);
            AssertHelper.DoesNotThrowException(() => rtfDocument.Footer.render());

        }


    }
}
