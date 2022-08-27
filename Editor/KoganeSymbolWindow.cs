using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Kogane.Internal
{
    internal sealed class KoganeSymbolWindow : EditorWindow
    {
        //==============================================================================
        // 定数(const)
        //==============================================================================
        private const string SEARCH_STRING_STATE_KEY = "KoganeSymbolWindow_SearchString";

        //==============================================================================
        // 変数
        //==============================================================================
        private SearchField          m_searchField;
        private KoganeSymbolTreeView m_symbolTreeView;
        private KoganeSymbolItem[]   m_list;

        //==============================================================================
        // プロパティ(static)
        //==============================================================================
        public static bool IsUpdate { private get; set; }

        //==============================================================================
        // 関数
        //==============================================================================
        private KoganeSymbolSetting GetSettings()
        {
            return KoganeSymbolSetting.instance;
        }

        [MenuItem( "Window/Kogane/Symbol" )]
        public static void Open()
        {
            GetWindow<KoganeSymbolWindow>( "Symbol" );
        }

        private void OnEnable()
        {
            var settings = GetSettings();
            var group    = EditorUserBuildSettings.selectedBuildTargetGroup;

            var enabledSymbols = PlayerSettings
                .GetScriptingDefineSymbolsForGroup( group )
                .Split( ';' );

            m_list = settings.List
                .Select( ( val, index ) => new KoganeSymbolItem( index + 1, val, enabledSymbols ) )
                .DefaultIfEmpty( new KoganeSymbolItem( 0, null, enabledSymbols ) )
                .ToArray();

            var state  = new TreeViewState();
            var header = new KoganeSymbolHeader( null );

            m_symbolTreeView = new KoganeSymbolTreeView( state, header, m_list )
            {
                searchString = SessionState.GetString( SEARCH_STRING_STATE_KEY, string.Empty )
            };

            m_searchField                         =  new SearchField();
            m_searchField.downOrUpArrowKeyPressed += m_symbolTreeView.SetFocusAndEnsureSelectedItem;
        }

        private void OnGUI()
        {
            if ( IsUpdate )
            {
                OnEnable();
                IsUpdate = false;
            }

            var enabled = GUI.enabled;
            GUI.enabled = !EditorApplication.isCompiling && !EditorApplication.isPlaying;

            using ( new EditorGUILayout.HorizontalScope( EditorStyles.toolbar ) )
            {
                if ( GUILayout.Button( "Save", EditorStyles.toolbarButton ) )
                {
                    var targetGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
                    var defineList  = m_list.Where( c => c.IsEnable ).Select( c => c.Name );
                    var defines     = string.Join( ";", defineList );

                    PlayerSettings.SetScriptingDefineSymbolsForGroup( targetGroup, defines );
                }

                if ( GUILayout.Button( "Copy", EditorStyles.toolbarButton ) )
                {
                    var list = m_list.Where( c => c.IsEnable ).Select( c => c.Name );
                    var text = string.Join( ";", list );

                    EditorGUIUtility.systemCopyBuffer = text;
                }

                if ( GUILayout.Button( "Reset", EditorStyles.toolbarButton ) )
                {
                    foreach ( var n in m_list )
                    {
                        n.IsEnable = n.IsDefault;
                    }
                }

                if ( GUILayout.Button( "Refresh", EditorStyles.toolbarButton ) )
                {
                    OnEnable();
                }

                if ( GUILayout.Button( "Setting", EditorStyles.toolbarButton ) )
                {
                    SettingsService.OpenProjectSettings( KoganeSymbolSettingProvider.PATH );
                }

                using ( var checkScope = new EditorGUI.ChangeCheckScope() )
                {
                    if ( m_symbolTreeView != null )
                    {
                        var searchString = m_searchField.OnToolbarGUI( m_symbolTreeView.searchString );

                        if ( checkScope.changed )
                        {
                            SessionState.SetString( SEARCH_STRING_STATE_KEY, searchString );
                            m_symbolTreeView.searchString = searchString;
                        }
                    }
                }
            }

            var singleLineHeight = EditorGUIUtility.singleLineHeight;

            var rect = new Rect
            {
                x      = 0,
                y      = singleLineHeight + 1,
                width  = position.width,
                height = position.height - singleLineHeight - 1
            };

            m_symbolTreeView?.OnGUI( rect );

            GUI.enabled = enabled;
        }
    }
}