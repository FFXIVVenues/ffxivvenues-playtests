using Microsoft.Playwright;

namespace FFXIVVenues.PlayTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class WhenUsingAddYourVenueModal : PageTest
{

    [SetUp]
    public async Task SetUp()
    {
        var host = Environment.GetEnvironmentVariable("HOST") ?? "https://ffxivvenues.com";
        if (!host.StartsWith("http"))
            host = "https://" + host;
        await Page.GotoAsync(host);
        await Page.GetByText("Add your venue").Nth(0).ClickAsync();
    }

    [Test]
    public async Task ThenModalShows()
    {
        await Page.GetByText("Join via Veni!").IsVisibleAsync();
        await Page.GetByText("Join the discord!").Nth(0).IsVisibleAsync();
        await Page.GetByText("Meet Veni Ki!").Nth(0).IsVisibleAsync();
    }

    [Test]
    public async Task ThenJoinDiscordButtonTakesYouToDiscordServer()
    {
        await Page.GetByText("Join the discord!").Nth(0).ClickAsync(); 
        var page = await Page.WaitForPopupAsync();
        await page.WaitForURLAsync(new Regex("discord\\.com"));
        await page.GetByText("Discord App Launched").IsVisibleAsync();
    }

    [Test]
    public async Task ThenMeetVeniButtonTakesYouToVeniOnDiscord()
    {
        await Page.GetByText("Meet Veni Ki!").Nth(0).ClickAsync();

        var page = await Page.WaitForPopupAsync();
        await page.WaitForURLAsync(new Regex("discord\\.com"));
    }

}