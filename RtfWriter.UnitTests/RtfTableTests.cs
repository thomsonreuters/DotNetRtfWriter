using Elistia.DotNetRtfWriter;
using NUnit.Framework;
using System;
using System.Text;

namespace RtfWriter.UnitTests
{
    [TestFixture]
    public class RtfTableTests
    {
        private const string RtlContent = "rtl";
        private const string LtrContent = "ltr";
        [Test]
        public void CheckDefaultDirectionForTable()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.AreEqual(ReadingDirection.LeftToRight, rtfTable.ReadingDirection);
        }

        [Test]
        public void CheckRightDirectionForTable()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            rtfTable.ReadingDirection = ReadingDirection.RightToLeft;
            Assert.AreEqual(ReadingDirection.RightToLeft, rtfTable.ReadingDirection);
        }

        [Test]
        public void AllignmentPropertyTestForTable()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            rtfTable.Alignment = Align.Center;
            Assert.AreEqual(Align.Center, rtfTable.Alignment);
        }

        [Test]
        public void StartNewPagePropertyTestForTable()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            rtfTable.StartNewPage = false;
            Assert.AreEqual(false, rtfTable.StartNewPage);
        }

        [Test]
        public void RowCountPropertyTestForTable()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.AreEqual(3, rtfTable.RowCount);
        }

        [Test]
        public void ColCountPropertyTestForTable()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            rtfTable.StartNewPage = false;
            Assert.AreEqual(3, rtfTable.ColCount);
        }

        [Test]
        public void GetCellForTableTest()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            var rtfTableCell = rtfTable.cell(1, 2);
            Assert.AreEqual(2, rtfTableCell.ColIndex);
        }

        [Test]
        public void SetColumnWidthForTableTest()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.DoesNotThrow(() => rtfTable.setColWidth(1, 20));
        }

        [Test]
        public void ShouldThrowExceptionWhenColumnValueIsNegative()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.Throws<Exception>(() => rtfTable.setColWidth(-1, 20));
        }

        [Test]
        public void RowHeightForTableTest()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.DoesNotThrow(() => rtfTable.setRowHeight(1, 20));
        }

        [Test]
        public void ShouldThrowExceptionWhenRowValueIsNegative()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.Throws<Exception>(() => rtfTable.setRowHeight(-1, 20));
        }

        [Test]
        public void SetRowKeepInSamePageTest()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.DoesNotThrow(() => rtfTable.setRowKeepInSamePage(1, true));
        }
        [Test]
        public void ShouldThrowExceptionWhenRowKeepInSamePageIsNegative()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.Throws<Exception>(() => rtfTable.setRowKeepInSamePage(-1, false));
        }

        [Test]
        public void MergeShouldThrowExceptionWhenTopRowIsNegative()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.Throws<Exception>(() => rtfTable.merge(-1, 2, 3, 3));
        }

        [Test]
        public void MergeShouldThrowExceptionWhenLeftColumnIsNegative()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.Throws<Exception>(() => rtfTable.merge(1, -2, 3, 3));
        }

        [Test]
        public void MergeShouldThrowExceptionWhenRowSpanIsLessThanOne()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.Throws<Exception>(() => rtfTable.merge(1, 2, -2, 3));
        }

        [Test]
        public void MergeShouldThrowExceptionWhenColumnSpanIsLessThanOne()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.Throws<Exception>(() => rtfTable.merge(1, 2, 2, -2));
        }

        [Test]
        public void MergeShouldReturnCellWhenRowSpanAndColSpanAreEqual()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            var rtfTableCell = rtfTable.merge(1, 1, 1, 1);
            Assert.AreEqual(1, rtfTableCell.RowIndex);
            Assert.AreEqual(1, rtfTableCell.ColIndex);
        }

        [Test]
        public void MergeInTableTest()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            var rtfTableCell = rtfTable.merge(1, 1, 1, 2);
            Assert.AreEqual(1, rtfTableCell.RowIndex);
            Assert.AreEqual(1, rtfTableCell.ColIndex);
        }

        [Test]
        public void LTR_TableRendererTests()
        {
            var rtfTable = new RtfTable(1, 2, 20, 20);
            var sb = new StringBuilder();

            sb.AppendLine(string.Format(@"{{\trowd\{0}row\trgaph\trpaddl0\trpaddt0\trpaddr0\trpaddb0", LtrContent))
                .AppendLine(@"\trleft0")
                .AppendLine()
                .AppendLine(
                    @"\clbrdrt\brdrw40\brdrs\brdrcf0\clbrdrr\brdrw20\brdrdot\brdrcf0\clbrdrb\brdrw40\brdrs\brdrcf0\clbrdrl\brdrw40\brdrs\brdrcf0\clvertalt\cellx200")
                .AppendLine(
                    @"\clbrdrt\brdrw40\brdrs\brdrcf0\clbrdrr\brdrw40\brdrs\brdrcf0\clbrdrb\brdrw40\brdrs\brdrcf0\clbrdrl\brdrw20\brdrdot\brdrcf0\clvertalt\cellx400")
                .AppendLine(@"\pard\intbl\fi0\ltrpar\ql")
                .AppendLine("CELL 0,0")
                .AppendLine()
                .AppendLine()
                .AppendLine(@"\cell")
                .AppendLine(@"\pard\intbl\fi0\ltrpar\ql")
                .AppendLine("CELL 0,1")
                .AppendLine()
                .AppendLine()
                .AppendLine(@"\cell")
                .AppendLine(@"\row}")
                .Append(@"\sl-400\slmult");

            rtfTable.Margins[Direction.Bottom] = 20;
            rtfTable.setInnerBorder(BorderStyle.Dotted, 1f);
            rtfTable.setOuterBorder(BorderStyle.Single, 2f);

			var expectString = sb.ToString();

            for (var i = 0; i < rtfTable.RowCount; i++)
            {
                for (var j = 0; j < rtfTable.ColCount; j++)
                {
                    rtfTable.cell(i, j).addParagraph().setText("CELL " + i + "," + j);
                }
            }

            var result = rtfTable.render();

            Assert.AreEqual(expectString, result);
        }

        [Test]
        public void RTL_TableRendereTestWithReadingDirectionRightToLeft()
        {
            var rtfTable = new RtfTable(1, 2, 20, 20, ReadingDirection.RightToLeft);
            var sb = new StringBuilder();

            sb.AppendLine(string.Format(@"{{\trowd\{0}row\trgaph\trpaddl0\trpaddt0\trpaddr0\trpaddb0", RtlContent))
                .AppendLine(@"\trleft0")
                .AppendLine()
                .AppendLine(
                    @"\clbrdrt\brdrw40\brdrs\brdrcf0\clbrdrr\brdrw20\brdrdot\brdrcf0\clbrdrb\brdrw40\brdrs\brdrcf0\clbrdrl\brdrw40\brdrs\brdrcf0\clvertalt\cellx200")
                .AppendLine(
                    @"\clbrdrt\brdrw40\brdrs\brdrcf0\clbrdrr\brdrw40\brdrs\brdrcf0\clbrdrb\brdrw40\brdrs\brdrcf0\clbrdrl\brdrw20\brdrdot\brdrcf0\clvertalt\cellx400")
                .AppendLine(@"\pard\intbl\fi0\rtlpar\ql")
                .AppendLine("CELL 0,0")
                .AppendLine()
                .AppendLine()
                .AppendLine(@"\cell")
                .AppendLine(@"\pard\intbl\fi0\rtlpar\ql")
                .AppendLine("CELL 0,1")
                .AppendLine()
                .AppendLine()
                .AppendLine(@"\cell")
                .AppendLine(@"\row}")
                .Append(@"\sl-400\slmult");

            rtfTable.Margins[Direction.Bottom] = 20;
            rtfTable.setInnerBorder(BorderStyle.Dotted, 1f);
            rtfTable.setOuterBorder(BorderStyle.Single, 2f);
			
			var expectString = sb.ToString();

            for (var i = 0; i < rtfTable.RowCount; i++)
            {
                for (var j = 0; j < rtfTable.ColCount; j++)
                {
                    rtfTable.cell(i, j).addParagraph().setText("CELL " + i.ToString() + "," + j.ToString());
                }
            }

            var result = rtfTable.render();

            Assert.AreEqual(expectString, result);
        }

        [Test]
        public void RTL_TableRendereTestWithReadingDirectionLeftToRight()
        {
            var rtfTable = new RtfTable(1, 2, 20, 20, ReadingDirection.LeftToRight);
            var sb = new StringBuilder();

            sb.AppendLine(string.Format(@"{{\trowd\{0}row\trgaph\trpaddl0\trpaddt0\trpaddr0\trpaddb0", LtrContent))
                .AppendLine(@"\trleft0")
                .AppendLine()
                .AppendLine(
                    @"\clbrdrt\brdrw40\brdrs\brdrcf0\clbrdrr\brdrw20\brdrdot\brdrcf0\clbrdrb\brdrw40\brdrs\brdrcf0\clbrdrl\brdrw40\brdrs\brdrcf0\clvertalt\cellx200")
                .AppendLine(
                    @"\clbrdrt\brdrw40\brdrs\brdrcf0\clbrdrr\brdrw40\brdrs\brdrcf0\clbrdrb\brdrw40\brdrs\brdrcf0\clbrdrl\brdrw20\brdrdot\brdrcf0\clvertalt\cellx400")
                .AppendLine(@"\pard\intbl\fi0\ltrpar\ql")
                .AppendLine("CELL 0,0")
                .AppendLine()
                .AppendLine()
                .AppendLine(@"\cell")
                .AppendLine(@"\pard\intbl\fi0\ltrpar\ql")
                .AppendLine("CELL 0,1")
                .AppendLine()
                .AppendLine()
                .AppendLine(@"\cell")
                .AppendLine(@"\row}")
                .Append(@"\sl-400\slmult");

            rtfTable.Margins[Direction.Bottom] = 20;
            rtfTable.setInnerBorder(BorderStyle.Dotted, 1f);
			rtfTable.setOuterBorder(BorderStyle.Single, 2f);

			var expectString = sb.ToString();

            for (var i = 0; i < rtfTable.RowCount; i++)
            {
                for (var j = 0; j < rtfTable.ColCount; j++)
                {
                    rtfTable.cell(i, j).addParagraph().setText("CELL " + i.ToString() + "," + j.ToString());
                }
            }

            var result = rtfTable.render();

            Assert.AreEqual(expectString, result);
        }
    }
}
