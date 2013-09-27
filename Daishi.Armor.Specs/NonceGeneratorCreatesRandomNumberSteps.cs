#region Includes

using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Armor.Specs {
    [Binding]
    public class NonceGeneratorCreatesRandomNumberSteps {
        private readonly List<Int64> nonces = new List<Int64>();

        [Given(@"I have generated (.*) random numbers")]
        public void GivenIHaveGeneratedRandomNumbers(int p0) {
            for (var i = 0; i < p0; i++) {
                var nonceGenerator = new NonceGenerator();
                nonceGenerator.Execute();

                nonces.Add(nonceGenerator.Nonce);
            }
        }

        [Then(@"each number should be unique")]
        public void ThenEachNumberShouldBeUnique() {
            Assert.AreEqual(nonces.Count, nonces.Distinct().Count());
        }
    }
}