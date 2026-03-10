using System.Collections.Concurrent;
using System.Reflection;
using libNOM.io;
using libNOM.io.Enums;
using libNOM.io.Interfaces;
using libNOM.io.Settings;
using Microsoft.Extensions.Options;
using Moq;
using NmsTracker.Domain.Entities.Saves;
using NmsTracker.Domain.VObs.Saves;
using NmsTracker.Infrastructure.Saves;

namespace NmsTracker.InfrastructureTests.Saves;

public class NmsPlatformAdapterTests {
    [Fact]
    public void Load_WhenPlatformMissing_ShouldThrowInvalidOperation() {
        Mock<IPlatform> nonSteamPlatformMock = Mock<IPlatform>();
        nonSteamPlatformMock.Setup(p => p.GetSaveContainers()).Returns([]);
        nonSteamPlatformMock.Setup(p => p.PlatformEnum).Returns(PlatformEnum.Unknown);
        // Non-steam platform
        PlatformCollection collection = CreateCollectionWith(nonSteamPlatformMock.Object);
        NmsPlatformAdapter adapter = new(collection, Options());
        //  Save with 'Steam' platform identifier
        Save save = new(new SaveId("id"), PlatformId.Steam, "id", false, false, DateTime.MinValue);
        // Platform 'Steam' not present in the collection -> InvalidOperationException
        Assert.Throws<InvalidOperationException>(() => adapter.Load(save));
        nonSteamPlatformMock.Verify(p => p.GetSaveContainers(), Times.Never);
    }

    [Fact]
    public void Load_WhenContainerMissing_ShouldThrowInvalidOperation() {
        Mock<IPlatform> platform = Mock<IPlatform>();

        platform.Setup(p => p.PlatformEnum).Returns(PlatformEnum.Steam);
        platform.Setup(p => p.GetSaveContainers()).Returns([]);

        PlatformCollection collection = CreateCollectionWith(platform.Object);

        NmsPlatformAdapter adapter = new(collection, Options());

        Save save =
            new(new SaveId("missing"), PlatformId.Steam, "missing", false, false,
                DateTime.MinValue);
        Assert.Throws<InvalidOperationException>(() => adapter.Load(save));
        platform.Verify(p => p.GetSaveContainers(), Times.Once);
        platform.Verify(p => p.Load(It.IsAny<IContainer>()), Times.Never);
    }

    [Fact]
    public void Load_WithExistingData_ShouldInvokeLoad() {
        const PlatformId platformid = PlatformId.Steam;
        const PlatformEnum platform = PlatformEnum.Steam;
        const string saveid = "save-1";
        const string savename = "Save 1";

        // AnalyzeLocal = false: Do not scan local directories for save files
        // PreferredPlatform = Unknown: Do not prefer any specific platform
        SaveId saveId = new(saveid);
        Save s = new(saveId, platformid, savename, false, false, DateTime.MinValue);

        // Mock container for the platform
        Mock<IContainer> containerMock = Mock<IContainer>();
        containerMock.SetupGet(c => c.Identifier).Returns(saveid);
        containerMock.SetupGet(c => c.SaveName).Returns(savename);
        IContainer container = containerMock.Object;

        // Mock Platform
        Mock<IPlatform> platformMock = Mock<IPlatform>();
        platformMock.Setup(p => p.Load(It.Is<IContainer>(c => c == container)));
        platformMock.SetupGet(p => p.PlatformEnum).Returns(platform);
        List<IContainer> containers = [container];
        platformMock.Setup(p => p.GetSaveContainers()).Returns(containers);

        PlatformCollection pc = CreateCollectionWith(platformMock.Object);

        NmsPlatformAdapter adapter = new(pc, Options());
        adapter.Load(s);

        platformMock.Verify(p => p.Load(containerMock.Object), Times.Once);
    }

    private static PlatformCollection CreateCollectionWith(IPlatform platform) {
        PlatformCollection collection = new(PlatformCollectionSettings());
        var dict =
            (ConcurrentDictionary<string, IPlatform>)typeof(PlatformCollection).GetField(
                    "_collection", BindingFlags.NonPublic | BindingFlags.Instance)!
                .GetValue(collection)!;

        dict[platform.PlatformEnum.ToString()] = platform;

        return collection;
    }

    #region Helpers

    private static PlatformCollectionSettings PlatformCollectionSettings() {
        return new PlatformCollectionSettings {
            AnalyzeLocal = false, PreferredPlatform = PlatformEnum.Unknown
        };
    }
    private static Mock<T> Mock<T>() where T : class {
        return new Mock<T>(MockBehavior.Strict);
    }
    private static IOptionsMonitor<PlatformOptions> Options() {
        Mock<IOptionsMonitor<PlatformOptions>> options = Mock<IOptionsMonitor<PlatformOptions>>();
        options.Setup(o => o.CurrentValue).Returns(new PlatformOptions());
        return options.Object;
    }

    #endregion

}
