using NUnit.Framework;

namespace NSubstitute.Acceptance.Specs.FieldReports
{
    public class CustomArgumentMatchersWhichDoNotHandleNulls
    {
        public interface IRequest { 
            string Get(string url);
            string[] GetMultiple(string[] url);
        }

        [Test]
        [Pending]
        public void Single_condition_that_does_not_handle_nulls()
        {
            var request = Substitute.For<IRequest>();
            request.Get(Arg.Is<string>(x => x.Contains("greeting"))).Returns("hello world");

            Assert.That(request.Get("greeting"), Is.EqualTo("hello world"));
            Assert.That(request.Get(null), Is.EqualTo(""));
        }

        [Test]
        [Pending]
        public void Single_negated_condition_that_does_not_handle_nulls()
        {
            var request = Substitute.For<IRequest>();
            request.Get(Arg.Is<string>(x => !x.Contains("please"))).Returns("ask nicely");

            Assert.That(request.Get("greeting"), Is.EqualTo("ask nicely"));
            Assert.That(request.Get(null), Is.EqualTo(""));
            //Assert.Fail("Should Get(null) return 'ask nicely' or string.empty here?");
        }

        [Test]
        [Pending]
        public void Multiple_conditions_that_require_an_object_reference()
        {
            var request = Substitute.For<IRequest>();
            request.Get(Arg.Is<string>(x => x.Contains("greeting"))).Returns("hello world");
            request.Get(Arg.Is<string>(x => x.Length < 5)).Returns("?");

            Assert.That(request.Get("greeting"), Is.EqualTo("hello world"));
            Assert.That(request.Get(""), Is.EqualTo("?"));
            Assert.That(request.Get(null), Is.EqualTo(""));
            //Assert.Fail("Should Get(null) return '?' or string.empty here?");
        }

        [Test]
        [Pending]
        public void Multiple_negated_conditions_that_requires_an_object_reference()
        {
            var request = Substitute.For<IRequest>();
            request.Get(Arg.Is<string>(x => !x.StartsWith("please"))).Returns("ask nicely");
            request.Get(Arg.Is<string>(x => x.Length > 10)).Returns("request too long");

            Assert.That(request.Get("greeting"), Is.EqualTo("ask nicely"));
            Assert.That(request.Get("please provide me with a greeting"), Is.EqualTo("request too long"));
            Assert.That(request.Get(null), Is.EqualTo(""));
        }

        [Test]
        [Pending]
        public void Condition_that_requires_object_ref_and_condition_that_covers_null()
        {
            var request = Substitute.For<IRequest>();
            request.Get(Arg.Is<string>(x => x.Contains("greeting"))).Returns("hello world");
            request.Get(null).Returns("?");

            Assert.That(request.Get(null), Is.EqualTo("?"));
        }

        [Test]
        [Pending]
        public void Multiple_conditions_with_multiple_returns()
        {
            var request = Substitute.For<IRequest>();
            request.Get(null).Returns("huh?", "what?", "pardon?");
            request.Get(Arg.Is<string>(x => x.Contains("greeting"))).Returns("hello world");

            Assert.That(request.Get(null), Is.EqualTo("huh?"));
            Assert.That(request.Get(null), Is.EqualTo("what?"));
        }

        [Test]
        [Pending]
        public void Multiple_conditions_where_one_requires_an_array_index_available()
        {
            var request = Substitute.For<IRequest>();
            request.GetMultiple(Arg.Is<string[]>(x => x[0] == "greeting")).Returns(new[] { "hello", "bye" });
            request.GetMultiple(new string[0]).Returns(new [] {"?"});

            Assert.That(request.GetMultiple(new [] {"greeting"}), Is.EqualTo(new[] {"hello", "bye"}));
            Assert.That(request.GetMultiple(new string[0]), Is.EqualTo(new[] { "?" }));
        }
    }
}