   M S B A M L   `   `   `  ����M  IGUI_FileSysWatcher, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:GUI_FileSysWatcherGUI_FileSysWatcher  5   	   '    !GUI_FileSysWatcher.DatabaseWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:GUI_FileSysWatcher     5   	     � Title$  
Query Form��5   	   $	��350��6   $	��800��6)   .��5	      � .+�5
   
   �� -   5         ��Name$ comboBox��5
       �� Text$  ��6%   $
��Left=�6-   $��
109,62,0,0q�6H   $	��Top=�6\   $��75��6t   $��23��6   .@�5      �� $��Any file type��6   $
��Left=�63   $	��127��6N   �� 5      $
��.txt��6   $
��Left=�6*   $	��127��6E   �� 5      $
��.doc��6   $
��Left=�6*   $	��127��6E   �� 5      $
��.pdf��6   $
��Left=�6*   $	��127��6E   �� 5      $
��.zip��6   $
��Left=�6*   $	��127��6E   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	gridBoxDB��5         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 6&   $
��Left=�66   $	��214��6Q   $��	10,95,0,0q�6^   $	��Top=�6q   $	��772��6�      AutoGenerateColumns . 6�   �� 5   
   $8��2Search by extension:
ex: txt or All files = empty��6   $
��Left=�6U   $��	10,10,0,0q�6p   $	��Top=�6�   $	��268��6�   $��47��6�   �� 5   
   -   5         ��Name$ extBox��5      $
��Left=�6"   $��23��6=   $��	29,62,0,0q�6I    �� TextWrapping$
 Wrap=�6\   $�� ��6p   $	��Top=�6x   $��75��6�   6�   �� 5   
   -   5         ��Name$ 	searchbtn��5      $��Search��6$   $
��Left=�65   $��
189,62,0,0q�6P   $	��Top=�6d   $��80��6|   $��23��6�   6�   �� 5   
   $	��  .��6   $
��left=�6   $
��Left=�6@   $��	10,61,0,0q�6[   $	��Top=�6n   $��24��6�   $��19��6�   �� 5   
   -   5        $ cleardb��5      $��Clear Database��6"   $
��Left=�6;   $��
681,65,0,0q�6V   $	��Top=�6j   $	��101��6�   6�   �� 5   
   -   5        	 ��Name$	 showCleardb��5      $�� ��6%   $
��Left=�60   $��
288,65,0,0q�6K   $	��Top=�6_   $	��377��6w   5      5      