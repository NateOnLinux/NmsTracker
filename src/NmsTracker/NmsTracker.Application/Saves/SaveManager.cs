using NmsTracker.Domain.Entities.Saves;

namespace NmsTracker.Application.Saves;

public class SaveManager(IPlatformAdapter platformAdapter) {
    public void Load(Save save) {
        platformAdapter.Load(save);
    }

    public void Unload(Save save) {
        platformAdapter.Unload(save);
    }
}
