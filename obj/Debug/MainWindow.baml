   M S B A M L   `   `   `  ����K  GWallpaper Wizard, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Wallpaper_WizardWallpaper_Wizard  5   	   !    Wallpaper_Wizard.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:Wallpaper_Wizard     5   	   \ XHardcodet.Wpf.TaskbarNotification, Version=1.0.5.0, Culture=neutral, PublicKeyToken=null)tb http://www.hardcodet.net/taskbar  5   	   -   5          � Title$  Wallpaper Wizard��5	   	   $��459.813��6"   $��808.411��63    � Icon$&  Assets/Wallpaper Wizard Icon.ico��6[   .��5
      � .+�5   
   3  -Hardcodet.Wpf.TaskbarNotification.TaskbarIcon     
IconSource$&  Assets/Wallpaper Wizard Icon.ico��6      ToolTipText$ Wallpaper Wizard��6G      MenuActivation$ LeftOrRightClick=�6f    � ContextMenu 5      �� 5      .@�5      m� -   5        $��Themes��5      6D   m� 5      -   5        $
��Exit��5      6<   5      5      �� 5   
   -   5        $��Import Theme��5      $��Right=�6(   $��	0,25,10,0q�6D   $	��Top=�6W   $	��101��6o   $��27��6{   6�   �� 5   
   -   5        $��How to Import?��5      $��Right=�6*   $��	0,69,10,0q�6F   $	��Top=�6Y   $	��101��6q   $��27��6}   6�   � 5   
   $
��Left=�6   $	��399��6*   $��	10,10,0,0q�67   $	��Top=�6J   $	��666��6b   .+�5      �� $	��665��6   $
��Auto=�6'   $��Disabled=�6J   .��5      � -   5        $��	themeList��5      5      5      5      �� 5   
   -   5        $��Apply��5      $��Right=�6!   $��
0,382,10,0q�6=   $	��Top=�6Q   $	��101��6i   $��27��6u   $��3q�6�   ���	#FF84DAA36�   �� 5   
   -   5        $��Change Location��5      $��Right=�6+   $��
0,336,10,0q�6G   $	��Top=�6[   $	��101��6s   $��28��6   6�   �� 5   
   -	   5         ��Name$ LocationText��5      $
��Left=�6*   $��689,299,0,0q�6E   $
��Wrap=�6Z   $	��Top=�6n   �� 6�   $�	Location:��6�   �� 6�   �� 6�   $�Chicago, US��6�   6�   �� 5   
   -
   5        $��Change API Key��5      $
��Left=�6*   $��696,108,0,0q�6E   $	��Top=�6Z   $��88��6r   ���	#00DDDDDD���	#00707070���	#FF0000EE6�   5      5       