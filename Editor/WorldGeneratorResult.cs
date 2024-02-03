using System;
using System.Text;
using static Fury.ECS.Editor.WorldGenerator;

namespace Fury.ECS.Editor
{
    public sealed class WorldGeneratorResult
    {
        public string OutputFileName { get; private set; }

        private readonly StringBuilder _sb = new StringBuilder();
        
        public WorldGeneratorResult(WorldGenerator.WorldInfo world)
        {
            OutputFileName = $"Assets/Scripts/Generated/Worlds/{world.Name}.cs";

            Process(world);
        }

        public string GetCode()
        {
            return _sb.ToString();
        }

        int _indent = 0;
        string _indentText = "";


        void WL(string str)
        {
            _sb.Append(_indentText);
            _sb.AppendLine(str);
        }

        void BeginIndent()
        {
            _indent++;
            _indentText = new string(' ', _indent * 4);
        }

        void EndIndent()
        {
            _indent--;
            _indentText = new string(' ', _indent * 4);
        }

        struct BlockScope : IDisposable
        {
            WorldGeneratorResult _g;
            bool _block;
            public BlockScope(WorldGeneratorResult g, bool block)
            {
                _g = g;
                _block = block;
                if (_block)
                    g?.WL("{");
                g?.BeginIndent();
            }

            public void Dispose()
            {
                _g?.EndIndent();
                if (_block)
                    _g.WL("}");
                _g = null;
            }
        }

        BlockScope Block(bool condition = true, bool block = true)
        {
            return new BlockScope(condition ? this : null, block);
        }

        void Process(WorldInfo world)
        {
            _sb.Clear();

            var hasNs = !string.IsNullOrWhiteSpace(world.Namespace);
            if (hasNs)
                WL($"namespace {world.Namespace}");
            using(Block(hasNs))
            {
                ProcessWorld(world);
            }
        }

        void ProcessWorld(WorldInfo world)
        {
            WL("using System.Runtime.CompilerServices;");
            WL($"public partial class {world.Name} : Fury.ECS.World");
            using(Block())
            {
                foreach (var a in world.Archetypes)
                {
                    WL($"public readonly Fury.ECS.Entities<{a.Name}> Entities{a.Name};");
                }
                foreach (var s in world.Systems)
                {
                    WL($"private readonly global::{s.FullName} Systems{s.Name};");
                }
                WL($"public {world.Name}()");
                using (Block())
                {
                    foreach (var a in world.Archetypes)
                    {
                        WL($"this.Entities{a.Name} = CreateEntities<{a.Name}>();");
                    }
                    foreach (var s in world.Systems)
                    {
                        WL($"this.Systems{s.Name} = new global::{s.FullName}();");
                    }
                }

                ProcessSystem(world, "Setup", null, s => s.IsSetup);
                ProcessSystem(world, "Cleanup", null, s => s.IsCleanup);
                ProcessSystem(world, "Update", "float", s => s.IsUpdate);
                ProcessSystem(world, "FixedUpdate", "float", s => s.IsFixedUpdate);

                WL("//Archetypes");
                foreach (var a in world.Archetypes)
                {
                    ProcessArchetype(a);
                }
            }
        }

        void ProcessArchetype(ArchetypeInfo archetype)
        {
            WL($"public readonly partial struct {archetype.Name}");
            using (Block())
            {
                foreach (var c in archetype.Components)
                {
                    WL($"private readonly Fury.ECS.Ref<global::{c.FullName}> _{c.Name};");
                }
                foreach (var c in archetype.Components)
                {
                    WL($"public ref global::{c.FullName} {c.Name} {{ [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ref _{c.Name}.Get(); }}");
                }
            }
        }

        void ProcessSystem(WorldInfo world, string methodName, string arg, Predicate<SystemInfo> predicate)
        {
            WL($"public sealed override void {methodName}({(arg == null ? "" : $"{arg} a")})");
            using (Block())
            {
                WL($"base.{methodName}({(arg == null ? "" : "a")});");
                foreach (var s in world.Systems)
                {
                    if (predicate(s))
                    {
                        WL($"this.Systems{s.Name}.{methodName}({(arg == null ? "" : "a")});");
                    }
                }
            }
        }
    }
}
