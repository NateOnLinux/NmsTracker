using Microsoft.Reactive.Testing;
using Moq;
using NmsTracker.Application.Saves;
using NmsTracker.Domain.Entities.Saves;
using NmsTracker.Domain.VObs.Saves;

namespace NmsTracker.ApplicationTests.Saves;

public class SaveManagerTests : ReactiveTest {
    [Fact]
    public void Saves_EmitsCombinedSaves_WhenPlatformsObservableEmits() {
        TestScheduler scheduler = new();

        Save saveA = Save("A", PlatformId.Steam);
        Save saveB = Save("B", PlatformId.Microsoft);

        Platform platform1 = Platform(PlatformId.Steam, saveA);
        Platform platform2 = Platform(PlatformId.Microsoft, saveB);

        ITestableObservable<PlatformChangeEvent> platformEvents =
            scheduler.CreateColdObservable(
                OnNext(100, new PlatformChangeEvent([platform1], DateTimeOffset.MinValue)),
                OnNext(200,
                    new PlatformChangeEvent([platform1, platform2], DateTimeOffset.MinValue)));

        Mock<IPlatformAdapter> adapterMock = Mock<IPlatformAdapter>();
        adapterMock.Setup(a => a.PlatformsObservable).Returns(platformEvents);

        var manager = new SaveManager(adapterMock.Object);

        ITestableObserver<SaveChangeEvent> observer = scheduler.CreateObserver<SaveChangeEvent>();
        manager.Saves.Subscribe(observer);

        scheduler.Start();

        Assert.Equal(2, observer.Messages.Count);
        Assert.Equal(saveA, observer.Messages[0].Value.Value.Saves[0]);
        Assert.Equal(saveA, observer.Messages[1].Value.Value.Saves[0]);
        Assert.Equal(saveB, observer.Messages[1].Value.Value.Saves[1]);
    }

    [Fact]
    public void Saves_ReplaysLastValueToNewSubscribers() {
        TestScheduler scheduler = new();

        Save saveA = Save("A", PlatformId.Steam);
        Platform platform1 = Platform(PlatformId.Steam, saveA);

        ITestableObservable<PlatformChangeEvent> platformEvents =
            scheduler.CreateColdObservable(OnNext(100,
                new PlatformChangeEvent([platform1], DateTimeOffset.MinValue)));

        Mock<IPlatformAdapter> adapterMock = Mock<IPlatformAdapter>();
        adapterMock.Setup(a => a.PlatformsObservable).Returns(platformEvents);

        var manager = new SaveManager(adapterMock.Object);

        ITestableObserver<SaveChangeEvent> observer1 = scheduler.CreateObserver<SaveChangeEvent>();
        manager.Saves.Subscribe(observer1);

        scheduler.Start();
        Assert.Single(observer1.Messages);
        Assert.Equal(saveA, observer1.Messages[0].Value.Value.Saves[0]);

        ITestableObserver<SaveChangeEvent> observer2 = scheduler.CreateObserver<SaveChangeEvent>();
        manager.Saves.Subscribe(observer2);

        Assert.Single(observer2.Messages);
        Assert.Equal(saveA, observer2.Messages[0].Value.Value.Saves[0]);
    }

    #region Helpers

    private static Platform Platform(PlatformId id, params Save[] saves) {
        return new Platform(id, new DirectoryInfo("."), saves);
    }
    private static Save Save(string name, PlatformId platform) {
        return new Save(new SaveId(name), platform, name, false, true, DateTime.MinValue);
    }
    private static Mock<T> Mock<T>() where T : class {
        return new Mock<T>(MockBehavior.Strict);
    }

    #endregion

}
