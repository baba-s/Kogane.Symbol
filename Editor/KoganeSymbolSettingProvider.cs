using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane.Internal
{
    internal sealed class KoganeSymbolSettingProvider : SettingsProvider
    {
        public const string PATH = "Kogane/Symbol";

        private Editor m_editor;

        private KoganeSymbolSettingProvider
        (
            string              path,
            SettingsScope       scopes,
            IEnumerable<string> keywords = null
        ) : base( path, scopes, keywords )
        {
        }

        public override void OnActivate( string searchContext, VisualElement rootElement )
        {
            var instance = KoganeSymbolSetting.instance;

            instance.hideFlags = HideFlags.HideAndDontSave & ~HideFlags.NotEditable;

            Editor.CreateCachedEditor( instance, null, ref m_editor );
        }

        public override void OnGUI( string searchContext )
        {
            using var changeCheckScope = new EditorGUI.ChangeCheckScope();

            m_editor.OnInspectorGUI();

            if ( GUILayout.Button( "Open Symbol Window" ) )
            {
                EditorWindow.GetWindow<KoganeSymbolWindow>( "Symbol" );
            }

            if ( !changeCheckScope.changed ) return;

            KoganeSymbolSetting.instance.Save();
            KoganeSymbolWindow.IsUpdate = true;
        }

        [SettingsProvider]
        private static SettingsProvider CreateSettingProvider()
        {
            return new KoganeSymbolSettingProvider
            (
                path: PATH,
                scopes: SettingsScope.Project
            );
        }
    }
}