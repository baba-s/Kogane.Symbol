using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace UniSymbolEditor
{
	[CreateAssetMenu( fileName = "UniSymbolSettings", menuName = "UniSymbolSettings" )]
	public sealed class UniSymbolSettings : ScriptableObject
	{
		//==============================================================================
		// 変数(SerializeField)
		//==============================================================================
		[SerializeField] [TableList( AlwaysExpanded = true )]
		private UniSymbolParam[] m_list = null;

		//==============================================================================
		// プロパティ
		//==============================================================================
		public UniSymbolParam[] List { get { return m_list; } }
	}

	[Serializable]
	public sealed class UniSymbolParam
	{
		//==============================================================================
		// 変数(SerializeField)
		//==============================================================================
		[SerializeField] private string m_name     = null;
		[SerializeField] private string m_comment  = null;

		[SerializeField] [TableColumnWidth( 42, resizable: false )]
		private Color m_color = Color.white;

		//==============================================================================
		// プロパティ
		//==============================================================================
		public string Name     => m_name;
		public string Comment  => m_comment;
		public Color  Color    => m_color;
	}
}