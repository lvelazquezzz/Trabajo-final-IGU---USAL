   M S B A M L   `   `   `  ����I  EPrácticaFinal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:PrácticaFinalPrácticaFinal  5   	        PrácticaFinal.CDEjecucion   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:PrácticaFinal     5   	     � Title$   ��5   	   $	��310��6   $	��250��6   .��5	      �  � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��50 �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   .+�5   
   �� $��Repeticiones: ��6   $��Center=�6*   $��Center=�6G   $��0��5      $��0��6!   �� 5   
   -   5         ��Name$ 
TextBoxRep��5      
�������$��30��6>   $	��100��6J   $��Center=�5      $��Center=�6/   $��0��5      $��1��6   6/   �� 5   
   $��Peso: ��6   $��Center=�6"   $��Center=�6?   $��1��5      $��0��6!   �� 5   
   -   5        $ TextBoxPeso��5      
�������$��30��6?   $	��100��6K   $��Center=�5      $��Center=�6/   $��1��5      $��1��6   6/   �� 5   
   $��Fecha y hora: ��6   $��Center=�6*   $��Center=�6G   $��2��5       $��0��6!   �� 5!   
   $��2��6   $��1��6"   $��Center=�62   .+�5"      (  "System.Windows.Controls.DatePicker  -   5        $��DatePickerTB��5"      $��30��6-   $	��100��69   $��Center=�5#      $��	0,2.5,3,5q�5$      6,   �� 5%      -   5        $��TextBoxHora��5%      $��30��6*   $	��100��66   $��Center=�5&      $��	0,2.5,3,5q�5'      6)   5(      �� 5+   
   -   5        $��BotonAddEjecucion��5+      $��3��6*   $��1��67   $��30��6G   $��90��6S   $��Center=�6^   $��Añadir��5,      {�.  64   5-      5.      