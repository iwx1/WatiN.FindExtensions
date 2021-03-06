#WatiN.FindExtensions

##Introduction

The WatiN.FindExtensions library was created partially as an experiment with GitHub, but also to address an issue I was running into when using [WatiN](http://watin.org/) (a .net browser automation library).

In WatiN, there is a Page class which can be used to [encapsulate page functionality](http://watinandmore.blogspot.com/2009/06/introducing-page-class.html).  The purpose is to reduce the duplicated code in tests for locating elements on any given page in your test application.  Let's look at a couple examples (using MSTest) using the DRY and not DRY approaches.

Not DRY:

	[TestMethod]
	public void Type_test_in_box() 
	{
		using(var browser = new IE()) 
		{
			var element = browser.TextField(Find.ById("elementId"));
			element.TypeText("test");
		}
	}

Now imagine you've got hundreds of tests like this where you rely upon magic strings for control lookups.  If you need to change a control type, you need to go through your hundreds of tests and make those changes.

Instead, if you encapsulate the handle of that element within a page class, you only need to change that page class when your application changes.  Here's what that looks like using a DRY approach:

	public class MyPage : WatiN.Core.Page
	{
		[FindBy(Id = "elementId")]
		public TextField Element { get; set; }
	}

	[TestMethod]
	public void Type_test_in_box() 
	{
		using(var browser = new IE()) 
		{
			var page = browser.Page<MyPage>();
			page.Element.TypeText("test");
		}
	}

This works really, really well.  Nice job, Jeroen van Menen and all contributors to the WatiN project.  I love it.

Here's where my problem comes in though.  I want to use page classes, but sometimes, I can't locate the elements I need based on the constraints accessible through the default FindByAttribute class (Alt, AltRegex, Class, ClassRegex, For, ForRegex, Id, IdRegex, Name, NameRegex, Text, TextRegex, Url, UrlRegex, Title, TitleRegex, Value, ValueRegex, Src, SrcRegex, Index).  In fact, WatiN has all the constraints I already want to use, but they aren't exposed as an attribute.

That's where the WatiN.FindExtensions come in.  This library will expose those additional constraints in an extended FindBy attribute for those of you who which to do more complex element lookups.

##Usage

The following element lookup methods have been added into the FindByAttribute:

- Ancestor attribute (value and regex)
		
		<div id="relatedElementParent">
			<input type="text" />
		</div>

		[ExtendedFindBy(AncestorAttributeName = "id", AncestorAttributeValue = "relatedElementParent")]
		public TextField MyTextField { get;set; }

		[ExtendedFindBy(AncestorAttributeName = "id", AncestorAttributeValueRegex = "^related")]
		public TextField MyTextField { get; set; }

- Generic Attribute (value and regex)

		<input type="text" data-extra-id="identifier of some sort" />

		[ExtendedFindBy(GenericAttributeName = "data-extra-id", GenericAttributeValue = "identifier of some sort")]
		public TextField MyTextField { get; set; }

		[ExtendedFindBy(GenericAttributeName = "data-extra-id", GenericAttributeValueRegex = "^identifier of some sort$")]
		public TextField MyTextField { get; set; }

- Css Selector

		<div>
			<div class="row">
				<label for="fieldId">Field Label:</label>
				<input id="fieldId" type="text" />
			</div>
		</div>

		[ExtendedFindBy(CssSelectorText = "#fieldId")]
		public TextField MyTextField { get; set; }

		[ExtendedFindBy(CssSelectorText = "div > div.row > label")]
		public Label MyLabel { get; set; }

- Label Text (value and regex)

	*Note: For this to work, the label must have a "for" value of the control you're trying to locate.*

		<label for="randomlygeneratedid">First Name:</label>
		<input id="randomlygeneratedid" type="text" />

		[ExtendedFindBy(LabelText = "First Name:")]
		public TextField MyTextField { get; set; }

		[ExtendedFindBy(LabelTextRegex = "^First")]
		public TextField MyTextField { get; set; }

- Rel Text (value and regex)

		<input rel="first-name" type="text" />

		[ExtendedFindBy(RelText = "first-name")]
		public TextField MyTextField { get; set; }

		[ExtendedFindBy(RelTextRegex = "^first-")]
		public TextField MyTextField { get; set; }

- Near Text (See the WatiN documentation regarding the Near constraint)

		<label>First Name:</label>
		<input type="text" />

		[ExtendedFindBy(NearText = "First Name:")]
		public TextField MyTextField { get; set; }
