
using System;
using System.Text;
using Elistia.DotNetRtfWriter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RtfWriter.UnitTests.Helpers;

namespace RtfWriter.UnitTests
{
    [TestClass]
    public class RtfTableTests
    {
        private const string RtlContent = "rtl";
        private const string LtrContent = "ltr";
        [TestMethod]
        public void CheckDefaultDirectionForTable()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.AreEqual(rtfTable.ReadingDirection, ReadingDirection.LeftToRight);
        }

        [TestMethod]
        public void CheckRightDirectionForTable()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            rtfTable.ReadingDirection = ReadingDirection.RightToLeft;
            Assert.AreEqual(rtfTable.ReadingDirection, ReadingDirection.RightToLeft);
        }

        [TestMethod]
        public void AllignmentPropertyTestForTable()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            rtfTable.Alignment = Align.Center;
            Assert.AreEqual(rtfTable.Alignment, Align.Center);
        }
        [TestMethod]
        public void StartNewPagePropertyTestForTable()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            rtfTable.StartNewPage = false;
            Assert.AreEqual(rtfTable.StartNewPage, false);
        }
        [TestMethod]
        public void RowCountPropertyTestForTable()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            Assert.AreEqual(rtfTable.RowCount, 3);
        }

        [TestMethod]
        public void ColCountPropertyTestForTable()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            rtfTable.StartNewPage = false;
            Assert.AreEqual(rtfTable.ColCount, 3);
        }

        [TestMethod]
        public void GetCellForTableTest()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            var rtfTableCell = rtfTable.cell(1, 2);
            Assert.AreEqual(rtfTableCell.ColIndex, 2);
        }

        [TestMethod]
        public void SetColumnWidthForTableTest()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            AssertHelper.DoesNotThrowException(() => rtfTable.setColWidth(1, 20));
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenColumnValueIsNegative()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            AssertHelper.Throws<Exception>(() => rtfTable.setColWidth(-1, 20));
        }

        [TestMethod]
        public void RowHeightForTableTest()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            AssertHelper.DoesNotThrowException(() => rtfTable.setRowHeight(1, 20));
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenRowValueIsNegative()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            AssertHelper.Throws<Exception>(() => rtfTable.setRowHeight(-1, 20));
        }

        [TestMethod]
        public void SetRowKeepInSamePageTest()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            AssertHelper.DoesNotThrowException(() => rtfTable.setRowKeepInSamePage(1, true));
        }
        [TestMethod]
        public void ShouldThrowExceptionWhenRowKeepInSamePageIsNegative()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            AssertHelper.Throws<Exception>(() => rtfTable.setRowKeepInSamePage(-1, false));
        }

        [TestMethod]
        public void MergeShouldThrowExceptionWhenTopRowIsNegative()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            AssertHelper.Throws<Exception>(() => rtfTable.merge(-1, 2, 3, 3));
        }

        [TestMethod]
        public void MergeShouldThrowExceptionWhenLeftColumnIsNegative()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            AssertHelper.Throws<Exception>(() => rtfTable.merge(1, -2, 3, 3));
        }

        [TestMethod]
        public void MergeShouldThrowExceptionWhenRowSpanIsLessThanOne()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            AssertHelper.Throws<Exception>(() => rtfTable.merge(1, 2, -2, 3));
        }
        [TestMethod]
        public void MergeShouldThrowExceptionWhenColumnSpanIsLessThanOne()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            AssertHelper.Throws<Exception>(() => rtfTable.merge(1, 2, 2, -2));
        }

        [TestMethod]
        public void MergeShouldReturnCellWhenRowSpanAndColSpanAreEqual()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            var rtfTableCell = rtfTable.merge(1, 1, 1, 1);
            Assert.AreEqual(rtfTableCell.RowIndex, 1);
            Assert.AreEqual(rtfTableCell.ColIndex, 1);

        }

        [TestMethod]
        public void MergeInTableTest()
        {
            var rtfTable = new RtfTable(3, 3, 20, 20);
            var rtfTableCell = rtfTable.merge(1, 1, 1, 2);
            Assert.AreEqual(rtfTableCell.RowIndex, 1);
            Assert.AreEqual(rtfTableCell.ColIndex, 1);

        }


        [TestMethod]
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

            for (var i = 0; i < rtfTable.RowCount; i++)
            {
                for (var j = 0; j < rtfTable.ColCount; j++)
                {
                    rtfTable.cell(i, j).addParagraph().setText("CELL " + i.ToString() + "," + j.ToString());
                }
            }

            var result = rtfTable.render();
            var expectString = sb.ToString();

            Assert.AreEqual(expectString, result);

        }

        [TestMethod]
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

            for (var i = 0; i < rtfTable.RowCount; i++)
            {
                for (var j = 0; j < rtfTable.ColCount; j++)
                {
                    rtfTable.cell(i, j).addParagraph().setText("CELL " + i.ToString() + "," + j.ToString());
                }
            }

            var result = rtfTable.render();
            var expectString = sb.ToString();

            Assert.AreEqual(expectString, result);

        }

        [TestMethod]
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

            for (var i = 0; i < rtfTable.RowCount; i++)
            {
                for (var j = 0; j < rtfTable.ColCount; j++)
                {
                    rtfTable.cell(i, j).addParagraph().setText("CELL " + i.ToString() + "," + j.ToString());
                }
            }

            var result = rtfTable.render();
            var expectString = sb.ToString();

            Assert.AreEqual(expectString, result);

        }

    }
}
