using RM.WeatherForLunch.Core.Models;
using RM.WeatherForLunch.Core.Services;
using RM.WeatherForLunch.Specflow.Mocks;
using TechTalk.SpecFlow.Assist;

namespace RM.WeatherForLunch.Specflow.StepDefinitions
{
    [Binding]
    public sealed class LunchForcastStepDefinitions
    {
        private readonly ScenarioContext scenarioContext;

        public LunchForcastStepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [Given(@"the weather is:")]
        public void GivenTheWeatherIs(Table table)
        {
            var currentCondition = table.CreateInstance<CurrentCondition>();
            var forcast = new Forcast()
            {
                CurrentConditions = new List<CurrentCondition>() { currentCondition }
            };
            scenarioContext[nameof(Forcast)] = forcast;
        }

        [When(@"I check the current weather forcast")]
        public async void WhenICheckTheCurrentWeatherForcast()
        {
            var forcast = scenarioContext[nameof(Forcast)].As<Forcast>();
            Assert.NotNull(forcast);

            var repository = new WeatherRepositoryMock();
            var api = new WeatherAPIMock(forcast);
            var lunchForcastService = new LunchForcastService(api, repository);
            var currentForcast = await lunchForcastService.GetLunchForcast("Dordrecht");
            scenarioContext[nameof(LunchForcast)] = currentForcast;
        }

        [Then(@"sit outside is (.*)")]
        public void ThenSitOutsideIs(string expectedBoolean)
        {
            var expected = bool.Parse(expectedBoolean);
            var lunchForcast = scenarioContext[nameof(LunchForcast)].As<LunchForcast>();

            Assert.Equal(lunchForcast.CanSitOutside, expected);
        }

    }
}
