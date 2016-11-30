using Elistia.DotNetRtfWriter;
using NUnit.Framework;
using System;
using System.Text;

namespace RtfWriter.UnitTests
{
    [TestFixture]
    public class RtfParagraphTests
    {
        private const string RtlContent = "rtl";
        private const string LtrContent = "ltr";

        [Test]
        public void CheckDefaultDirectionForParagraph()
        {
            var rtfParagraph = new RtfParagraph(true, true);
            Assert.AreEqual(ReadingDirection.LeftToRight, rtfParagraph.ReadingDirection);
        }

        [Test]
        public void CheckRightDirectionForParagraph()
        {
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.ReadingDirection = ReadingDirection.RightToLeft;

            Assert.AreEqual(ReadingDirection.RightToLeft, rtfParagraph.ReadingDirection);
        }

        [Test]
        public void AddCharFormatForParagraphWithoutBeginandEnd()
        {
            var text = "Test Paragraph";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.setText(text);
			var charFormat = rtfParagraph.addCharFormat();
			Assert.AreEqual(text.Length, rtfParagraph.Text.Length);
            Assert.IsNotNull(charFormat);
        }

        [Test]
        public void TestDefaultCharFormatProperty()
        {
            var text = "Test Paragraph";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.setText(text);
            Assert.DoesNotThrow(() => { var defaultCharformat = rtfParagraph.DefaultCharFormat; });
        }


        [Test]
        public void TestLineSpacingPropertySet()
        {
            var text = "Test Paragraph";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.LineSpacing = 2;
            rtfParagraph.setText(text);
            Assert.AreEqual(2, rtfParagraph.LineSpacing);
        }

        [Test]
        public void TestFirstLineIndentPropertySet()
        {
            var text = "Test Paragraph";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.FirstLineIndent = 2;
            rtfParagraph.setText(text);
            Assert.AreEqual(2, rtfParagraph.FirstLineIndent);
        }

        [Test]
        public void TestStartNewPagePropertySet()
        {
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.StartNewPage = false;
            Assert.AreEqual(false, rtfParagraph.StartNewPage);
        }

        [Test]
        public void TestMarginPropertyGet()
        {
            var rtfParagraph = new RtfParagraph(true, true);
            Assert.IsNotNull(rtfParagraph.Margins);
        }

        [Test]
        public void AddCharFormatForParagraph()
        {
            var text = "Test Paragraph";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.LineSpacing = 2;
            rtfParagraph.FirstLineIndent = 2;
            rtfParagraph.StartNewPage = false;
            rtfParagraph.setText(text);
            var charFormat = rtfParagraph.addCharFormat(1, 6);
            Assert.AreEqual(text.Length, rtfParagraph.Text.Length);
            Assert.IsNotNull(charFormat);
        }

        [Test]
        public void AddFootNoteForParagraph()
        {
            var text = "Test FootNote";
            var rtfParagraph = new RtfParagraph(true, true);
            rtfParagraph.setText(text);
            Assert.DoesNotThrow(() => rtfParagraph.addFootnote(1));
        }

        [Test]
        public void AddControlWordForParagraph()
        {
            var rtfParagraph = new RtfParagraph(true, true);

            Assert.DoesNotThrow(() => rtfParagraph.addControlWord(1, RtfFieldControlWord.FieldType.Page));
        }

        [Test]
        public void BuildTokenListTest()
        {
            var rtfParagraph = new RtfParagraph(true, true, ReadingDirection.LeftToRight);
            rtfParagraph.setText("Test Token Method");
            rtfParagraph.addCharFormat(-1, -1);
            Assert.DoesNotThrow(() => rtfParagraph.render());
        }

        [Test]
        public void BuildTokenListTestForTotalParagraph()
        {
            var text = "Test Token Method";
            var rtfParagraph = new RtfParagraph(true, true, ReadingDirection.LeftToRight);
            rtfParagraph.setText(text);
            rtfParagraph.addCharFormat(0, text.Length - 1);
            Assert.DoesNotThrow(() => rtfParagraph.render());
        }

        [Test]
        public void GenerateExceptionWhenEnd_GreaterThanOrEqualTo_TextLength()
        {
            var text = "Test Token Method";
            int end = text.Length;
            var rtfParagraph = new RtfParagraph(true, true, ReadingDirection.LeftToRight);
            rtfParagraph.setText(text);
            Assert.Throws<Exception>(() => rtfParagraph.addCharFormat(0, text.Length));
        }

        [Test]
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
			Assert.AreEqual(expectString, result);
        }


        [Test]
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
			Assert.AreEqual(expectString, result);
        }
    }
}
