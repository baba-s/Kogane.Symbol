using System;
using UnityEngine;

namespace Kogane.Internal
{
	internal sealed class UniSymbolSettings : ScriptableObject
	{
		//==============================================================================
		// 変数(SerializeField)
		//==============================================================================
		[SerializeField] private UniSymbolParam[] m_list = new UniSymbolParam[0];

		//==============================================================================
		// プロパティ
		//==============================================================================
		public UniSymbolParam[] List => m_list;
	}

	[Serializable]
	public sealed class UniSymbolParam
	{
		//==============================================================================
		// 変数(SerializeField)
		//==============================================================================
		[SerializeField] private string m_name    = default;
		[SerializeField] private string m_comment = default;
		[SerializeField] private Color  m_color   = Color.white;

		//==============================================================================
		// プロパティ
		//==============================================================================
		public string Name    => m_name;
		public string Comment => m_comment;
		public Color  Color   => m_color;
	}
}