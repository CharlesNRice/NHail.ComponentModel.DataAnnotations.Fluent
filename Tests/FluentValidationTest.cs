using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHail.ComponentModel.DataAnnotations.Fluent;

namespace Tests
{
    [TestClass]
    public class FluentValidationTest
    {
        [TestInitialize]
        public void Initialize()
        {
            var configuration = new AttributeConfiguration<POCO>();
            // Require the Required property
            configuration.ValidationsFor(p => p.Required).Add<RequiredAttribute>();

            // Required but give our own error message
            configuration.ValidationsFor(p => p.RequiredWithErrorMessage)
                .Add<RequiredAttribute>(r => r.ErrorMessage = Messages.RequiredIsRequired);

            // Valid when true and not valid when false
            configuration.ValidationsFor(p => p.Valid)
                .Add(
                    valid =>
                        valid
                            ? ValidationResult.Success
                            : new ValidationResult(Messages.ValidPropNotValid, new[] {nameof(POCO.Valid)}));
        }

        [TestMethod]
        public void TestRequiredFails()
        {
            var poco = new POCO();
            var context = new ValidationContext(poco)
            {
                MemberName = nameof(POCO.Required)
            };
            var results = new List<ValidationResult>();
            if (Validator.TryValidateProperty(poco.Required, context, results))
            {
                Assert.Fail("Required was not required!!!");
            }

            Assert.IsNotNull(results.FirstOrDefault(r => r.MemberNames.Any(m => m == nameof(POCO.Required))));
        }

        [TestMethod]
        public void TestRequiredPasses()
        {
            var poco = new POCO();
            poco.Required = "Not Empty";

            var context = new ValidationContext(poco)
            {
                MemberName = nameof(POCO.Required)
            };
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateProperty(poco.Required, context, results))
            {
                Assert.Fail("Required was filled in");
            }

            Assert.IsNull(results.FirstOrDefault(r => r.MemberNames.Any(m => m == nameof(POCO.Required))));
        }

        [TestMethod]
        public void TestRequiredWithMessageFails()
        {
            var poco = new POCO();
            var context = new ValidationContext(poco)
            {
                MemberName = nameof(POCO.RequiredWithErrorMessage)
            };
            var results = new List<ValidationResult>();
            if (Validator.TryValidateProperty(poco.RequiredWithErrorMessage, context, results))
            {
                Assert.Fail("RequiredWithErrorMessage was not required!!!");
            }

            Assert.IsNotNull(
                results.FirstOrDefault(
                    r =>
                        r.MemberNames.Any(m => m == nameof(POCO.RequiredWithErrorMessage)) &&
                        r.ErrorMessage == Messages.RequiredIsRequired));
        }

        [TestMethod]
        public void TestValidMethodFails()
        {
            var poco = new POCO();
            var context = new ValidationContext(poco)
            {
                MemberName = nameof(POCO.Valid)
            };
            var results = new List<ValidationResult>();
            if (Validator.TryValidateProperty(poco.Valid, context, results))
            {
                Assert.Fail("Valid Property Passed!!!");
            }

            Assert.IsNotNull(
                results.FirstOrDefault(
                    r =>
                        r.MemberNames.Any(m => m == nameof(POCO.Valid)) &&
                        r.ErrorMessage == Messages.ValidPropNotValid));
        }

        [TestMethod]
        public void TestValidMethodPasses()
        {
            var poco = new POCO();
            poco.Valid = true;

            var context = new ValidationContext(poco)
            {
                MemberName = nameof(POCO.Valid)
            };
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateProperty(poco.Valid, context, results))
            {
                Assert.Fail("Valid Property Failed!!!");
            }

            Assert.IsNull(
                results.FirstOrDefault(
                    r =>
                        r.MemberNames.Any(m => m == nameof(POCO.Valid)) &&
                        r.ErrorMessage == Messages.ValidPropNotValid));
        }
    }
}
