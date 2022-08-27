using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace Kogane.Internal
{
    internal sealed class KoganeSymbolItem : TreeViewItem
    {
        //==============================================================================
        // 変数
        //==============================================================================
        private readonly KoganeSymbolParam m_param;

        //==============================================================================
        // プロパティ
        //==============================================================================
        public bool IsEnable  { get; set; }
        public bool IsDefault { get; }

        public bool   IsValid  => m_param != null;
        public string Name     => m_param?.Name ?? string.Empty;
        public string Comment  => m_param?.Comment ?? string.Empty;
        public Color  Color    => m_param?.Color ?? Color.white;
        public bool   IsChange => IsEnable != IsDefault;

        //==============================================================================
        // 関数
        //==============================================================================
        public KoganeSymbolItem
        (
            int               id,
            KoganeSymbolParam param,
            string[]          enabledSymbols
        ) : base( id )
        {
            m_param   = param;
            IsEnable  = enabledSymbols.Contains( Name );
            IsDefault = IsEnable;
        }
    }
}