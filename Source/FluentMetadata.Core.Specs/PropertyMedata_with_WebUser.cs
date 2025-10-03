using FluentMetadata.Rules;
using FluentMetadata.Specs.SampleClasses;

namespace FluentMetadata.Specs
{
    [TestClass]
    public class PropertyMedata_with_WebUser : MetadataTestBase
    {
        private readonly Metadata lastLogin, username, id, passWordHash, role, bounceCount;

        public PropertyMedata_with_WebUser()
        {
            username = QueryFluentMetadata.GetMetadataFor(typeof(WebUser), "Username");
            id = QueryFluentMetadata.GetMetadataFor(typeof(WebUser), "Id");
            lastLogin = QueryFluentMetadata.GetMetadataFor(typeof(WebUser), "LastLogin");
            passWordHash = QueryFluentMetadata.GetMetadataFor(typeof(WebUser), "PasswordHash");
            role = QueryFluentMetadata.GetMetadataFor(typeof(WebUser), "Role");
            bounceCount = QueryFluentMetadata.GetMetadataFor(typeof(WebUser), "BounceCount");
        }

        [TestMethod]
        public void Username_ModelName_is_Username()
        {
            Assert.AreEqual("Username", username.ModelName);
        }

        [TestMethod]
        public void Username_ModelType_is_string()
        {
            Assert.AreEqual(typeof(string), username.ModelType);
        }

        [TestMethod]
        public void Username_DisplayName_is_Benutzername()
        {
            Assert.AreEqual("Benutzername", username.GetDisplayName());
        }

        [TestMethod]
        public void Username_Required_is_true()
        {
            Assert.IsTrue(username.Required.Value);
        }

        [TestMethod]
        public void Username_MinLength_is_3()
        {
            Assert.AreEqual(3, username.GetMinimumLength());
        }

        [TestMethod]
        public void Username_MaxLength_is_256()
        {
            Assert.AreEqual(256, username.GetMaximumLength());
        }

        [TestMethod]
        public void PassWordHash_MinLength_is_32()
        {
            Assert.AreEqual(32, passWordHash.GetMinimumLength());
        }

        [TestMethod]
        public void PassWordHash_MaxLength_is_null()
        {
            Assert.IsNull(passWordHash.GetMaximumLength());
        }

        [TestMethod]
        public void Role_MinLength_is_null()
        {
            Assert.IsNull(role.GetMinimumLength());
        }

        [TestMethod]
        public void Role_MaxLength_is_256()
        {
            Assert.AreEqual(256, role.GetMaximumLength());
        }

        [TestMethod]
        public void Id_ModelName_is_Id()
        {
            Assert.AreEqual("Id", id.ModelName);
        }

        [TestMethod]
        public void Id_ModelType_is_int()
        {
            Assert.AreEqual(typeof(int), id.ModelType);
        }

        [TestMethod]
        public void Id_Required_is_false()
        {
            Assert.IsFalse(id.Required.HasValue);
        }

        [TestMethod]
        public void Last_Login_Minimum_is_2010_1_23()
        {
            Assert.AreEqual(new DateTime(2010, 1, 23), lastLogin.GetRangeMinimum());
        }

        [TestMethod]
        public void Last_Login_Maximum_is_DoomsDay()
        {
            Assert.AreEqual(DateTime.MaxValue, lastLogin.GetRangeMaximum());
        }

        [TestMethod]
        public void Generic_bounceCount_rule_is_valid_when_email_has_bounced_twice()
        {
            var bounceCountRule = bounceCount.Rules
                .OfType<GenericRule<int>>()
                .Single();
            var webUser = new WebUser();

            webUser.MailHasBounced();
            webUser.MailHasBounced();

            Assert.IsTrue(bounceCountRule.IsValid(webUser.BounceCount));
        }

        [TestMethod]
        public void Generic_bounceCount_rule_is_invalid_when_email_has_bounced_thrice()
        {
            var bounceCountRule = bounceCount.Rules
                .OfType<GenericRule<int>>()
                .Single();
            var webUser = new WebUser();

            webUser.MailHasBounced();
            webUser.MailHasBounced();
            webUser.MailHasBounced();

            Console.WriteLine(bounceCountRule.FormatErrorMessage(bounceCount.GetDisplayName()));
            Assert.IsFalse(bounceCountRule.IsValid(webUser.BounceCount));
        }
    }
}