using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Kogane.Internal
{
    internal sealed class KoganeSymbolHeader : MultiColumnHeader
    {
        //==============================================================================
        // 関数
        //==============================================================================
        public KoganeSymbolHeader( MultiColumnHeaderState state ) : base( state )
        {
            const int nonTitleColumnWidth = 18;
            const int copyColumnWidth     = 44;

            var nonTitleColumn = new MultiColumnHeaderState.Column
            {
                width               = nonTitleColumnWidth,
                minWidth            = nonTitleColumnWidth,
                maxWidth            = nonTitleColumnWidth,
                headerContent       = new GUIContent( string.Empty ),
                headerTextAlignment = TextAlignment.Center,
            };

            var columns = new[]
            {
                new MultiColumnHeaderState.Column
                {
                    headerContent       = new GUIContent( "No" ),
                    headerTextAlignment = TextAlignment.Center,
                    width               = 24,
                    maxWidth            = 24,
                },
                nonTitleColumn,
                new MultiColumnHeaderState.Column
                {
                    headerContent       = new GUIContent( "Name" ),
                    headerTextAlignment = TextAlignment.Center,
                },
                new MultiColumnHeaderState.Column
                {
                    headerContent       = new GUIContent( "Comment" ),
                    headerTextAlignment = TextAlignment.Center,
                },
                new MultiColumnHeaderState.Column
                {
                    width               = copyColumnWidth,
                    minWidth            = copyColumnWidth,
                    maxWidth            = copyColumnWidth,
                    headerContent       = new GUIContent( "Copy" ),
                    headerTextAlignment = TextAlignment.Center,
                },
            };

            this.state = new MultiColumnHeaderState( columns );
        }
    }
}