using System;
using System.Text;
using Elistia.DotNetRtfWriter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RtfWriter.UnitTests.Helpers;

namespace RtfWriter.UnitTests
{
    [TestClass]
    public class RtfParagraphTests
    {
        private const string RtlContent = "rtl";
        private const string LtrContent = "ltr";
        [TestMethod]
        public void CheckDefaultDirectionForParagraph()
        {
            var rtfParagraph = new RtfParagraph(true, true);
            Assert.AreEqual(rtfParagraph.ReadingDirection, ReadingDirection.LeftToRight);

        }

        [TestMethod]
        public void CheckRightDirectionForParagraph()
        {
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.ReadingDirection = ReadingDirection.RightToLeft;

            Assert.AreEqual(rtfParagraph.ReadingDirection, ReadingDirection.RightToLeft);

        }

        [TestMethod]
        public void AddCharFormatForParagraphWithoutBeginandEnd()
        {
            var text = "Test Paragraph";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.setText(text);
            var charFormat = rtfParagraph.addCharFormat();
            Assert.AreEqual(rtfParagraph.Text.Length, text.Length);
            Assert.IsNotNull(charFormat);
        }

        [TestMethod]
        public void TestDefaultCharFormatProperty()
        {
            var text = "Test Paragraph";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.setText(text);
            AssertHelper.DoesNotThrowException(() => { var defaultCharformat = rtfParagraph.DefaultCharFormat; });

        }


        [TestMethod]
        public void TestLineSpacingPropertySet()
        {
            var text = "Test Paragraph";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.LineSpacing = 2;
            rtfParagraph.setText(text);
            Assert.AreEqual(rtfParagraph.LineSpacing, 2);

        }
        [TestMethod]
        public void TestFirstLineIndentPropertySet()
        {
            var text = "Test Paragraph";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.FirstLineIndent = 2;
            rtfParagraph.setText(text);
            Assert.AreEqual(rtfParagraph.FirstLineIndent, 2);

        }
        [TestMethod]
        public void TestStartNewPagePropertySet()
        {
            var text = "Test Paragraph";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.StartNewPage = false;
            Assert.AreEqual(rtfParagraph.StartNewPage, false);

        }

        [TestMethod]
        public void TestMarginPropertyGet()
        {
            var text = "Test Paragraph";
            var rtfParagraph = new RtfParagraph(true, true);
            Assert.IsNotNull(rtfParagraph.Margins);

        }


        [TestMethod]
        public void AddCharFormatForParagraph()
        {
            var text = "Test Paragraph";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.LineSpacing = 2;
            rtfParagraph.FirstLineIndent = 2;
            rtfParagraph.StartNewPage = false;
            rtfParagraph.setText(text);
            var charFormat = rtfParagraph.addCharFormat(1, 6);
            Assert.AreEqual(rtfParagraph.Text.Length, text.Length);
            Assert.IsNotNull(charFormat);
        }
        [TestMethod]
        public void AddFootNoteForParagraph()
        {
            var text = "Test FootNote";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.setText(text);
            AssertHelper.DoesNotThrowException(() => rtfParagraph.addFootnote(1));


        }
        [TestMethod]
        public void AddControlWordForParagraph()
        {

            var rtfParagraph = new RtfParagraph(true, true);

            AssertHelper.DoesNotThrowException(() => rtfParagraph.addControlWord(1, RtfFieldControlWord.FieldType.Page));

        }

        [TestMethod]
        public void BuildTokenListTest()
        {
            var rtfParagraph = new RtfParagraph(true, true, ReadingDirection.LeftToRight);
            rtfParagraph.setText("Test Token Method");
            rtfParagraph.addCharFormat(-1, -1);
            AssertHelper.DoesNotThrowException(() => rtfParagraph.render());

        }

        [TestMethod]
        public void BuildTokenListTestForTotalParagraph()
        {
            var text = "Test Token Method";
            var rtfParagraph = new RtfParagraph(true, true, ReadingDirection.LeftToRight);
            rtfParagraph.setText(text);
            rtfParagraph.addCharFormat(0, text.Length - 1);
            AssertHelper.DoesNotThrowException(() => rtfParagraph.render());

        }

        [TestMethod]
        public void GenerateExceptionWhenEnd_GreaterThanOrEqualTo_TextLength()
        {
            var text = "Test Token Method";
            int end = text.Length;
            var rtfParagraph = new RtfParagraph(true, true, ReadingDirection.LeftToRight);
            rtfParagraph.setText(text);
            AssertHelper.Throws<Exception>(() => rtfParagraph.addCharFormat(0, text.Length));
        }

        [TestMethod]
        public void LTR_ParagraphRender()
        {
            var textPara = "Test Paragraph";
            var sb = new StringBuilder();

            sb.AppendLine(string.Format(@"{{\pard\fi0\{0}par\ql", LtrContent))
              .AppendLine(textPara)
              .AppendLine(@"\par}");

            var expectString = sb.ToString();

            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.setText(textPara);
            var result = rtfParagraph.render();
            Assert.AreEqual(result, expectString);

        }


        [TestMethod]

        public void RTL_ParagraphRender()
        {
            var textPara = "Test Paragraph";
            var sb = new StringBuilder();

            sb.AppendLine(string.Format(@"{{\pard\fi0\{0}par\ql", RtlContent))
              .AppendLine(textPara)
              .AppendLine(@"\par}");

            var expectString = sb.ToString();

            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.ReadingDirection = ReadingDirection.RightToLeft;
            rtfParagraph.setText(textPara);
            var result = rtfParagraph.render();
            Assert.AreEqual(result, expectString);

        }

    }


}
