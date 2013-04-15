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

        public string AncestorAttributeName { get; set; }
        public string AncestorAttributeValue { get; set; }
        public string AncestorAttributeValueRegex { get; set; }
        public string GenericAttributeName { get; set; }
        public string GenericAttributeValue { get; set; }
        public string GenericAttributeValueRegex { get; set; }
        public string LabelText { get; set; }
        public string LabelTextRegex { get; set; }
        public string RelText { get; set; }
        public string RelTextRegex { get; set; }
        public string NearText { get; set; }
        
        protected override Constraint GetConstraint()
        {
            var constraint = base.GetConstraint();

            if (constraint is AnyConstraint)
            {
                // no constraint was created from the base
                constraint = null;

                Combine(ref constraint, CreateStringConstraint(Find.Near, NearText));
                Combine(ref constraint, CreateStringConstraint(Find.ByLabelText, LabelText));
                Combine(ref constraint, CreateRegexConstraint(Find.ByLabelText, LabelTextRegex));
                Combine(ref constraint, CreateAncestorSelectorStringConstraint(Find.ByExistenceOfRelatedElement<Element>, AncestorAttributeName, AncestorAttributeValue));
                Combine(ref constraint, CreateAncestorSelectorRegexConstraint(Find.ByExistenceOfRelatedElement<Element>, AncestorAttributeName, AncestorAttributeValueRegex));
                Combine(ref constraint, CreateGenericAttributeStringConstraint(Find.By, "rel", RelText));
                Combine(ref constraint, CreateGenericAttributeRegexConstraint(Find.By, "rel", RelTextRegex));
                Combine(ref constraint, CreateGenericAttributeStringConstraint(Find.By, GenericAttributeName, GenericAttributeValue));
                Combine(ref constraint, CreateGenericAttributeRegexConstraint(Find.By, GenericAttributeName, GenericAttributeValueRegex));
            }

            return constraint ?? Find.Any;
        }

        private delegate Constraint ElementSelectorConstraintFactory(ElementSelector<Element> selector);
        private delegate Constraint GenericAttributeStringConstraintFactory(string attributeName, string attributeValue);
        private delegate Constraint GenericAttributeRegexConstraintFactory(string attributeName, Regex attributeValue);

        private static Constraint CreateAncestorSelectorRegexConstraint(ElementSelectorConstraintFactory factory, string attributeName, string attributeValueRegex)
        {
            if (attributeName == null || attributeValueRegex == null) return null;
            return factory(el => el.Ancestor(Find.By(attributeName, new Regex(attributeValueRegex))));
        }

        private static Constraint CreateAncestorSelectorStringConstraint(ElementSelectorConstraintFactory factory, string attributeName, string attributeValue)
        {
            if (attributeName == null || attributeValue == null) return null;
            return factory(el => el.Ancestor(Find.By(attributeName, attributeValue)));
        }

        private static Constraint CreateGenericAttributeRegexConstraint(GenericAttributeRegexConstraintFactory factory, string attributeName, string attributeValue)
        {
            return attributeName != null && attributeValue != null ? factory(attributeName, new Regex(attributeValue)) : null;
        }

        private static Constraint CreateGenericAttributeStringConstraint(GenericAttributeStringConstraintFactory factory, string attributeName, string attributeValue)
        {
            return attributeName != null && attributeValue != null ? factory(attributeName, attributeValue) : null;
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
