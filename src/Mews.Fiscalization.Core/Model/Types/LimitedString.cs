using System.Collections.Generic;

namespace Mews.Fiscalization.Core.Model
{
    public abstract class LimitedString : ValueWrapper<string>
    {
        protected LimitedString(string value, StringLimitation limitation)
            : base(value, limitation)
        {
        }

        protected LimitedString(string value, IEnumerable<StringLimitation> limitations)
            : base(value, limitations)
        {
        }
    }
}
