   M S B A M L   `   `   `  ���� 
  YF-WMS    YF_WMS.WPF_Material   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  物料管理��$	��500��$	��670��.��� .+�&   System.Windows.Controls.DataGrid  -   $��datagridmaterial��$
��Left=�$��10,111,-418,0q�$	��Top=�$	��349��$	��660��   AutoGenerateColumns .     CanUserAddRows .   %� IsVirtualizing .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 物料编号��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 150    
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Material_ID+  $ 物料规格��$	 150  .  �� *Material_Spec+  $ 物料数量��$	 100  .  �� *Material_Qty+  $ 料箱编号��$	 100  .  �� *Box_ID+	 � ContextMenu	 �� -   $��dgmenu1��
 �� 	StaysOpen
 . .@�m� -   $��删除��m� $��撤回��m� -   $��编辑��m� $��查看库存���� -   $��手动添加��$
��Left=�$��	10,10,0,0q�$	��Top=�$��75�� J� RenderTransformOrigin$ 0.181,-0.736&��� -   $��查找��$
��Left=�$��
430,83,0,0q�$	��Top=�$��75���� -   $��selectmeterial��$
��Left=�$��23��$��
305,83,0,0q� �� TextWrapping$
 Wrap=�$�� ��$	��Top=�$	��120���� -   $��导出��$
��Left=�$��	10,84,0,0q�$	��Top=�$��75���� -	   $��导入��$
��Left=�$��	90,10,0,0q�$	��Top=�$��75��