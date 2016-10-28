using System;
using System.Text;

namespace Elistia.DotNetRtfWriter
{
    public class RtfSectionFooter : RtfBlockList
    {
        internal RtfSectionFooter(RtfSection parent)
            : this(parent, ReadingDirection.LeftToRight)
        {

        }
        internal RtfSectionFooter(RtfSection parent, ReadingDirection direction)
            : base(true, true, true, true, true)
        {
            ReadingDirection = direction;
            if (parent == null)
            {
                throw new Exception("Section footer can only be placed within a section ");
            }
        }

        public override string render()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine(string.Format(@"{{\footerr \{0}par \pard\plain", ContentDirection));
            result.AppendLine(@"\par ");
            result.Append(base.render());
            result.AppendLine(@"\par");
            result.AppendLine(@"}");

            return result.ToString();
        }
    }
}
