using UnityEditor;
using UnityEngine;

namespace Kogane.Internal
{
	/// <summary>
	/// UniSymbolParam の Inspector の表示を変更するエディタ拡張
	/// </summary>
	[CustomPropertyDrawer( typeof( UniSymbolParam ) )]
	internal sealed class UniSymbolParamDrawer : PropertyDrawer
	{
		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// GUI を表示する時に呼び出されます
		/// </summary>
		public override void OnGUI
		(
			Rect               position,
			SerializedProperty property,
			GUIContent         label
		)
		{
			using ( new EditorGUI.PropertyScope( position, label, property ) )
			{
				position.height = EditorGUIUtility.singleLineHeight;

				const float textWidth  = 0.425f;
				const float colorWidth = 0.125f;
				const float space      = 4;

				var nameRect    = new Rect( position ) { width = position.width * textWidth };
				var commentRect = new Rect( position ) { x     = nameRect.xMax + space, width    = position.width * textWidth };
				var colorRect   = new Rect( position ) { x     = commentRect.xMax + space, width = position.width * colorWidth };

				var nameProperty    = property.FindPropertyRelative( "m_name" );
				var commentProperty = property.FindPropertyRelative( "m_comment" );
				var colorProperty   = property.FindPropertyRelative( "m_color" );

				PropertyField( "", nameRect, nameProperty );
				PropertyField( "", commentRect, commentProperty );
				PropertyField( "", colorRect, colorProperty );
			}
		}

		/// <summary>
		/// PropertyField を実行します
		/// </summary>
		private void PropertyField
		(
			string             label,
			Rect               position,
			SerializedProperty property
		)
		{
			EditorGUI.PropertyField( position, property, new GUIContent( label ) );
		}
	}
}