using System;

namespace DNX.Helpers.WinForms.HotKeys
{
    public class HotKeyRegistration
    {
        private const int SI_BASE_ID = 1000;

        public static int CurrentId { get; private set; } = SI_BASE_ID;

        public int Id { get; }

        public string Name { get; }

        public HotKeyDefinition Definition { get; }

        public Func<bool> Handler { get; }

        public static Func<bool> BuildHandler(Action handler)
        {
            return delegate
            {
                handler();
                return true;
            };
        }

        public HotKeyRegistration(string name, HotKeyDefinition definition, Func<bool> handler)
        {
            Id         = CurrentId++;
            Name       = name;
            Definition = definition;
            Handler    = handler;
        }

        public HotKeyRegistration(string name, HotKeyDefinition definition, Action handler)
            : this(name, definition, BuildHandler(handler))
        {
        }

        //public HotKeyRegistration(string name, HotKeyDefinition definition, Action<string> handler)
        //{
        //    Id         = CurrentId++;
        //    Name       = name;
        //    Definition = definition;
        //    Handler    = delegate
        //    {
        //        handler(name);
        //        return true;
        //    };
        //}
    }
}
