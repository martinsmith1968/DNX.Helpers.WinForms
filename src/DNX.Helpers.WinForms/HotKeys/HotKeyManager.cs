using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DNX.Helpers.WinForms.HotKeys.Exceptions;

// ReSharper disable LocalizableElement
// ReSharper disable InconsistentNaming

namespace DNX.Helpers.WinForms.HotKeys
{
    public  class HotKeyManager
    {
        private static readonly IDictionary<int, HotKeyRegistration> _registeredHotKeys = new ConcurrentDictionary<int, HotKeyRegistration>();
        private static readonly IDictionary<string, int> _registeredHotKeyNames = new ConcurrentDictionary<string, int>();

        private static readonly HotKeyMessageHandler _hotKeyHandler = new HotKeyMessageHandler(HandleHotKey);

        public static IntPtr Handle => IntPtr.Zero;

        ~HotKeyManager()
        {
            ReleaseUnmanagedResources();
        }

        private static void ReleaseUnmanagedResources()
        {
            _hotKeyHandler?.Dispose();

            while (_registeredHotKeys.Any())
            {
                UnRegisterHotKeyById(_registeredHotKeys.Keys.First());
            }
        }

        internal static HotKeyRegistration GetRegistrationById(int hotKeyId)
        {
            var hotKeyRegistration = _registeredHotKeys.ContainsKey(hotKeyId)
                ? _registeredHotKeys[hotKeyId]
                : null;

            return hotKeyRegistration;
        }

        internal static HotKeyRegistration GetRegistrationByName(string name)
        {
            var hotKeyId = _registeredHotKeyNames.ContainsKey(name)
                ? _registeredHotKeyNames[name]
                : 0;

            return GetRegistrationById(hotKeyId);
        }

        public static void UnRegisterHotKey(HotKeyRegistration hotKeyRegistration)
        {
            if (hotKeyRegistration == null)
                return;

            UnRegisterHotKeyById(hotKeyRegistration.Id);
        }

        public static void UnRegisterHotKeyByName(string name)
        {
            var hotKeyRegistration = GetRegistrationByName(name);
            if (hotKeyRegistration == null)
                return;

            UnRegisterHotKeyById(hotKeyRegistration.Id);
        }

        public static void UnRegisterHotKeyById(int id)
        {
            var hotKeyRegistration = GetRegistrationById(id);
            if (hotKeyRegistration == null)
                return;

            WinInternals.UnregisterHotKey(Handle, id);

            _registeredHotKeys.Remove(hotKeyRegistration.Id);
            _registeredHotKeyNames.Remove(hotKeyRegistration.Name);
        }

        public static HotKeyRegistration RegisterHotKey(string name, Action handler, Keys key, HotKeyModifier modifier)
        {
            return RegisterHotKey(name, HotKeyRegistration.BuildHandler(handler), key, modifier);
        }

        public static HotKeyRegistration RegisterHotKey(string name, Func<bool> handler, Keys key, HotKeyModifier modifier)
        {
            if (_registeredHotKeyNames.ContainsKey(name))
                throw new ArgumentOutOfRangeException(nameof(name), $"Hot Key already registered: {name}");
            if (key == Keys.None)
                throw new ArgumentOutOfRangeException(nameof(key), $"Invalid or Unsupported Key: {key}");
            if (modifier == HotKeyModifier.None)
                throw new ArgumentOutOfRangeException(nameof(modifier), $"Invalid or Unsupported Modifier: {modifier}");

            var definition         = new HotKeyDefinition(key, modifier);
            var hotKeyRegistration = new HotKeyRegistration(name, definition, handler);

            var isKeyRegistered = WinInternals.RegisterHotKey(Handle, hotKeyRegistration.Id, (uint)modifier, (uint)key);
            if (!isKeyRegistered)
                throw new HotKeyInUseException(definition);

            _registeredHotKeys.Add(hotKeyRegistration.Id, hotKeyRegistration);
            _registeredHotKeyNames.Add(hotKeyRegistration.Name, hotKeyRegistration.Id);

            return hotKeyRegistration;
        }

        public static bool HandleHotKey(int hotKeyId)
        {
            var hotKeyRegistration = GetRegistrationById(hotKeyId);
            if (hotKeyRegistration == null)
                return false;

            var result = hotKeyRegistration.Handler();

            return result;
        }
    }
}
