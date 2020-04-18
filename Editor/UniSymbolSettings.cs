using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace UniSymbolEditor
{
	public sealed class UniSymbolSettings : ScriptableObject
	{
		//==============================================================================
		// 変数(SerializeField)
		//==============================================================================
		[SerializeField][TableList( AlwaysExpanded = true )][OnValueChanged( "OnValueChanged" )]
		private UniSymbolParam[] m_list = new UniSymbolParam[0];

		//==============================================================================
		// プロパティ
		//==============================================================================
		public UniSymbolParam[] List => m_list;
		
		//==============================================================================
		// 関数
		//==============================================================================
		[Button( "Open UniSymbol" )]
		private static void OpenWindow()
		{
			UniSymbolWindow.Open();
		}

		private void OnValueChanged()
		{
			UniSymbolWindow.IsUpdate = true;
		}
	}

	[Serializable]
	public sealed class UniSymbolParam
	{
		//==============================================================================
		// 変数(SerializeField)
		//==============================================================================
		[SerializeField][OnValueChanged( "OnValueChanged" )] private string m_name    = null;
		[SerializeField][OnValueChanged( "OnValueChanged" )] private string m_comment = null;

		[SerializeField][TableColumnWidth( 42, resizable: false )][OnValueChanged( "OnValueChanged" )]
		private Color m_color = Color.white;

		//==============================================================================
		// プロパティ
		//==============================================================================
		public string Name    => m_name;
		public string Comment => m_comment;
		public Color  Color   => m_color;
		
		//==============================================================================
		// 関数
		//==============================================================================
		private void OnValueChanged()
		{
			UniSymbolWindow.IsUpdate = true;
		}
	}
}