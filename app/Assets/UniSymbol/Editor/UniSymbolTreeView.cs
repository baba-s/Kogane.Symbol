using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace UniSymbolEditor
{
	public sealed class UniSymbolTreeView : TreeView
	{
		//==============================================================================
		// 列挙型
		//==============================================================================
		private enum ColumnType
		{
			NUMBER,
			IS_ENABLE,
			NAME,
			COMMENT,
			COPY,
		}

		//==============================================================================
		// 変数(readonly)
		//==============================================================================
		private readonly IReadOnlyList<UniSymbolItem> m_list;

		//==============================================================================
		// 関数
		//==============================================================================
		public UniSymbolTreeView
		(
			TreeViewState             state,
			MultiColumnHeader         header,
			IReadOnlyList<UniSymbolItem> list
		) : base( state, header )
		{
			rowHeight                     =  16;
			showAlternatingRowBackgrounds =  true;
			header.sortingChanged         += SortItems;
			m_list                        =  list;

			header.ResizeToFit();
			Reload();
		}

		protected override TreeViewItem BuildRoot()
		{
			var root = new TreeViewItem { depth = -1 };

			foreach ( var n in m_list )
			{
				root.AddChild( n );
			}

			return root;
		}

		protected override void RowGUI( RowGUIArgs args )
		{
			var item = args.item as UniSymbolItem;

			if ( item == null || !item.IsValid ) return;

			var labelStyle = args.selected ? EditorStyles.whiteLabel : EditorStyles.label;
			labelStyle.alignment = TextAnchor.MiddleLeft;
			labelStyle.fontStyle = item.IsChange ? FontStyle.Bold : FontStyle.Normal;

			var columns = args.GetNumVisibleColumns();

			for ( var i = 0; i < columns; i++ )
			{
				var rect        = args.GetCellRect( i );
				var columnIndex = ( ColumnType ) args.GetColumn( i );

				var color = GUI.color;
				GUI.color = item.Color;

				switch ( columnIndex )
				{
					case ColumnType.NUMBER:
						EditorGUI.LabelField( rect, item.id.ToString(), labelStyle );
						break;
					case ColumnType.IS_ENABLE:
						item.IsEnable = EditorGUI.Toggle( rect, item.IsEnable );
						break;
					case ColumnType.NAME:
						EditorGUI.SelectableLabel( rect, item.Name, labelStyle );
						break;
					case ColumnType.COMMENT:
						EditorGUI.LabelField( rect, item.Comment, labelStyle );
						break;
					case ColumnType.COPY:
						GUI.Button( rect, "copy", EditorStyles.miniButton );
						break;
				}

				GUI.color = color;
			}
		}

		protected override bool DoesItemMatchSearch( TreeViewItem treeViewItem, string search )
		{
			var item = treeViewItem as UniSymbolItem;

			if ( item == null ) return false;

			var name    = item.Name;
			var comment = item.Comment;

			return name.Contains( search ) | comment.Contains( search );
		}

		private void SortItems( MultiColumnHeader header )
		{
			var index     = ( ColumnType ) header.sortedColumnIndex;
			var ascending = header.IsSortedAscending( header.sortedColumnIndex );

			IOrderedEnumerable<UniSymbolItem> ordered = null;

			switch ( index )
			{
				case ColumnType.NUMBER:
				case ColumnType.COPY:
					ordered = m_list.OrderBy( c => c.id );
					break;
				case ColumnType.IS_ENABLE:
					ordered = m_list
					          .OrderBy( c => !c.IsEnable )
					          .ThenBy( c => c.id )
						;
					break;
				case ColumnType.NAME:
					ordered = m_list
					          .OrderBy( c => c.Name )
					          .ThenBy( c => c.id )
						;
					break;
				case ColumnType.COMMENT:
					ordered = m_list
					          .OrderBy( c => c.Comment )
					          .ThenBy( c => c.id )
						;
					break;
			}

			var items = ordered.AsEnumerable();

			if ( !ascending )
			{
				items = items.Reverse();
			}

			rootItem.children = items.Cast<TreeViewItem>().ToList();
			BuildRows( rootItem );
		}
	}
}