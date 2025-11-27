using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Naninovel;


[InitializeAtRuntime(@override: typeof(CharacterManager))]
public class CharacterManagerFuture : CharacterManager
{
    [Serializable]
    public new class GameState
    {
        public SerializableLiteralStringMap CharIdToAvatarPathMap = new();
        public SerializableLiteralStringMap CharIdToLinkedPrinterMap = new();
    }

    private readonly SerializableLiteralStringMap linkedPrinterByCharId = new();

    public override async UniTask LoadServiceState(GameStateMap stateMap)
    {
        await base.LoadServiceState(stateMap);

        var state = stateMap.GetState<GameState>();
        if (state is null)
        {
            if (avatarPathByCharId.Count > 0)
                foreach (var charId in avatarPathByCharId.Keys.ToArray())
                    RemoveAvatarTextureFor(charId);
            // feat: linked printers
            if (linkedPrinterByCharId.Count > 0)
                foreach (var charId in linkedPrinterByCharId.Keys.ToArray())
                    RemoveLinkedPrinterFor(charId);
            return;
        }

        // Remove non-existing avatar mappings.
        if (avatarPathByCharId.Count > 0)
            foreach (var charId in avatarPathByCharId.Keys.ToArray())
                if (!state.CharIdToAvatarPathMap.ContainsKey(charId))
                    RemoveAvatarTextureFor(charId);
        // Add new or changed avatar mappings.
        foreach (var kv in state.CharIdToAvatarPathMap)
            SetAvatarTexturePathFor(kv.Key, kv.Value);

        // Remove non-existing linked printer mappings.
        if (linkedPrinterByCharId.Count > 0)
            foreach (var charId in linkedPrinterByCharId.Keys.ToArray())
                if (!state.CharIdToLinkedPrinterMap.ContainsKey(charId))
                    RemoveLinkedPrinterFor(charId);
        // Add new or changed linked printer mappings.
        foreach (var kv in state.CharIdToLinkedPrinterMap)
            SetLinkedPrinterFor(kv.Key, kv.Value);
    }

    public virtual string GetLinkedPrinterFor(string characterId)
    {
        if (string.IsNullOrEmpty(characterId)) return null;
        return linkedPrinterByCharId.TryGetValue(characterId, out var printerId) ? printerId : null;
    }

    public virtual void SetLinkedPrinterFor(string characterId, string printerId)
    {
        if (string.IsNullOrEmpty(characterId))
        {
            Engine.Warn("Failed to set linked printer: character ID is null or empty.");
            return;
        }

        if (string.IsNullOrEmpty(printerId))
        {
            RemoveLinkedPrinterFor(characterId);
            return;
        }

        linkedPrinterByCharId[characterId] = printerId;
    }

    public virtual void RemoveLinkedPrinterFor(string characterId)
    {
        if (!string.IsNullOrEmpty(characterId))
            linkedPrinterByCharId.Remove(characterId);
    }


}