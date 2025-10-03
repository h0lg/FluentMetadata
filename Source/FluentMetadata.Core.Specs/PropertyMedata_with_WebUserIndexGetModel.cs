using FluentMetadata.Rules;
using FluentMetadata.Specs.SampleClasses;

namespace FluentMetadata.Specs
{
    [TestClass]
    public class PropertyMedata_with_WebUserIndexGetModel : MetadataTestBase
    {
        private readonly Metadata username, id, autorName, email, role, secondaryRoles, passwordHash, comment, active;

        public PropertyMedata_with_WebUserIndexGetModel()
        {
            username = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexGetModel), "Username");
            id = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexGetModel), "Id");
            email = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexGetModel), "EMail");
            autorName = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexGetModel), "AutorName");
            role = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexGetModel), "Role");
            secondaryRoles = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexGetModel), "SecondaryRoles");
            passwordHash = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexGetModel), "PasswordHash");
            comment = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexGetModel), "Comment");
            active = QueryFluentMetadata.GetMetadataFor(typeof(WebUserIndexGetModel), "Active");
        }

        [TestMethod] public void Username_ModelName_is_Username() { Assert.AreEqual("Username", username.ModelName); }
        [TestMethod] public void Username_ModelType_is_string() { Assert.AreEqual(typeof(string), username.ModelType); }
        [TestMethod] public void Username_DisplayName_is_Benutzername() { Assert.AreEqual("Benutzername", username.GetDisplayName()); }
        [TestMethod] public void Username_Required_is_true() { Assert.IsTrue(username.Required.Value); }
        [TestMethod] public void Id_ModelName_is_Id() { Assert.AreEqual("Id", id.ModelName); }
        [TestMethod] public void Id_ModelType_is_int() { Assert.AreEqual(typeof(int), id.ModelType); }
        [TestMethod] public void Id_Required_is_false() { Assert.IsFalse(id.Required.HasValue); }
        [TestMethod] public void AutorName_DisplayName_is_emaN() { Assert.AreEqual("emaN", autorName.GetDisplayName()); }
        [TestMethod] public void EMail_DataTypeName_is_EmailAddress() { Assert.AreEqual("EmailAddress", email.DataTypeName); }
        [TestMethod] public void Username_Description_is_Name_des_Benutzers() { Assert.AreEqual("Name des Benutzers", username.GetDescription()); }
        [TestMethod] public void EMail_DisplayFormat_is_MailtoLink() { Assert.AreEqual("<a href='mailto:{0}'>{0}</a>", email.GetDisplayFormat()); }
        [TestMethod] public void EMail_EditorFormat_is_plain_value() { Assert.AreEqual("{0}", email.GetEditorFormat()); }

        [TestMethod]
        public void Id_HideSurroundingHtml_is_true()
        {
            Assert.IsTrue(id.HideSurroundingHtml.HasValue);
            Assert.IsTrue(id.HideSurroundingHtml.Value);
        }

        [TestMethod] public void Username_ReadOnly_is_true() { Assert.IsTrue(username.ReadOnly); }
        [TestMethod] public void AutorName_NullDisplayText_is_Anonymous_Autor() { Assert.AreEqual("Anonymous Autor", autorName.GetNullDisplayText()); }
        [TestMethod] public void Id_ShowDisplay_is_false() { Assert.IsFalse(id.ShowDisplay); }
        [TestMethod] public void Id_ShowEditor_is_false() { Assert.IsFalse(id.ShowEditor); }
        [TestMethod] public void Role_TemplateHint_is_Roles() { Assert.AreEqual("Roles", role.TemplateHint); }
        [TestMethod] public void EMail_Watermark_is_dummy_address() { Assert.AreEqual("john@doe.com", email.GetWatermark()); }
        [TestMethod] public void Username_ConvertEmptyStringToNull_is_false() { Assert.IsFalse(username.ConvertEmptyStringToNull); }

        [TestMethod]
        public void Id_Hidden_is_true()
        {
            Assert.IsTrue(id.Hidden.HasValue);
            Assert.IsTrue(id.Hidden.Value);
        }

        [TestMethod]
        public void Username_GetMaximumLength_is_256()
        {
            var maxLength = username.GetMaximumLength();
            Assert.IsTrue(maxLength.HasValue);
            Assert.AreEqual(256, maxLength);
        }

        [TestMethod] public void Username_ContainerType_is_WebUserIndexGetModel() { Assert.AreEqual(typeof(WebUserIndexGetModel), username.ContainerType); }

        [TestMethod]
        public void IsNotRequiredOverWritesCopiedIsRequired()
        {
            Assert.IsFalse(passwordHash.Required.Value);
            Assert.AreEqual(0, passwordHash.Rules.OfType<RequiredRule>().Count());
        }

        [TestMethod] public void LengthIsCopiedFromNonPublicProperty() { Assert.AreEqual(32, passwordHash.GetMinimumLength()); }
        [TestMethod] public void DisplayNameOfPropertyWithComplexTypeIsCopied() { Assert.AreEqual("Secondaly lores", secondaryRoles.GetDisplayName()); }
        [TestMethod] public void Comment_RequestValidationEnabled_is_false() { Assert.IsFalse(comment.RequestValidationEnabled); }
    }
}