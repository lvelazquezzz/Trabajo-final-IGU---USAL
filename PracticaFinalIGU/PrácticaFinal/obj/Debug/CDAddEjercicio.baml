   M S B A M L   `   `   `  ����I  EPrácticaFinal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:PrácticaFinalPrácticaFinal  5   	   #    PrácticaFinal.CDAddEjercicio   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:PrácticaFinal     5   	     � Title$   ��5   	   $	��330��6   $	��250��6   .��5	      �  � RowDefinitions 5
   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      �� 5      .+�5   
   �� $��Nombre del ejercicio: ��6   $��Center=�62   $��Center=�6O   $��0��5      $��0��6   �� 5   
   -   5         ��Name$ TextBoxNombre��5      
�������$��30��6A   $	��100��6M   $��Center=�5   
   $��Center=�6'   $��0��5   
   $��1��6   6'   �� 5   
   $��Descripción (opcional): ��6   $��Center=�64   $��Center=�6Q   $��1��5      $��0��6   �� 5   
   -   5        $ TextBoxDescripcion��5      
�������$��80��6F   $	��100��6R   $��Center=�5   
   $��Center=�6'   $��5q�6B   $��1��5   
   $��1��6   6'   �� 5    
   $��Grupo(s) muscular(es): ��6   $��Center=�63   $��Center=�6P   $��2��5!      $��0��6   �� 5"   
   -   5        $��ListBoxGruposMusculares��5#      $��2��5"      $��1��6   $��5q�6/    �� SelectionMode$ Multiple=�6:   .@�5$      �� $��Pecho��6   �� 5%      $
��Core��6   �� 5&      $��Brazos��6   �� 5'      $��Espalda��6   �� 5(      $��Piernas��6   5)      �� 5+   
   -   5        $��ButtonAnadirEjercicio��5+   .   $��3��6   $��1��6   $��30��5,      $��10q�6   $��90��6)   {�.  5-      5.      50      