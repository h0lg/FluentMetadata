using FluentMetadata.Rules;

namespace FluentMetadata.Specs.Rules
{
    public abstract class ReminderPropertyMustBeLessThanOtherRuleSpecs(PropertyMustBeLessThanOtherRule<Reminder> sut)
    {
        protected readonly PropertyMustBeLessThanOtherRule<Reminder> Sut = sut;
    }

    [TestClass]
    public class If_a_date_should_be_before_another : ReminderPropertyMustBeLessThanOtherRuleSpecs
    {
        public If_a_date_should_be_before_another()
            : base(new PropertyMustBeLessThanOtherRule<Reminder>(x => x.AlertDate, x => x.EventDate)) { }

        [TestMethod]
        public void It_is_valid_if_it_is_earlier_than_the_other()
        {
            var model = new Reminder { EventDate = DateTime.Now.AddDays(1) };
            model.AlertDate = model.EventDate.AddHours(-2);
            Assert.IsTrue(Sut.IsValid(model));
        }

        [TestMethod]
        public void It_is_invalid_if_it_is_equal_to_the_other()
        {
            var model = new Reminder { EventDate = DateTime.Now.AddDays(1) };
            model.AlertDate = model.EventDate;
            Assert.IsFalse(Sut.IsValid(model));
        }

        [TestMethod]
        public void It_is_invalid_if_it_is_later_than_the_other()
        {
            var model = new Reminder { EventDate = DateTime.Now.AddDays(1) };
            model.AlertDate = model.EventDate.AddHours(2);
            Assert.IsFalse(Sut.IsValid(model));
        }
    }

    [TestClass]
    public class If_an_int_property_should_be_less_than_another : ReminderPropertyMustBeLessThanOtherRuleSpecs
    {
        public If_an_int_property_should_be_less_than_another()
            : base(new PropertyMustBeLessThanOtherRule<Reminder>(x => x.AlertDayOfWeek, x => x.EventDayOfWeek)) { }

        [TestMethod]
        public void It_is_valid_if_it_is_less_than_the_other()
        {
            var model = new Reminder { EventDayOfWeek = 5, AlertDayOfWeek = 3 };
            Assert.IsTrue(Sut.IsValid(model));
        }
    }

    public class Reminder
    {
        public DateTime AlertDate { get; set; }
        public DateTime EventDate { get; set; }
        public int AlertDayOfWeek { get; set; }
        public int EventDayOfWeek { get; set; }
    }
}
