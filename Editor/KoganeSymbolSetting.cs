using System;
using UnityEditor;
using UnityEngine;

namespace Kogane
{
    [FilePath( "ProjectSettings/Kogane/SymbolSetting.asset", FilePathAttribute.Location.ProjectFolder )]
    public sealed class KoganeSymbolSetting : ScriptableSingleton<KoganeSymbolSetting>
    {
        //==============================================================================
        // 変数(SerializeField)
        //==============================================================================
        [SerializeField] private KoganeSymbolParam[] m_list = Array.Empty<KoganeSymbolParam>();

        //==============================================================================
        // プロパティ
        //==============================================================================
        public KoganeSymbolParam[] List => m_list;

        //==============================================================================
        // 関数
        //==============================================================================
        public void Save()
        {
            Save( true );
        }
    }

    [Serializable]
    public sealed class KoganeSymbolParam
    {
        //==============================================================================
        // 変数(SerializeField)
        //==============================================================================
        // ReorderableList で要素を追加する時はここに指定した初期値は無視される
        // ReorderableList.onAddCallback で初期値を指定する必要がある
        [SerializeField] private string m_name;
        [SerializeField] private string m_comment;
        [SerializeField] private Color  m_color = Color.white;

        //==============================================================================
        // プロパティ
        //==============================================================================
        public string Name    => m_name;
        public string Comment => m_comment;
        public Color  Color   => m_color;
    }
}