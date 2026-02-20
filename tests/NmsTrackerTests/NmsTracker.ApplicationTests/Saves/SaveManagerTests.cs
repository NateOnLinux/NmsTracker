using Microsoft.Reactive.Testing;
using Moq;
using NmsTracker.Application.Saves;
using NmsTracker.Domain.Entities.Saves;
using NmsTracker.Domain.VObs.Saves;

namespace NmsTracker.ApplicationTests.Saves;

public class SaveListenerTests {

    public static TheoryData<List<Platform>> Platforms => [
        new List<Platform>(),
        new List<Platform>([new Platform(PlatformId.Steam, new DirectoryInfo("~/Steam"), [])])
    ];
    [Fact]
    public void Saves_EmitsSaves_WhenPlatformsObservableEmits() {
        TestScheduler scheduler = new();
        // Build save data
        Save save1 =
            new(new SaveId("Save 1"), PlatformId.Steam, "Steam-01", false, true, DateTime.MinValue);
        Save save2 =
            new(new SaveId("Save 2"), PlatformId.Steam, "Steam-02", false, true, DateTime.MinValue);
        Save save3 =
            new(new SaveId("Save 1"), PlatformId.Gog, "Gog-01", false, true, DateTime.MinValue);
        Save save4 =
            new(new SaveId("Save 2"), PlatformId.Gog, "Gog-02", false, true, DateTime.MinValue);
        Platform platformSteam =
            new(PlatformId.Steam, new DirectoryInfo("./Steam-Test"), [save1, save2]);
        Platform platformGog = new(PlatformId.Gog, new DirectoryInfo("./Gog-Test"), [save3, save4]);
        List<Platform> platforms = [platformSteam, platformGog];

        ITestableObservable<List<Platform>> platformSubject =
            scheduler.CreateColdObservable(ReactiveTest.OnNext(10, platforms),
                ReactiveTest.OnCompleted<List<Platform>>(10));

        Mock<IPlatformAdapter> adapter = new();

        adapter.Setup(a => a.PlatformsObservable).Returns(platformSubject);

        IReadOnlyList<Save> saves = [];

        SaveManager sut = new(adapter.Object);
        using IDisposable sub = sut.Saves.Subscribe(s => saves = s);

        scheduler.Start();

        Assert.NotNull(saves);
        Assert.Equal(4, saves.Count);
        Assert.Contains(save1, saves);
        Assert.Contains(save2, saves);
        Assert.Contains(save3, saves);
        Assert.Contains(save4, saves);
    }
    [Theory]
    [MemberData(nameof(Platforms))]
    public void Saves_EmitsEmptyList_WhenPlatformsNullOrEmpty(List<Platform> platforms) {
        TestScheduler scheduler = new();
        ITestableObservable<List<Platform>> platformSubject =
            scheduler.CreateColdObservable(ReactiveTest.OnNext(10, platforms),
                ReactiveTest.OnCompleted<List<Platform>>(10));

        IReadOnlyList<Save> saves = [];

        Mock<IPlatformAdapter> adapter = new();
        adapter.Setup(a => a.PlatformsObservable).Returns(platformSubject);

        SaveManager sut = new(adapter.Object);
        using IDisposable sub = sut.Saves.Subscribe(s => saves = s);

        scheduler.Start();

        Assert.NotNull(saves);
        Assert.Equal(0, saves.Count);
    }
}
