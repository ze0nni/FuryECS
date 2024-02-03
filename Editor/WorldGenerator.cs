using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Fury.ECS.Editor
{
    [InitializeOnLoad]
    public static partial class WorldGenerator
    {
        static WorldGenerator()
        {
            AssemblyReloadEvents.afterAssemblyReload += () => Rebuild();
        }

        [MenuItem("Fury/ECS/Rebuild")]
        public static void Rebuild()
        {
            var results = new Dictionary<string, WorldGeneratorResult>();
            foreach (var worldType in TypeCache.GetTypesWithAttribute<WorldAttribute>())
            {
                try
                {
                    var result = new WorldGeneratorResult(new WorldInfo(worldType));

                    if (string.IsNullOrWhiteSpace(result.OutputFileName))
                        throw new ArgumentException($"Empty OutputFileName");

                    results.Add(result.OutputFileName, result);
                } catch(Exception exc)
                {
                    Debug.LogError($"World {worldType} not created");
                    Debug.LogException(exc);
                }
            }

            foreach (var (output, result) in results)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(output));
                File.WriteAllText(output, result.GetCode());
            }
        }
    }
}
