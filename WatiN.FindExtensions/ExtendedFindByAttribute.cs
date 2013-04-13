using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WatiN.Core;
using WatiN.Core.Constraints;

namespace WatiN.FindExtensions
{
    /// <summary>
    /// Extends the default WatiN FindByAttribute to expose additional constraints at the attribute level.  Patterns have been followed
    /// from the WatiN base FindByAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ExtendedFindByAttribute : FindByAttribute
    {
        public ExtendedFindByAttribute() : base()
        {
        }

        public string LabelText { get; set; }
        public string LabelTextRegex { get; set; }

        protected override Constraint GetConstraint()
        {
            var constraint = base.GetConstraint();

            if (constraint is AnyConstraint)
            {
                // no constraint was created from the base
                constraint = null;

                Combine(ref constraint, CreateStringConstraint(Find.ByLabelText, LabelText));
                Combine(ref constraint, CreateRegexConstraint(Find.ByLabelText, LabelTextRegex));
            }

            return constraint ?? Find.Any;
        }


        // private methods copied from base class
        private delegate Constraint StringConstraintFactory(string value);
        private delegate Constraint RegexConstraintFactory(Regex value);

        private static Constraint CreateStringConstraint(StringConstraintFactory factory, string value)
        {
            return value != null ? factory(value) : null;
        }

        private static Constraint CreateRegexConstraint(RegexConstraintFactory factory, string value)
        {
            return value != null ? factory(new Regex(value)) : null;
        }

        private static void Combine(ref Constraint constraint, Constraint otherConstraint)
        {
            if (otherConstraint != null)
            {
                constraint = constraint != null ? constraint & otherConstraint : otherConstraint;
            }
        }
    }
}
